using LendADogDemo.Entities.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LendADogDemo.Entities.DataContexts
{
    public class LendADogDemoDb : DbContext
    {
        public LendADogDemoDb() : base("DefaultConnection")
        {
            Database.SetInitializer<LendADogDemoDb>(null); // Remove default initializer
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<DogOwner> DogOwners { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogPhoto> DogPhotos { get; set; }
        public DbSet<MainMessageBoard> MainMessages { get; set; }
        public DbSet<PrivateMessageBoard> PrivateMessages { get; set; }
        public DbSet<RequestMessage> RequestMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // IMPORTANT: we are mapping the entity User to the same table as the entity ApplicationUser
            modelBuilder.Entity<DogOwner>().ToTable("AspNetUsers");

            modelBuilder.Properties<DateTime>()
                .Configure(x => x.HasColumnType("datetime2").HasPrecision(0));
        }

        public DbQuery<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }
    }
}
