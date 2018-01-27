using Dashboard.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Models
{
    public class User:IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public string Salt { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }

        public User()
        {
            this.UserInRoles = new HashSet<UserInRole>();
        }
    }
}
