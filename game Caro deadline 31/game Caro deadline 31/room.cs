﻿using System;
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
        public static string ipAndPort;
        private static List<string> listUser = new List<string>();
        private static Panel pnl_PlayerList;
        delegate void CreateBtnPlayerOnline(string str);
        public room()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            pnl_PlayerList = new Panel()
            {
                Width = 300,
                Height = 400,
                Location = new Point(0, 50),
            };

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
            lblPlayerName.Text = lblPlayerName.Text + client.namePlayer1;
        }
       
        void loadUser()
        {
            while (true)
            {
                listUser = client.listUser;  
            }
        }
     

        private void btn_PlayerOnline_Click(object sender, EventArgs e)
        {
            listUser = client.listUser;
            listView1.Clear();
            for (int i=0;i<listUser.Count();i++)
            {

                if (listUser[i] != client.Client.LocalEndPoint.ToString())
                {
                    listView1.Items.Add(listUser[i]);
                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var item = e.Item;
            string[] ip_port = item.Text.Split(':');
            
            //---------------------------------
            if (client.Client.Connected == true)
            {
                string playString = "P:" + ip_port[0] + ":" + ip_port[1] + ":" + client.namePlayer1;
                ipAndPort="S:"+ client.Client.LocalEndPoint.ToString() + ":" + ip_port[0] + ":" + ip_port[1];
                byte[] byteSend = Encoding.ASCII.GetBytes(playString);
                client.Client.Send(byteSend);
            }

            //---------------------------------
        }
       
    }
}
