using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gateway
{
    class TCPClient
    {

        private TcpClient clientSocket = new TcpClient();

        public TCPClient()
        {
            new Thread(delegate ()
            {
                InitSocket();
            }).Start();
        }

        private void InitSocket()
        {
            try
            {
                clientSocket.Connect("192.168.0.12", 9999);
                DisplayText("Client Started");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SendData(string sendData)
        {
            NetworkStream stream = clientSocket.GetStream();
            byte[] sbuffer = Encoding.Unicode.GetBytes(sendData);
            stream.Write(sbuffer, 0, sbuffer.Length);
            stream.Flush();

            byte[] rbuffer = new byte[1024];
            stream.Read(rbuffer, 0, rbuffer.Length);
            string msg = Encoding.Unicode.GetString(rbuffer);
            DisplayText(msg);

        }

        private void Close()
        {
            if (clientSocket != null)
                clientSocket.Close();
        }

        private void DisplayText(string text)
        {
            MessageBox.Show(text);
        }

    }
}
