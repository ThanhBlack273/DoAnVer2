using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using socketdata;
using socketmanager;
using System.Net.NetworkInformation;

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
        string IP;
        public Caro(string yourname1, string yourname2)
        {
            InitializeComponent();
            label1.Text = yourname1;
            label2.Text = yourname2;
            board = new GameBoard(banco);
            board.GameOver += Board_GameOver;
            socket = new SocketManager();
            //board.PlayerClicked += Board_PlayerClicked;
            NewGame();
        }
        public Caro(string soip)
        {
            InitializeComponent();
            board = new GameBoard(banco);
            board.GameOver += Board_GameOver;
            socket = new SocketManager();
            //board.PlayerClicked += Board_PlayerClicked;
            this.IP = soip;
        }

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
            socket.IP = IP;

            if (!socket.ConnectServer())
            {
                socket.IsServer = true;
                socket.CreateServer();
                MessageBox.Show("Bạn đang là Server", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                socket.IsServer = false;
                nghe();
                MessageBox.Show("Kết nối thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
            this.Close();
            menu menu = new menu();
            menu.Show();
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
            socket.Send(new SocketData((int)SocketCommand.SEND_MESSAGE, hienchat.Text, new Point()));
            nghe();
        }

       
        private void nghe()
        {
            Thread ListenThread = new Thread(() =>
            {
                try
                {
                    SocketData data = (SocketData)socket.Receive();
                    ProcessData(data);
                }
                catch { }
            });

            ListenThread.IsBackground = true;
            ListenThread.Start();

        }
        private void ProcessData(SocketData data)
        {
            if (data.Command == (int)SocketCommand.SEND_MESSAGE)
            {
                hienchat.Text = data.Message;
            }
        }

        private void Caro_Shown(object sender, EventArgs e)
        {
            IP = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(IP))
                IP = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
        }
    }
}
