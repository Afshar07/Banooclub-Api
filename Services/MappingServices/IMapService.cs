namespace BanooClub.Services.MappingServices
{
    public interface IMapService
    {
        string ReverseGeoCoding(double latitude, double longitude);
    }
}
