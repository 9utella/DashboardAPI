using Dashboard.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Models
{
    public class Role:IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }

        public Role()
        {
            this.UserInRoles = new HashSet<UserInRole>();
        }
    }
}
