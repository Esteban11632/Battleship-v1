using System;

namespace Battleshipv1
{
    class Program
    {
        const int gridSize = 10;
        char[,] playerGrid = new char[gridSize, gridSize];
        char[,] computerGrid = new char[gridSize, gridSize];
        char[,] playerDisplayGrid = new char[gridSize, gridSize];
        char[,] computerDisplayGrid = new char[gridSize, gridSize];
        Random random = new Random();

        static void Main(string[] args)
        {
            Program game = new Program();
            game.InitializeGrids();
            game.PlaceComputerShips();
            game.PlacePlayerShips();
            game.PlayGame();
        }

        void InitializeGrids()
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    playerGrid[row, col] = ' ';
                    computerGrid[row, col] = ' ';
                    playerDisplayGrid[row, col] = ' ';
                    computerDisplayGrid[row, col] = ' ';
                }
            }
        }

        void PlaceComputerShips()
        {
            // Simple random placement of ships (for demonstration purposes)
            PlaceShip(computerGrid, 5); // Carrier
            PlaceShip(computerGrid, 4); // Battleship
            PlaceShip(computerGrid, 3); // Destroyer
            PlaceShip(computerGrid, 3); // Submarine
            PlaceShip(computerGrid, 2); // Patrol Boat
        }

        void PlacePlayerShips()
        {
            // For simplicity, player ships are also placed randomly in this example.
            Console.WriteLine("Placing player ships...");
            PlaceShip(playerGrid, 5); // Carrier
            PlaceShip(playerGrid, 4); // Battleship
            PlaceShip(playerGrid, 3); // Destroyer
            PlaceShip(playerGrid, 3); // Submarine
            PlaceShip(playerGrid, 2); // Patrol Boat
        }

        void PlaceShip(char[,] grid, int length)
        {
            bool placed = false;

            while (!placed)
            {
                int row = random.Next(0, gridSize);
                int col = random.Next(0, gridSize);
                bool horizontal = random.Next(0, 2) == 0;

                if (CanPlaceShip(grid, row, col, length, horizontal))
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (horizontal)
                        {
                            grid[row, col + i] = 'S';
                        }
                        else
                        {
                            grid[row + i, col] = 'S';
                        }
                    }
                    placed = true;
                }
            }
        }

        bool CanPlaceShip(char[,] grid, int row, int col, int length, bool horizontal)
        {
            if (horizontal)
            {
                if (col + length > gridSize) return false;
                for (int i = 0; i < length; i++)
                {
                    if (grid[row, col + i] != ' ') return false;
                }
            }
            else
            {
                if (row + length > gridSize) return false;
                for (int i = 0; i < length; i++)
                {
                    if (grid[row + i, col] != ' ') return false;
                }
            }
            return true;
        }

        void PlayGame()
        {
            bool playerTurn = true;
            while (true)
            {
                Console.Clear();
                DisplayGrids();

                if (playerTurn)
                {
                    PlayerTurn();
                    if (IsGameOver(computerGrid))
                    {
                        Console.WriteLine("Congratulations! You sank all the computer's ships!");
                        break;
                    }
                }
                else
                {
                    ComputerTurn();
                    if (IsGameOver(playerGrid))
                    {
                        Console.WriteLine("The computer sank all your ships! You lose.");
                        break;
                    }
                }

                playerTurn = !playerTurn;
            }
        }

        void DisplayGrids()
        {
            Console.WriteLine("Player's Grid:                 Computer's Grid:");
            Console.WriteLine("  1  2  3  4  5  6  7  8  9  10    1  2  3  4  5  6  7  8  9  10");
            char[] rowLabels = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

            for (int row = 0; row < gridSize; row++)
            {
                Console.Write(rowLabels[row] + "|");
                for (int col = 0; col < gridSize; col++)
                {
                    Console.Write(playerDisplayGrid[row, col] + " |");
                }

                Console.Write("    " + rowLabels[row] + "|");

                for (int col = 0; col < gridSize; col++)
                {
                    Console.Write(computerDisplayGrid[row, col] + " |");
                }
                Console.WriteLine();
            }
        }

        void PlayerTurn()
        {
            Console.WriteLine("Your turn! Enter coordinates to shoot (e.g., A5):");
            string input = Console.ReadLine().ToUpper();
            int row = input[0] - 'A';
            int col = int.Parse(input.Substring(1)) - 1;

            if (computerGrid[row, col] == 'S')
            {
                Console.WriteLine("Hit!");
                computerGrid[row, col] = 'H';
                computerDisplayGrid[row, col] = 'H';
            }
            else if (computerGrid[row, col] == ' ')
            {
                Console.WriteLine("Miss!");
                computerGrid[row, col] = 'M';
                computerDisplayGrid[row, col] = 'M';
            }
            else
            {
                Console.WriteLine("You already shot there!");
            }
        }

        void ComputerTurn()
        {
            Console.WriteLine("Computer's turn...");
            int row, col;
            do
            {
                row = random.Next(0, gridSize);
                col = random.Next(0, gridSize);
            } while (playerGrid[row, col] == 'H' || playerGrid[row, col] == 'M');

            if (playerGrid[row, col] == 'S')
            {
                Console.WriteLine("Computer hit your ship!");
                playerGrid[row, col] = 'H';
                playerDisplayGrid[row, col] = 'H';
            }
            else if (playerGrid[row, col] == ' ')
            {
                Console.WriteLine("Computer missed!");
                playerGrid[row, col] = 'M';
                playerDisplayGrid[row, col] = 'M';
            }
        }

        bool IsGameOver(char[,] grid)
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (grid[row, col] == 'S') return false;
                }
            }
            return true;
        }
    }
}

