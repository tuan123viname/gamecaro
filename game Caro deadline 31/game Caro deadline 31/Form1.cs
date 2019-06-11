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
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace game_Caro_deadline_31
{
    [Serializable]
    public partial class chessBoard : Form
    {
        public static Socket Client;
        chessBoard_Manager board;
        public chessBoard()
        {
            InitializeComponent();

            board = new chessBoard_Manager(avatar, playerName);
            board.PlayerMark += Board_PlayerMark;
            board.EndedGame += Board_EndedGame;
            NewGame();
        }

        private void Board_EndedGame(object sender, EventArgs e)
        {
            Endgame();
        }
        void Endgame()
        {
            progress.Value = 0;
            timer1.Stop();
            MessageBox.Show("ket thuc game");
            panel1.Enabled = false;
        }
        private void Board_PlayerMark(object sender, EventArgs e)
        {
            timer1.Start();
            progress.Value = 0;
            PlayerInfo info=board.PlayTimeLine.Pop();
            sendData(info);

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progress.Step = 2;
            progress.PerformStep();
            if (progress.Value >= 100)
                Endgame();

        }


        void NewGame()
        {
            timer1.Stop();
            progress.Value = 0;
            panel1.Controls.Clear();
            board.drawChessBoard(panel1);
        }
        void UndoGame()
        {

        }
        void QuitGame()
        {

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuitGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoGame();
        }
        public void check()
        {
            string[] ipAndPort;
            if (room.ipAndPort!=null)
              ipAndPort = room.ipAndPort.Split(':');
            else
            {
                ipAndPort = client.ipAndPort.Split(':');
            }
            string ip = ipAndPort[1];
            int port = Int32.Parse(ipAndPort[2]);
            if(ipAndPort[0]=="S")
            {
                createServer(ip, port);
            }
            else
            {
                createClient(ip,port);
            }

          
        }
        void createServer(string ip,int port)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipep);
            server.Listen(1);
            Thread thrd= new Thread(()=>{
                Client = server.Accept();
        });
            
        }
        void createClient(string ip, int port)
        {

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try { Client.Connect(ipep); }
            catch { };
           
       
        }
        public void listenClient()
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
                        handleReceiveData(obj);
                        //----------------------------------------------

                    }
                }
                catch { }
              
            }
            
        }
        public void sendData(Object obj)
        {
            byte[] data = new byte[1024];
             data = SerializeData(obj);
            Client.Send(data);
        }
        public void handleReceiveData(Object obj)
        {
            SocketData data = (SocketData)obj;
            if(data.Command==1)
            {
                //new game
            }
            else if(data.Command==2)
            {
                board.OtherPlayerClick(data.Point);
            }
            else
            {
                //thoat game
            }
        }
        byte[] SerializeData(Object o)
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
        object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }

        private void chessBoard_Load(object sender, EventArgs e)
        {
            check();
            CheckForIllegalCrossThreadCalls = false;
            Thread thrd = new Thread(new ThreadStart(listenClient));
            thrd.IsBackground = true;
            thrd.Start();
        }
    }
}
