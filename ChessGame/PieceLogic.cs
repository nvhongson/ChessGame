public class PieceLogic
{
    public bool IsValidMove(ChessPieceType pieceType, ChessPiece?[,] board, int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // First guard against coordinates outside the 8x8 board.
        if (!IsInsideBoard(sourceRow, sourceCol) || !IsInsideBoard(destRow, destCol))
        {
            return false;
        }

        // A move must actually change squares.
        if (sourceRow == destRow && sourceCol == destCol)
        {
            return false;
        }

        // Load the source and destination pieces once so every piece rule can
        // use the same values.
        ChessPiece? sourcePiece = board[sourceRow, sourceCol];
        ChessPiece? destinationPiece = board[destRow, destCol];

        // There must be a source piece, and it cannot capture a friendly piece.
        if (sourcePiece == null || (destinationPiece != null && destinationPiece.Side == sourcePiece.Side))
        {
            return false;
        }

        // Row/column differences describe the shape of the move.
        int rowDifference = destRow - sourceRow;
        int colDifference = destCol - sourceCol;
        int absoluteRowDifference = Math.Abs(rowDifference);
        int absoluteColDifference = Math.Abs(colDifference);

        // Dispatch to the movement rule for the selected piece type.
        switch (pieceType)
        {
            case ChessPieceType.Pawn:
                return IsValidPawnMove(board, sourcePiece, sourceRow, sourceCol, destRow, destCol);

            case ChessPieceType.Rook:
                // Rooks move horizontally or vertically, and cannot jump pieces.
                return (sourceRow == destRow || sourceCol == destCol) &&
                    IsPathClear(board, sourceRow, sourceCol, destRow, destCol);

            case ChessPieceType.Knight:
                // Knights move in an L shape and are the only piece here that can jump.
                return (absoluteRowDifference == 2 && absoluteColDifference == 1) ||
                    (absoluteRowDifference == 1 && absoluteColDifference == 2);

            case ChessPieceType.Bishop:
                // Bishops move diagonally, and cannot jump pieces.
                return absoluteRowDifference == absoluteColDifference &&
                    IsPathClear(board, sourceRow, sourceCol, destRow, destCol);

            case ChessPieceType.Queen:
                // Queens combine rook and bishop movement.
                bool movesStraight = sourceRow == destRow || sourceCol == destCol;
                bool movesDiagonally = absoluteRowDifference == absoluteColDifference;
                return (movesStraight || movesDiagonally) &&
                    IsPathClear(board, sourceRow, sourceCol, destRow, destCol);

            case ChessPieceType.King:
                // Kings move one square in any direction. Check rules are not added yet.
                return absoluteRowDifference <= 1 && absoluteColDifference <= 1;

            default:
                return false;
        }
    }

    private bool IsValidPawnMove(ChessPiece?[,] board, ChessPiece pawn, int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // White pawns move toward smaller row numbers; black pawns move toward larger rows.
        int direction = pawn.Side == ChessPieceSide.White ? -1 : 1;
        int startingRow = pawn.Side == ChessPieceSide.White ? 6 : 1;
        int rowDifference = destRow - sourceRow;
        int colDifference = destCol - sourceCol;
        ChessPiece? destinationPiece = board[destRow, destCol];

        // One-square forward pawn move into an empty square.
        if (colDifference == 0 && rowDifference == direction && destinationPiece == null)
        {
            return true;
        }

        // Two-square forward pawn move from the starting row, only if both
        // the skipped square and destination square are empty.
        if (colDifference == 0 && sourceRow == startingRow && rowDifference == 2 * direction &&
            destinationPiece == null && board[sourceRow + direction, sourceCol] == null)
        {
            return true;
        }

        // Diagonal pawn move is valid only when capturing an opposing piece.
        return Math.Abs(colDifference) == 1 && rowDifference == direction &&
            destinationPiece != null && destinationPiece.Side != pawn.Side;
    }

    private bool IsPathClear(ChessPiece?[,] board, int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // Work out the one-square step needed to walk from source to destination.
        int rowStep = Math.Sign(destRow - sourceRow);
        int colStep = Math.Sign(destCol - sourceCol);
        int currentRow = sourceRow + rowStep;
        int currentCol = sourceCol + colStep;

        // Check every square between source and destination. The destination is
        // not checked here because capturing is handled by the caller.
        while (currentRow != destRow || currentCol != destCol)
        {
            if (board[currentRow, currentCol] != null)
            {
                return false;
            }

            currentRow += rowStep;
            currentCol += colStep;
        }

        return true;
    }

    private bool IsInsideBoard(int row, int col)
    {
        // Board arrays are zero-based, so valid indexes are 0 through 7.
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }
}
