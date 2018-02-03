using Dashboard.DAL.Core;
using Dashboard.DAL.Extensions;
using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IEntityRepository<User> userRepository;
        private readonly IEntityRepository<Role> roleRepository;
        private readonly IEntityRepository<UserInRole> userInRoleRepository;
        private readonly ICryptoService cryptoService;

        public MembershipService(IEntityRepository<User> userRepository, IEntityRepository<Role> roleRepository, IEntityRepository<UserInRole> userInRoleRepository, ICryptoService cryptoService)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userInRoleRepository = userInRoleRepository;
            this.cryptoService = cryptoService;
        }

        public ValidUserContext ValidateUser(string username, string password)
        {

            var userCtx = new ValidUserContext();
            var user = userRepository.GetSingleByUsername(username);
            if (user != null && isUserValid(user, password))
            {

                var userRoles = GetUserRoles(user.Key);
                userCtx.User = new UserWithRoles()
                {
                    User = user,
                    Roles = userRoles
                };

                var identity = new GenericIdentity(user.Name);
                userCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }

            return userCtx;
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password)
        {

            return CreateUser(username, password, email, roles: null);
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string role)
        {

            return CreateUser(username, password, email, roles: new[] { role });
        }

        public OperationResult<UserWithRoles> CreateUser(string username, string email, string password, string[] roles)
        {

            var existingUser = userRepository.GetAll().Any(
                x => x.Name == username);

            if (existingUser)
            {

                return new OperationResult<UserWithRoles>(false);
            }

            var passwordSalt = cryptoService.GenerateSalt();

            var user = new User()
            {
                Name = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = cryptoService.EncryptPassword(password, passwordSalt),
                RegistrationDate = DateTime.Now
            };

            userRepository.Add(user);
            userRepository.Save();

            if (roles != null && roles.Length > 0)
            {

                foreach (var roleName in roles)
                {

                    addUserToRole(user, roleName);
                }
            }

            return new OperationResult<UserWithRoles>(true)
            {
                Entity = GetUserWithRoles(user)
            };
        }

        public UserWithRoles UpdateUser(
            User user,
            string username,
            string email)
        {

            user.Name = username;
            user.Email = email;

            userRepository.Edit(user);
            userRepository.Save();

            return GetUserWithRoles(user);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {

            var user = userRepository.GetSingleByUsername(username);

            if (user != null && isPasswordValid(user, oldPassword))
            {

                user.HashedPassword =
                    cryptoService.EncryptPassword(newPassword, user.Salt);

                userRepository.Edit(user);
                userRepository.Save();

                return true;
            }

            return false;
        }

        public bool AddToRole(string username, string role)
        {

            var user = userRepository.GetSingleByUsername(username);
            if (user != null)
            {

                addUserToRole(user, role);
                return true;
            }

            return false;
        }

        public bool AddToRole(Guid userKey, string role)
        {

            var user = userRepository.GetSingle(userKey);
            if (user != null)
            {

                addUserToRole(user, role);
                return true;
            }

            return false;
        }

        public bool RemoveFromRole(string username, string role)
        {

            var user = userRepository.GetSingleByUsername(username);
            var roleEntity = roleRepository.GetSingleByRoleName(role);

            if (user != null && roleEntity != null)
            {

                var userInRole = userInRoleRepository.GetAll()
                    .FirstOrDefault(x => x.RoleKey == roleEntity.Key
                        && x.UserKey == user.Key);

                if (userInRole != null)
                {

                    userInRoleRepository.Delete(userInRole);
                    userInRoleRepository.Save();
                }
            }

            return false;
        }

        public IEnumerable<Role> GetRoles()
        {

            return roleRepository.GetAll();
        }

        public Role GetRole(Guid key)
        {

            return roleRepository.GetSingle(key);
        }

        public Role GetRole(string name)
        {

            return roleRepository.GetSingleByRoleName(name);
        }

        public PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize)
        {

            var users = userRepository.Paginate<Guid>(pageIndex, pageSize, x => x.Key);

            return new PaginatedList<UserWithRoles>(
                users.PageIndex,
                users.PageSize,
                users.TotalCount,
                users.Select(user => new UserWithRoles()
                {
                    User = user,
                    Roles = GetUserRoles(user.Key)
                }).AsQueryable());
        }

        public UserWithRoles GetUser(Guid key)
        {

            var user = userRepository.GetSingle(key);
            return GetUserWithRoles(user);
        }

        public UserWithRoles GetUser(string name)
        {

            var user = userRepository.GetSingleByUsername(name);
            return GetUserWithRoles(user);
        }

        // Private helpers

        private bool isUserValid(User user, string password)
        {

            if (isPasswordValid(user, password))
            {

                return !user.IsLocked;
            }

            return false;
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(cryptoService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private void addUserToRole(User user, string roleName)
        {
            var role = roleRepository.GetSingleByRoleName(roleName);
            if (role == null)
            {
                var tempRole = new Role()
                {
                    Name = roleName
                };

                roleRepository.Add(tempRole);
                roleRepository.Save();
                role = tempRole;
            }

            var userInRole = new UserInRole()
            {
                RoleKey = role.Key,
                UserKey = user.Key
            };

            userInRoleRepository.Add(userInRole);
            userInRoleRepository.Save();
        }

        private IEnumerable<Role> GetUserRoles(Guid userKey)
        {

            var userInRoles = userInRoleRepository
                .FindBy(x => x.UserKey == userKey).ToList();

            if (userInRoles != null && userInRoles.Count > 0)
            {

                var userRoleKeys = userInRoles.Select(
                    x => x.RoleKey).ToArray();

                var userRoles = roleRepository
                    .FindBy(x => userRoleKeys.Contains(x.Key));

                return userRoles;
            }

            return Enumerable.Empty<Role>();
        }

        private UserWithRoles GetUserWithRoles(User user)
        {

            if (user != null)
            {

                var userRoles = GetUserRoles(user.Key);
                return new UserWithRoles()
                {
                    User = user,
                    Roles = userRoles
                };
            }

            return null;
        }
    }
}
