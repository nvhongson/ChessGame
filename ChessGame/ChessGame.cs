public class ChessGame
{
    // ChessGame acts as the controller for one match. It keeps the board object
    // and tracks which player is allowed to move next.
    private Board chessBoard;
    private bool isWhiteTurn;

    public ChessGame()
    {
        // A new game always starts from the standard chess setup with White first.
        chessBoard = new Board();
        chessBoard.Initialize();
        isWhiteTurn = true;
    }

    public void Display()
    {
        // ChessGame delegates board drawing to Board so display code stays in
        // the class that owns the squares.
        chessBoard.Display();
    }

    public bool IsGameOver()
    {
        // Future game-over rules can be added here, such as checkmate,
        // stalemate, resignation, or king capture.
        return false;
    }

    public bool MakeMove(string move)
    {
        // Reject blank input before parsing so invalid console input cannot
        // cause string indexing or coordinate errors.
        if (string.IsNullOrWhiteSpace(move))
        {
            Console.WriteLine("Invalid move format. Use a move like 'e2 e4'.");
            return false;
        }

        // Convert input like "e2 e4" into zero-based board array positions.
        if (!ChessUtility.TryParseMove(move.ToLower(), out int sourceRow, out int sourceCol, out int destRow, out int destCol))
        {
            Console.WriteLine("Invalid move format. Use a move like 'e2 e4'.");
            return false;
        }

        Console.WriteLine($"Parsed coordinates: Source ({sourceRow}, {sourceCol}), Destination ({destRow}, {destCol})");

        // Only update the board if the move passes turn, capture, and piece rules.
        if (ValidateMove(sourceRow, sourceCol, destRow, destCol))
        {
            chessBoard.MovePiece(sourceRow, sourceCol, destRow, destCol);

            // Switch turns only after a successful move.
            isWhiteTurn = !isWhiteTurn;

            return true;
        }

        // Invalid moves leave the board unchanged and the same player retries.
        Console.WriteLine("Invalid move. Try again.");
        return false;
    }

    private bool ValidateMove(int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // Pull out the board and the two involved squares to make the checks
        // easier to read.
        ChessPiece?[,] board = chessBoard.GetBoard();
        ChessPiece? sourcePiece = board[sourceRow, sourceCol];
        ChessPiece? destinationPiece = board[destRow, destCol];

        // The source square must contain a piece belonging to the current player.
        if (sourcePiece == null || (isWhiteTurn && sourcePiece.Side == ChessPieceSide.Black) ||
            (!isWhiteTurn && sourcePiece.Side == ChessPieceSide.White))
        {
            Console.WriteLine("Invalid move: No piece to move or it's not your turn.");
            return false;
        }

        // A player cannot move onto a square already occupied by their own piece.
        if (destinationPiece != null && destinationPiece.Side == sourcePiece.Side)
        {
            Console.WriteLine("Invalid move: You cannot capture your own piece.");
            return false;
        }

        // PieceLogic contains the movement rules for each piece type.
        PieceLogic pieceLogic = new PieceLogic();

        return pieceLogic.IsValidMove(sourcePiece.Type, board, sourceRow, sourceCol, destRow, destCol);
    }

    public bool IsWhiteTurn()
    {
        // Used by Program to print whose turn it is.
        return isWhiteTurn;
    }

    public bool IsBlackTurn()
    {
        // Convenience method for code that wants to ask from Black's perspective.
        return !isWhiteTurn;
    }
}
