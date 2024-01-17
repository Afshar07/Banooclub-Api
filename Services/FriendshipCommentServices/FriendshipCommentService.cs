using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipCommentServices
{
    public class FriendshipCommentService : IFriendshipCommentService
    {
        private readonly IBanooClubEFRepository<FriendshipComment> friendshipCommentRepository;

        public FriendshipCommentService(IBanooClubEFRepository<FriendshipComment> friendshipCommentRepository)
        {
            this.friendshipCommentRepository = friendshipCommentRepository;
        }
        public async Task Create(FriendshipComment inputDto)
        {
            friendshipCommentRepository.Insert(inputDto);
        }

        public async Task<FriendshipComment> Update(FriendshipComment item)
        {
            await friendshipCommentRepository.Update(item);
            return item;
        }

        public async Task<List<FriendshipComment>> GetAll()
        {
            List<FriendshipComment> dbFriendshipComments = new List<FriendshipComment>();
            dbFriendshipComments = friendshipCommentRepository.GetQuery().ToList();
            return dbFriendshipComments;
        }

        public async Task Delete(long id)
        {
            var dbFriendshipComment = friendshipCommentRepository.GetQuery().FirstOrDefault(z => z.FriendshipCommentId == id);
            await friendshipCommentRepository.Delete(dbFriendshipComment);
        }

        public async Task<FriendshipComment> Get(long id)
        {
            var dbFriendshipComment = friendshipCommentRepository.GetQuery().FirstOrDefault(z => z.FriendshipCommentId == id);
            return dbFriendshipComment;
        }

    }
}
