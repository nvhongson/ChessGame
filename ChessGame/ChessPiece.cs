public enum ChessPieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public enum ChessPieceSide
{
    White,
    Black
}

public class ChessPiece
{
    public ChessPieceType Type { get; set; }
    public ChessPieceSide Side { get; set; }
    public bool HasMoved { get; set; } 
    public bool FirstMove { get; set; }
    public ChessPiece(ChessPieceType type, ChessPieceSide side)
    {
        Type = type;
        Side = side;
        HasMoved = false;
        FirstMove = true; 
    }
    public virtual bool IsValidMove(ChessPiece[,] board, int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // Implement logic to validate move for each piece type
        return true;
    }
    public override string ToString()
    {
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
