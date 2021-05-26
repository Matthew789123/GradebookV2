namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gradeDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grades", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grades", "Value", c => c.Int(nullable: false));
        }
    }
}
