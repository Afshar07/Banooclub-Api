using BanooClub.Extentions;
using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BanooClub.Services.MessageServices
{
    public class MessageService : Hub<IMessageService> , IMessageService
    {
        private readonly IBanooClubEFRepository<Message> messageRepository;
        private readonly IBanooClubEFRepository<MessageRecipient> messageRecipientRepository;
        private readonly IBanooClubEFRepository<UserGroup> userGroupRepository;
        private readonly IHubContext<MessageService> hubContext;
        private readonly IBanooClubEFRepository<Group> groupRepository;
        private readonly IBanooClubEFRepository<SocialMedia> mediaRepository;
        private readonly IHttpContextAccessor _accessor;
        public MessageService(IBanooClubEFRepository<Message> messageRepository, IBanooClubEFRepository<SocialMedia> mediaRepository, IBanooClubEFRepository<Group> groupRepository, IBanooClubEFRepository<UserGroup> userGroupRepository, IHubContext<MessageService> hubContext, IHttpContextAccessor accessor, IBanooClubEFRepository<MessageRecipient> messageRecipientRepository)
        {
            this.messageRepository = messageRepository;
            _accessor = accessor;
            this.messageRecipientRepository = messageRecipientRepository;
            this.hubContext = hubContext;
            this.groupRepository = groupRepository;
            this.mediaRepository = mediaRepository;
            this.userGroupRepository = userGroupRepository;
        }
        public async Task Create(Message inputDto)
        {
            messageRepository.Insert(inputDto);
        }

        public async Task<long> SendMessage(Message inputDto)
        {
            try
            {
                inputDto.UserId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
                inputDto.MessageId = 0;
                inputDto.IsDeleted=false;
                inputDto.CreateDate = DateTime.Now;
                var dbMessage = messageRepository.Insert(inputDto);
                var MessageRec = new MessageRecipient()
                {
                    IsDeleted=false,
                    GroupId=(inputDto.RecipientGroupId ==null || inputDto.RecipientUserId==0) ? null : inputDto.RecipientGroupId,
                    UserId=(inputDto.RecipientUserId == null || inputDto.RecipientUserId==0) ? null : inputDto.RecipientUserId,
                    IsRead=false,
                    IsDelivered=false,
                    MessageId=dbMessage.MessageId,
                    MessageRecipientId=0
                };
                var MSG = messageRecipientRepository.Insert(MessageRec);
                await hubContext.Clients.All.SendAsync("SendMessage", inputDto.UserId);
                return MSG.MessageId;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<bool> ReadMessage(long userId)
        {
            var myselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var beforeRead = messageRecipientRepository.GetQuery().Where(z => z.UserId == myselfId && z.IsRead ==true).Count();
            var cmd = "update Social.MessageRecipients set IsRead = 1 " +
                " where MessageId in " +
                " (select M.MessageId from Social.Messages M join Social.MessageRecipients MR on MR.MessageId = M.MessageId" +
                $" where MR.UserId = {myselfId} and M.UserId = {userId} )";

            try
            {
                var MR = await messageRepository.DapperSqlQuery(cmd);
                var afterRead = messageRecipientRepository.GetQuery().Where(z => z.UserId == myselfId && z.IsRead ==true).Count();
                if (afterRead != beforeRead)
                {
                    await hubContext.Clients.All.SendAsync("ReadMessage", myselfId);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task DeliverMessage()
        {
            try
            {
                var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
                var dbBeforDeliver = messageRecipientRepository.GetQuery().Where(z => z.UserId == userId && z.IsDelivered ==true).Count();
                string cmd = $"update Social.MessageRecipients set IsDelivered = 1 where UserId = {userId}";
                await messageRecipientRepository.DapperSqlQuery(cmd);
                var dbAfterDeliver = messageRecipientRepository.GetQuery().Where(z => z.UserId == userId && z.IsDelivered ==true).Count();
                if (dbAfterDeliver != dbBeforDeliver)
                {
                    await hubContext.Clients.All.SendAsync("DeliverMessage", userId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Message> Update(Message item)
        {
            await messageRepository.Update(item);
            return item;
        }

        public async Task<List<Message>> GetAll()
        {
            List<Message> messages = new List<Message>();
            messages = messageRepository.GetQuery().ToList();
            return messages;
        }

        public async Task Delete(long id)
        {
            var message = messageRepository.GetQuery().FirstOrDefault(z => z.MessageId == id);
            await messageRepository.Delete(message);
        }

        public async Task<Message> Get(long id)
        {
            var message = messageRepository.GetQuery().FirstOrDefault(z => z.MessageId == id);
            return message;
        }
        public async Task<List<MessageDTO>> GetByUserId()
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            string cmd = "select * from (select distinct 0 as GroupId, M.IsForwarded, "+
                " (select Count(M.MessageId) from Social.Messages M join Social.MessageRecipients MR on M.MessageId=MR.MessageId where M.UserId =SenderU.UserId and MR.IsRead =0) as NotReadCount, "+
                $" CASE WHEN SenderU.UserId = {userId} THEN RecieverU.UserId ELSE SenderU.UserId END as UserId , "+
                $" CASE WHEN SenderU.UserId = {userId} THEN RecieverU.Name +' '+ RecieverU.FamilyName ELSE SenderU.Name +' '+ SenderU.FamilyName END as UserName, "+
                " M.Subject ,M.CreateDate ,MR.IsRead, "+
                $" CASE WHEN SenderU.UserId = {userId} THEN SMReciever.PictureUrl ELSE SMSender.PictureUrl END as UserPhoto "+
                " from Social.MessageRecipients MR "+
                "  join social.Messages M on MR.MessageId = M.MessageId "+
                "  join[User].users SenderU on SenderU.UserId = M.UserId "+
                "  join[User].Users RecieverU on RecieverU.UserId = MR.UserId "+
                "  join Social.Medias SMSender on SMSender.ObjectId = M.UserId "+
                "  join Social.Medias SMReciever on SMReciever.ObjectId = MR.UserId "+
                "  where M.MessageId in (select max(M.MessageId) as messageId from Social.MessageRecipients MR "+
                "  join social.Messages M on MR.MessageId = M.MessageId "+
                "  join [User].users U on U.UserId = M.UserId "+
                $" where MR.UserId = {userId} or M.UserId = {userId} "+
                "  group by U.UserId) and SMSender.Type = 2  and SMReciever.Type =2 "+
                "  union "+
                "  select distinct G.GroupId, M.IsForwarded, " +
                "  (select Count(M.MessageId) from Social.Messages M join Social.MessageRecipients MR on M.MessageId=MR.MessageId where MR.GroupId = G.GroupId and MR.IsRead =0) as NotReadCount, "+
                " U.UserId as UserId, U.Name + ' ' + U.FamilyName as UserName,M.Subject,M.CreateDate,MR.IsRead , SMSender.PictureUrl as UserPhoto from Social.UserGroups UG "+
                "  join Social.Groups G on G.GroupId = UG.GroupId "+
                "  join Social.MessageRecipients MR on MR.GroupId=G.GroupId "+
                "  join Social.Messages M on M.MessageId=MR.MessageId "+
                "  join [User].Users U on U.UserId=M.UserId "+
                "  join Social.Medias SMSender on SMSender.ObjectId = M.UserId "+
                "  join Social.Medias SMReciever on SMReciever.ObjectId = G.GroupId "+
                "  where M.MessageId in (select max(M.MessageId) as messageId from Social.MessageRecipients MR "+
                "  join social.Messages M on MR.MessageId = M.MessageId "+
                "  join [User].users U on U.UserId = M.UserId "+
                $"   where M.UserId = {userId} "+
                "  group by U.UserId) and SMReciever.Type = 11  and SMSender.Type = 2) as Result " +
                "  order By Result.CreateDate Desc ";

            string NEWCmd = "select * from (select u.UserId,(u.Name + ' ' + u.FamilyName) as UserName,m.Subject as Subject,m.createDate as CreateDate,SM.PictureUrl as UserPhoto, "+
                " (select count(*) from Social.Messages m "+
                " join Social.MessageRecipients mr on m.MessageId = mr.MessageId "+
                $" where (mr.UserId = {userId}) and(m.UserId = tbl1.UserId or mr.UserId = tbl1.UserId) and mr.IsRead = 0) as UnReadCount "+
                " from( "+
                " select UserId, MAX(MessageId) as MessageId from( "+
                " (select mr.UserId, MAX(m.MessageId) as MessageId from Social.Messages m "+
                " join Social.MessageRecipients mr on m.MessageId = mr.MessageId "+
                $" where m.UserId = {userId} "+
                " group by mr.UserId) "+
                " union "+
                " (select m.UserId, MAX(m.MessageId) as MessageId from Social.Messages m "+
                " join Social.MessageRecipients mr on m.MessageId = mr.MessageId "+
                $" where mr.UserId = {userId} "+
                " group by m.UserId)) as tbl "+
                " group by UserId) tbl1 "+
                " join Social.Messages m on tbl1.MessageId = m.MessageId "+
                " join[User].[Users] u on u.UserId = tbl1.UserId " +
                " join Social.Medias SM on SM.ObjectId = u.UserId where Sm.Type= 2) as Result" +
                " order By Result.CreateDate Desc ";

            string FinalCmd = "select * from (select 0 as GroupId , u.UserId,(u.Name + ' ' + u.FamilyName) as UserName,m.Subject as Subject,m.createDate as CreateDate,SM.PictureUrl as UserPhoto, " +
                " (select count(*) from Social.Messages m " +
                " join Social.MessageRecipients mr on m.MessageId = mr.MessageId " +
                $" where (mr.UserId = {userId}) and(m.UserId = tbl1.UserId or mr.UserId = tbl1.UserId) and mr.IsRead = 0) as UnReadCount " +
                " from( " +
                " select UserId, MAX(MessageId) as MessageId from( " +
                " (select mr.UserId, MAX(m.MessageId) as MessageId from Social.Messages m " +
                " join Social.MessageRecipients mr on m.MessageId = mr.MessageId " +
                $" where m.UserId = {userId} " +
                " group by mr.UserId) " +
                " union " +
                " (select m.UserId, MAX(m.MessageId) as MessageId from Social.Messages m " +
                " join Social.MessageRecipients mr on m.MessageId = mr.MessageId " +
                $" where mr.UserId = {userId} " +
                " group by m.UserId)) as tbl " +
                " group by UserId) tbl1 " +
                " join Social.Messages m on tbl1.MessageId = m.MessageId " +
                " join[User].[Users] u on u.UserId = tbl1.UserId " +
                " left join Social.Medias SM on SM.ObjectId = u.UserId and Sm.Type= 2) as Result" +
                " order By Result.CreateDate Desc ";


            string groupchatcmd = "select * from (select distinct 0 as GroupId, M.IsForwarded, "+

                "(select Count(M.MessageId) from Social.Messages M join Social.MessageRecipients MR on M.MessageId = MR.MessageId where M.UserId = SenderU.UserId and MR.IsRead = 0) as NotReadCount, " +
                 $" CASE WHEN SenderU.UserId =  {userId} THEN RecieverU.UserId ELSE SenderU.UserId END as UserId , " +
                 $" CASE WHEN SenderU.UserId =  {userId} THEN RecieverU.Name + ' ' + RecieverU.FamilyName ELSE SenderU.Name + ' ' + SenderU.FamilyName END as UserName,  " +
                 " M.Subject ,M.CreateDate ,MR.IsRead "+
                  //, CASE WHEN SenderU.UserId = 21 THEN SMReciever.PictureUrl ELSE SMSender.PictureUrl END as UserPhoto


                  " from Social.MessageRecipients MR" +
                  " join social.Messages M on MR.MessageId = M.MessageId " +
                  " join[User].users SenderU on SenderU.UserId = M.UserId " +
                  " join[User].Users RecieverU on RecieverU.UserId = MR.UserId " +
            //join Social.Medias SMSender on SMSender.ObjectId = M.UserId
            //join Social.Medias SMReciever on SMReciever.ObjectId = MR.UserId
            " where M.MessageId in (select max(M.MessageId) as messageId from Social.MessageRecipients MR " +
                  " join social.Messages M on MR.MessageId = M.MessageId " +
                  " join [User].users U on U.UserId = M.UserId " +
                  $" where MR.UserId =  {userId} or M.UserId =  {userId} " +
                  " group by U.UserId) " +

                 //" and SMSender.Type = 2  and SMReciever.Type = 2 " +


                 "  union " +

                  " select distinct G.GroupId, M.IsForwarded,  " +
                  " (select Count(M.MessageId) from Social.Messages M join Social.MessageRecipients MR on M.MessageId = MR.MessageId where MR.GroupId = G.GroupId and MR.IsRead = 0) as NotReadCount,  " +
                  " U.UserId as UserId, U.Name + ' ' + U.FamilyName as UserName,M.Subject,M.CreateDate,MR.IsRead " +
                  //, SMSender.PictureUrl as UserPhoto

                  " from Social.UserGroups UG " +
                  " join Social.Groups G on G.GroupId = UG.GroupId " +
                  " join Social.MessageRecipients MR on MR.GroupId = G.GroupId " +



                  " join Social.Messages M on M.MessageId = MR.MessageId " +
                  " join [User].Users U on U.UserId = M.UserId " +


            //join Social.Medias SMSender on SMSender.ObjectId = M.UserId
            //join Social.Medias SMReciever on SMReciever.ObjectId = G.GroupId



            "  where M.MessageId in (select max(M.MessageId) as messageId from Social.MessageRecipients MR " +
                  " join social.Messages M on MR.MessageId = M.MessageId " +
                 "  join [User].users U on U.UserId = M.UserId " +
                  //where MR.UserId = 21 or M.UserId = 21
                  " group by U.UserId)  " +

            //and SMReciever.Type = 11  and SMSender.Type = 2
            " ) as Result " +
                 "  order By Result.CreateDate Desc";

            var messages = await messageRepository.DapperSqlQuery(FinalCmd);
            var SerializeObject = JsonSerializer.Serialize<object>(messages);
            var serializedMessage = JsonSerializer.Deserialize<List<MessageDTO>>(SerializeObject);
            var groupIds = userGroupRepository.GetQuery().Where(z => z.UserId == userId).Select(x => x.GroupId).ToList();
            if (groupIds.Count() > 0)
            {
                var dbGroups = groupRepository.GetQuery().Where(z => groupIds.Contains(z.GroupId)).ToList();
                foreach (var group in dbGroups)
                {
                    var MR = messageRecipientRepository.GetQuery().OrderByDescending(z => z.MessageId).FirstOrDefault(x => x.GroupId == group.GroupId);
                    var LastMessageId = MR == null ? 0 : MR.MessageId;
                    var dbGroupMedia = mediaRepository.GetQuery().FirstOrDefault(z => z.ObjectId == group.GroupId && z.Type == MediaTypes.Group);
                    var groupM = dbGroupMedia == null ? "" : dbGroupMedia.PictureUrl;
                    if (LastMessageId != 0)
                    {
                        var lastMessage = messageRepository.GetQuery().FirstOrDefault(z => z.MessageId == LastMessageId);
                        var res = new MessageDTO() { CreateDate = lastMessage.CreateDate, GroupId = group.GroupId, Subject = lastMessage.Subject, UserId = 0, UnReadCount = 0, UserName = group.Name, UserPhoto =groupM };
                        serializedMessage.Add(res);
                    }
                    else
                    {

                        serializedMessage.Add(new MessageDTO() { CreateDate = group.CreateDate, GroupId =group.GroupId, Subject = "", UnReadCount= 0, UserId = 0, UserName = group.Name, UserPhoto = groupM });
                    }

                }
            }


            serializedMessage = serializedMessage.OrderByDescending(z => z.CreateDate).ToList();
            return serializedMessage;
        }

        public async Task<object> GetUserConversation(long userId, long messageId, int count)
        {
            var MyselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var completationCmd = messageId ==0 ? "" : $"and M.MessageId < {messageId}";
            string cmd = "select M.UserId as CreatorUserId, M.MessageId , M.MessageBody , M.Subject , M.IsForwarded , M.CreateDate , M.ParentMessageId , MR.IsRead ,MR.IsDelivered " +
                " from Social.Messages M " +
                " join Social.MessageRecipients MR on M.MessageId = MR.MessageId " +
                //" join Social.Medias SM on SM.ObjectId = M.UserId "+
                $" where MR.userId in ({userId},{MyselfId}) and M.userId in ({userId},{MyselfId})" +
                //$" and SM.Type =2 " +
                $"{completationCmd}" +
                $" order by M.CreateDate Desc OFFSET 0 ROWS FETCH NEXT {count} ROWS ONLY ";
            var messages = await messageRepository.DapperSqlQuery(cmd);
            var SerializeObject = JsonSerializer.Serialize<object>(messages);
            return SerializeObject;
        }
        public async Task<object> GetGroupConversation(long groupId, long messageId, int count)
        {
            var MyselfId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            var completationCmd = messageId ==0 ? "" : $"and M.MessageId < {messageId}";
            string cmd = "select M.UserId as CreatorUserId, M.MessageId , M.MessageBody , M.Subject , M.IsForwarded , M.CreateDate , M.ParentMessageId , MR.IsRead , MR.IsDelivered ,SM.PictureUrl UserPhoto" +
                " from Social.Messages M " +
                " join Social.MessageRecipients MR on M.MessageId = MR.MessageId " +
                " left join Social.Medias SM on SM.ObjectId = M.UserId and SM.Type = 2"+
                $" where MR.GroupId = {groupId}" +
                $" {completationCmd} " +
                $" order by M.CreateDate Desc OFFSET 0 ROWS FETCH NEXT {count} ROWS ONLY ";
            var messages = await messageRepository.DapperSqlQuery(cmd);
            var SerializeObject = JsonSerializer.Serialize<object>(messages);
            return SerializeObject;
        }
    }
}
