using System;

public class Turn
{
    // This helper class tracks the active player separately from ChessGame.
    // ChessGame currently handles turns itself, but this class is still useful
    // as an example of separating turn logic into its own object.
    private ChessPieceSide currentPlayerSide;

    public Turn()
    {
        // White always moves first in chess.
        currentPlayerSide = ChessPieceSide.White;
    }

    public bool IsWhiteTurn()
    {
        // Returns true when the active side is White.
        return currentPlayerSide == ChessPieceSide.White;
    }

    public bool IsBlackTurn()
    {
        // Returns true when the active side is Black.
        return currentPlayerSide == ChessPieceSide.Black;
    }

    public bool MakeMove(ChessPiece?[,] board, string move)
    {
        // Reject blank input before trying to parse coordinates.
        if (string.IsNullOrWhiteSpace(move))
        {
            Console.WriteLine("Invalid move format. Try again.");
            return false;
        }

        // Convert the player's text move into board array indexes.
        if (!ChessUtility.TryParseMove(move.ToLower(), out int sourceRow, out int sourceCol, out int destRow, out int destCol))
        {
            Console.WriteLine("Invalid move format. Try again.");
            return false;
        }

        ChessPiece? sourcePiece = board[sourceRow, sourceCol];

        // A move cannot start from an empty square.
        if (sourcePiece == null)
        {
            Console.WriteLine("No piece at the source square. Try again.");
            return false;
        }

        // The selected piece must belong to the player whose turn it is.
        if ((currentPlayerSide == ChessPieceSide.White && sourcePiece.Side == ChessPieceSide.Black) ||
            (currentPlayerSide == ChessPieceSide.Black && sourcePiece.Side == ChessPieceSide.White))
        {
            Console.WriteLine("It's not your turn. Try again.");
            return false;
        }

        // Ask PieceLogic whether the selected piece can legally move that way.
        PieceLogic pieceLogic = new PieceLogic();
        if (!pieceLogic.IsValidMove(sourcePiece.Type, board, sourceRow, sourceCol, destRow, destCol))
        {
            Console.WriteLine("Invalid move for the selected piece. Try again.");
            return false;
        }

        // Move the piece to the destination square.
        board[destRow, destCol] = sourcePiece;
        board[sourceRow, sourceCol] = null;

        // Switch to the other side after a successful move.
        currentPlayerSide = (currentPlayerSide == ChessPieceSide.White) ? ChessPieceSide.Black : ChessPieceSide.White;

        return true;
    }
}
