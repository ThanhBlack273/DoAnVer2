using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    
    public partial class Caro : Form
    {
        #region Properties
        GameBoard board;
        public Caro()
        {
            InitializeComponent();
            board = new GameBoard(banco);
            board.GameOver += Board_GameOver;

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





    }
    
}
