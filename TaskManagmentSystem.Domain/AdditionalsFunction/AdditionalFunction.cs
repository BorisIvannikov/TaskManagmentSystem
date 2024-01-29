using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentSystem.Domain.AdditionalsFunction
{
    public class AdditionalFunction
    {
        public static string HashPassword(string password)
        {

            var sha256 = SHA256.Create();
            
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            string hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash;

            
        }
    }
}
