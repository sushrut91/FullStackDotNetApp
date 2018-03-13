namespace FullStackApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_department : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "DepartmentId");
        }
    }
}
