using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WcfSapoManagerLibrary
{
    public class User
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        private int _hashPassword;
        public string Name { get; set; }
        public bool IsContributor { get; set; }
        public bool IsAdmin { get; set; }

        public int Password
        {
            get
            {
                return _hashPassword;
            }
        }

        public void SetPassword(string password)
        {
            _hashPassword = password.GetHashCode();
        }

        public static bool AutenticateContributor(User user, string password)
        {
            if (password.GetHashCode() == user.Password)
                if (user.IsContributor)
                    return true;
            return false;
        }

        public User(string name, string password, bool isContributor, bool isAdmin)
        {
            Name = name;
            _hashPassword = password.GetHashCode();
            IsContributor = isContributor;
            IsAdmin = isAdmin;
        }

        public User(string name, string password)
        {
            Name = name;
            _hashPassword = password.GetHashCode();
            IsContributor = false;
            IsAdmin = false;
        }


    }
}
