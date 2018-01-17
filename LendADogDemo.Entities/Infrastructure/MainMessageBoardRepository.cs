using LendADogDemo.Entities.Interfaces;
using LendADogDemo.Entities.Models;
using LendADogDemo.Entities.DataContexts;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace LendADogDemo.Entities.Infrastructure
{
    class MainMessageBoardRepository : GenericRepository<MainMessageBoard>,IMainMessageBoardRepository
    {
        public MainMessageBoardRepository(LendADogDemoDb context) : base(context)
        {

        }
    }
}
