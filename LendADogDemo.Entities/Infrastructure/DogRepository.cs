using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using LendADogDemo.Entities.UoW;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogRepository : GenericRepository<Dog>, IDogRepository
    {
        public DogRepository(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }

        public IEnumerable<Dog> GetDogPerOwner(string dogOwnerId)
        {
            return dbSet
                //.Include(g => g.DogPhotos)
                .Where(d => d.DogOwnerID == dogOwnerId)
                .AsNoTracking();
        }
    }
}
