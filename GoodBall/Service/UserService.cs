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
using System.Web;
using System.Web.SessionState;


namespace Service
{
    public class UserService : SingModel<UserService>
    {
        private static readonly UserRepository userRepository = UserRepository.Instance;
        private static readonly RechargeRecordRepository rechargeRepository = RechargeRecordRepository.Instance;
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

        public void AdminLogin(string userName, string password)
        {
            if (userName != ConfigHelper.GetAdminName())
            {
                throw new ServiceException("用户名错误");
            }

            string md5Password = MD5Helper.MD5Encrypt64(password);
            var result = userRepository.Find(x => x.UserName == userName && x.Password == md5Password);
            if (result.Any())
            {
                CookieHelper.WriteEncryptCookie(userKey, JsonConvert.SerializeObject(result.FirstOrDefault().ToModel<UserDto>()), DateTime.Now);
            }
            else
            {
                throw new ServiceException("密码错误");
            }
        }

        public void CustomerLogin(string userName, string password)
        {
            string md5Password = MD5Helper.MD5Encrypt64(password);
            var result = userRepository.Find(x => x.UserName == userName && x.Password == md5Password);
            if (result.Any())
            {
                CookieHelper.WriteEncryptCookie(userKey, JsonConvert.SerializeObject(result.FirstOrDefault().ToModel<UserDto>()), DateTime.Now);
            }
            else
            {
                throw new ServiceException("密码错误");
            }
        }

        public void CustomerLogout()
        {
            CookieHelper.RemoveCookie(userKey);
        }

        public User AddUser(UserDto user)
        {
            if (userRepository.Find(x => x.UserName == user.UserName || x.Phone == user.Phone).Any())
            {
                throw new ServiceException("用户名或者手机号码已存在");
            }
            var phoneSession = HttpContext.Current.Session[user.Phone];
            if (phoneSession == null)
            {
                throw new ServiceException("验证码无效或者已过期");
            }
            if (user.Code != phoneSession.ToString())
            {
                throw new ServiceException("验证码错误");
            }

            user.UserName = user.Phone;
            user.NickName = user.Phone;
            return userRepository.InsertReturnEntity(user.ToModel<User>());
        }

        public void UpdateUser(UserDto user)
        {
            var entity = userRepository.Find(x => x.Id == user.Id).FirstOrDefault();
            if (entity == null)
            {
                throw new ServiceException("不存在当前用户");
            }
            if (userRepository.Find(x => x.UserName == user.UserName && x.Id != user.Id).Any())
            {
                throw new ServiceException("已存在相同的用户名");
            }
            if (userRepository.Find(x => x.NickName == user.NickName && x.Id != user.Id).Any())
            {
                throw new ServiceException("已存在相同的昵称");
            }
            if (userRepository.Find(x => x.Phone == user.Phone && x.Id != user.Id).Any())
            {
                throw new ServiceException("已存在相同的电话号码");
            }
          
            entity.UserName = user.UserName;
            entity.NickName = user.NickName;
            entity.Password = user.Password;
            entity.Password = MD5Helper.MD5Encrypt64(user.Password);
            entity.Integral = user.Integral;
            entity.IconUrl = user.IconUrl;
            
            userRepository.Save(entity);
            CookieHelper.RemoveCookie(userKey);
            CookieHelper.WriteEncryptCookie(userKey, JsonConvert.SerializeObject(entity.ToModel<UserDto>()), DateTime.Now);
        }

        public void UpdateUserByWechat(UserDto user)
        {
            if (!userRepository.Find(x => x.Id == user.Id).Any())
            {
                throw new ServiceException("不存在当前用户");
            }
            if (userRepository.Find(x => x.UserName == user.UserName && x.Id != user.Id).Any())
            {
                throw new ServiceException("已存在相同的用户名");
            }
            if (userRepository.Find(x => x.NickName == user.NickName && x.Id != user.Id).Any())
            {
                throw new ServiceException("已存在相同的昵称");
            }
            if (userRepository.Find(x => x.Phone == user.Phone && x.Id != user.Id).Any())
            {
                throw new ServiceException("已存在相同的电话号码");
            }

            userRepository.Save(x => x.Id == user.Id, x => new User{NickName = user.NickName, Phone = user.Phone, IconUrl = user.IconUrl});
            CookieHelper.RemoveCookie(userKey);
            CookieHelper.WriteEncryptCookie(userKey, JsonConvert.SerializeObject(userRepository.Find(x => x.Id == user.Id).FirstOrDefault().ToModel<UserDto>()), DateTime.Now);
        }

        public void UpdateUserPhone(UserDto user)
        {
            if (!userRepository.Find(x => x.Id == user.Id).Any())
            {
                throw new ServiceException("不存在当前用户");
            }
            if (userRepository.Find(x => x.Phone == user.Phone && x.Id != user.Id).Any())
            {
                throw new ServiceException("已存在相同的电话号码");
            }

            userRepository.Save(x => x.Id == user.Id, x => new User { Phone = user.Phone });
            CookieHelper.RemoveCookie(userKey);
            CookieHelper.WriteEncryptCookie(userKey, JsonConvert.SerializeObject(userRepository.Find(x => x.Id == user.Id).FirstOrDefault().ToModel<UserDto>()), DateTime.Now);
        }

        public void UpdateUserBalance(long userId, int price)
        {
            var entity = userRepository.Find(x => x.Id == userId).FirstOrDefault();
            if (entity == null)
            {
                throw new ServiceException("不存在当前用户");
            }
            entity.Balance += price;

            var rechargeRecord = new RechargeRecord();
            rechargeRecord.CreateTime = DateTime.Now;
            rechargeRecord.Operator = GetCurrentUser().UserName;
            rechargeRecord.Price = price;
            rechargeRecord.UserId = userId;



            userRepository.Transaction(()=>
            {
                userRepository.Save(entity);
                rechargeRepository.Insert(rechargeRecord);
            });
        }

        public void DeleteUser(long id)
        {
            userRepository.Delete(x => x.Id == id);
        }

        public List<UserDto> GetUserListByPage(string userName, int size, int index, out int total, bool isCustomer = false)
        {
            var query = userRepository.Source;
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName.Contains(userName));
            }
            if (isCustomer)
            {
                var adminName = ConfigHelper.GetAdminName();
                query = query.Where(x => x.UserName != adminName);
            }
            query = query.OrderByDescending(x => x.CreateTime);
            return userRepository.FindForPaging(size, index, query, out total).ToList().ToListModel<User, UserDto>();
        }

        public UserDto GetUser(long id)
        {
            return userRepository.Find(x => x.Id == id).FirstOrDefault().ToModel<UserDto>();
        }

        public bool SendSmsCode(string phone)
        {
            var code = SmsService.GetPhoneNumber(4, true);
            HttpContext.Current.Session[phone] = code;
            HttpContext.Current.Session.Timeout = 1;
            return SmsService.SendSms(phone, string.Format("您的验证码是：{0}。请不要把验证码泄露给其他人。", code));
        }

        public static UserDto GetCurrentUser()
        {
            var userStr = CookieHelper.GetDecryptCookie(userKey);
            return JsonConvert.DeserializeObject<UserDto>(userStr);
        }
    }
}
