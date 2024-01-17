using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.UserServices;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.RoomateDocServices
{
    public class RoomateDocService : IRoomateDocService
    {
        private readonly IBanooClubEFRepository<RoomateDoc> roomateDocRepository;
        private readonly IBanooClubEFRepository<Roomate> roomateRepository;
        private readonly IUserService userService; 

        public RoomateDocService(IBanooClubEFRepository<RoomateDoc> roomateDocRepository
            , IBanooClubEFRepository<Roomate> roomateRepository
            ,IUserService userService)
        {
            this.roomateDocRepository = roomateDocRepository;
            this.roomateRepository = roomateRepository;
            this.userService = userService;
        }
        public async Task Create(RoomateDoc inputDto)
        {
            inputDto.Status = (int)DocStatus.NotConfirmed;
            inputDto.IsDeleted = false;
            roomateDocRepository.Insert(inputDto);
        }

        public async Task<RoomateDoc> Update(RoomateDoc item)
        {
            item.Status = (int)DocStatus.NotConfirmed;
            item.IsDeleted = false;
            await roomateDocRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string search)
        {
            List<RoomateDoc> docs = new List<RoomateDoc>();
            docs = roomateDocRepository.GetQuery().OrderByDescending(z=>z.RoomateDocId).Where(x=>x.StateDesc.Contains(search)).Skip((pageNumber-1)*count).Take(count).ToList();
            docs.ForEach(z => z.PassportImage = string.IsNullOrEmpty(z.PassportImage) ? null : "media/gallery/PassportDocs/" + z.PassportImage);
            docs.ForEach(z => z.KartMelliImage = string.IsNullOrEmpty(z.KartMelliImage) ? null : "media/gallery/KartMelliDocs/" + z.KartMelliImage);
            docs.ForEach(z => z.UserInfo=userService.Get(z.UserId));
            var RoomateDocCount = roomateDocRepository.GetQuery().Where(x => x.StateDesc.Contains(search)).Count();
            var obj = new
            {
                RoomateDocs=docs,
                RoomateDocsCount=RoomateDocCount
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var roomateDoc = roomateDocRepository.GetQuery().FirstOrDefault(z => z.RoomateDocId == id);
            await roomateDocRepository.Delete(roomateDoc);
        }

        public async Task<RoomateDoc> Get(long id)
        {
            var doc = roomateDocRepository.GetQuery().FirstOrDefault(z => z.RoomateDocId == id);
            doc.PassportImage = doc == null ? null : "media/gallery/PassportDocs/" + doc.PassportImage;
            doc.KartMelliImage = doc == null ? null : "media/gallery/KartMelliDocs/" + doc.KartMelliImage;
            return doc;
        }
        public RoomateDoc GetByUserId(long userId)
        {
            var doc = roomateDocRepository.GetQuery().FirstOrDefault(z => z.UserId == userId);
            doc.PassportImage = doc == null ? null : "media/gallery/PassportDocs/" + doc.PassportImage;
            doc.KartMelliImage = doc == null ? null : "media/gallery/KartMelliDocs/" + doc.KartMelliImage;
            return doc;
        }
        public async Task<RoomateDoc> ChangeStatus(long roomateDocId, string StateDesc, int status)
        {
            var dbRoomateDoc = roomateDocRepository.GetQuery().FirstOrDefault(z => z.RoomateDocId == roomateDocId);
            dbRoomateDoc.Status = status;
            dbRoomateDoc.StateDesc = StateDesc;
            dbRoomateDoc.UpdatedDate = DateTime.Now;

            if (status == (int)DocStatus.Confirmed)
            {
                var dbLastRoomate = roomateRepository.GetQuery().FirstOrDefault(z => z.UserId == dbRoomateDoc.UserId);
                if (dbLastRoomate == null)
                {
                    var dbRoomateInsert = roomateRepository.Insert(new Roomate()
                    {
                        IsDeleted = false,
                        ReceptionDate = null,
                        Address = "",
                        RoomateGender = "",
                        RoomateId = 0,
                        RoomType = 0,
                        BedroomCount = 0,
                        BedroomType = 0,
                        BathroomCount = 0,
                        BathroomType = 0,
                        OwnerType = 0,
                        UserId = dbRoomateDoc.UserId,
                    });
                }
                else
                {
                    await roomateRepository.Update(dbLastRoomate);
                }
            }
            return await roomateDocRepository.Update(dbRoomateDoc);
        }

        public object GetNotConfirmed(int pageNumber, int count,string search)
        {
            var docs = roomateDocRepository.GetQuery().Where(z => z.Status == (int)DocStatus.NotConfirmed && z.StateDesc.Contains(search)).
                OrderBy(z => z.UpdatedDate).Skip((pageNumber-1)*count).Take(count).ToList();
            docs.ForEach(z => z.PassportImage = string.IsNullOrEmpty(z.PassportImage) ? null : "media/gallery/PassportDocs/" + z.PassportImage);
            docs.ForEach(z => z.KartMelliImage = string.IsNullOrEmpty(z.KartMelliImage) ? null : "media/gallery/KartMelliDocs/" + z.KartMelliImage);
            docs.ForEach(z => z.UserInfo=userService.Get(z.UserId));

            var RoomateDocCount = roomateDocRepository.GetQuery().Where(z => z.Status == (int)DocStatus.NotConfirmed && z.StateDesc.Contains(search)).Count();
            var obj = new
            {
                RoomateDocs = docs,
                RoomateDocsCount = RoomateDocCount
            };
            return obj;
        }
        public object GetRejected(int pageNumber, int count,string search)
        {
            var docs = roomateDocRepository.GetQuery().Where(z => z.Status == (int)DocStatus.Rejected && z.StateDesc.Contains(search)).
                OrderBy(z => z.UpdatedDate).Skip((pageNumber-1)*count).Take(count).ToList();
            docs.ForEach(z => z.PassportImage = string.IsNullOrEmpty(z.PassportImage) ? null : "media/gallery/PassportDocs/" + z.PassportImage);
            docs.ForEach(z => z.KartMelliImage = string.IsNullOrEmpty(z.KartMelliImage) ? null : "media/gallery/KartMelliDocs/" + z.KartMelliImage);
            docs.ForEach(z => z.UserInfo=userService.Get(z.UserId));
             
            var RoomateDocCount = roomateDocRepository.GetQuery().Where(z => z.Status == (int)DocStatus.Rejected && z.StateDesc.Contains(search)).Count();
            var obj = new
            {
                RoomateDocs = docs,
                RoomateDocsCount = RoomateDocCount
            };
            return obj;
        }
    }
}
