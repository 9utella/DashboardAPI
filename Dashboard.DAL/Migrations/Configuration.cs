namespace Dashboard.DAL.Migrations
{
    using Dashboard.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Dashboard.DAL.Core.EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Dashboard.DAL.Core.EntityContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Users.AddOrUpdate(
                user => user.Email,
                new User { Key = Guid.NewGuid(), Name = "user1", Email = "user1@email.com" },
                new User { Key = Guid.NewGuid(), Name = "user2", Email = "user2@email.com" },
                new User { Key = Guid.NewGuid(), Name = "user3", Email = "user3@email.com" }
                );

            context.Roles.AddOrUpdate(
                role => role.Name,
                new Role { Key = Guid.NewGuid(), Name = "Admin" },
                new Role { Key = Guid.NewGuid(), Name = "Manager" },
                new Role { Key = Guid.NewGuid(), Name = "User" }
                );

            context.UserInRoles.AddOrUpdate(
                new UserInRole
                {
                    Key = Guid.NewGuid(),
                    User = context.Users.AsEnumerable<User>().Where(user => user.Name == "user1").FirstOrDefault(),
                    Role = context.Roles.AsEnumerable<Role>().Where(role => role.Name == "Admin").FirstOrDefault()
                },
                new UserInRole
                {
                    Key = Guid.NewGuid(),
                    User = context.Users.Where(user => user.Name == "user2").FirstOrDefault(),
                    Role = context.Roles.Where(role => role.Name == "Manager").FirstOrDefault()
                },
                new UserInRole
                {
                    Key = Guid.NewGuid(),
                    User = context.Users.Where(user => user.Name == "user3").FirstOrDefault(),
                    Role = context.Roles.Where(role => role.Name == "User").FirstOrDefault()
                }
        );
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
