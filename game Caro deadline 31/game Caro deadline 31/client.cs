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
        public static string namePlayer1 = "Player 1";
        public static string namePlayer2 = "Player 2";
        private string IP = "127.0.0.1";
        private int Port = 9999;
        public static string ipAndPort;
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
            namePlayer1 = tb_PlayerName.Text;
            Connect();
            CheckForIllegalCrossThreadCalls = false;
            
            
            Thread load = new Thread(new ThreadStart(listenFromServer));
            load.IsBackground = true;
            load.Start(); 

        }

        room openedForm = null;
        void Connect()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), Port);

            Client.Connect(iep);
            if (Client.Connected == true)
            {
                
                if (openedForm == null)
                {
                    openedForm = new room();
                    openedForm.Show();
                }
            }
        }
        void listenFromServer()
        {
            while (true)
            {
                    byte[] byteReceive = new byte[1024];
                    Client.Receive(byteReceive);
                    if (byteReceive != null)
                    {
                        object obj = DeserializeData(byteReceive);
                        string[] rcvString = (string[])obj;
                        //----------------------------------------------
                        string str = rcvString[0];
                        if (str[0] == 'P')
                        {
                            string[] arrstr = str.Split(':');
                            string ip_port_server = arrstr[1] + ":" + arrstr[2];
                            string name = arrstr[3];
                            string acceptString = "";
                            
                            DialogResult dialogResult = MessageBox.Show("Người chơi " + ip_port_server + " muốn chơi cờ với bạn. Bạn có đồng ý không ?", "Thông báo", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                acceptString = "Y:" + ip_port_server + ":" + namePlayer1;
                                ipAndPort = "C:" + ip_port_server;
                                namePlayer2 = namePlayer1;
                                namePlayer1 = name;
                                byte[] byteSend = Encoding.ASCII.GetBytes(acceptString);
                                Client.Send(byteSend);
                                Client.Close();
                                if (openedForm != null)
                                {
                                    openedForm.Close();
                                    openedForm = null;
                                }
                                Form frm = new chessBoard();
                                frm.ShowDialog();
                             
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                acceptString = "N:" + ip_port_server;
                                byte[] byteSend = Encoding.ASCII.GetBytes(acceptString);
                                Client.Send(byteSend);
                            }
                            continue;
                        }
                        if (str[0] == 'Y')
                        {
                            string[] arrstr = str.Split(':');
                            ipAndPort = "S:" + arrstr[1] + ":" + arrstr[2];
                            namePlayer2 = arrstr[3];
                            client.Client.Close();
                            
                            if (openedForm != null)
                            {
                                openedForm.Close();
                                openedForm = null;
                            }

                            Form frm = new chessBoard();
                            frm.ShowDialog();

                            break;
                        }
                        if (str[0] == 'N')
                        {
                            MessageBox.Show("Người chơi không đồng ý ghép đôi !");
                            break;
                        }
                        //----------------------------------------------

                        listUser.Clear();
                        foreach (string s in rcvString)
                        {
                            listUser.Add(s);
                        }
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
    }
}
