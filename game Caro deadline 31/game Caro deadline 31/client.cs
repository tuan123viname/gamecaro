using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace game_Caro_deadline_31
{

    [Serializable]
    public partial class client : Form
    {
        public static List<string> IpUser=new List<string>();
        public static List<int> PortUser=new List<int>();
        public static List<string> listUser = new List<string>();
        public static string namePlayer;
       private string IP = "127.0.0.1";
        private int Port = 9999;
        public static Socket Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public client()
        {
            InitializeComponent();
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_ConnectServer_Click(object sender, EventArgs e)
        {
            Connect();
            CheckForIllegalCrossThreadCalls = false;
           
            
            Thread load = new Thread(new ThreadStart(listenFromServer));
            load.IsBackground = true;
            load.Start(); 

        }
        void Connect()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), Port);

            Client.Connect(iep);
            if (Client.Connected == true)
            {
                // namePlayer = tb_PlayerName.Text.ToString();
                //byte[] sentByte = Encoding.ASCII.GetBytes(namePlayer);
                //client.Send(sentByte);
                Point point = new Point(3, 5);
                object obj = (object)point;
                byte[] byteSend = SerializeData(obj);
                Client.Send(byteSend);
                room form = new room();
                form.Show();
            }
           
               
            
            //Client.Close();
        }
        void listenFromServer()
        {
          
            while (true)
            {
                try
                {
                  
                    byte[] byteReceive = new byte[1024];
                    Client.Receive(byteReceive);
                    if (byteReceive != null)
                    {
                        object obj = DeserializeData(byteReceive);
                        string[] rcvString = (string[])obj;

                        //----------------------------------------------
                        if (rcvString[0][0] == 'P')
                        {
                            //Mở form bàn cờ, kết nối đến người chơi đóng vai trò server bằng ip, port trong rcvString
                            return;
                        }
                        //----------------------------------------------

                        listUser.Clear();
                        foreach (string str in rcvString)
                        {
                            listUser.Add(str);
                        }

                    }
                    //splitString();
                }
                catch
                {

                }
            }
        }
        public byte[] SerializeData(Object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
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

        private void client_Load(object sender, EventArgs e)
        {
            
        }
        void splitString()
        {


            for (int i = 0; i < listUser.Count(); i++)
            {
                string[] temp = listUser[i].Split(':');
                IpUser.Add(temp[0]);
                PortUser.Add(Int32.Parse(temp[1]));
            }
             
        }
        
    }
}
