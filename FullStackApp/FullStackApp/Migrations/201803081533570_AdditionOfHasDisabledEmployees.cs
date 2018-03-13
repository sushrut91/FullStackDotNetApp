namespace FullStackApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionOfHasDisabledEmployees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "HasDisabledEmployees", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "HasDisabledEmployees");
        }
    }
}
