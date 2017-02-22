using DataCollection.Entity;
using Helper;
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

        private UserService() { }

        public bool Login(string userName, string password)
        {
            return userRepository.Find(x => x.UserName == userName && x.Password == MD5Helper.MD5Encrypt64(password)).Any();
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
            return new User();
        }
    }
}
