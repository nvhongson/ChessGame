using System;

class Program
{
    static void Main()
    {
        // Controls whether a brand-new ChessGame object should be created
        // after the current game ends.
        bool playAgain = true;

        while (playAgain)
        {
            // ChessGame owns the board and turn state for one play session.
            ChessGame chessGame = new ChessGame();

            // Show the starting position before the first player enters a move.
            Console.WriteLine("Initial Board State:");
            chessGame.Display();

            // Main turn loop. At the moment IsGameOver always returns false,
            // so this loop ends only when input closes or game-over logic is added.
            while (!chessGame.IsGameOver())
            {
                Console.WriteLine(chessGame.IsWhiteTurn() ? "White's turn" : "Black's turn");
                Console.WriteLine("Enter your move (e.g., 'a2 b4'): ");

                // Read one move from the console. A null value means the input
                // stream was closed, which can happen during automated testing.
                string? move = Console.ReadLine();
                if (move == null)
                {
                    return;
                }

                // MakeMove handles parsing, validation, moving the piece, and
                // switching turns. The Program class only reports the result.
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

            // Ask whether to create another game once game-over logic exists.
            Console.WriteLine("Game Over!");
            Console.WriteLine("Do you want to play again? (yes/no)");
            string? response = Console.ReadLine();

            playAgain = response != null && (response.ToLower() == "yes" || response.ToLower() == "y");
        }
    }
}
