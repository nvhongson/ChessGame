using System;

public class Turn
{
    private ChessPieceSide currentPlayerSide;

    public Turn()
    {
        // Default to white's turn at the start of the game
        currentPlayerSide = ChessPieceSide.White;
    }

    public bool IsWhiteTurn()
    {
        return currentPlayerSide == ChessPieceSide.White;
    }

    public bool IsBlackTurn()
    {
        return currentPlayerSide == ChessPieceSide.Black;
    }

    public bool MakeMove(ChessPiece[,] board, string move)
    {
        // Convert the move string to source and destination coordinates
        if (!ChessUtility.TryParseMove(move, out int sourceRow, out int sourceCol, out int destRow, out int destCol))
        {
            Console.WriteLine("Invalid move format. Try again.");
            return false;
        }

        ChessPiece sourcePiece = board[sourceRow, sourceCol];

        // Check if the source position has a valid piece
        if (sourcePiece == null)
        {
            Console.WriteLine("No piece at the source square. Try again.");
            return false;
        }

        // Check if it's the correct player's turn
        if ((currentPlayerSide == ChessPieceSide.White && sourcePiece.Side == ChessPieceSide.Black) ||
            (currentPlayerSide == ChessPieceSide.Black && sourcePiece.Side == ChessPieceSide.White))
        {
            Console.WriteLine("It's not your turn. Try again.");
            return false;
        }

        // Check if the move is valid for the selected piece
        if (!sourcePiece.IsValidMove(board, sourceRow, sourceCol, destRow, destCol))
        {
            Console.WriteLine("Invalid move for the selected piece. Try again.");
            return false;
        }

        // Move the piece to the destination
        board[destRow, destCol] = sourcePiece;
        board[sourceRow, sourceCol] = null;

        // Switch turns after a valid move
        currentPlayerSide = (currentPlayerSide == ChessPieceSide.White) ? ChessPieceSide.Black : ChessPieceSide.White;

        return true;
    }
}
