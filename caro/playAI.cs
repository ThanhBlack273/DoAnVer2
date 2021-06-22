using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace caro
{
    public partial class playAI : Form
    {
        public playAI()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, EventArgs e)
        {
            string yourname1 = tbName.Text;
            string yourname2 = "Com";
            int mod = 3;
            Caro caro = new Caro(yourname1, yourname2, mod);
            caro.Show();
            this.Hide();

        }
    }
}
