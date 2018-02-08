using System;
using LendADogDemo.Entities.DataContexts;

namespace LendADogDemo.Entities.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        LendADogDemoDb Context { get; }

        void Commit();
    }
}
