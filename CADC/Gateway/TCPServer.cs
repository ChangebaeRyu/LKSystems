using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gateway
{
    public class TCPServer
    {
        TcpListener serverSocket = null;
        TcpClient clientSocket = null;

        public TCPServer()
        {
            // socket start
            new Thread(delegate ()
            {
                InitSocket();
            }).Start();
        }

        private void InitSocket()
        {
            try
            {
                //serverSocket = new TcpListener(IPAddress.Any, 9999);
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                serverSocket = new TcpListener(localAddr, port);

                clientSocket = default(TcpClient);
                serverSocket.Start();
                DisplayText(" >> Server Started");

                clientSocket = serverSocket.AcceptTcpClient();
                DisplayText(" >> Accept connection from client");

                Thread threadHandler = new Thread(new ParameterizedThreadStart(OnAccepted));
                threadHandler.IsBackground = true;
                threadHandler.Start(clientSocket);
            }
            catch (SocketException se)
            {
                DisplayText(string.Format("InitSocket : SocketException : {0}", se.Message));
            }
            catch (Exception ex)
            {
                DisplayText(string.Format("InitSocket : Exception : {0}", ex.Message));
            }
        }

        private void OnAccepted(object sender)
        {
            TcpClient clientSocket = sender as TcpClient;

            while (true)
            {
                try
                {
                    NetworkStream stream = clientSocket.GetStream();
                    byte[] buffer = new byte[1024];

                    stream.Read(buffer, 0, buffer.Length);
                    string msg = Encoding.ASCII.GetString(buffer);
                    //msg = msg.Substring(0, msg.IndexOf("$"));
                    DisplayText(" >> Data from client - " + msg);
                    CmdManager cmdMgr = new CmdManager(msg);

                    string response = "Last Message from client - " + msg;
                    byte[] sbuffer = Encoding.ASCII.GetBytes(response);

                    // Send back a response.
                    stream.Write(sbuffer, 0, sbuffer.Length);
                    stream.Flush();

                    DisplayText(" >> " + response);
                }
                catch (SocketException se)
                {
                    DisplayText(string.Format("OnAccepted : SocketException : {0}", se.Message));
                    break;
                }
                catch (Exception ex)
                {
                    DisplayText(string.Format("OnAccepted : Exception : {0}", ex.Message));
                    break;
                }
            }

            clientSocket.Close();
        }

        private void DisplayText(string text)
        {
            MessageBox.Show(text);

        }

        public void DIsconnect()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }

            if (serverSocket != null)
            {
                serverSocket.Stop();
                serverSocket = null;
            }
        }


    }
}
