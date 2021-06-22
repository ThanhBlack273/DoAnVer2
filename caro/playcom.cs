using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace caro
{
    public partial class playcom : Form
    {
        public playcom()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            menu menu = new menu();
            menu.Show();
        }

        private void start_Click(object sender, EventArgs e)
        {
            string yourname1 = textBox1.Text;
            string yourname2 = textBox2.Text;
            int mod = 2;
            Caro caro = new Caro(yourname1,yourname2, mod);
            caro.Show();
            this.Hide();

        }
    }
}
