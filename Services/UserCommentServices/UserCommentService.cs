using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.UserCommentServices
{
    public class UserCommentService : IUserCommentService
    {
        private readonly IBanooClubEFRepository<UserComment> userCommentRepository;

        public UserCommentService(IBanooClubEFRepository<UserComment> userCommentRepository)
        {
            this.userCommentRepository = userCommentRepository;
        }
        public async Task Create(UserComment inputDto)
        {
            userCommentRepository.Insert(inputDto);
        }

        public async Task<UserComment> Update(UserComment item)
        {
            await userCommentRepository.Update(item);
            return item;
        }

        public async Task<List<UserComment>> GetAll()
        {
            List<UserComment> dbUserComments = new List<UserComment>();
            dbUserComments = userCommentRepository.GetQuery().ToList();
            return dbUserComments;
        }

        public async Task Delete(long id)
        {
            var dbUserComment = userCommentRepository.GetQuery().FirstOrDefault(z => z.UserCommentId == id);
            await userCommentRepository.Delete(dbUserComment);
        }

        public async Task<UserComment> Get(long id)
        {
            var dbUserComment = userCommentRepository.GetQuery().FirstOrDefault(z => z.UserCommentId == id);
            return dbUserComment;
        }

    }
}
