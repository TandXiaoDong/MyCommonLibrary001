using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommonUtils.DEncrypt
{
    public class MD5Tool
    {
        /// <summary>
        /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <param name="bit">位数，一般取值16 或 32</param>
        /// <returns>返回的加密后的字符串</returns>
        public string MD5Encrypt(string password, int bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (bit == 16)
                return tmp.ToString().Substring(8, 16);
            else
            if (bit == 32) return tmp.ToString();//默认情况
            else return string.Empty;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }

        #region MD5加密
        /// <summary>
        ///  对字符串进行加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string MD5create(string input)
        {
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes((input == null) ? ("") : (input)));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("x2");
            }
            //返回加密过的密码
            return pwd;
        }
        #endregion

        /// <summary>
        /// 给一个字符串进行MD5加密
        /// </summary>
        /// <param name="s">待加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string s)
        { 
            if (s != null)
            {
                try
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(s);
                    return Encrypt(buffer);
                }
                catch { }
            }
            return "";
        }
        public static string Encrypt(byte[] data)
        {
            string result = "";
            if (data != null)
            {
                try
                {
                    HashAlgorithm algorithm = MD5.Create();
                    byte[] hashBytes = algorithm.ComputeHash(data);
                    result = BitConverter.ToString(hashBytes).Replace("-", "");
                }
                catch { }
            }
            return result;
        }
    }
}
