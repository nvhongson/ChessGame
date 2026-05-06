using System;

public static class ChessUtility
{
    public static bool TryParseMove(string move, out int sourceRow, out int sourceCol, out int destRow, out int destCol)
    {
        // Default all output values to invalid indexes. If parsing fails, the
        // caller can safely ignore these values.
        sourceRow = -1;
        sourceCol = -1;
        destRow = -1;
        destCol = -1;

        // Remove extra spaces at the start or end, while still requiring the
        // actual move format to be "file-rank space file-rank".
        move = move.Trim();

        // Expected format is exactly five characters, such as "e2 e4".
        if (move.Length != 5 || move[2] != ' ')
        {
            return false;
        }

        // Convert the source file letter into a zero-based board column.
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
            default: return false;
        }

        // Convert the source rank character into a number before changing it
        // into a board row.
        if (!int.TryParse(move[1].ToString(), out sourceRow))
        {
            return false;
        }

        // Convert the destination file letter into a zero-based board column.
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
            default: return false;
        }

        // Convert the destination rank character into a number before changing
        // it into a board row.
        if (!int.TryParse(move[4].ToString(), out destRow))
        {
            return false;
        }

        // Chess ranks only run from 1 through 8.
        if (sourceRow < 1 || sourceRow > 8 || destRow < 1 || destRow > 8)
        {
            return false;
        }

        // Convert chess ranks to board rows. Rank 8 is row 0, rank 1 is row 7.
        sourceRow = 8 - sourceRow;
        destRow = 8 - destRow;

        return true;
    }
}
