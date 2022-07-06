using BanooClub.Models;
using Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace BanooClub.Services.StateServices
{
    public class StateService : IStateService
    {
        private readonly IBanooClubEFRepository<State> stateRepository;
        public StateService(IBanooClubEFRepository<State> stateRepository)
        {
            this.stateRepository = stateRepository;
        }

        public object GetStates(int pageNumber, int count)
        {

            List<State> States = new List<State>();
            States = stateRepository.GetQuery().ToList();
            var StatesCount = States.Count();
            if (pageNumber != 0 && count != 0)
            {
                States = stateRepository.GetQuery().Skip((pageNumber-1)*count).Take(count).ToList();
            }

            var obj = new
            {
                States = States,
                StatesCount = StatesCount
            };
            return obj;
        }
    }
}
