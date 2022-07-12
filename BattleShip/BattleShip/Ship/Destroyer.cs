using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Ship
{
    internal class Destroyer : Ship
    {
        public override string Name { get; set; } = "Destroyer";
        public override int Length { get; set; } = 3;
        public override List<Position> Position { get; set; }
    }
}
