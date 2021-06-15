using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace caro
{
    class GameBoard
    {

        #region Properties
        private Panel board;

        private int currentPlayer;
        private TextBox playerName;
        private PictureBox avatar;

        private List<Player> listPlayers;
        private List<List<Button>> matrixPositions;

        private event EventHandler<BtnClickEvent> playerClicked;
        //private event EventHandler gameOver;

        //private Stack<PlayInfo> stkUndoStep;
        //private Stack<PlayInfo> stkRedoStep;

        //private int playMode = 0;
        //private bool IsAI = false;

        public Panel Board
        {
            get { return board; }
            set { board = value; }
        }

        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        public TextBox PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public PictureBox Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }

        public List<Player> ListPlayers
        {
            get { return listPlayers; }
            set { listPlayers = value; }
        }

        public List<List<Button>> MatrixPositions
        {
            get { return matrixPositions; }
            set { matrixPositions = value; }
        }

        public event EventHandler<BtnClickEvent> PlayerClicked
        {
            add { playerClicked += value; }
            remove { playerClicked -= value; }
        }

        //public event EventHandler GameOver
        //{
        //    add { gameOver += value; }
        //    remove { gameOver -= value; }
        //}

        //public Stack<PlayInfo> StkUndoStep
        //{
        //    get { return stkUndoStep; }
        //    set { stkUndoStep = value; }
        //}

        //public Stack<PlayInfo> StkRedoStep
        //{
        //    get { return stkRedoStep; }
        //    set { stkRedoStep = value; }
        //}

        //public int PlayMode
        //{
        //    get { return playMode; }
        //    set { playMode = value; }
        //}
        #endregion

        #region Intialize
        public GameBoard(Panel board)
        {
            this.Board = board;
            //this.PlayerName = PlayerName;
            //this.Avatar = Avatar;

            this.CurrentPlayer = 0;
            this.ListPlayers = new List<Player>()
            {
                //new Player("Quân Đặng", Image.FromFile(Application.StartupPath + "\\images\\Quan.jpg"),
                //                        Image.FromFile(Application.StartupPath + "\\images\\X.png")),

                //new Player("Bà Xã", Image.FromFile(Application.StartupPath + "\\images\\Lisa.jpg"),
                //                   Image.FromFile(Application.StartupPath + "\\images\\O.png"))

                new Player("player1", Image.FromFile(Application.StartupPath + "\\Resources\\ava2.png"),
                                        Image.FromFile(Application.StartupPath + "\\Resources\\x.png")),

                new Player("player2", Image.FromFile(Application.StartupPath + "\\Resources\\ava1.png"),
                                   Image.FromFile(Application.StartupPath + "\\Resources\\o.png"))
            };
        }
        #endregion

        #region Methods       
        public void DrawGameBoard()
        {
            board.Enabled = true;
            board.Controls.Clear();

            //StkUndoStep = new Stack<PlayInfo>();
            //StkRedoStep = new Stack<PlayInfo>();

            this.CurrentPlayer = 0;
            //ChangePlayer();

            int LocX, LocY;
            int nRows = Constance.nRows;
            int nCols = Constance.nCols;

            Button OldButton = new Button();
            OldButton.Width = OldButton.Height = 0;
            OldButton.Location = new Point(0, 0);

            MatrixPositions = new List<List<Button>>();

            for (int i = 0; i < nRows; i++)
            {
                MatrixPositions.Add(new List<Button>());

                for (int j = 0; j < nCols; j++)
                {
                    LocX = OldButton.Location.X + OldButton.Width;
                    LocY = OldButton.Location.Y;

                    Button btn = new Button()
                    {
                        Width = Constance.CellWidth,
                        Height = Constance.CellHeight,

                        Location = new Point(LocX, LocY),
                        //Tag = i.ToString(), // Để xác định button đang ở hàng nào

                        BackColor = Color.Lavender,
                        BackgroundImageLayout = ImageLayout.Stretch
                    };

                    btn.Click += btn_Click;
                    MatrixPositions[i].Add(btn);

                    Board.Controls.Add(btn);
                    OldButton = btn;
                }

                OldButton.Location = new Point(0, OldButton.Location.Y + Constance.CellHeight);
                OldButton.Width = OldButton.Height = 0;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackgroundImage != null)
                return; // Nếu ô đã được đánh thì ko cho đánh lại

            btn.BackgroundImage = ListPlayers[CurrentPlayer].Symbol;

            //btn.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\x.png");

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
            ChangePlayer();

            if (playerClicked != null)
                playerClicked(this, new BtnClickEvent(GetButtonCoordinate(btn)));
        }
        private Point GetButtonCoordinate(Button btn)
        {
            int Vertical = Convert.ToInt32(btn.Tag);
            int Horizontal = MatrixPositions[Vertical].IndexOf(btn);

            Point Coordinate = new Point(Horizontal, Vertical);
            return Coordinate;
        }
        #region 2 players
        private void ChangePlayer()
        {
            //PlayerName.Text = ListPlayers[CurrentPlayer].Name;
            //Avatar.Image = ListPlayers[CurrentPlayer].Avatar;
        }
        #endregion
        #endregion
    }
}
