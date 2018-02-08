using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using LendADogDemo.Entities.UoW;

namespace LendADogDemo.Entities.Infrastructure
{
    public class RequestMessageRepository : GenericRepository<RequestMessage>,IRequestMessageRepository
    {
        public RequestMessageRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }

        public IEnumerable<RequestMessage> GetUnconfirmedRequests(string userID)
        {
            return dbSet
                .Where(x => x.ReciverID == userID)
                .Include(x => x.SenderOfRequest)
                .Where(x=>x.SenderOfRequest.IsConfirmed == false)
                .AsNoTracking();
        }
    }
}
