using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace game_Caro_deadline_31
{
    public class Player
    {
       
        #region initialize
        public Player(string name,Image mark, Image avt,int idPlayer)
        {
            this.Name = name;
            this.Mark = mark;
            this.Avt = avt;
            this.idPlayer = idPlayer;
          
        }
        public string Name { get => name; set => name = value; }
        public Image Mark { get => mark; set => mark = value; }
        public Image Avt { get => avt; set => avt = value; }
        public int IdPlayer { get => idPlayer; set => idPlayer = value; }

        #endregion
        #region properties
        private string name;
        private Image mark;
        private Image avt;
        private int idPlayer;
        #endregion
        #region method
       
        #endregion
    }
}
