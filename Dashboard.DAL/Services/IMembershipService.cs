using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Services
{
    public interface IMembershipService
    {
        ValidUSerContext ValidateUSer(string userName, string password);
        OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string[] roles);
        OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string [] roles);
        UserWithRoles UpdateUser(User user, string username, string email);

        bool ChangePassword(string username, string oldPassword, string newPassword);

        bool AddToRole(Guid userKey, string role);
        bool AddToRole(string userName, string role);
        bool RemoveFromRole(string userName, string role);

        IEnumerable<Role> GetRoles();
        Role GetRole(Guid key);
        Role GetRole(string name);

        PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize);
        UserWithRoles GetUser(Guid key);
        UserWithRoles GetUser(string name);
    }
}
