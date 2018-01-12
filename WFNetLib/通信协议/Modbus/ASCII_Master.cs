using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.Modbus
{
    public class ASCII_Master
    {
        public static byte[] TxCommand(byte[] txBuffer)
        {
            byte[] ret = new byte[txBuffer.Length * 2 + 5];
            ret[0] = (byte)':';
            for (int i = 0; i < txBuffer.Length; i++)
            {
                BytesOP.Byte2ASCII(txBuffer[i],out ret[1 + (i << 1)],out ret[1 + (i << 1) + 1]);
            }
            byte sum = Verify.GetVerify_byteSum(txBuffer);
            BytesOP.Byte2ASCII(sum, out ret[txBuffer.Length * 2 + 1], out ret[txBuffer.Length * 2+2]);
            ret[txBuffer.Length * 2 + 3] = 0x0d;
            ret[txBuffer.Length * 2 + 4] = 0x0a;
            return ret;
        }
    }
}
