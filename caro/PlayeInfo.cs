using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace caro
{
    class PlayeInfo
    {
        private Point point;
        private int currentPlayer;
        private Image symbol;

        public Point Point { get => point; set => point = value; }
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public Image Symbol { get => symbol; set => symbol = value; }

        public PlayeInfo()
        {

        }

        public PlayeInfo(Point point, int currentPlayer, Image symbol)
        {
            this.Point = point;
            this.CurrentPlayer = currentPlayer;
            this.Symbol = symbol;
        }
    }
}
