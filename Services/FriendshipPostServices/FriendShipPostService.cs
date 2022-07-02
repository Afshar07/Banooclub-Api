using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipPostServices
{
    public class FriendShipPostService : IFriendshipPostService
    {
        private readonly IBanooClubEFRepository<FriendshipPost> friendshipPostRepository;

        public FriendShipPostService(IBanooClubEFRepository<FriendshipPost> friendshipPostRepository)
        {
            this.friendshipPostRepository = friendshipPostRepository;
        }
        public async Task Create(FriendshipPost inputDto)
        {
            friendshipPostRepository.Insert(inputDto);
        }

        public async Task<FriendshipPost> Update(FriendshipPost item)
        {
            await friendshipPostRepository.Update(item);
            return item;
        }

        public async Task<List<FriendshipPost>> GetAll()
        {
            List<FriendshipPost> dbFriendships = new List<FriendshipPost>();
            dbFriendships = friendshipPostRepository.GetQuery().ToList();
            return dbFriendships;
        }

        public async Task Delete(long id)
        {
            var dbFriendship = friendshipPostRepository.GetQuery().FirstOrDefault(z => z.FriendshipPostId == id);
            await friendshipPostRepository.Delete(dbFriendship);
        }

        public async Task<FriendshipPost> Get(long id)
        {
            var dbFriendship = friendshipPostRepository.GetQuery().FirstOrDefault(z => z.FriendshipPostId == id);
            return dbFriendship;
        }

    }
}
