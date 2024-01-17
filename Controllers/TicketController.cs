using BanooClub.Models;
using BanooClub.Services.TicketServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BanooClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("GetAllByParentId")]
        public IActionResult GetAllByParentId(long parentId)
        {
            var result = _ticketService.GetAllByParentId(parentId).Result;

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }
        [HttpPost("GetAll"), Authorize(Roles = "Admin")]
        public IActionResult GetAll(int pageNumber, int count, string searchCommand, byte ticketType)
        {
            var result = _ticketService.GetAll(pageNumber, count, searchCommand, ticketType);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("GetAllForCustomer")]
        public IActionResult GetAllForCustomer(int pageNumber, int count)
        {
            var result = _ticketService.GetAllForCustomer(pageNumber, count);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket(Ticket model)
        {
            var result = await _ticketService.CreateTicket(model);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("DeleteTicket")]
        public async Task<IActionResult> DeleteTicket(long ticketId)
        {
            var result = _ticketService.DeleteTicket(ticketId).Result;

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("ChangeTicketStatus"), Authorize(Roles = "Admin")]
        public IActionResult ChangeTicketStatus(long parentId)
        {
            var result = _ticketService.ChangeTicketStatus(parentId);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("ChangeTicketType"), Authorize(Roles = "Admin")]
        public IActionResult ChangeTicketType(long parentId, byte ticketType)
        {
            var result = _ticketService.ChangeTicketType(parentId, ticketType);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("CloseTicket"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CloseTicket(long parentId)
        {
            var result = await _ticketService.CloseTicket(parentId);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("GetTicketsByFilter"), Authorize(Roles = "Admin")]
        public IActionResult GetTicketsByFilter(long? userType, int? type, bool? isRead, int pageNumber, int count, string search)
        {
            search = search == null ? "" : search;
            var result = _ticketService.GetTicketsByFilter(userType, type, isRead, pageNumber, count, search);

            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result.ErrorMessage);
        }

    }
}
