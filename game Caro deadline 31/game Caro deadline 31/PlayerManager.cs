using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace game_Caro_deadline_31
{
    class PlayerManager
    {
        #region initialize
        public PlayerManager(Player player)
        {
            ListPlayer.Add(player);
        }
        #endregion
        #region properties
        private List<Player> listPlayer;
        public List<Player> ListPlayer { get => listPlayer; set => listPlayer = value; }
        #endregion
        #region method
        #endregion
    }
}
