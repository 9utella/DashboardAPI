namespace Dashboard.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIsLockedProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsLocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsLocked");
        }
    }
}
