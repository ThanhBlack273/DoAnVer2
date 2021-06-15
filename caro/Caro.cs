using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caro
{
    
    public partial class Caro : Form
    {
        #region Properties
        GameBoard board;
        public Caro()
        {
            InitializeComponent();
            board = new GameBoard(banco);
            board.DrawGameBoard();
            //board.PlayerClicked += Board_PlayerClicked;

        }
        #endregion
        private void Caro_Load(object sender, EventArgs e)
        {
            
            
        }
    }
    
}
