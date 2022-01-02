using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenManager.Core.Utils
{
    public static class Extensions
    {
        public static bool ToBool(this object obj, bool defaultValue = default(bool))
        {
            if (obj == null)
                return defaultValue;

            return new List<string>() { "yes", "y", "true", "1" }.Contains(obj.ToString().ToLower());
        }

        public static int ToInt(this object obj, int defaultValue = default(int))
        {
            if (obj == null)
                return defaultValue;

            int result;
            return !int.TryParse(obj.ToString(), out result) ? defaultValue : result;
        }

        public static long ToLong(this object obj, long defaultValue = default(long))
        {
            if (obj == null)
                return defaultValue;

            long result;
            if (!long.TryParse(obj.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static double ToDouble(this object obj, double defaultValue = default(double))
        {
            if (obj == null)
                return defaultValue;

            double result;
            if (!double.TryParse(obj.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static decimal ToDecimal(this object obj, decimal defaultValue = default(decimal))
        {
            if (obj == null)
                return defaultValue;

            decimal result;
            if (!decimal.TryParse(obj.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static short ToShort(this object obj, short defaultValue = default(short))
        {
            if (obj == null)
                return defaultValue;

            short result;
            if (!short.TryParse(obj.ToString(), out result))
                return defaultValue;

            return result;
        }

        public static byte ToByte(this object obj, byte defaultValue = default(byte))
        {
            if (obj == null)
                return defaultValue;

            byte result;
            if (!byte.TryParse(obj.ToString(), out result))
                return defaultValue;

            return result;
        }
    }
}
