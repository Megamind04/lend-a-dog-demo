using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using LendADogDemo.Entities.UoW;

namespace LendADogDemo.Entities.Infrastructure
{
    public class MainMessageBoardRepository : GenericRepository<MainMessageBoard>,IMainMessageBoardRepository
    {
        public MainMessageBoardRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }

        public IEnumerable<MainMessageBoard> GetUnansweredMessagesWithDogs()
        {
            return dbSet.Where(m => m.Answered == false).Include(d => d.Dog).Include(o=>o.RequestSender);
        }
    }
}
