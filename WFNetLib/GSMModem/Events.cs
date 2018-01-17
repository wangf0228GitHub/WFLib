using System;
using System.Collections.Generic;

using System.Text;
using WFNetLib.Net;

namespace WFNetLib.GSMModem
{
    public class TCPDataEventArgs : EventArgs
    {
        public TCPDataRecombine TCPData;
        public TCPDataEventArgs(TCPDataRecombine tcpData)
        {
            TCPData = tcpData;
        }
        public TCPDataEventArgs()
        {
            TCPData = null;
        }        
    }
    public delegate void TCPDataEvent(object sender, TCPDataEventArgs e);

    public class SpecialDataReceivedEventArgs : EventArgs
    {
        public readonly string ATCommand;
        public SpecialDataReceivedEventArgs(string atcomm)
        {
            this.ATCommand = atcomm;
        }
        public SpecialDataReceivedEventArgs()
        {

        }
    }
    public delegate void SpecialDataReceivedEvent(object sender, SpecialDataReceivedEventArgs e);
}
