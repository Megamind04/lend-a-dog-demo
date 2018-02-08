namespace LendADogDemo.Entities.DataContexts.LendADogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingNamesAndRelations : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RequestMessage", name: "ReceiverID", newName: "ReciverID");
            RenameColumn(table: "dbo.PrivateMessageBoard", name: "ReceiverID", newName: "RrecivedFromID");
            RenameColumn(table: "dbo.PrivateMessageBoard", name: "SenderID", newName: "SendFromID");
            RenameIndex(table: "dbo.RequestMessage", name: "IX_ReceiverID", newName: "IX_ReciverID");
            RenameIndex(table: "dbo.PrivateMessageBoard", name: "IX_SenderID", newName: "IX_SendFromID");
            RenameIndex(table: "dbo.PrivateMessageBoard", name: "IX_ReceiverID", newName: "IX_RrecivedFromID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PrivateMessageBoard", name: "IX_RrecivedFromID", newName: "IX_ReceiverID");
            RenameIndex(table: "dbo.PrivateMessageBoard", name: "IX_SendFromID", newName: "IX_SenderID");
            RenameIndex(table: "dbo.RequestMessage", name: "IX_ReciverID", newName: "IX_ReceiverID");
            RenameColumn(table: "dbo.PrivateMessageBoard", name: "SendFromID", newName: "SenderID");
            RenameColumn(table: "dbo.PrivateMessageBoard", name: "RrecivedFromID", newName: "ReceiverID");
            RenameColumn(table: "dbo.RequestMessage", name: "ReciverID", newName: "ReceiverID");
        }
    }
}
