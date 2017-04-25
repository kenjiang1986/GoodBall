using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodBall.Dto
{
    public class UserDto
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public int Balance { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }

        public string CreateTime { get; set; }

        public string IconUrl { get; set; }
    }
}
