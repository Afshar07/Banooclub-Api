using BanooClub.Models;
using BanooClub.Models.Enums;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanooClub.Services.PlanServices
{
    public class PlanService : IPlanService
    {
        private readonly IBanooClubEFRepository<Plan> PlanRepository;
        public PlanService(IBanooClubEFRepository<Plan> PlanRepository)
        {
            this.PlanRepository = PlanRepository;
        }
        public async Task Create(Plan inputDto)
        {
            var pnl = PlanRepository.GetQuery().FirstOrDefault(x => x.Type == inputDto.Type && x.EntityType == inputDto.EntityType);
            if (pnl == null)
            {
                inputDto.UpdateDate = DateTime.UtcNow;
                PlanRepository.Insert(inputDto);
            }
        }

        public async Task<Plan> Update(Plan item)
        {
            item.UpdateDate = DateTime.Now;
            await PlanRepository.Update(item);
            return item;
        }

        public object GetAll(int pageNumber, int count)
        {

            List<Plan> Plans = new List<Plan>();
            if (count == 0 && pageNumber == 0)
            {

                Plans = PlanRepository.GetQuery().OrderByDescending(x => x.CreateDate).ToList();
            }
            else
            {

                Plans = PlanRepository.GetQuery().OrderByDescending(x => x.CreateDate).Skip((pageNumber - 1) * count).Take(count).ToList();
            }

            var PlansCount = PlanRepository.GetQuery().Count();
            var obj = new
            {
                Plans = Plans,
                PlansCount = PlansCount,
            };
            return obj;
        }

        public async Task Delete(long id)
        {
            var Plan = PlanRepository.GetQuery().FirstOrDefault(z => z.PlanId == id);
            Plan.UpdateDate = DateTime.Now;
            await PlanRepository.Delete(Plan);
        }

        public async Task<Plan> Get(long id)
        {
            var Plan = PlanRepository.GetQuery().FirstOrDefault(z => z.PlanId == id);
            return Plan;
        }

        public async Task<Plan> GetPanelByType(PlanTypes planType)
        {
            var plan = PlanRepository.GetQuery()
                 .FirstOrDefault(z => z.Type == (int)planType);
            return plan;
        }
    }
}
