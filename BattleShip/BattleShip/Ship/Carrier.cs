using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Ship
{
    internal class Carrier : Ship
    {
        public override string Name { get; set; } = "Carrier";
        public override int Length { get; set; } = 5;
        public override List<Position> Position { get; set; }
    }
}
