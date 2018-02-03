using Dashboard.DAL.Core;
using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Extensions
{
    public static class RoleRepositoryExtensions
    {
        public static Role GetSingleByRoleName(this IEntityRepository<Role> roleRepository, string roleName)
        {
            return roleRepository.GetAll().FirstOrDefault(x => x.Name == roleName);
        }
    }
}