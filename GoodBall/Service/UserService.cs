using System.Configuration;
using DataCollection.Entity;
using Helper;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : SingModel<UserService>
    {
        private static readonly UserRepository userRepository = UserRepository.Instance;
        private static string userKey = "GBUser";

        private UserService() { }

        public bool Login(string userName, string password)
        {
            var userQuery = userRepository.Find(x => x.UserName == userName && x.Password == MD5Helper.MD5Encrypt64(password));
            var result = userQuery.Any();
            if (result)
            {
                CookieHelper.WriteCookie(userKey, userQuery.FirstOrDefault().UserName);
            }

            return result;
        }

        public bool AdminLogin(string userName, string password)
        {
            var userQuery = userRepository.Find(x => x.UserName == userName && x.Password == MD5Helper.MD5Encrypt64(password));
            var result = userQuery.Any();
            if (result && userName == ConfigurationManager.AppSettings["AdminName"])
            {
                CookieHelper.WriteCookie(userKey, userQuery.FirstOrDefault().UserName);
            }

            return result;
        }

        public User AddUser(User user)
        {
            return userRepository.InsertReturnEntity(user);
        }

        public bool UpdateUser(User user)
        {
            return userRepository.Save(user);
        }

        public bool DeleteUser(User user)
        {
            return userRepository.Delete(user);
        }

        public static User GetCurrentUser()
        {
            var userStr = CookieHelper.GetCookie(userKey);
            return JsonConvert.DeserializeObject<User>(userStr);
        }
    }
}
