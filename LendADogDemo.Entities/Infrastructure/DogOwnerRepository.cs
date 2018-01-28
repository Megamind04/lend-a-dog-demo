using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace LendADogDemo.Entities.Infrastructure
{
    public class DogOwnerRepository : GenericRepository<DogOwner> ,IDogOwnerRepository
    {
        public DogOwnerRepository(LendADogDemoDb context) : base(context)
        {

        }

        public DogOwner GetByEmail(string email)
        {
            return dbSet.FirstOrDefault(x => x.Email == email);
        }
    }
}
