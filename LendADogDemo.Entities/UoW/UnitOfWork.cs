using System;
using LendADogDemo.Entities.DataContexts;

namespace LendADogDemo.Entities.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LendADogDemoDb context;

        public UnitOfWork(LendADogDemoDb _context)
        {
            this.context = _context;
        }

        public LendADogDemoDb Context { get { return context; } }
        
        public void Commit()
        {
            context.Commit();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
