using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.Net.Sockets;

namespace game_Caro_deadline_31
{

    public class SocketData
    {
        private int command;

        public int Command { get => command; set => command = value; }
        public Point Point { get => point; set => point = value; }

        private Point point;
        public SocketData(int command,Point point)
        {
            this.Command = command;
            this.Point = point;
        }
    }
  
}
