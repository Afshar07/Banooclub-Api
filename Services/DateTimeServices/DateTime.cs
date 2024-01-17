namespace BanooClub.Services.DateTimeServices
{
    internal class DateTime : IDateTime
    {
        public System.DateTime Now() => System.DateTime.Now;
        public System.DateTime MaxValue() => System.DateTime.MaxValue;
    }
}
