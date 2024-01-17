using Infrastructure;
using BanooClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.TagServices
{
    public class TagService : ITagService
    {
        private readonly IBanooClubEFRepository<Tag> tagRepository;
        public TagService(IBanooClubEFRepository<Tag> tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        public async Task<long> Create(Tag inputDto)
        {
            inputDto.IsDeleted = false;
            inputDto.CreateDate = DateTime.Now;
            var creation = tagRepository.Insert(inputDto);
            return creation.TagId;
        }

        public async Task<Tag> Update(Tag item)
        {
            await tagRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count, string searchCommand)
        {
            if (searchCommand == null)
            {
                searchCommand = "";
            }
            List<Tag> tags = new List<Tag>();
            tags = tagRepository.GetQuery().Where(z => z.Title.Contains(searchCommand)).OrderByDescending(z => z.CreateDate).ToList();
            var tagsCount = tags.Count();
            if (pageNumber != 0 && count != 0)
            {
                tags = tags.Skip((pageNumber - 1) * count).Take(count).ToList();
            }
            var obj = new
            {
                Tags = tags,
                TagsCount = tagsCount,
            };
            return obj;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var forum = tagRepository.GetQuery().FirstOrDefault(z => z.TagId == id);
                await tagRepository.Delete(forum);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Tag> Get(long id)
        {
            var tag = tagRepository.GetQuery().FirstOrDefault(z => z.TagId == id);
            return tag;
        }
    }
}
