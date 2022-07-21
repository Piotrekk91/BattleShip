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
            var fpPointNotMarked = _firstPlayer.GameBoard.GameField.Where(c => c.IsMarked == false).ToList();
            var randFPHitPoint = fpPointNotMarked.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
            var fpHitPoint = _firstPlayer.GameBoard.GameField.SingleOrDefault(c => c.X == randFPHitPoint.X && c.Y == randFPHitPoint.Y);

            var spPointNotMarked = _secondPlayer.GameBoard.GameField.Where(c => c.IsMarked == false).ToList();
            var randSPHitPoint = spPointNotMarked.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
            var spHitPoint = _secondPlayer.GameBoard.GameField.SingleOrDefault(c => c.X == randSPHitPoint.X && c.Y == randSPHitPoint.Y);

            if (fpHitPoint == null)
            {
                throw new ArgumentNullException(nameof(randFPHitPoint), "Incorrect hit point selected!");
            }
            else if (spHitPoint == null)
            {
                throw new ArgumentNullException(nameof(randSPHitPoint), "Incorrect hit point selected!");
            }

            fpHitPoint.IsMarked = true;
            spHitPoint.IsMarked = true;

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
