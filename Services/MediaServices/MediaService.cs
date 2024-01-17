using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.MediaServices
{
    public class MediaService : IMediaService
    {
        private readonly IBanooClubEFRepository<Media> mediaRepository;

        public MediaService(IBanooClubEFRepository<Media> mediaRepository)
        {
            this.mediaRepository = mediaRepository;
        }
        public async Task Create(Media inputDto)
        {
            mediaRepository.Insert(inputDto);
        }

        public async Task<Media> Update(Media item)
        {
            await mediaRepository.Update(item);
            return item;
        }

        public async Task<List<Media>> GetAll()
        {
            List<Media> medias = new List<Media>();
            medias = mediaRepository.GetQuery().ToList();
            return medias;
        }

        public async Task Delete(long id)
        {
            var media = mediaRepository.GetQuery().FirstOrDefault(z => z.MediaId == id);
            await mediaRepository.Delete(media);
        }

        public async Task<Media> Get(long id)
        {
            var media = mediaRepository.GetQuery().FirstOrDefault(z => z.MediaId == id);
            return media;
        }

    }
}
