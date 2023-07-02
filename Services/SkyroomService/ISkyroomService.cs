using System.Threading.Tasks;

namespace BanooClub.Services.SkyroomService
{
    public interface ISkyroomService
    {
        Task<ReponseVM<long>> CreateRoome(InputModel<CreateRoomInputModel> input);
        Task<ReponseVM<string>> GetRoomUrl(InputModel<GetRomeInfoInputModel> input);
    }
}
