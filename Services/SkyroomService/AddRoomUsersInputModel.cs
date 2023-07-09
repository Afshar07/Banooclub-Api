using System.Collections.Generic;

namespace BanooClub.Services.SkyroomService
{
    public class AddRoomUsersInputModel
    {
        public AddRoomUsersInputModel()
        {
            users = new List<UserAddRoomUsersInputModel>();
        }

        public long room_id { get; set; }
        public List<UserAddRoomUsersInputModel> users { get; set; }
    }
}
