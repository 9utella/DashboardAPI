﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Services
{
    public class CryptoService : ICryptoService
    {
        public string EncryptPassword(string password, string salt)
        {
            if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt))
            {
                throw new ArgumentNullException("One of params for EncryptPassword is null.");
            }
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }

        public string GenerateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider=new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }
    }
}
