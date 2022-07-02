using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BanooClub.Extentions;
using System;

namespace BanooClub.Services.WishListServices
{
    public class WishListService : IWishListService
    {
        private readonly IBanooClubEFRepository<WishList> wishRepository;
        private readonly IHttpContextAccessor _accessor;

        public WishListService(IBanooClubEFRepository<WishList> wishRepository, IHttpContextAccessor accessor)
        {
            this.wishRepository = wishRepository;
            _accessor = accessor;
        }
        public async Task Create(WishList inputDto)
        {
            var userId = _accessor.HttpContext.User.Identity.IsAuthenticated
                    ? _accessor.HttpContext.User.Identity.GetUserId()
                    : 0;
            inputDto.UserId = userId;
            inputDto.CreateDate = DateTime.Now;
            wishRepository.Insert(inputDto);
        }

        public async Task<WishList> Update(WishList item)
        {
            await wishRepository.Update(item);
            return item;
        }

        public async Task<List<WishList>> GetAll()
        {
            List<WishList> wishes = new List<WishList>();
            wishes = wishRepository.GetQuery().ToList();
            return wishes;
        }

        public async Task Delete(long id)
        {
            var wish = wishRepository.GetQuery().FirstOrDefault(z => z.WishListId == id);
            await wishRepository.Delete(wish);
        }

        public async Task<WishList> Get(long id)
        {
            var wish = wishRepository.GetQuery().FirstOrDefault(z => z.WishListId == id);
            return wish;
        }
        public List<WishList> GetByUserId(long userId)
        {
            return wishRepository.GetQuery().Where(z => z.UserId == userId).ToList();
        }
    }
}
