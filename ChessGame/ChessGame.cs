public class ChessGame
{
    private Board chessBoard;
    private bool isWhiteTurn;

    public ChessGame()
    {
        chessBoard = new Board();
        chessBoard.Initialize();
        isWhiteTurn = true; // White goes first
    }

    public void Display()
    {
        chessBoard.Display(); // Call Display method without passing isWhiteTurn
    }

    public bool IsGameOver()
    {
        // Add logic to check if the game is over
        return false;
    }

    public bool MakeMove(string move)
    {
        // Parse the input move string
        int sourceCol = move[0] - 'a';
        int sourceRow = 8 - (move[1] - '0');
        int destCol = move[3] - 'a';
        int destRow = 8 - (move[4] - '0');

        Console.WriteLine($"Parsed coordinates: Source ({sourceRow}, {sourceCol}), Destination ({destRow}, {destCol})");

        // Validate the move
        if (ValidateMove(sourceRow, sourceCol, destRow, destCol))
        {
            // Move the piece if it's a valid move
            chessBoard.MovePiece(sourceRow, sourceCol, destRow, destCol);

            // Switch turns after a valid move
            isWhiteTurn = !isWhiteTurn;

            Console.WriteLine("Move successful!");
            Console.WriteLine("Current Board State:");
            Display(); // Print the current board state

            return true;
        }

        // If the move is invalid, print an error message
        Console.WriteLine("Invalid move. Try again.");
        return false;
    }

    private bool ValidateMove(int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // Check if the source cell has a piece and if it's the correct player's turn
        if (chessBoard.GetBoard()[sourceRow, sourceCol] == null || (isWhiteTurn && chessBoard.GetBoard()[sourceRow, sourceCol].Side == ChessPieceSide.Black) ||
            (!isWhiteTurn && chessBoard.GetBoard()[sourceRow, sourceCol].Side == ChessPieceSide.White))
        {
            Console.WriteLine("Invalid move: No piece to move or it's not your turn.");
            return false;
        }

        // Create an instance of PieceLogic
        PieceLogic pieceLogic = new PieceLogic();

        // Validate the move using PieceLogic
        return pieceLogic.IsValidMove(chessBoard.GetBoard()[sourceRow, sourceCol].Type, chessBoard.GetBoard(), sourceRow, sourceCol, destRow, destCol);
    }

    public bool IsWhiteTurn()
    {
        return isWhiteTurn;
    }

    public bool IsBlackTurn()
    {
        return !isWhiteTurn;
    }
}
