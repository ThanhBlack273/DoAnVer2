using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using socketmanager;

namespace caro
{
    public partial class _2playersinlan : Form
    {
        SocketManager socket;
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
            socket = new SocketManager();
            Caro caro = new Caro();
            caro.Show();
            this.Hide();
           
        }
      

    }
}

