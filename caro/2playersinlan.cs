using System;
using System.Windows.Forms;
using socketmanager;
using System.Threading;
using socketdata;

namespace caro
{
    public partial class _2playersinlan : Form
    {
        SocketManager socket;
        public _2playersinlan()
        {
            InitializeComponent();
            Caro ca = new Caro(txbIP.Text);
            socket = new SocketManager();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            menu menu = new menu();
            menu.Show();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            Caro caro = new Caro();
            caro.Show();
            this.Hide();
            socket.IP = txbIP.Text;
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
                SocketData data = (SocketData)socket.Receive();
            });
            ListenThread.IsBackground = true;
            ListenThread.Start();

        }
    }
}

