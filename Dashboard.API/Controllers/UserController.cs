using Dashboard.BLL.IServices;
using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dashboard.API.Controllers
{
    public class UserController:ApiController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IQueryable<User> GetUsers()
        {
            return userService.Get();
        }
    }
}
