using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtils.Logger;

namespace CommonUtils.ByteHelper
{
    public class FloatConvert
    {
        public static string FloatConvertByte(float f)
        {
            //var f = Convert.ToSingle(fStr);
            byte[] b = BitConverter.GetBytes(f);
            return BitConverter.ToString(b).Replace("-","");
        }
    }
}
