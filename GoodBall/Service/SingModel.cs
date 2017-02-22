using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public abstract class SingModel<TSing>
    {
        private static readonly Lazy<TSing> _instance = new Lazy<TSing>(() =>
        {
            var ctors = typeof(TSing).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (ctors.Count() != 1)
                throw new Exception(String.Format("类型{0}必须有构造函数！", typeof(TSing)));
            var ctor = ctors.SingleOrDefault(c => c.GetParameters().Count() == 0 && c.IsPrivate);
            if (ctor == null)
                throw new Exception(String.Format("{0}必须有私有且无参的构造函数", typeof(TSing)));
            return (TSing)ctor.Invoke(null);
        });
        public static TSing Instance
        {
            get { return _instance.Value; }
        }
    }
}
