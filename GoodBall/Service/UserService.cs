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
using DataCollection;
using GoodBall.Dto;
using GoodBall;

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

        public bool UpdateUser(UserDto user)
        {
            return userRepository.Save(user.ToModel<User>());
        }

        public bool DeleteUser(User user)
        {
            return userRepository.Delete(user);
        }

        public List<UserDto> GetUserListByPage(string userName, int size, int index, out int total)
        {
            var query = userRepository.Source;
            if(!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName == userName);
            }
            return userRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<User, UserDto>();
        }

        public UserDto GetUser(long id)
        {
            return userRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<UserDto>();
        }

        public static User GetCurrentUser()
        {
            var userStr = CookieHelper.GetCookie(userKey);
            return JsonConvert.DeserializeObject<User>(userStr);
        }
    }
}
