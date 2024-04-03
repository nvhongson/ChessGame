using System;

public class Board
{
    private ChessPiece[,] board;

    public void Initialize()
    {
        board = new ChessPiece[8, 8];

        // Initialize the board with pieces
        // White pieces
        board[0, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.White);
        board[0, 1] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.White);
        board[0, 2] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.White);
        board[0, 3] = new ChessPiece(ChessPieceType.Queen, ChessPieceSide.White);
        board[0, 4] = new ChessPiece(ChessPieceType.King, ChessPieceSide.White);
        board[0, 5] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.White);
        board[0, 6] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.White);
        board[0, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.White);

        for (int col = 0; col < 8; col++)
        {
            board[1, col] = new ChessPiece(ChessPieceType.Pawn, ChessPieceSide.White);
        }

        // Black pieces
        board[7, 0] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.Black);
        board[7, 1] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.Black);
        board[7, 2] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.Black);
        board[7, 3] = new ChessPiece(ChessPieceType.Queen, ChessPieceSide.Black);
        board[7, 4] = new ChessPiece(ChessPieceType.King, ChessPieceSide.Black);
        board[7, 5] = new ChessPiece(ChessPieceType.Bishop, ChessPieceSide.Black);
        board[7, 6] = new ChessPiece(ChessPieceType.Knight, ChessPieceSide.Black);
        board[7, 7] = new ChessPiece(ChessPieceType.Rook, ChessPieceSide.Black);

        for (int col = 0; col < 8; col++)
        {
            board[6, col] = new ChessPiece(ChessPieceType.Pawn, ChessPieceSide.Black);
        }
    }

    public void Display()
    {
        Console.WriteLine("   a b c d e f g h");
        Console.WriteLine("  -----------------");

        // Print the side indicator
        Console.WriteLine("White Side");

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

        // Print the side indicator
        Console.WriteLine("Black Side");
        Console.WriteLine();
    }

    public ChessPiece[,] GetBoard()
    {
        return board;
    }

    public void MovePiece(int sourceRow, int sourceCol, int destRow, int destCol)
    {
        board[destRow, destCol] = board[sourceRow, sourceCol];
        board[sourceRow, sourceCol] = null;
    }
}
