using BattleShip.Enums;
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
            var startDate = DateTime.Now;
            _firstPlayer = new Player("Player_1");
            _secondPlayer = new Player("Player_2");
            var roundNumber = 1;
            
            while(true)
            {
                SimulateRound();
                var gameResult = CheckGameResult(out var winner);

                switch (gameResult)
                {
                    case GameResult.Tie:
                        Console.WriteLine($"Game ended in {roundNumber} rounds with result: {GameResult.Tie}");
                        CalculateGameTime(startDate);
                        return;
                    case GameResult.GameOver:
                        Console.WriteLine($"Game ended in {roundNumber} rounds with result: {winner?.Name} is the winner");
                        CalculateGameTime(startDate);
                        return;
                    case GameResult.Playing:
                        break;
                }
                roundNumber++;
            }
        }
        private void CalculateGameTime(DateTime startDate)   
        {
            var endDate = DateTime.Now;
            var timeDiff = endDate.Subtract(startDate).TotalSeconds;
            Console.WriteLine($"Simulation took: {timeDiff} seconds");
        }

        private void SimulateRound()
        {
            var randFPIndex = _random.Next(_firstPlayer.GameBoard.GameField.Count);
            var randFPShot = _firstPlayer.GameBoard.GameField[randFPIndex];

            var randSPIndex = _random.Next(_secondPlayer.GameBoard.GameField.Count);
            var randSPShot = _secondPlayer.GameBoard.GameField[randSPIndex];

            var fpShootPoint = _firstPlayer.GameBoard.GameField.SingleOrDefault(c => c.X == randFPShot.X && c.Y == randFPShot.Y);
            var spShootPoint = _secondPlayer.GameBoard.GameField.SingleOrDefault(c => c.X == randSPShot.X && c.Y == randSPShot.Y);

            if (fpShootPoint == null)
            {
                throw new ArgumentNullException(nameof(randFPShot), "Incorrect hit point selected!");
            }
            else if (spShootPoint == null)
            {
                throw new ArgumentNullException(nameof(randSPShot), "Incorrect hit point selected!");
            }

            if (fpShootPoint.IsMarked == true || spShootPoint.IsMarked == true)
            {
                SimulateRound();
                return;
            }

            fpShootPoint.IsMarked = true;
            spShootPoint.IsMarked = true;

            CheckShipsStatuses();
        }
        private void CheckShipsStatuses()
        {
            var fpDestroyedShips = _firstPlayer.GameBoard.Ships.Where(s => !s.IsDestroyed && s.ShipCoordinates.All(p => p.IsMarked)).ToList();
            var spDestroyedShips = _secondPlayer.GameBoard.Ships.Where(s => !s.IsDestroyed && s.ShipCoordinates.All(p => p.IsMarked)).ToList();
            fpDestroyedShips.ForEach(s => s.IsDestroyed = true);
            spDestroyedShips.ForEach(s => s.IsDestroyed = true);

        }
        private GameResult CheckGameResult(out Player? winner)
        {
            winner = null; 
            var firstPlayerDestroyed = _firstPlayer.GameBoard.Ships.All(s => s.IsDestroyed);
            var secondPlayerDestroyed = _secondPlayer.GameBoard.Ships.All(s => s.IsDestroyed);

            switch (firstPlayerDestroyed)
            {
                case true when secondPlayerDestroyed:
                    return GameResult.Tie;
                case true:
                    winner = _secondPlayer;
                    return GameResult.GameOver;
                case false when secondPlayerDestroyed:
                    winner = _firstPlayer;
                    return GameResult.GameOver;
                default:
                    return GameResult.Playing;
            }
        }
    }
}
