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
        string IP = "127.0.0.1";
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
            Listen();
        }

        private void ProcessData(SocketData data)
        {


            switch (data.Command)
            {
                //case (int)SocketCommand.SEND_POINT:
                //    // Có thay đổi giao diện muốn chạy ngọt phải để trong đây
                //    this.Invoke((MethodInvoker)(() =>
                //    {
                //        board.OtherPlayerClicked(data.Point);
                //        pn_GameBoard.Enabled = true;

                //        pgb_CountDown.Value = 0;
                //        tm_CountDown.Start();

                //        undoToolStripMenuItem.Enabled = true;
                //        redoToolStripMenuItem.Enabled = true;

                //        btn_Undo.Enabled = true;
                //        btn_Redo.Enabled = true;
                //    }));
                //    break;

                case (int)SocketCommand.SEND_MESSAGE:
                    hienchat.Text = data.Message;
                    break;

                //    case (int)SocketCommand.NEW_GAME:
                //        this.Invoke((MethodInvoker)(() =>
                //        {
                //            NewGame();
                //            pn_GameBoard.Enabled = false;
                //        }));
                //        break;

                //    case (int)SocketCommand.UNDO:
                //        this.Invoke((MethodInvoker)(() =>
                //        {
                //            pgb_CountDown.Value = 0;
                //            board.Undo();
                //        }));
                //        break;

                //    case (int)SocketCommand.REDO:
                //        this.Invoke((MethodInvoker)(() =>
                //        {
                //            // pgb_CountDown.Value = 0;
                //            board.Redo();
                //        }));
                //        break;

                //    case (int)SocketCommand.END_GAME:
                //        this.Invoke((MethodInvoker)(() =>
                //        {
                //            EndGame();
                //            MessageBox.Show(PlayerName + " đã chiến thắng ♥ !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }));
                //        break;

                //    case (int)SocketCommand.TIME_OUT:
                //        this.Invoke((MethodInvoker)(() =>
                //        {
                //            EndGame();
                //            MessageBox.Show("Hết giờ rồi !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }));
                //        break;

                //    case (int)SocketCommand.QUIT:
                //        this.Invoke((MethodInvoker)(() =>
                //        {
                //            tm_CountDown.Stop();
                //            EndGame();

                //            board.PlayMode = 2;
                //            socket.CloseConnect();

                //            MessageBox.Show("Đối thủ đã chạy mất dép", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }));
                //        break;

                default:
                    break;
            }
            Listen();
        }

        private void Caro_Shown(object sender, EventArgs e)
        {
            IP = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(IP))
               IP = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
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
                Listen();
                MessageBox.Show("Kết nối thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
    }
    private void Listen()
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
    }
}
