using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class EnumHelper
    {
        public static System.Collections.ArrayList GetName(Type enumType)
        {
            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            string[] n = System.Enum.GetNames(enumType);
            foreach (string item in n)
                arr.Add(item);
            return arr;

        }

        public static T ToEnum<T>(string strEnum)
        {
            T t = (T)Enum.Parse(typeof(T), strEnum);
            return t;
        }

        public static System.Collections.Hashtable EnumToHashtable(Type enumType)
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            Array arr = System.Enum.GetValues(enumType);
            for (int i = 0; i < arr.Length; i++)
                ht.Add(Convert.ToInt16(arr.GetValue(i)), arr.GetValue(i).ToString());
            return ht;
        }
    }
}
