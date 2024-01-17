//using BanooClub.Models;
//using Infrastructure;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BanooClub.Services.RequestConsultServices
//{
//    public class RequestConsultService : IRequestConsultService
//    {
//        private readonly IBanooClubEFRepository<RequestConsult> requestConsultRepository;

//        public RequestConsultService(IBanooClubEFRepository<RequestConsult> requestConsultRepository)
//        {
//            this.requestConsultRepository = requestConsultRepository;
//        }
//        public async Task Create(RequestConsult inputDto)
//        {
//            requestConsultRepository.Insert(inputDto);
//        }

//        public async Task<RequestConsult> Update(RequestConsult item)
//        {
//            await requestConsultRepository.Update(item);
//            return item;
//        }

//        public async Task<List<RequestConsult>> GetAll()
//        {
//            List<RequestConsult> dbRequestConsults = new List<RequestConsult>();
//            dbRequestConsults = requestConsultRepository.GetQuery().ToList();
//            return dbRequestConsults;
//        }

//        public async Task Delete(long id)
//        {
//            var dbRequestConsult = requestConsultRepository.GetQuery().FirstOrDefault(z => z.RequestConsultId == id);
//            await requestConsultRepository.Delete(dbRequestConsult);
//        }

//        public async Task<RequestConsult> Get(long id)
//        {
//            var dbRequestConsult = requestConsultRepository.GetQuery().FirstOrDefault(z => z.RequestConsultId == id);
//            return dbRequestConsult;
//        }

//    }
//}
