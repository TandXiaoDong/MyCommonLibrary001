using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
//using SuperSocket.Facility.Protocol;

namespace SuperSocket.ClientSocket.AppBase
{
   public  class MyReceiveFilter:FixedHeaderReceiveFilter<MyPackageInfo> 
    {

        /// +-------+---+-------------------------------+
        /// |request| l |                               |
        /// | name  | e |    request body               |
        /// |  (2)  | n |                               |
        /// |       |(2)|                               |
        /// +-------+---+-------------------------------+
        public MyReceiveFilter() : base(4)//2-header + 2-dataLen
        {
            
        }

        //protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        //{
        //    return (int)header[offset + 2] * 256 + (int)header[offset + 3];
        //}

        //protected override MyPackageInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        //{
        //    var body = bodyBuffer.Skip(offset).Take(length).ToArray();
        //    return new MyPackageInfo(header.Array, body);
        //}

        protected override int GetBodyLengthFromHeader(IBufferStream bufferStream, int length)
        {
            ArraySegment<byte> buffers = bufferStream.Buffers[0];
            byte[] array = buffers.ToArray();
            if (array.Length < 4)
                return 0;
            //int len = array[length - 1] * 256 + array[length - 2];//长度-低位在前
            int len = array[length - 2] * 256 + array[length - 1];//高位在前
            //int len = (int)array[buffers.Offset + 2] * 256 + (int)array[buffers.Offset + 3];
            
            return len;//+ 2
        }

        public override MyPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            //第三个参数用0,1都可以
            byte[] header = bufferStream.Buffers[0].ToArray();
            if (bufferStream.Buffers.Count > 1)
            {
                byte[] bodyBuffer = bufferStream.Buffers[1].ToArray();
                return new MyPackageInfo(header, bodyBuffer);
            }
            //byte[] allBuffer = bufferStream.Buffers[0].Array.CloneRange(0, (int)bufferStream.Length);
            //合并所有buffer
            //byte[] allBuffer
            return new MyPackageInfo(header, new byte[] { });
        }
    }
}
