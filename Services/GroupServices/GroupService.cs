using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Models.Urls;
using BanooClub.Services.SocialMediaServices;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.GroupServices
{
    public class GroupService : IGroupService
    {
        private readonly IBanooClubEFRepository<Group> groupRepository;
        private readonly IBanooClubEFRepository<UserGroup> userGroupRepository;
        private readonly ISocialMediaService _mediaService;
        private readonly IBanooClubEFRepository<SocialMedia> _mediaRepository;
        public GroupService(IBanooClubEFRepository<Group> groupRepository
            , IBanooClubEFRepository<UserGroup> userGroupRepository
            , IBanooClubEFRepository<SocialMedia> mediaRepository
            , ISocialMediaService mediaService)
        {
            this.groupRepository = groupRepository;
            this.userGroupRepository = userGroupRepository;
            _mediaRepository = mediaRepository;
            _mediaService = mediaService;
        }
        public async Task Create(Group inputDto)
        {
            inputDto.IsDeleted = false;
            var dbGroup = groupRepository.Insert(inputDto);
            foreach (var userId in inputDto.Members)
            {
                UserGroup userGroup = new UserGroup()
                {
                    CreateDate=DateTime.Now,
                    IsActive=true,
                    IsDeleted=false,
                    GroupId=dbGroup.GroupId,
                    UserGroupId=0,
                    UserId=userId
                };
                userGroupRepository.Insert(userGroup);
            }
            if (!string.IsNullOrEmpty(inputDto.FileData))
            {
                var outPut = _mediaService.SaveImage(inputDto.FileData, EntityUrls.Group);
                SocialMedia dbMedia = new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = dbGroup.GroupId,
                    PictureUrl = outPut.ImageName,
                    Type = MediaTypes.Group,
                    MediaId = 0
                };
                await _mediaRepository.InsertAsync(dbMedia);
            }
        }

        public async Task<Group> Update(Group item)
        {
            var dbLastGroup = _mediaRepository.GetQuery()
                .FirstOrDefault(z => z.ObjectId == item.GroupId && z.Type == MediaTypes.Group);

            if (item.FileData == "Delete")
            {
                if (dbLastGroup != null)
                {
                    dbLastGroup.PictureUrl = "";
                    await _mediaRepository.Update(dbLastGroup);
                    await _mediaRepository.Delete(dbLastGroup);
                }

            }
            else if (!string.IsNullOrEmpty(item.FileData))
            {
                var outPut = _mediaService.SaveImage(item.FileData, EntityUrls.Group);
                if (outPut.IsSuccess)
                {
                    SocialMedia dbMedia = new SocialMedia()
                    {
                        IsDeleted = false,
                        ObjectId = item.GroupId,
                        PictureUrl = outPut.ImageName,
                        Type = MediaTypes.Group,
                        MediaId = 0
                    };

                    if (dbLastGroup != null)
                    {
                        dbLastGroup.PictureUrl = outPut.ImageName;
                        await _mediaRepository.Update(dbLastGroup);
                        item.FileData = outPut.ImageName;
                    }
                    else
                    {
                        await _mediaRepository.InsertAsync(dbMedia);
                        item.FileData = outPut.ImageName;
                    }
                }
            }
            await groupRepository.Update(item);
            return item;
        }

        public async Task<List<Group>> GetAll()
        {
            List<Group> groups = new List<Group>();
            groups = groupRepository.GetQuery().ToList();
            groups.ForEach(z => z.FileData= _mediaRepository.GetQuery().FirstOrDefault(x => x.ObjectId == z.GroupId && x.Type == MediaTypes.Group) == null ?
             "" : "media/gallery/Group/" + _mediaRepository.GetQuery().FirstOrDefault(x => x.ObjectId == z.GroupId && x.Type == MediaTypes.Group).PictureUrl);

            foreach (var group in groups)
            {
                group.Members = userGroupRepository.GetQuery().Where(z => z.GroupId == group.GroupId).Select(x => x.UserId).ToList();
            }
            return groups;
        }

        public async Task Delete(long id)
        {
            var group = groupRepository.GetQuery().FirstOrDefault(z => z.GroupId == id);
            await groupRepository.Delete(group);
        }

        public async Task<Group> Get(long id)
        {
            var group = groupRepository.GetQuery().FirstOrDefault(z => z.GroupId == id);
            group.FileData = _mediaRepository.GetQuery().FirstOrDefault(x => x.ObjectId == group.GroupId && x.Type == MediaTypes.Group) == null ?
             "" : "media/gallery/Group/" + _mediaRepository.GetQuery().FirstOrDefault(x => x.ObjectId == group.GroupId && x.Type == MediaTypes.Group).PictureUrl;
            return group;
        }
        public async Task<bool> ChangeGroupStatus(long groupId)
        {
            var group = groupRepository.GetQuery().FirstOrDefault(z => z.GroupId == groupId);
            group.IsActive = group.IsActive == true ? false : true;
            await groupRepository.Update(group);
            return group.IsActive;
        }
    }
}
