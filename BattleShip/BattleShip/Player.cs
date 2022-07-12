using BattleShip.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard GameBoard { get; set; }

        public Player(string name)
        {
            Name = name;
            GameBoard = new GameBoard();
        }
      
    }
}
