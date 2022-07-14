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
        public List<Ship> Ships { get; set; }
        public List<Coordinate> GameField { get; set; }
        private readonly Random _random = new Random();

        public GameBoard()
        {
            Ships = new List<Ship>();
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
   
        public List<Coordinate> GenerateShip(ShipType shipType)
        {
            var availableListCoords = new List<Coordinate>();

            foreach(var coord in GameField)
            {
                if (coord.IsAvailable)
                {
                    availableListCoords.Add(coord);                    
                }
            }            
            var shipCoords = FindShipCoordinates(availableListCoords,shipType);

            DisableSelectedCoordinates(shipCoords);

            AddShipModelToShipList(shipType, shipCoords);

            DisableNeightboursCoordinates(shipCoords, availableListCoords);

            return shipCoords;
        }

        public void DisableSelectedCoordinates(List<Coordinate> shipCoords)
        {
            foreach(var coord in shipCoords)
            {
                coord.IsAvailable = false;
            }
        }

        public void AddShipModelToShipList(ShipType shipType, List<Coordinate> shipCoords)
        {
            var ship = new Ship(shipType, shipCoords);
            Ships.Add(ship);            
        }
        public void DisableNeightboursCoordinates(List<Coordinate> shipCoords, List<Coordinate> availableListCoords)
        {
            var disabledCoords = new List<Coordinate>();

            foreach(var coord in shipCoords)
            {
                var xValues = Enumerable.Range(coord.X - 1, 3).ToList();
                var yValues = Enumerable.Range(coord.Y - 1, 3).ToList();

                var coordsToDisable = availableListCoords.Where(c => xValues.Contains(c.X)
                    && yValues.Contains(c.Y) && !disabledCoords.Any(d => d.X == c.X && d.Y == c.Y)).ToList();
                
                coordsToDisable.ForEach(c => c.IsAvailable = false);

                disabledCoords.AddRange(coordsToDisable);
            }
        }

        public List<Coordinate> FindShipCoordinates(List<Coordinate> availableListCoords, ShipType shipType)
        {
            var randCoord = _random.Next(availableListCoords.Count);
            var startPoint = availableListCoords[randCoord];
            var randDirection = DirectionChoice();
            var shipCoords = new List<Coordinate>();
            shipCoords.Add(startPoint);
            var shipLength = (int)shipType;

            if (randDirection == Direction.Right)
            {
                for (int i = 1; i < shipLength; i++)
                {
                    var nextPoint = availableListCoords.SingleOrDefault(element => element.X == startPoint.X + i
                    && element.Y == startPoint.Y);
                    if(nextPoint != null)
                    {
                        shipCoords.Add(nextPoint);
                    }
                    else
                    {
                        return FindShipCoordinates(availableListCoords, shipType);                        
                    }

                }
            }
            else if (randDirection == Direction.Down)
            {
                for (int i = 1; i < shipLength; i++)
                {
                    var nextPoint = availableListCoords.SingleOrDefault(element => element.X == startPoint.X
                    && element.Y == startPoint.Y + i);
                    if (nextPoint != null)
                    {
                        shipCoords.Add(nextPoint);
                    }
                    else
                    {
                        return FindShipCoordinates(availableListCoords,shipType);
                    }
                }
            }
            return shipCoords;
        }

        public Direction DirectionChoice()
        {

            var randDirection = (Direction)_random.Next(0, 2);

            return randDirection;
        } 
    }
}
