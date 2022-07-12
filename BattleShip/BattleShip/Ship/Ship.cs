using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Ship
{
    internal abstract class Ship
    {
        public abstract string Name { get; set; }
        public abstract int Length { get; set; }
        public abstract List<Position> Position { get; set; }
    }
}
