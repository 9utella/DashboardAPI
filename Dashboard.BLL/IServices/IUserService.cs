﻿using Dashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.BLL.IServices
{
    public interface IUserService
    {
        IQueryable<User> Get();
    }
}
