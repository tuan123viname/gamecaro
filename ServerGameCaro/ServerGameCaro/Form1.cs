using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ServerGameCaro
{
    [Serializable]
    public partial class Server : Form
    {
        
        private List<Socket> ListSocket = new List<Socket>();
        
        public static string IP = "127.0.0.1";
        public static int Port = 9999;
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), Port);
        private Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThrd = new Thread(new ThreadStart(listenerHandler));
            serverThrd.Start();
        }
      
        void listenerHandler()
        {
            try
            {
                listenerSocket.Bind(iep);
                listenerSocket.Listen(50);
            }
            catch
            {
                MessageBox.Show("port dang dc su dung");
                return;
            }
            while(true)
            {
                Socket clientSocket;
                clientSocket = listenerSocket.Accept();
                ListSocket.Add(clientSocket);
                Thread send = new Thread(sendHandler);
                send.IsBackground = true;
                send.Start(clientSocket);
                Thread receive = new Thread(receiveHandler);

                receive.IsBackground = true;
                receive.Start(clientSocket);
               
            }
        }
        void sendHandler(object obj)
        {
            if (obj != null)
            {
                //Socket sk = (Socket)obj;
                //byte[] byteSend = new byte[1024];
                //    byteSend=SerializeData(sk.RemoteEndPoint.ToString());
                //try
                //{
                //    sk.Send(byteSend);
                //}
                //catch
                //{

                //}

                string[] listUser = new string[ListSocket.Count];
                for (int i = 0; i < ListSocket.Count; i++)
                {
                    listUser[i] = ListSocket[i].RemoteEndPoint.ToString();
                }
                byte[] byteSend = new byte[1024];
                byteSend = SerializeData(listUser);
                try
                {
                    foreach(Socket s in ListSocket)
                    {
                        s.Send(byteSend);
                    }
                }
                catch
                {

                }
            }

        }

        void receiveHandler(object obj)
        {
            if (obj != null)
            {
                Socket client = obj as Socket;
                try
                {
                    byte[] byteReceive = new byte[1024];
                    client.Receive(byteReceive);
                    //---------------------------------
                    string rcvString = Encoding.ASCII.GetString(byteReceive);
                    if (rcvString[0] == 'P')
                    {
                        string[] arrString = rcvString.Split(':');
                        string ip1 = arrString[1];
                        string port1 = arrString[2];
                        string ip2 = arrString[3];
                        string port2 = arrString[4];
                        port2 = port2.Replace("\0", string.Empty);

                        //MessageBox.Show(ip1 + "\r\n" + port1 + "\r\n" + ip2 + "\r\n" + port2);

                        byte[] byteSend = new byte[1024];
                        string[] sendString = new string[1];
                        sendString[0] = "P:" + client.RemoteEndPoint.ToString();
                        byteSend = SerializeData(sendString);
                        string client2 = ip2 + ":" + port2;
                        foreach (Socket s in ListSocket)
                        {
                            if (s.RemoteEndPoint.ToString() == client2)
                            {
                                s.Send(byteSend);
                            }
                        }
                        
                    }
                    if(rcvString[0] == 'Y' || rcvString[0] == 'N')
                    {
                        byte[] byteSend = new byte[1024];
                        string[] sendString = new string[1];
                        rcvString = rcvString.Replace("\0", string.Empty);
                        string ip_port_server = rcvString.Replace("Y:", string.Empty);
                        ip_port_server = ip_port_server.Replace("N:", string.Empty);
                        sendString[0] = rcvString;
                        byteSend = SerializeData(sendString);
                        foreach (Socket s in ListSocket)
                        {
                            if (s.RemoteEndPoint.ToString() == ip_port_server)
                            {
                                s.Send(byteSend);
                            }
                        }
                    }
                    //---------------------------------
                    //object data = DeserializeData(byteReceive);
                    //Point point = (Point)data;
                    //MessageBox.Show(point.X.ToString(), point.Y.ToString());
                }
                catch
                {

                }
            }
        }
        public byte[] SerializeData(Object obj)
        {

            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Giải nén mảng byte[] thành đối tượng object
        /// </summary>
        /// <param name="theByteArray"></param>
        /// <returns></returns>
        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }
    }
}
