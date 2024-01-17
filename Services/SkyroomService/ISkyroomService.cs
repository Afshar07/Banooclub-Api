using System.Threading.Tasks;

namespace BanooClub.Services.SkyroomService
{
    public interface ISkyroomService
    {
        Task<ReponseVM<long>> CreateRoome(InputModel<CreateRoomInputModel> input);
        Task<ReponseVM<long>> AddRoomUsers(InputModel<AddRoomUsersInputModel> input);
        Task<ReponseVM<string>> GetRoomUrl(InputModel<GetRomeInfoInputModel> input);
        Task<ReponseVM<long>> CreateUser(InputModel<CreateUserInputModel> input);
        Task<ReponseVM<SearchResultResponseVM>> GetUser(InputModel<GetUserInputModel> input);
    }
}
