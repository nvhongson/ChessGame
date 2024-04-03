using System;

public static class ChessUtility
{
    public static bool TryParseMove(string move, out int sourceRow, out int sourceCol, out int destRow, out int destCol)
    {
        sourceRow = -1;
        sourceCol = -1;
        destRow = -1;
        destCol = -1;

        // Ensure the move string is in the correct format
        if (move.Length != 5)
            return false;

        // Convert the source column from character to integer index (0-based)
        switch (move[0])
        {
            case 'a': sourceCol = 0; break;
            case 'b': sourceCol = 1; break;
            case 'c': sourceCol = 2; break;
            case 'd': sourceCol = 3; break;
            case 'e': sourceCol = 4; break;
            case 'f': sourceCol = 5; break;
            case 'g': sourceCol = 6; break;
            case 'h': sourceCol = 7; break;
            default: return false; // Invalid column
        }

        // Convert the source row from character to integer index (0-based)
        if (!int.TryParse(move[1].ToString(), out sourceRow))
            return false;

        // Convert the destination column from character to integer index (0-based)
        switch (move[3])
        {
            case 'a': destCol = 0; break;
            case 'b': destCol = 1; break;
            case 'c': destCol = 2; break;
            case 'd': destCol = 3; break;
            case 'e': destCol = 4; break;
            case 'f': destCol = 5; break;
            case 'g': destCol = 6; break;
            case 'h': destCol = 7; break;
            default: return false; // Invalid column
        }

        // Convert the destination row from character to integer index (0-based)
        if (!int.TryParse(move[4].ToString(), out destRow))
            return false;

        // Adjust row and column indices to be zero-based
        sourceRow--;
        destRow--;

        return true;
    }
}
