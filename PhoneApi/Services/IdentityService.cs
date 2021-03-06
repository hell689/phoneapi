﻿using PhoneApi.Models;
using PhoneApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Services
{
    public class IdentityService : IIdentityService
    {

        private List<User> users = new List<User>
        {
            new User{Login = "admin", Password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", Role="Administrator"} //123
        };
        public User GetUser(string login)
        {
            return users.Find(u => u.Login == login);
        }
    }
}
