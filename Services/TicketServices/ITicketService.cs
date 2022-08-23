using BanooClub.Models;
using BanooClub.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanooClub.Services.TicketServices
{
    public interface ITicketService
    {
        Task<IServiceResult<Ticket>> FindAsync(long ticketId);
        IServiceResult<object> GetAll(int pageNumber, int count);
        IServiceResult<object> GetAllForCustomer(int pageNumber, int count);
        Task<IServiceResult<object>> GetAllByParentId(long parentId);
        Task<IServiceResult> DeleteTicket(long ticketId);
        Task<IServiceResult<List<long>>> CreateTicket(Ticket model);
        IServiceResult<bool> ChangeTicketStatus(long ticketId);
        IServiceResult<Ticket> GetById(long id);
        Task<IServiceResult<bool>> CloseTicket(long parentId);
        int UserUnreadTicketCounts();
        IServiceResult<object> GetTicketsByFilter(long? UserType, int? Type, bool? isRead, int pageNumber, int count,string search);
    }
}
