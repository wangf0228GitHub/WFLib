using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFNetLib.TCP;
using WFNetLib.PacketProc;
using System.IO;
using WFNetLib.MyControls;

namespace 异步服务器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.Text == "开始")
            {
                toolStripButton1.Text = "停止";
                switch (toolStripComboBox2.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        tcpAsyncServer.SaveDataProcessCallback = new SaveDataProcessCallbackDelegate(NetPacket.SaveDataProcessCallbackProc);
                        break;
                    case 2:
                        break;
                }
                tcpAsyncServer.Start();
                this.Text = tcpAsyncServer.TCPServerName + ":" + tcpAsyncServer.TCPServerPort;
            }
            else
            {
                toolStripButton1.Text = "开始";
                tcpAsyncServer.Stop();
            }
        }
        TCPAsyncServer tcpAsyncServer = null;
        private void Form1_Load(object sender, EventArgs e)
        {            
            toolStripComboBox2.SelectedIndex = 0;
            tcpAsyncServer = new TCPAsyncServer();            
            tcpAsyncServer.ErrorServerEvent += new TCPErrorEvent(tcpAsyncServer_ErrorServer);
            tcpAsyncServer.AcceptServerEvent += new TCPAcceptEvent(tcpAsyncServer_AcceptServer);
            tcpAsyncServer.DisconnectServerEvent += new TCPDisconnectEvent(tcpAsyncServer_DisconnectServer);
            tcpAsyncServer.ReceiveServerEvent += new TCPReceiveEvent(tcpAsyncServer_ReceiveServerEvent);
        }
        private void tcpAsyncServer_ReceiveServerEvent(object sender, ReceiveServerEventArgs e)
        {
            this.Invoke((EventHandler)(delegate
            {
                NetPacket np=(NetPacket)e.netPacket;
                switch(np.PacketHead.PType)
                {
                    case NetPacket.NetPacketType.STRING:
                        textBox1.AppendText("收到字符串" + np.Data.ToString() + "\r\n");
                        tcpAsyncServer.Send(e.Client, NetPacket.MakeStringPacket("服务器收到数据" + np.Data.ToString()));
                        break;
                    case NetPacket.NetPacketType.BINARY:
                        NetFile nf = (NetFile)np.Data;
                        textBox1.AppendText("收到文件" + nf.FileName + ",正在后台生成.....\r\n");
                        string file = System.Windows.Forms.Application.StartupPath + "\\收到的文件\\" + nf.FileName;
                        FileInfo f = new FileInfo(file);
                        if (!Directory.Exists(f.DirectoryName))
                            Directory.CreateDirectory(f.DirectoryName);
                        FileStream fs = null;
                        fs = new FileStream(file, FileMode.Create);
                        fs.BeginWrite(nf.Content, 0, nf.Content.Length, NetFileWriteOK, fs);                        
                        break;
                    case NetPacket.NetPacketType.COMPLEX:
                        NetObject no = (NetObject)np.Data;
                        textBox1.AppendText("收到序列对象" + no.TypeName + ",正在后台生成.....\r\n");
                        break;
                }
            }));
        }
        void NetFileWriteOK(IAsyncResult asyncResult)
        {
            FileStream fs = (FileStream)asyncResult.AsyncState;
            fs.EndWrite(asyncResult);
            fs.Close();
            this.Invoke((EventHandler)(delegate
            {
                textBox1.AppendText("接收的文件生成完成\r\n");

            }));
        }
        private void tcpAsyncServer_ErrorServer(object sender, ErrorServerEventArgs e)
        {
            this.Invoke((EventHandler)(delegate
            {
                textBox1.AppendText(e.Error.Message + "\r\n");

            }));
        }
        private void tcpAsyncServer_AcceptServer(object sender, AcceptServerEventArgs e)
        {
            this.Invoke((EventHandler)(delegate
            {
                wfComboBoxItem c = new wfComboBoxItem(e.Client.ClientSocket.RemoteEndPoint.ToString(), e.Client.key);
                int i=toolStripComboBox1.Items.Add(c);
                toolStripComboBox1.SelectedIndex = i;
            }));
        }
        private void tcpAsyncServer_DisconnectServer(object sender, DisconnectEventArgs e)
        {
            this.Invoke((EventHandler)(delegate
            {
                foreach (object o in toolStripComboBox1.Items)
                {
                    if (o.ToString() == e.Client.ClientSocket.RemoteEndPoint.ToString())
                    {
                        toolStripComboBox1.Items.Remove(o);
                        break;
                    }
                }
            }));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 32000)
                textBox1.Clear();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            wfComboBoxItem c = (wfComboBoxItem)toolStripComboBox1.Items[toolStripComboBox1.SelectedIndex];

            tcpAsyncServer.Send((string)c.Value,NetPacket.MakeStringPacket("dasfadsf"));
        }
    }
}
