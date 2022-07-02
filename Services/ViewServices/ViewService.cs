using Infrastructure;
using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.ViewServices
{
    public class ViewService : IViewService
    {
        private readonly IBanooClubEFRepository<View> viewRepository;
        public ViewService(IBanooClubEFRepository<View> viewRepository)
        {
            this.viewRepository = viewRepository;
        }
        public async Task<long> Create(View inputDto)
        {
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = viewRepository.Insert(inputDto);
            return creation.ViewId;
        }

        public async Task<View> Update(View item)
        {
            await viewRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {
            List<View> views = new List<View>();
            views = viewRepository.GetQuery().OrderByDescending(z => z.CreateDate).ToList();
            var ViewsCount = views.Count();
            if (pageNumber != 0 && count != 0)
            {
                views = views.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            var obj = new
            {
                Views = views,
                ViewsCount = ViewsCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var view = viewRepository.GetQuery().FirstOrDefault(z => z.ViewId == id);
                await viewRepository.Delete(view);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<View> Get(long id)
        {
            var view = viewRepository.GetQuery().FirstOrDefault(z => z.ViewId == id);
            return view;
        }
    }
}
