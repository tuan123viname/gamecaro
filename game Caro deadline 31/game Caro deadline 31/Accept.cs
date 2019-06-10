using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_Caro_deadline_31
{
    public partial class Accept : Form
    {
        public static int accept;
        private string mess;
        public Accept()
        {
            InitializeComponent();
        }
        public Accept(string str):this()
        {
            //InitializeComponent();
            mess = str;
            label1.Text = "nguoi choi" + mess + "muon ghep doi voi ban";

        }

        private void Accept_Load(object sender, EventArgs e)
        {
           
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            accept = 1;
            Form frm = new chessBoard();
            frm.Show();
            
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            accept = 0;
            this.Close();
        }
    }
}
