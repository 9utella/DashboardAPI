namespace Dashboard.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTableUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "HashedPassword", c => c.String());
            AddColumn("dbo.Users", "Salt", c => c.String());
            AddColumn("dbo.Users", "RegistrationDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "RegistrationDate");
            DropColumn("dbo.Users", "Salt");
            DropColumn("dbo.Users", "HashedPassword");
        }
    }
}
