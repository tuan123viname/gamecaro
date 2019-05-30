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
using System.Threading;
namespace game_Caro_deadline_31
{
    public partial class room : Form
    {
        private static List<string> listIpUser = new List<string>();
        private static List<int> listPortUser = new List<int>();
        private static Panel pnl_PlayerList;
        delegate void CreateBtnPlayerOnline(string str);
        public room()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            Thread LoadUser = new Thread(new ThreadStart(loadUser));
            LoadUser.IsBackground = true;
            LoadUser.Start();
            pnl_PlayerList = new Panel()
            {
                Width = 300,
                Height = 400,
                Location = new Point(0, 50),
            };
       
            tb_PlayerName.Text = client.namePlayer;
          

        }

        private void tb_PlayerName_TextChanged(object sender, EventArgs e)
        {
            
        }

      
        private void Btn_Click(object sender, EventArgs e)
        {
            Form frm = new chessBoard();
            frm.ShowDialog();
        }

        private void room_Load(object sender, EventArgs e)
        {
            
        }
       
        void loadUser()
        {
         
            while (true)
            {

                listIpUser = client.IpUser;
                listPortUser = client.PortUser;
                
            }
        }
     

        private void btn_PlayerOnline_Click(object sender, EventArgs e)
        {
            for(int i=0;i<listIpUser.Count();i++)
            {

                listView1.Items.Add(listIpUser[i]+listPortUser[i]);
           
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item;
            string ip= listIpUser[item.Index];
            int port =listPortUser[item.Index];

        }
    }
}
