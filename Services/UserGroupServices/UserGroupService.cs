using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.UserGroupServices
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IBanooClubEFRepository<UserGroup> userGroupRepository;
        public UserGroupService(IBanooClubEFRepository<UserGroup> userGroupRepository)
        {
            this.userGroupRepository = userGroupRepository;
        }
        public async Task Create(UserGroup inputDto)
        {
            userGroupRepository.Insert(inputDto);
        }

        public async Task<UserGroup> Update(UserGroup item)
        {
            await userGroupRepository.Update(item);
            return item;
        }

        public async Task<List<UserGroup>> GetAll()
        {
            List<UserGroup> userGroups = new List<UserGroup>();
            userGroups = userGroupRepository.GetQuery().ToList();
            return userGroups;
        }

        public async Task Delete(long id)
        {
            var userGroup = userGroupRepository.GetQuery().FirstOrDefault(z => z.UserGroupId == id);
            await userGroupRepository.Delete(userGroup);
        }

        public async Task<UserGroup> Get(long id)
        {
            var userGroup = userGroupRepository.GetQuery().FirstOrDefault(z => z.UserGroupId == id);
            return userGroup;
        }
        public List<UserGroup> GetByUserId(long userId)
        {
            return userGroupRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
    }
}
