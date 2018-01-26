using Dashboard.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Models
{
    public class UserInRole:IEntity
    {
        [Key]
        public Guid Key { get; set; }

        public Guid UserKey { get; set; }
        public virtual User User { get; set; }

        public Guid RoleKey { get; set; }
        public virtual Role Role { get; set; }

        public UserInRole()
        {

        }
    }
}
