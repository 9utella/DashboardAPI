using Dashboard.DAL.Core;
using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Services
{
    public class MembershipService: IMembershipService
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

        public ValidUSerContext ValidateUSer(string userName, string password)
        {
            var userCtx = new ValidUserContext();
            var user = userRepository.GetSingleByUSerName(userName);
            if(user != null && isUserValid(user, password))
            {
                var userRoles = GetUSerRoles(user.Key);
                userCtx.User = new UserWithRoles()
                {
                    User = user,
                    Roles = userRoles
                };
                var identity = new GenericIdentity(user.Name);
                userCtx.Principal = new GenericPrincipal(identity, userRoles.Select(_ => _.Name).ToArray());
            }
            return userCtx;
        }

        private bool isUserValid(User user, string password)
        {
            if(isPasswordValid(user, password))
            {
                return !user.IsLocked
            }
        }
    }
}
