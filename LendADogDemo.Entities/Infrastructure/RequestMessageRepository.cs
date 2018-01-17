using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace LendADogDemo.Entities.Infrastructure
{
    public class RequestMessageRepository : GenericRepository<RequestMessage>,IRequestMessageRepository
    {
        public RequestMessageRepository(LendADogDemoDb context) : base(context)
        {

        }

        public IEnumerable<RequestMessage> GetWithDogOwnerSender(string dogOwnerId)
        {
            return dbSet.Where(x => x.ReceiverID == dogOwnerId).Include(u => u.SendRequestMessage);
        }
    }
}
