using System;
using System.Windows.Forms;
using socketmanager;
using System.Threading;
using socketdata;

namespace caro
{
    public partial class _2playersinlan : Form
    {
        public _2playersinlan()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            menu menu = new menu();
            menu.Show();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            string ip = txbIP.Text;
            int mod = 1;
            Caro caro = new Caro(name, ip, mod);
            caro.Show();
            this.Hide();
        }
    }
}

