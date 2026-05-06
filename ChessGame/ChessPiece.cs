// Lists every type of chess piece the game can place on the board.
public enum ChessPieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

// Identifies which player owns a piece.
public enum ChessPieceSide
{
    White,
    Black
}

public class ChessPiece
{
    // Type controls movement rules and display symbol.
    public ChessPieceType Type { get; set; }

    // Side controls turn ownership and whether another piece can be captured.
    public ChessPieceSide Side { get; set; }

    // Movement flags are useful for special rules such as castling or first
    // pawn movement. The current game updates them when a piece moves.
    public bool HasMoved { get; set; }
    public bool FirstMove { get; set; }

    public ChessPiece(ChessPieceType type, ChessPieceSide side)
    {
        // Store the piece identity when the board creates each piece.
        Type = type;
        Side = side;
        HasMoved = false;
        FirstMove = true;
    }

    public virtual bool IsValidMove(ChessPiece?[,] board, int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // Delegate movement validation to PieceLogic so this class stays focused
        // on representing a piece's data.
        PieceLogic pieceLogic = new PieceLogic();
        return pieceLogic.IsValidMove(Type, board, sourceRow, sourceCol, destRow, destCol);
    }

    public override string ToString()
    {
        // Uppercase symbols are white pieces; lowercase symbols are black pieces.
        switch (Type)
        {
            case ChessPieceType.Pawn: return Side == ChessPieceSide.White ? "P" : "p";
            case ChessPieceType.Rook: return Side == ChessPieceSide.White ? "R" : "r";
            case ChessPieceType.Knight: return Side == ChessPieceSide.White ? "N" : "n";
            case ChessPieceType.Bishop: return Side == ChessPieceSide.White ? "B" : "b";
            case ChessPieceType.Queen: return Side == ChessPieceSide.White ? "Q" : "q";
            case ChessPieceType.King: return Side == ChessPieceSide.White ? "K" : "k";
            default: return " ";
        }
    }
}
