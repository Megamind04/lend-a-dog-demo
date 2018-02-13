using LendADogDemo.Entities.Models;
using System.Collections.Generic;

namespace LendADogDemo.Entities.Interfaces
{
    public interface IDogRepository : IRepository<Dog>
    {
        IEnumerable<Dog> GetDogPerOwner(string dogOwnerId);
    }
}
