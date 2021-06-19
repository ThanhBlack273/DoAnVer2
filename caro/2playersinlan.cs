using System;
using System.Windows.Forms;
using socketmanager;
using System.Threading;

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
                socket.CreateServer();

                Thread listenThread = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(500);
                        try
                        {
                            Listen();
                            break;
                        }
                        catch
                        {

                        }
                    }
                });
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            else
            {
                Thread listenThread = new Thread(() =>
                {
                    Listen();
                });
                listenThread.IsBackground = true;
                listenThread.Start();

                socket.Send("Thông tin từ Client");
            }
        }
        void Listen()
        {
            string data = (string)socket.Receive();

            MessageBox.Show(data);
        }


    }
}

