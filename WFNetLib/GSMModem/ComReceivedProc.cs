using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using WFNetLib.Net;
using System.Timers;
using System.Threading;

namespace WFNetLib.GSMModem
{
    public partial class GsmModem
    {

        void COM_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string temp = Com.ReadTo("\r\n");                      
        }      

    }
}
