using BattleShip.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class GameBoard
    {
        public List<Ship> Ship { get; set; }
        public List<Coordinate> GameField { get; set; }
        private readonly Random _random = new Random();

        public GameBoard()
        {
            Ship = new List<Ship>();
            GameField = new List<Coordinate>();
        }

        public void GenerateGameField()
        {
            for(int x = 1; x < 11; x++)
            {
                for(int y = 1; y < 11; y++)
                {
                    var coordinate = new Coordinate(x, y);
                    GameField.Add(coordinate);
                }
            }
        }
   
        public List<Coordinate> ShipPlacementInAvailablePlace()
        {
            var availableListCoord = new List<Coordinate>();

            foreach(var coord in GameField)
            {
                if (coord.IsAvailable)
                {
                    availableListCoord.Add(coord);                    
                }
            }            
            var shipCoord = FindShipCoordinates(availableListCoord);

            DisableSelectedCoordinates(shipCoord);

            AddShipModelToShipList(ShipType.Carrier, shipCoord);

            return shipCoord;
        }
        public void DisableSelectedCoordinates(List<Coordinate> shipCoord)
        {
            foreach(var coord in shipCoord)
            {
                coord.IsAvailable = false;
            }
        }
        public void AddShipModelToShipList(ShipType shipType, List<Coordinate> shipCoord)
        {
            var ship = new Ship(shipType, shipCoord);
            Ship.Add(ship);            
        }

        public List<Coordinate> FindShipCoordinates(List<Coordinate> availableListCoord)
        {
            var randCoord = _random.Next(availableListCoord.Count);
            var startPoint = availableListCoord[randCoord];
            var randDirection = DirectionChoice();
            var shipCoord = new List<Coordinate>();
            shipCoord.Add(startPoint);

            if (randDirection == Direction.Right)
            {
                for (int i = 1; i < 5; i++)
                {
                    var nextPoint = availableListCoord.SingleOrDefault(element => element.X == startPoint.X + i
                    && element.Y == startPoint.Y);
                    if(nextPoint != null)
                    {
                        shipCoord.Add(nextPoint);
                    }
                    else
                    {
                        return FindShipCoordinates(availableListCoord);                        
                    }

                }
            }
            else if (randDirection == Direction.Down)
            {
                for (int i = 1; i < 5; i++)
                {
                    var nextPoint = availableListCoord.SingleOrDefault(element => element.X == startPoint.X
                    && element.Y == startPoint.Y + i);
                    if (nextPoint != null)
                    {
                        shipCoord.Add(nextPoint);
                    }
                    else
                    {
                        return FindShipCoordinates(availableListCoord);
                    }
                }
            }
            return shipCoord;
        }

        public Direction DirectionChoice()
        {

            var randDirection = (Direction)_random.Next(0, 2);

            return randDirection;
        } 
    }
}
