using System;

class Program
{
    static void Main()
    {
        bool playAgain = true;

        while (playAgain)
        {
            ChessGame chessGame = new ChessGame();

            // Display the initial board
            Console.WriteLine("Initial Board State:");
            chessGame.Display();

            // Game loop
            while (!chessGame.IsGameOver())
            {
                Console.WriteLine(chessGame.IsWhiteTurn() ? "White's turn" : "Black's turn");
                Console.WriteLine("Enter your move (e.g., 'a2 b4'): ");
                string move = Console.ReadLine();

                if (chessGame.MakeMove(move))
                {
                    Console.WriteLine("Move successful!");
                    chessGame.Display();
                }
                else
                {
                    Console.WriteLine("Please enter again");
                }
            }

            // Game over
            Console.WriteLine("Game Over!");
            Console.WriteLine("Do you want to play again? (yes/no)");
            string response = Console.ReadLine().ToLower();

            playAgain = (response == "yes" || response == "y");
        }
    }
}