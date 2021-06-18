using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using socketdata;
using socketmanager;

namespace caro
{

    public partial class Caro : Form
    {
        #region Properties
        GameBoard board;
        SocketManager socket;
        public Caro()
        {
            InitializeComponent();
            board = new GameBoard(banco);
            board.GameOver += Board_GameOver;
            socket = new SocketManager();
            //board.PlayerClicked += Board_PlayerClicked;
            NewGame();

        }
        #endregion


        #region Methods
        void NewGame()
        {
            //pgb_CountDown.Value = 0;
            //tm_CountDown.Stop();

            //undoToolStripMenuItem.Enabled = true;
            //redoToolStripMenuItem.Enabled = true;

            //btn_Undo.Enabled = true;
            //btn_Redo.Enabled = true;

            board.DrawGameBoard();
        }
        void EndGame()
        {
            //undoToolStripMenuItem.Enabled = false;
            //redoToolStripMenuItem.Enabled = false;

            //btn_Undo.Enabled = false;
            //btn_Redo.Enabled = false;

            //tm_CountDown.Stop();
            banco.Enabled = false;
        }

        private void Caro_Load(object sender, EventArgs e)
        {


        }

        private void Caro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
            
            //else
            //{
            //    try
            //    {
            //        socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
            //    }
            //    catch { }
            //}
        }

        private void Board_GameOver(object sender, EventArgs e)
        {
            EndGame();

            //if (board.PlayMode == 1)
            //    socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
        }





        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            board.Undo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            board.Redo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void send_Click(object sender, EventArgs e)
        {
            
           hienchat.Text += "- " + "" + ": " + nhapchat.Text + "\r\n";
           nhapchat.Text = null;
           socket.Send(new SocketData((int)SocketCommand.SEND_MESSAGE, hienchat.Text, new Point() ));
        }

    }
}
