using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Ship
{
    internal class Battleship : Ship
    {
        public override string Name { get; set; } = "BattleShip";
        public override int Length { get; set; } = 4;
        public override List<Position> Position { get; set; }

    }
}
