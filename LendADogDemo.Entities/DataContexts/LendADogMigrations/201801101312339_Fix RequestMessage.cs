namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRequestMessage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dog", "DogOwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessageBoard", "ReceiverID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessageBoard", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MainMessageBoard", "DogOwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequestMessage", "DogOwnerID", "dbo.AspNetUsers");
            DropIndex("dbo.Dog", new[] { "DogOwnerID" });
            DropIndex("dbo.PrivateMessageBoard", new[] { "SenderID" });
            DropIndex("dbo.PrivateMessageBoard", new[] { "ReceiverID" });
            DropIndex("dbo.RequestMessage", new[] { "DogOwnerID" });
            DropIndex("dbo.RequestMessage", new[] { "RequestFrom" });
            DropIndex("dbo.MainMessageBoard", new[] { "DogOwnerID" });
            RenameColumn(table: "dbo.RequestMessage", name: "DogOwnerID", newName: "ReceiverID");
            AddColumn("dbo.RequestMessage", "SendFromID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Dog", "DogOwnerID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Dog", "DogSize", c => c.Int(nullable: false));
            AlterColumn("dbo.Dog", "DogName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Dog", "Description", c => c.String(maxLength: 150));
            AlterColumn("dbo.PrivateMessageBoard", "SenderID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PrivateMessageBoard", "ReceiverID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PrivateMessageBoard", "Message", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.RequestMessage", "ReceiverID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.MainMessageBoard", "DogOwnerID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.MainMessageBoard", "RequestMessage", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.Dog", "DogOwnerID");
            CreateIndex("dbo.RequestMessage", "SendFromID");
            CreateIndex("dbo.RequestMessage", "ReceiverID");
            CreateIndex("dbo.PrivateMessageBoard", "SenderID");
            CreateIndex("dbo.PrivateMessageBoard", "ReceiverID");
            CreateIndex("dbo.MainMessageBoard", "DogOwnerID");
            AddForeignKey("dbo.RequestMessage", "SendFromID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Dog", "DogOwnerID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrivateMessageBoard", "ReceiverID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrivateMessageBoard", "SenderID", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.MainMessageBoard", "DogOwnerID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequestMessage", "ReceiverID", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            DropColumn("dbo.RequestMessage", "RequestFrom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestMessage", "RequestFrom", c => c.String());
            DropForeignKey("dbo.RequestMessage", "ReceiverID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MainMessageBoard", "DogOwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessageBoard", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateMessageBoard", "ReceiverID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dog", "DogOwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequestMessage", "SendFromID", "dbo.AspNetUsers");
            DropIndex("dbo.MainMessageBoard", new[] { "DogOwnerID" });
            DropIndex("dbo.PrivateMessageBoard", new[] { "ReceiverID" });
            DropIndex("dbo.PrivateMessageBoard", new[] { "SenderID" });
            DropIndex("dbo.RequestMessage", new[] { "ReceiverID" });
            DropIndex("dbo.RequestMessage", new[] { "SendFromID" });
            DropIndex("dbo.Dog", new[] { "DogOwnerID" });
            AlterColumn("dbo.MainMessageBoard", "RequestMessage", c => c.String());
            AlterColumn("dbo.MainMessageBoard", "DogOwnerID", c => c.String(maxLength: 128));
            AlterColumn("dbo.RequestMessage", "ReceiverID", c => c.String(maxLength: 128));
            AlterColumn("dbo.PrivateMessageBoard", "Message", c => c.String());
            AlterColumn("dbo.PrivateMessageBoard", "ReceiverID", c => c.String(maxLength: 128));
            AlterColumn("dbo.PrivateMessageBoard", "SenderID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Dog", "Description", c => c.String());
            AlterColumn("dbo.Dog", "DogName", c => c.String());
            AlterColumn("dbo.Dog", "DogSize", c => c.Int());
            AlterColumn("dbo.Dog", "DogOwnerID", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropColumn("dbo.RequestMessage", "SendFromID");
            RenameColumn(table: "dbo.RequestMessage", name: "ReceiverID", newName: "DogOwnerID");
            CreateIndex("dbo.MainMessageBoard", "DogOwnerID");
            CreateIndex("dbo.RequestMessage", "DogOwnerID");
            CreateIndex("dbo.PrivateMessageBoard", "ReceiverID");
            CreateIndex("dbo.PrivateMessageBoard", "SenderID");
            CreateIndex("dbo.Dog", "DogOwnerID");
            AddForeignKey("dbo.RequestMessage", "DogOwnerID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.MainMessageBoard", "DogOwnerID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrivateMessageBoard", "SenderID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrivateMessageBoard", "ReceiverID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Dog", "DogOwnerID", "dbo.AspNetUsers", "Id");
        }
    }
}
