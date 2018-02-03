using Dashboard.BLL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.DAL.Models;
using Dashboard.DAL.Core;

namespace Dashboard.BLL.Services
{
    public class UserService : IUserService
    {
        private IEntityRepository<User> userRepository;

        public UserService(IEntityRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IQueryable<User> Get()
        {
            var users = userRepository.GetAll();
            return users;
        }
    }
}
