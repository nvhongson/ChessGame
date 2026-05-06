using System;

public class Board
{
    // The board is an 8x8 array. A square can be null when it is empty,
    // which is why the array stores nullable ChessPiece values.
    private ChessPiece?[,] board = new ChessPiece?[8, 8];

    public void Initialize()
    {
        // Start with a fresh empty board before placing pieces.
        board = new ChessPiece?[8, 8];

        // Black pieces start at ranks 8 and 7. In the array, rank 8 is row 0.
        board[0, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.Black);
        board[0, 1] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.Black);
        board[0, 2] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.Black);
        board[0, 3] = new ChessPiece(ChessPieceType.Queen, ChessPieceSide.Black);
        board[0, 4] = new ChessPiece(ChessPieceType.King, ChessPieceSide.Black);
        board[0, 5] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.Black);
        board[0, 6] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.Black);
        board[0, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.Black);

        // Fill the full black pawn row.
        for (int col = 0; col < 8; col++)
        {
            board[1, col] = new ChessPiece(ChessPieceType.Pawn, ChessPieceSide.Black);
        }

        // White pieces start at ranks 1 and 2. In the array, rank 1 is row 7.
        board[7, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.White);
        board[7, 1] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.White);
        board[7, 2] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.White);
        board[7, 3] = new ChessPiece(ChessPieceType.Queen, ChessPieceSide.White);
        board[7, 4] = new ChessPiece(ChessPieceType.King, ChessPieceSide.White);
        board[7, 5] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.White);
        board[7, 6] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.White);
        board[7, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.White);

        // Fill the full white pawn row.
        for (int col = 0; col < 8; col++)
        {
            board[6, col] = new ChessPiece(ChessPieceType.Pawn, ChessPieceSide.White);
        }
    }

    public void Display()
    {
        // Print column labels first so players can enter moves like "e2 e4".
        Console.WriteLine("   a b c d e f g h");
        Console.WriteLine("  -----------------");

        Console.WriteLine("Black Side");

        // Print each board row with its chess rank. Empty squares display as "-".
        for (int row = 0; row < 8; row++)
        {
            Console.Write(8 - row + "| ");
            for (int col = 0; col < 8; col++)
            {
                Console.Write(board[row, col]?.ToString() ?? "-");
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("White Side");
        Console.WriteLine();
    }

    public ChessPiece?[,] GetBoard()
    {
        // Return the board array so game logic can inspect and validate squares.
        return board;
    }

    public void MovePiece(int sourceRow, int sourceCol, int destRow, int destCol)
    {
        // Move the source piece into the destination square, then empty the old square.
        ChessPiece? movedPiece = board[sourceRow, sourceCol];
        board[destRow, destCol] = movedPiece;
        board[sourceRow, sourceCol] = null;

        // Track movement state for future rules such as castling or first pawn move.
        if (movedPiece != null)
        {
            movedPiece.HasMoved = true;
            movedPiece.FirstMove = false;
        }
    }
}
