using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using LendADogDemo.Entities.UoW;

namespace LendADogDemo.Entities.Infrastructure
{
    public class PrivateMessageBoardRepository : GenericRepository<PrivateMessageBoard>,IPrivateMessageBoardRepository
    {
        public PrivateMessageBoardRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }

        public IEnumerable<PrivateMessageBoard> GetByDogOwnerId(string userId)
        {
            return dbSet
                .Include(x => x.SenderOfPrivateMessage)
                .Include(x => x.ReceiverOfPrivateMessage)
                .Where(x => x.RrecivedFromID == userId || x.SendFromID == userId)
                .AsNoTracking();      
        }

        public IEnumerable<PrivateMessageBoard> GetMessagesBetweenTwoUsers(string userId,string otherId)
        {
            return dbSet
                .Where(x => (x.RrecivedFromID == userId && x.SendFromID == otherId)
                || (x.SendFromID == userId && x.RrecivedFromID == otherId))
                .Include(x => x.SenderOfPrivateMessage)
                .OrderBy(d => d.CreateDate);
        }
    }
}
