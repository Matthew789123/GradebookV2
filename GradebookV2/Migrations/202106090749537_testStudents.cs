namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testStudents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Test_TestId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Test_TestId");
            AddForeignKey("dbo.AspNetUsers", "Test_TestId", "dbo.Tests", "TestId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Test_TestId", "dbo.Tests");
            DropIndex("dbo.AspNetUsers", new[] { "Test_TestId" });
            DropColumn("dbo.AspNetUsers", "Test_TestId");
        }
    }
}
