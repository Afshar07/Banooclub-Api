using BanooClub.Models;
using BanooClub.Models.Enums;
using BanooClub.Services.Common;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.SocialMediaServices
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IBanooClubEFRepository<SocialMedia> socialMediaRepository;

        public SocialMediaService(IBanooClubEFRepository<SocialMedia> socialMediaRepository)
        {
            this.socialMediaRepository = socialMediaRepository;
        }

        public async Task<IServiceResult<Dictionary<string, string>>> Upload(List<IFormFile> files, int objectId, MediaTypes type)
        {
            var data = new Dictionary<string, string>();

            var now = DateTime.Now;
            var folder = Path.Combine(now.Year.ToString(), now.Month.ToString(), now.Day.ToString());
            string currentDirectory = Directory.GetCurrentDirectory();

            if (!Directory.Exists(Path.Combine(currentDirectory, "wwwroot", folder)))
                Directory.CreateDirectory(Path.Combine(currentDirectory, "wwwroot", folder));

            foreach (var file in files)
            {
                string newFileName = $"{now.Hour}-{now.Minute}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                using (var stream = new FileStream(Path.Combine(currentDirectory, "wwwroot", folder, newFileName), FileMode.Create))
                    await file.CopyToAsync(stream);
                var val = "/" + Path.Combine(folder, newFileName).Replace("\\", "/");
                data.Add(file.FileName, val);
                await socialMediaRepository.InsertAsync(new SocialMedia()
                {
                    IsDeleted = false,
                    ObjectId = objectId,
                    PictureUrl = val,
                    Type = type
                });
            }

            return new ServiceResult<Dictionary<string, string>>().Ok(data);
        }

        public IServiceResult<int> Create(SocialMedia media)
        {
            try
            {
                socialMediaRepository.Insert(media);
                return new ServiceResult<int>().Ok(1);
            }
            catch (Exception ex)
            {
                return new ServiceResult<int>().Ok(0);
            }


        }

        public Task<IServiceResult<List<SocialMedia>>> GetByObjectId(int objectId, MediaTypes type)
                => Task.FromResult(new ServiceResult<List<SocialMedia>>().Ok(socialMediaRepository.GetQuery().Where(z => z.ObjectId == objectId & z.Type == type).ToList()));

        public IServiceResult DeleteByMediaId(int mediaId)
        {
            var media = socialMediaRepository.GetQuery().FirstOrDefault(z => z.MediaId == mediaId);
            socialMediaRepository.Delete(media);
            return new ServiceResult().Ok();
        }
        public Task<IServiceResult> DeleteByObjectId(int objectId, MediaTypes type)
        {
            var mediae = socialMediaRepository.GetQuery().Where(z => z.ObjectId == objectId & z.Type == type).ToList();

            foreach (var item in mediae)
                socialMediaRepository.Delete(item);

            return Task.FromResult(new ServiceResult().Ok());
        }
        public OutPutSaveImage SaveImageNew(string base64, string path, int? priority)
        {
            OutPutSaveImage outPutSave = new OutPutSaveImage();
            try
            {
                if (base64.StartsWith("data:image/png;base64,"))
                {
                    base64 = base64.Split(",")[1];
                }
                byte[] contents = Convert.FromBase64String(base64);

                string fileName = System.Guid.NewGuid() + ".jpg";
                if (priority != null && priority == 3)
                {
                    fileName = System.Guid.NewGuid() + ".mp4";
                }
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

                System.IO.File.WriteAllBytes(imagePath, contents);
                outPutSave.ImageName = fileName;
                outPutSave.IsSuccess = true;
                return outPutSave;
            }
            catch (Exception ex)
            {
                outPutSave.IsSuccess = false;
                outPutSave.ImageName = "";
                return outPutSave;
            }

        }
        public OutPutSaveImage SaveImage(string base64, string path)
        {
            OutPutSaveImage outPutSave = new OutPutSaveImage();
            try
            {
                if (base64.StartsWith("data:image/png;base64,"))
                {
                    base64 = base64.Split(",")[1];
                }
                byte[] contents = Convert.FromBase64String(base64);

                string fileName = System.Guid.NewGuid() + ".jpg";
                if (path.Contains("Video"))
                {
                    fileName = System.Guid.NewGuid() + ".mp4";
                }
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

                System.IO.File.WriteAllBytes(imagePath, contents);
                outPutSave.ImageName = fileName;
                outPutSave.IsSuccess = true;
                return outPutSave;
            }
            catch (Exception ex)
            {
                outPutSave.IsSuccess = false;
                outPutSave.ImageName = "";
                return outPutSave;
            }

        }
    }
}
