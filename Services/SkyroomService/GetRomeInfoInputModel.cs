namespace BanooClub.Services.SkyroomService
{
    public class GetRomeInfoInputModel
    {
        public GetRomeInfoInputModel()
        {
            language = "fa";
        }
        public long room_id { get; set; }
        public string language { get; set; }
    }
}
