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
        public static bool isEndGame = false;
        public static Socket Client;
        public static Socket server;
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
            MessageBox.Show("Kết thúc game!");
            panel1.Enabled = false;
            isEndGame = true;
            //listenOtherPlayer();
        }
        private void Board_PlayerMark(object sender, EventArgs e)
        {
            //progress.Value = 0;
            //timer1.Start();
            timer1.Stop();
            progress.Value = 0;
            PlayerInfo info =board.PlayTimeLine.Pop();
            sendData(info);
            panel1.Enabled = false;

            //menuToolStripMenuItem.Enabled = false;
            listenOtherPlayer();
            
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progress.Step = 2;
            progress.PerformStep();
            if (progress.Value >= 100)
            {
                Point point = new Point(-3, -3);
                PlayerInfo info = new PlayerInfo(point, 0);
                sendData(info);
                Endgame();
            }

        }


        private void NewGame()
        {
            //board = new chessBoard_Manager(avatar, playerName);
            //board.PlayerMark += Board_PlayerMark;
            //board.EndedGame += Board_EndedGame;

            timer1.Stop();
            progress.Value = 0;
            panel1.Controls.Clear();
            board.drawChessBoard(panel1);
            isEndGame = false;
            

        }

        void UndoGame()
        {

        }
        void QuitGame()
        {

            //Client.Close();
            //try
            //{
            //    server.Close();
            //}
            //catch
            //{ }
            //this.Close();
            Application.Exit();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point point = new Point(-1, -1);
            PlayerInfo info = new PlayerInfo(point, 0);
            sendData(info);
            NewGame();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point point = new Point(-2, -2);
            PlayerInfo info = new PlayerInfo(point, 0);
            sendData(info);
            QuitGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoGame();
        }
        public void check()
        {
            //string[] ipAndPort;
            //if (room.ipAndPort!=null)
            //  ipAndPort = room.ipAndPort.Split(':');
            //else
            //{
            //    ipAndPort = client.ipAndPort.Split(':');
            //}
            string[] ipAndPort = client.ipAndPort.Split(':');
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
            panel1.Enabled = true;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipep);
            server.Listen(1);
            Thread thrd = new Thread(() =>
            {
                Client = server.Accept();
            });
            thrd.IsBackground = true;
            thrd.Start();

        }
        void createClient(string ip, int port)
        {
            //menuToolStripMenuItem.Enabled = false;
            panel1.Enabled = false;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (Client.Connected == false)
            {
                try { Client.Connect(ipep); }
                catch { };
            }
            listenOtherPlayer();
            
        }
        Thread listenThread;
        static bool isNewGame = false;
        //bool Stop = false;
        void listenOtherPlayer()
        {
            
            Thread listenThread = new Thread(() =>
            {

            //{
            try
            {
                byte[] byteReceive = new byte[1024];
                        Client.Receive(byteReceive);
                        object obj = DeserializeData(byteReceive);
                        PlayerInfo info = (PlayerInfo)obj;

                    //menuToolStripMenuItem.Enabled = true;
                    if (info.Point.X == -1)
                    {
                        //isNewGame = true;
                        //Stop = true;

                        this.Invoke((MethodInvoker)(() =>
                        {
                            NewGame();
                            panel1.Enabled = false;
                        }));

                    }
                    else if (info.Point.X == -2)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            QuitGame();
                        }));
                    }
                    else if (info.Point.X == -3)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            Endgame();
                        }));
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            board.OtherPlayerClick(info.Point);

                            timer1.Start();
                            progress.Value = 0;

                            if (!isEndGame)
                                panel1.Enabled = true;
                        }));
                        
                    }

                    //listenOtherPlayer();
                }
                catch (Exception e)
                {

                }

            });
            listenThread.IsBackground = true;
            listenThread.Start();

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
            //Thread thrd = new Thread(new ThreadStart(listenClient));
            //thrd.IsBackground = true;
            //thrd.Start();
        }
    }
}
