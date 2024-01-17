//using BanooClub.Models;
//using Infrastructure;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BanooClub.Services.ConcultCommentServices
//{
//    public class ConsultCommentService : IConsultCommentService
//    {
//        private readonly IBanooClubEFRepository<ConsultComment> ConsultCommentRepository;

//        public ConsultCommentService(IBanooClubEFRepository<ConsultComment> ConsultCommentRepository)
//        {
//            this.ConsultCommentRepository = ConsultCommentRepository;
//        }
//        public async Task Create(ConsultComment inputDto)
//        {
//            ConsultCommentRepository.Insert(inputDto);
//        }

//        public async Task<ConsultComment> Update(ConsultComment item)
//        {
//            await ConsultCommentRepository.Update(item);
//            return item;
//        }

//        public object GetAll(int pageNumber, int count)
//        {
//            List<ConsultComment> dbComment = new List<ConsultComment>();
//            dbComment = ConsultCommentRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
//            var dbCommentsCount = ConsultCommentRepository.GetQuery().Count();
//            var obj = new
//            {
//                Comments = dbComment,
//                CommentsCount = dbCommentsCount
//            };
//            return obj;
//        }

//        public async Task Delete(long id)
//        {
//            var dbComment = ConsultCommentRepository.GetQuery().FirstOrDefault(z => z.ConsultCommentId == id);
//            await ConsultCommentRepository.Delete(dbComment);
//        }

//        public async Task<ConsultComment> Get(long id)
//        {
//            var dbComment = ConsultCommentRepository.GetQuery().FirstOrDefault(z => z.ConsultCommentId == id);
//            return dbComment;
//        }

//    }
//}
