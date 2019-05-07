using System.Numerics;

public class BoardRules
{
    enum Player { blank = 0, black = 1, white = 2, capture = 5, validMove = 9};

    // List of vectors for use later on
    Vector2[] vectors = new Vector2[]
        {
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(-1, 0),
        new Vector2(0, -1),
        new Vector2(1, 1),
        new Vector2(-1, -1),
        new Vector2(-1, 1),
        new Vector2(1, -1)
        };

    // This method is used to check for opposite pieces
    public void ValidMove(int[,] bitboard, int playerturn)
    {
        if (playerturn == (int)Player.black)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == (int)Player.white)
                    {
                        // If a opposite piece is found run CheckForAdjacent()
                        CheckForAdjacent(bitboard, i, j, playerturn);
                    }
                }
            }
        }

        else if (playerturn == (int)Player.white)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == (int)Player.black)
                    {
                        // If a opposite piece is found run CheckForAdjacent()
                        CheckForAdjacent(bitboard, i, j, playerturn);
                    }
                }
            }
        }
    }

    // This method is used to check if there are any tiles that follow the rule of capturing
    void CheckForAdjacent(int[,] bitboard, int i, int j, int playerturn)
    {
        if (playerturn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                // Check if the next tile is inside the bitboard
                if (InRange(getValueX + i, getValueY + j))
                {
                    // Follow vector. If white piece is found follow this vector
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.white)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                // If this tile is already a valid move or white, stop following this vector
                                if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.white)
                                {
                                    l = 8;
                                }
                                
                                // If this tile is blank, mark as valid move
                                else if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    bitboard[i - getValueX * l, j - getValueY * l] = (int)Player.validMove;
                                    l = 8;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (playerturn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                // Check if the next tile is inside the bitboard
                if (InRange(getValueX + i, getValueY + j))
                {
                    // Follow vector. If black piece is found follow this vector
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.black)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                // If this tile is already a valid move or black, stop following this vector
                                if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.black)
                                {
                                    l = 8;
                                }

                                // If this tile is blank, mark as valid move
                                else if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    bitboard[i - getValueX * l, j - getValueY * l] = (int)Player.validMove;
                                    l = 8;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // This method is used to mark bitboard tile as a captured piece
    public void CaptureEnemyPlayer(int[,] bitboard, int i, int j, int playerturn)
    {
        if (playerturn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;

                // Check if the next tile is inside the bitboard
                if (InRange(getValueX + i, getValueY + j))
                {
                    for (int l = 1; l < 8; l++)
                    {
                        // Follow vector
                        if (InRange(i + getValueX * l, j + getValueY * l))
                        {
                            if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.validMove || bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.capture)
                            {
                                l = 10;
                            }

                            // Only if a black piece is found, backtace the vector
                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.black)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    // Mark all pieces in the backtracing vectors way, as captured pieces
                                    bitboard[i + getValueX * l, j + getValueY * l] = (int)Player.capture;
                                }
                                l = 10;
                            }
                        }
                    }
                }
            }
        }

        if (playerturn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;

                // Check if the next tile is inside the bitboard
                if (InRange(getValueX + i, getValueY + j))
                {
                    // Follow vector
                    for (int l = 1; l < 8; l++)
                    {
                        if (InRange(i + getValueX * l, j + getValueY * l))
                        {
                            if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.validMove || bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.capture)
                            {
                                l = 10;
                            }

                            // Only if a white piece is found, backtace the vector
                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.white)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    // Mark all pieces in the backtracing vectors way, as captured pieces
                                    bitboard[i + getValueX * l, j + getValueY * l] = (int)Player.capture;
                                }
                                l = 10;
                            }
                        }
                    }
                }
            }
        }
    }

    // This method is used to check if x and y are in a specific range
    public bool InRange (int x, int y)
    {
        if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            return true;
        else
            return false;
    }

    // This method is used to check if there are any valid moves in the bitboard 
    public bool CheckForNine(int[,] bitboard)
    {
        bool returnValue = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bitboard[i, j] == 9)
                {
                    returnValue = false;
                }
            }
        }
        return returnValue;
    }

    // This method is used to prepare the bitboard for MiniMax 
    int[,] bitboardOnlyResetTurn(int[,] bitboard, int playerturn)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // If theres an available move, change the texture of the current tile
                if (bitboard[i, j] == 9)
                {
                    bitboard[i, j] = 0;
                }

                // If theres a captured piece, change the piece
                else if (bitboard[i, j] == 5)
                {
                    // Change the bitboard value to the current player
                    bitboard[i, j] = playerturn;
                }
            }
        }
        return bitboard;
    }

    // This method is used to get the next board state for MiniMax
    public int[,] GetNextBoardState(int[,] bitboard, int playerturn, int bitboardX, int bitboardY)
    {
        // Clone bitboard
        int[,] bitboardCopy = (int[,])bitboard.Clone();
        // Make bitboard move
        bitboardCopy[bitboardX, bitboardY] = playerturn;
        // Capture pieces
        CaptureEnemyPlayer(bitboardCopy, bitboardY, bitboardX, playerturn);
        // Reset bitboard
        bitboardCopy = bitboardOnlyResetTurn(bitboardCopy, playerturn);
        // Calculate valid moves
        ValidMove(bitboardCopy, playerturn);
        // return bitboard
        return bitboardCopy;
    }
}
