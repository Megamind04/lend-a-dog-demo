using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogRepository : GenericRepository<Dog>, IDogRepository
    {
        public DogRepository(LendADogDemoDb context) : base(context)
        {
            
        }
        
        public IEnumerable<Dog> GetDogWithPhotos(string dogOwnerId)
        {
            return dbSet.Where(d => d.DogOwnerID == dogOwnerId).Include(g => g.DogPhotos);
        }
    }
}
