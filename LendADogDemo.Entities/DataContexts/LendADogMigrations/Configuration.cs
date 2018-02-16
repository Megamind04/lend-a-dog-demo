namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using LendADogDemo.Entities.Helpers;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LendADogDemo.Entities.DataContexts.LendADogDemoDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\LendADogMigrations";
        }

        protected override void Seed(LendADogDemo.Entities.DataContexts.LendADogDemoDb context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            if (!context.Dogs.Any())
            {
                context.RandomDogsPerDogOwner(7, 3);
            }

            if (!context.RequestMessages.Any())
            {
                context.RandomRequestMessages(5);
            }

            if (!context.MainMessages.Any())
            {
                context.RandomMainBoardMessages(2);
            }

            if (!context.PrivateMessages.Any())
            {
                context.RandomPrivateBoardMessages(5, 3);
            }
        }
    }
}
