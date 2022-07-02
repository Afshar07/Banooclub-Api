using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipPostCommentServices
{
    public class FriendshipPostCommentService : IFriendshipPostCommentService
    {
        private readonly IBanooClubEFRepository<FriendshipPostComment> friendshipPostCommentRepository;

        public FriendshipPostCommentService(IBanooClubEFRepository<FriendshipPostComment> friendshipPostCommentRepository)
        {
            this.friendshipPostCommentRepository = friendshipPostCommentRepository;
        }
        public async Task Create(FriendshipPostComment inputDto)
        {
            friendshipPostCommentRepository.Insert(inputDto);
        }

        public async Task<FriendshipPostComment> Update(FriendshipPostComment item)
        {
            await friendshipPostCommentRepository.Update(item);
            return item;
        }

        public async Task<List<FriendshipPostComment>> GetAll()
        {
            List<FriendshipPostComment> dbFriendships = new List<FriendshipPostComment>();
            dbFriendships = friendshipPostCommentRepository.GetQuery().ToList();
            return dbFriendships;
        }

        public async Task Delete(long id)
        {
            var dbFriendship = friendshipPostCommentRepository.GetQuery().FirstOrDefault(z => z.FriendshipPostCommentId == id);
            await friendshipPostCommentRepository.Delete(dbFriendship);
        }

        public async Task<FriendshipPostComment> Get(long id)
        {
            var dbFriendship = friendshipPostCommentRepository.GetQuery().FirstOrDefault(z => z.FriendshipPostCommentId == id);
            return dbFriendship;
        }

    }
}
