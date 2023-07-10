namespace BanooClub.Services.SkyroomService
{
    public class CreateRoomInputModel
    {
        public string name { get; set; }
        public string title { get; set; }
        public bool guest_login { get; set; }
        public bool op_login_first { get; set; }
        public int max_users { get; set; }
    }
}
