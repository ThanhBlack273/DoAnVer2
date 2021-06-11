using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            Close();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            Caro caro = new Caro();
            caro.Show();
        }
    }
}
