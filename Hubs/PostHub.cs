using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BanooClub.Hubs
{
    public class PostHub : Hub
    {
        public async Task PostCreated(long userId, string message)   // test
        {
            await Clients.All.SendAsync("PostCreated", userId, message);
        }
    }
}
