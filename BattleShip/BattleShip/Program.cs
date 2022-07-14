// See https://aka.ms/new-console-template for more information
using BattleShip;



var gameboard = new GameBoard();
gameboard.GenerateGameField();
gameboard.GenerateShip(BattleShip.Enums.ShipType.BattleShip);
gameboard.GenerateShip(BattleShip.Enums.ShipType.PatrolBoat);
gameboard.GenerateShip(BattleShip.Enums.ShipType.Destroyer);
gameboard.GenerateShip(BattleShip.Enums.ShipType.Carrier);
PrintGameBoard(gameboard);
//gameboard.GameField.ForEach(x => Console.WriteLine(x));


static void PrintGameBoard(GameBoard gameBoard)
{
    for( int i = 1; i < 11; i++)
    {
        for(int j = 1; j < 11; j++)
        {
            var gameField = gameBoard.GameField.First(element => element.X == j && element.Y == i);
            var shipFields = gameBoard.Ships.Select(ship => ship.ShipCoordinates).ToList();
            var isShipField = shipFields.Any(e => e.Contains(gameField)); 

            if (!gameField.IsAvailable && isShipField)
            {
                Console.Write("[o]");
                
            }
            else
            {
                Console.Write("[~]");

            }

        }
        Console.WriteLine();
    }
}

