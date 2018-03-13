namespace FullStackApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDepartmentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "DepartmentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "DepartmentType");
        }
    }
}
