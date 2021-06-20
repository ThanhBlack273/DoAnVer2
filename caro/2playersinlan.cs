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
            Caro caro = new Caro();
            caro.Show();
            this.Hide();
        }
    }
}

