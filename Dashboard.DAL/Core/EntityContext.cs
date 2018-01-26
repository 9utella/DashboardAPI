using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Core
{
    public class EntityContext:DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserInRole> UserInRoles { get; set; }

        public EntityContext():base("Dashboard")
        {

        }
    }
}
