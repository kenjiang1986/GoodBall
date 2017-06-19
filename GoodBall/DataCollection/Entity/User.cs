using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollection.Entity
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }

        public string Remark { get; set; }

        public DateTime CreateTime { get; set; }

        public string IconUrl { get; set; }

        public bool IsAdmin { get; set; }

        public string OpenId { get; set; }

        public virtual ICollection<Promote> PromoteList { get; set; }
    
    }
}
