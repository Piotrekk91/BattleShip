using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class BattleShipApp
    {
        private Player _firstPlayer;
        private Player _secondPlayer;
        private readonly Random _random = new Random(); 

        public void StartGame()
        {
            var dateTime = DateTime.Now;
            _firstPlayer = new Player("Player_1");
            _secondPlayer = new Player("Player_2");
            var gameResult = true;
            while(gameResult)
            {
                var randFPIndex = _random.Next(_firstPlayer.GameBoard.GameField.Count);
                var randShot = _firstPlayer.GameBoard.GameField[randFPIndex];

                var randSPIndex = _random.Next(_secondPlayer.GameBoard.GameField.Count);
                var randSPShot = _secondPlayer.GameBoard.GameField[randSPIndex];

                var fpShootPoint = _firstPlayer.GameBoard.GameField.SingleOrDefault(c => c.X == randShot.X && c.Y == randShot.Y);
                var spShootPoint = _secondPlayer.GameBoard.GameField.SingleOrDefault(c =>c.X == randSPShot.X && c.Y == randSPShot.Y);

                if (fpShootPoint == null || spShootPoint == null)
                {
                    gameResult = false;
                }
                fpShootPoint.IsMarked = true;
                spShootPoint.IsMarked = true;
            }
        }
    }
}
