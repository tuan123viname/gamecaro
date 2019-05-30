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
    public partial class chessBoard : Form
    {
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
    }
}
