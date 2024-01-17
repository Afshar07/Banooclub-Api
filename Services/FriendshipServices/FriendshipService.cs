using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.FriendshipServices
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IBanooClubEFRepository<Friendship> friendshipRepository;

        public FriendshipService(IBanooClubEFRepository<Friendship> friendshipRepository)
        {
            this.friendshipRepository = friendshipRepository;
        }
        public async Task Create(Friendship inputDto)
        {
            friendshipRepository.Insert(inputDto);
        }

        public async Task<Friendship> Update(Friendship item)
        {
            await friendshipRepository.Update(item);
            return item;
        }

        public async Task<List<Friendship>> GetAll()
        {
            List<Friendship> dbFriendships = new List<Friendship>();
            dbFriendships = friendshipRepository.GetQuery().ToList();
            return dbFriendships;
        }

        public async Task Delete(long id)
        {
            var dbFriendship = friendshipRepository.GetQuery().FirstOrDefault(z => z.FriendshipId == id);
            await friendshipRepository.Delete(dbFriendship);
        }

        public async Task<Friendship> Get(long id)
        {
            var dbFriendship = friendshipRepository.GetQuery().FirstOrDefault(z => z.FriendshipId == id);
            return dbFriendship;
        }

    }
}
