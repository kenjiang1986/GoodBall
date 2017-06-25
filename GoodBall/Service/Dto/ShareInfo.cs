using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class ShareInfo
    {
        string corpId = string.Empty;
        public string CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }
        string ticket = string.Empty;

        public string Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }
        string noncestr = string.Empty;

        public string Noncestr
        {
            get { return noncestr; }
            set { noncestr = value; }
        }
        string timestamp = string.Empty;

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        private string paySign = string.Empty;
        public string PaySign
        {
            get { return paySign; }
            set { paySign = value; }
        }

        private string package = string.Empty;

        public string Package
        {
            get { return package; }
            set { package = value; }
        }
    }
}
