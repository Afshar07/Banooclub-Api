namespace BanooClub.Services.SkyroomService
{
    public class InputModel<T> where T : class
    {
        public string action { get; set; }
        public T @params {get;set;}
    }
}
