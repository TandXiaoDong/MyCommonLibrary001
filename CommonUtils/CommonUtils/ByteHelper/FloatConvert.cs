using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.ByteHelper
{
    public class FloatConvert
    {
        public static string FloatConvertByte(float f)
        {
            //var f = Convert.ToSingle(fStr);
            byte[] b = BitConverter.GetBytes(f);
            var fByte = "";
            foreach (int tmp in b)
            {
                fByte += Convert.ToString(tmp, 16);
            }
            return fByte;
        }
    }
}
