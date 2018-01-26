namespace Dashboard.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
