using LendADogDemo.Entities.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace LendADogDemo.Entities.DataContexts
{
    public class LendADogDemoDb : DbContext
    {
        public LendADogDemoDb() : base("DefaultConnection")
        {
            Database.SetInitializer<LendADogDemoDb>(null); // Remove default initializer
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
           // Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<DogOwner> DogOwners { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogPhoto> DogPhotos { get; set; }
        public DbSet<MainMessageBoard> MainMessages { get; set; }
        public DbSet<PrivateMessageBoard> PrivateMessages { get; set; }
        public DbSet<RequestMessage> RequestMessages { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<DogOwner>().ToTable("AspNetUsers");

            modelBuilder.Properties<DateTime>()
                .Configure(x => x.HasColumnType("datetime2").HasPrecision(0));

            modelBuilder.Entity<DogOwner>().HasMany(c => c.Dogs).WithRequired().HasForeignKey(c => c.DogOwnerID);

            modelBuilder.Entity<DogOwner>().HasMany(c => c.ReceivedRequestMessages).WithRequired().HasForeignKey(c => c.ReciverID);

            modelBuilder.Entity<Dog>().HasMany(c => c.DogPhotos).WithRequired().HasForeignKey(c => c.DogID);

            modelBuilder.Entity<MainMessageBoard>().HasRequired(c => c.Dog).WithMany().HasForeignKey(c => c.DogID).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);

        }

        public DbQuery<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }
    }
}
