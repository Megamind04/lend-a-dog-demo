namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestMessage", "CreateDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AddColumn("dbo.PrivateMessageBoard", "CreateDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AddColumn("dbo.MainMessageBoard", "CreateDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            DropColumn("dbo.MainMessageBoard", "MessageCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MainMessageBoard", "MessageCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.MainMessageBoard", "CreateDate");
            DropColumn("dbo.PrivateMessageBoard", "CreateDate");
            DropColumn("dbo.RequestMessage", "CreateDate");
        }
    }
}
