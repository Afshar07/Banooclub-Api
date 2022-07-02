namespace BanooClub.Services.CommonServices
{
    public interface ICommonService
    {
        object GetIndexData();
        object GetUserIndex(long userId);
        object GetUserIndexByUserName(string userName);
    }
}
