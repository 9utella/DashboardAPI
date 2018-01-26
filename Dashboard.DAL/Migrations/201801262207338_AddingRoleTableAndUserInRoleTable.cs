namespace Dashboard.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRoleTableAndUserInRoleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInRoles",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        UserKey = c.Guid(nullable: false),
                        RoleKey = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Roles", t => t.RoleKey, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserKey, cascadeDelete: true)
                .Index(t => t.UserKey)
                .Index(t => t.RoleKey);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInRoles", "UserKey", "dbo.Users");
            DropForeignKey("dbo.UserInRoles", "RoleKey", "dbo.Roles");
            DropIndex("dbo.UserInRoles", new[] { "RoleKey" });
            DropIndex("dbo.UserInRoles", new[] { "UserKey" });
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropTable("dbo.Roles");
            DropTable("dbo.UserInRoles");
        }
    }
}
