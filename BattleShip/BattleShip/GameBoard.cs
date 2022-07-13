﻿using BattleShip.Enums;
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
        public void GenerateShip()
        {
            var listCoord = new List<Coordinate>();

            for(int i = 0; i < 5; i++)
            {
                var coord = GameField[i];
                coord.IsAvailable = false;
                listCoord.Add(coord);
            }
            var ship = new Ship(ShipType.Carrier,listCoord);
            Ship.Add(ship);
        }
        
        public void ShipPlacementInAvailablePlace()
        {
            var availableListCoord = new List<Coordinate>();

            foreach(var coord in GameField)
            {
                if (coord.IsAvailable)
                {
                    availableListCoord.Add(coord);                    
                }
            }
            FindShipCoordinates(availableListCoord);
        }

        public List<Coordinate> FindShipCoordinates(List<Coordinate> availableListCoord)
        {
            var randCoord = _random.Next(availableListCoord.Count);
            var startPoint = availableListCoord[randCoord];
            var randDirection = DirectionChoice();
            var shipCoord = new List<Coordinate>();

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
            return shipCoord;
        }

        public Direction DirectionChoice()
        {

            var randDirection = (Direction)_random.Next(0, 2);

            return randDirection;
        } 
    }
}
