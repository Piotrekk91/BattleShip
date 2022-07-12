using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class GameBoard
    {
        public List<Boat> Boats { get; set; }
        public List<Coordinate> GameField { get; set; }

        public GameBoard()
        {
            Boats = new List<Boat>();
            GameField = new List<Coordinate>();
        }
    }
}
