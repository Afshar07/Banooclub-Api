using Infrastructure;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ServiceCommentServices
{
    public class ServiceCommentService : IServiceCommentService
    {
        private readonly IBanooClubEFRepository<ServiceComment> serviceCommentRepository;
        private readonly IHttpContextAccessor _accessor;
        public ServiceCommentService(IBanooClubEFRepository<ServiceComment> serviceCommentRepository , IHttpContextAccessor accessor)
        {
            this.serviceCommentRepository = serviceCommentRepository;
            _accessor = accessor;
        }
        public async Task<long> Create(ServiceComment inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;  
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = serviceCommentRepository.Insert(inputDto);
            return creation.ServiceCommentId;
        }

        public async Task<ServiceComment> Update(ServiceComment item)
        {
            await serviceCommentRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<ServiceComment> serviceComments = new List<ServiceComment>();
            serviceComments = serviceCommentRepository.GetQuery().Where(z => z.Text.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var serviceCommentsCount = serviceComments.Count();
            
            if (pageNumber != 0 && count != 0)
            {
                serviceComments = serviceComments.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            var obj = new
            {
                ServiceComments = serviceComments,
                ServiceCommentsCount = serviceCommentsCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var serviceComment = serviceCommentRepository.GetQuery().FirstOrDefault(z => z.ServiceCommentId == id);
                await serviceCommentRepository.Delete(serviceComment);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ServiceComment> Get(long id)
        {
            var serviceComment = serviceCommentRepository.GetQuery().FirstOrDefault(z => z.ServiceCommentId == id);
            return serviceComment;
        }
    }
}
