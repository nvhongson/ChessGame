public class PieceLogic
{
    public bool IsValidMove(ChessPieceType pieceType, ChessPiece[,] board, int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // Check if the destination cell is within the board bounds
        if (destRow < 0 || destRow >= 8 || destCol < 0 || destCol >= 8)
        {
            return false;
        }

        // Always return true to allow any move
        return true;
    }
}
