namespace BanooClub.Services.SkyroomService
{
    public class CreateUserInputModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public int status { get; set; }
        public bool is_public { get; set; }
    }
}
