using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowsocks.Util
{
    public static class Base64Util
    {
        public static string DecodeBase64(string val)
        {
            return Encoding.UTF8.GetString(Base64Util.DecodeBase64ToBytes(val));
        }

        public static byte[] DecodeBase64ToBytes(string val)
        {
            string str = val.PadRight(val.Length + (4 - val.Length % 4) % 4, '=');
            return Convert.FromBase64String(str);
        }

        public static string DecodeStandardSSRUrlSafeBase64(string val)
        {
            if (val.IndexOf('=') >= 0)
            {
                throw new FormatException();
            }
            return Encoding.UTF8.GetString(Base64Util.DecodeUrlSafeBase64ToBytes(val));
        }

        public static string DecodeUrlSafeBase64(string val)
        {
            return Encoding.UTF8.GetString(Base64Util.DecodeUrlSafeBase64ToBytes(val));
        }

        public static byte[] DecodeUrlSafeBase64ToBytes(string val)
        {
            string str = val.Replace('-', '+').Replace('\u005F', '/').PadRight(val.Length + (4 - val.Length % 4) % 4, '=');
            return Convert.FromBase64String(str);
        }

        public static string EncodeUrlSafeBase64(byte[] val, bool trim)
        {
            if (!trim)
            {
                return Convert.ToBase64String(val).Replace('+', '-').Replace('/', '\u005F');
            }
            return Convert.ToBase64String(val).Replace('+', '-').Replace('/', '\u005F').TrimEnd(new char[] { '=' });
        }

        public static string EncodeUrlSafeBase64(string val, bool trim = true)
        {
            return Base64Util.EncodeUrlSafeBase64(Encoding.UTF8.GetBytes(val), trim);
        }
    }
}
