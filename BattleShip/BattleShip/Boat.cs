using BattleShip.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Boat
    {
        public ShipType ShipType { get; set; }

        public List<Coordinate> ShipCoordinates { get; set; }
        public bool IsDestroyed { get; set; }

        public Boat(ShipType shipType, List<Coordinate> shipCoordinates)
        {
            ShipType = shipType;
            ShipCoordinates = shipCoordinates;
            IsDestroyed = false;
        }
    }
}
