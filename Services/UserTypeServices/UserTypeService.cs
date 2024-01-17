using BanooClub.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.UserTypeServices
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IBanooClubEFRepository<UserType> userTypeRepository;

        public UserTypeService(IBanooClubEFRepository<UserType> userTypeRepository)
        {
            this.userTypeRepository = userTypeRepository;
        }
        public async Task Create(UserType inputDto)
        {
            userTypeRepository.Insert(inputDto);
        }

        public async Task<UserType> Update(UserType item)
        {
            await userTypeRepository.Update(item);
            return item;
        }

        public async Task<List<UserType>> GetAll()
        {
            List<UserType> dbUserTypes = new List<UserType>();
            dbUserTypes = userTypeRepository.GetQuery().ToList();
            return dbUserTypes;
        }

        public async Task Delete(long id)
        {
            var dbUserType = userTypeRepository.GetQuery().FirstOrDefault(z => z.UserTypeId == id);
            await userTypeRepository.Delete(dbUserType);
        }

        public async Task<UserType> Get(long id)
        {
            var dbUserType = userTypeRepository.GetQuery().FirstOrDefault(z => z.UserTypeId == id);
            return dbUserType;
        }
        public List<UserType> GetByCommand(string sql)
        {
            var dbUserType = userTypeRepository.SqlExecute(sql).ToList();
            return dbUserType;
        }
        public long UpdateByCmd(string name, long id)
        {
            string UpdateCmd = $"Update [User].[Types] Set Name = '{name}' Where UserTypeId={id}";
            try
            {
                var result = userTypeRepository.SqlExecute(UpdateCmd);
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
