using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Services
{
    public class UserWithRoles
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
