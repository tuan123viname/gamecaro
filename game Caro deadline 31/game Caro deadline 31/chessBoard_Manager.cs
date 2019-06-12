using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace game_Caro_deadline_31
{
    public class chessBoard_Manager
    {
        #region initialize
        public chessBoard_Manager(Panel pnl, TextBox text)
        {
            listPlayer = new List<Player>();
            Player player1 = new Player(client.namePlayer, Image.FromFile("1.jpg"), Image.FromFile("luffy.png"), 1);
            listPlayer.Add(player1);
            Player player2 = new Player(client.nameOtherPlayer, Image.FromFile("2.png"), Image.FromFile("zoro.jpg"), 2);
            listPlayer.Add(player2);
            this.avtPlayer = pnl;
            this.PlayerName = text;
        }


        #endregion
        #region properties
        private static List<List<Button>> chessBoard;
        private List<Player> player;
        private int currPlayer = 0;
        private Panel avtPlayer;
        private TextBox playerName;
        public List<Player> listPlayer { get => player; set => player = value; }
        public int CurrPlayer { get => currPlayer; set => currPlayer = value; }
        public Panel AvtPlayer { get => avtPlayer; set => avtPlayer = value; }
        public TextBox PlayerName { get => playerName; set => playerName = value; }
        private Stack<PlayerInfo> playTimeLine;

        public Stack<PlayerInfo> PlayTimeLine;
        #endregion
        #region method
        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add { endedGame += value; }
            remove { endedGame -= value; }
        }
        private event EventHandler<ButtonClickEvent> playerMark;
        public event EventHandler<ButtonClickEvent> PlayerMark
        {
            add { playerMark += value; }
            remove { playerMark -= value; }
        }
        public static void createChessBoard()
        {
            chessBoard = new List<List<Button>>();
            Button oldBtn = new Button()
            {
                Width = 30,
                Height = 30,
                Location = new Point()
                {
                    X = -30,
                    Y = -30
                }
            };
            for(int i=0;i<contain.chessBoardHeight;i++)
            {
                oldBtn.Location = new Point()
                {
                    X = -30,
                    Y = oldBtn.Location.Y + contain.btnHeight
                };
                List<Button> listBtn = new List<Button>();
                for (int j=0;j<contain.chessBoardWidth;j++)
                {
                    Button btn = new Button()
                    {
                        Width = contain.btnWidth,
                        Height = contain.btnHeight,
                        Location = new Point(oldBtn.Location.X + contain.btnWidth, oldBtn.Location.Y)
                    };
                    listBtn.Add(btn);
                    btn.Tag = i.ToString();
                    
                    oldBtn = btn;
                    
                }
                chessBoard.Add(listBtn);
            }
        }
        public void drawChessBoard(Panel pnl)
        {
            createChessBoard();
            pnl.Enabled = true;
            pnl.Controls.Clear();
            PlayTimeLine = new Stack<PlayerInfo>();
            changePlayer();
            foreach (List<Button> list in chessBoard)
            {
                foreach(Button btn in list)
                {
                    pnl.Controls.Add(btn);
                    btn.Click += playerClick;
                   
                }
            }
           
            

        }
       public void playerClick(object sender, EventArgs e)
        {
           
            Button btn= sender as Button;
           
            if (btn.BackgroundImage!=null)
            {
                return;
            }
            btn.BackgroundImage = listPlayer[CurrPlayer].Mark;
            PlayTimeLine.Push(new PlayerInfo(getPoint(btn), CurrPlayer));

            if (CurrPlayer == 0)
                CurrPlayer = 1;
            else
                CurrPlayer = 0;

            changePlayer();

            if (playerMark != null)
                playerMark(this, new ButtonClickEvent(getPoint(btn)));

            if (isEndGame(btn) == true)
                EndGame();
        }
        public void OtherPlayerClick(Point point)
        {
            Button btn = chessBoard[point.Y][point.X];

            if (btn.BackgroundImage != null)
                return;

            btn.BackgroundImage = listPlayer[CurrPlayer].Mark;
            PlayTimeLine.Push(new PlayerInfo(getPoint(btn), CurrPlayer));

            if (CurrPlayer == 0)
                CurrPlayer = 1;
            else
                CurrPlayer = 0;

            changePlayer();

            if (isEndGame(btn))
            {
                EndGame();
            }
        }
    
        void changePlayer()
        {
            AvtPlayer.BackgroundImage = listPlayer[CurrPlayer].Avt;
            PlayerName.Text = listPlayer[CurrPlayer].Name;
        }
        bool isEndGame(Button btn)
        {
            return endHorizotal(btn) || endVertical(btn) || endPrimary(btn) || endSub( btn);
        }
        public void EndGame()
        {
            if (endedGame != null)
                endedGame(this, new EventArgs());
           
        }
        bool endHorizotal(Button btn)
        {
            Point point= getPoint(btn);
            int count = 1;
            for(int i=point.X-1;i>=0;i--)
            {

                if (btn.BackgroundImage != chessBoard[point.Y][i].BackgroundImage)
                    break;
                    if (btn.BackgroundImage == chessBoard[point.Y][i].BackgroundImage)
                    count++;
            }
            for(int i=point.X+1;i<chessBoard[point.Y].Count;i++)
            {
                if (btn.BackgroundImage != chessBoard[point.Y][i].BackgroundImage)
                    break;
                if (btn.BackgroundImage == chessBoard[point.Y][i].BackgroundImage)
                    count++;
            }
            if (count >= 5)
                return true;
            return false;
        }
        bool endVertical(Button btn)
        {
            Point point = getPoint(btn);
            int count = 1;
            for (int i = point.Y - 1; i >= 0; i--)
            {

                if (btn.BackgroundImage != chessBoard[i][point.X].BackgroundImage)
                    break;
                if (btn.BackgroundImage == chessBoard[i][point.X].BackgroundImage)
                    count++;
            }
            for (int i = point.Y + 1; i < chessBoard[point.X].Count; i++)
            {
                if (btn.BackgroundImage != chessBoard[i][point.X].BackgroundImage)
                    break;
                if (btn.BackgroundImage == chessBoard[i][point.X].BackgroundImage)
                    count++;
            }
            if (count >= 5)
                return true;
            return false;

        }
        bool endPrimary(Button btn)
        {
           Point point= getPoint(btn);
            int count = 0;
            int i = point.Y;
            int j = point.X;
           while(i>=0&&j>=0)
            {

                if (btn.BackgroundImage != chessBoard[i][j].BackgroundImage)
                    break;
                    if (btn.BackgroundImage == chessBoard[i][j].BackgroundImage)
                    count++;
                i--;
                j--;
            }
             i = point.Y+1;
            j = point.X+1;
            while (i <chessBoard[0].Count && j < chessBoard.Count)
            {

                if (btn.BackgroundImage != chessBoard[i][j].BackgroundImage)
                    break;
                if (btn.BackgroundImage == chessBoard[i][j].BackgroundImage)
                    count++;
                i++;
                j++;
            }
            if (count >= 5)
                return true;
            return false;
        }
        bool endSub(Button btn)
        {
            Point point = getPoint(btn);
            int count = 0;
            int i = point.Y;
            int j = point.X;
            while (i >= 0 && j < chessBoard[0].Count)
            {

                if (btn.BackgroundImage != chessBoard[i][j].BackgroundImage)
                    break;
                if (btn.BackgroundImage == chessBoard[i][j].BackgroundImage)
                    count++;
                i--;
                j++;
            }
            i = point.Y + 1;
            j = point.X -1;
            while (i < chessBoard[0].Count && j>=0)
            {

                if (btn.BackgroundImage != chessBoard[i][j].BackgroundImage)
                    break;
                if (btn.BackgroundImage == chessBoard[i][j].BackgroundImage)
                    count++;
                i++;
                j--;
            }
            if (count >= 5)
                return true;
            return false;
        }
        Point getPoint(Button btn)
        {
            Point point = new Point();
            point.Y = Int32.Parse(btn.Tag.ToString());
            for(int i=0;i<chessBoard[point.Y].Count;i++)
            {
                if(btn.Location==chessBoard[point.Y][i].Location)
                {
                    point.X = i;
                }
            }
            return point;
        }
        #endregion
    }
}
public class ButtonClickEvent : EventArgs
{
    private Point clickedPoint;

    public Point ClickedPoint
    {
        get { return clickedPoint; }
        set { clickedPoint = value; }
    }

    public ButtonClickEvent(Point point)
    {
        this.ClickedPoint = point;
    }
}

