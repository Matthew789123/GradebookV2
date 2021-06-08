namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ParentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "ParentId" });
            DropIndex("dbo.Messages", new[] { "TeacherId" });
            DropIndex("dbo.Messages", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        UserMessageId = c.Int(nullable: false, identity: true),
                        SenderId = c.String(maxLength: 128),
                        ReceiverId = c.String(maxLength: 128),
                        MessageId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserMessageId)
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.MessageId)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Messages", "ParentId");
            DropColumn("dbo.Messages", "TeacherId");
            DropColumn("dbo.Messages", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Messages", "TeacherId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Messages", "ParentId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.UserMessages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserMessages", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserMessages", "ReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserMessages", "MessageId", "dbo.Messages");
            DropIndex("dbo.UserMessages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserMessages", new[] { "MessageId" });
            DropIndex("dbo.UserMessages", new[] { "ReceiverId" });
            DropIndex("dbo.UserMessages", new[] { "SenderId" });
            DropTable("dbo.UserMessages");
            CreateIndex("dbo.Messages", "ApplicationUser_Id");
            CreateIndex("dbo.Messages", "TeacherId");
            CreateIndex("dbo.Messages", "ParentId");
            AddForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "TeacherId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "ParentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
