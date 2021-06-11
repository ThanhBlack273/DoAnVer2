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
            Close();
        }

        private void start_Click(object sender, EventArgs e)
        {
            Caro caro = new Caro();
            caro.Show();
        }
    }
}
