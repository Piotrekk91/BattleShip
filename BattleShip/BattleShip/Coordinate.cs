using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsMarked { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
            IsAvailable = true;
            IsMarked =  false;
        }

        public override string ToString()
        {
            var returnString = ($"X:{X}, Y:{Y}, {IsAvailable}");
            return returnString;
        }
    }
}
