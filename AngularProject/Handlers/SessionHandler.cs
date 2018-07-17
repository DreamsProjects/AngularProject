//using AngularProject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using AngularProject.Models;

namespace AngularProject.Handlers
{
    public static class SessionHandler
    {
        public enum ReturnValue
        {
            Successful,
            WrongUserOrPassword,
            AccountLocked
        }

        public static ReturnValue Login(string username, string password = null)
        {
            if (password == null) return ReturnValue.WrongUserOrPassword;

            HashAlgorithm hash = new SHA256Managed();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = hash.ComputeHash(passwordBytes);
            string passwordSHA256 = Convert.ToBase64String(hashBytes);
            string passwordSHA2562 = GetSHA256Password(password);
            TblPerson user = ContextMethods.GetUserFromLogin(username, password, passwordSHA256, passwordSHA2562);

            if(user != null)
            {
                return ReturnValue.Successful;
            }


            return ReturnValue.WrongUserOrPassword;
        }

        public static string GetSHA256Password(string input) //kryptera lösenord
        {
            SHA256 hasher = SHA256.Create();
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var h in hash)
            {
                sb.Append(h.ToString("x2").ToLower());
            }
            var shaResult = sb.ToString();
            return shaResult;
        }
    }
}
