using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BanooClub.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task PostCreated(long userId, string message)   // test
        {
            await Clients.All.SendAsync("PostCreated", userId, message);
        }
    }
}
