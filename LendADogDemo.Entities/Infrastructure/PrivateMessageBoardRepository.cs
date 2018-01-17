using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;

namespace LendADogDemo.Entities.Infrastructure
{
    public class PrivateMessageBoardRepository : GenericRepository<PrivateMessageBoard>,IPrivateMessageBoardRepository
    {
        public PrivateMessageBoardRepository(LendADogDemoDb context) : base(context)
        {

        }

        public IEnumerable<PrivateMessageBoard> GetByDogOwnerId(string userId)
        {
            return dbSet.Where(x => x.ReceiverID == userId || x.SenderID == userId)
                .Include(x => x.SenderDogOwner).Include(x => x.ReceiverDogOwner);
        }
    }
}
