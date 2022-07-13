using BattleShip.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Ship
    {
        public ShipType ShipType { get; set; }

        public List<Coordinate> ShipCoordinates { get; set; }
        public bool IsDestroyed { get; set; }

        public Ship(ShipType shipType, List<Coordinate> shipCoordinates)
        {
            ShipType = shipType;
            ShipCoordinates = shipCoordinates;
            IsDestroyed = false;
        }
    }
}
