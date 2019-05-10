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
    public void ValidMove(int[,] board2D, int playerTurn)
    {
        if (playerTurn == (int)Player.black)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board2D[i, j] == (int)Player.white)
                    {
                        // If a opposite piece is found run CheckForAdjacent()
                        CheckForAdjacent(board2D, i, j, playerTurn);
                    }
                }
            }
        }

        else if (playerTurn == (int)Player.white)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board2D[i, j] == (int)Player.black)
                    {
                        // If a opposite piece is found run CheckForAdjacent()
                        CheckForAdjacent(board2D, i, j, playerTurn);
                    }
                }
            }
        }
    }

    // This method is used to check if there are any tiles that follow the rule of capturing
    void CheckForAdjacent(int[,] board2D, int i, int j, int playerTurn)
    {
        if (playerTurn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                // Check if the next tile is inside the board
                if (InRange(getValueX + i, getValueY + j))
                {
                    // Follow vector. If white piece is found follow this vector
                    if (board2D[i + getValueX, j + getValueY] == (int)Player.white)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                // If this tile is already a valid move or white, stop following this vector
                                if (board2D[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || board2D[i - getValueX * l, j - getValueY * l] == (int)Player.white)
                                {
                                    l = 8;
                                }
                                
                                // If this tile is blank, mark as valid move
                                else if (board2D[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    board2D[i - getValueX * l, j - getValueY * l] = (int)Player.validMove;
                                    l = 8;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (playerTurn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                // Check if the next tile is inside the board
                if (InRange(getValueX + i, getValueY + j))
                {
                    // Follow vector. If black piece is found follow this vector
                    if (board2D[i + getValueX, j + getValueY] == (int)Player.black)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                // If this tile is already a valid move or black, stop following this vector
                                if (board2D[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || board2D[i - getValueX * l, j - getValueY * l] == (int)Player.black)
                                {
                                    l = 8;
                                }

                                // If this tile is blank, mark as valid move
                                else if (board2D[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    board2D[i - getValueX * l, j - getValueY * l] = (int)Player.validMove;
                                    l = 8;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // This method is used to mark board tile as a captured piece
    public void CaptureEnemyPlayer(int[,] board2D, int i, int j, int playerTurn)
    {
        if (playerTurn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;

                // Check if the next tile is inside the board
                if (InRange(getValueX + i, getValueY + j))
                {
                    for (int l = 1; l < 8; l++)
                    {
                        // Follow vector
                        if (InRange(i + getValueX * l, j + getValueY * l))
                        {
                            if (board2D[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (board2D[i + getValueX * l, j + getValueY * l] == (int)Player.validMove || board2D[i + getValueX * l, j + getValueY * l] == (int)Player.capture)
                            {
                                l = 10;
                            }

                            // Only if a black piece is found, backtace the vector
                            else if (board2D[i + getValueX * l, j + getValueY * l] == (int)Player.black)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    // Mark all pieces in the backtracing vectors way, as captured pieces
                                    board2D[i + getValueX * l, j + getValueY * l] = (int)Player.capture;
                                }
                                l = 10;
                            }
                        }
                    }
                }
            }
        }

        if (playerTurn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;

                // Check if the next tile is inside the board
                if (InRange(getValueX + i, getValueY + j))
                {
                    // Follow vector
                    for (int l = 1; l < 8; l++)
                    {
                        if (InRange(i + getValueX * l, j + getValueY * l))
                        {
                            if (board2D[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (board2D[i + getValueX * l, j + getValueY * l] == (int)Player.validMove || board2D[i + getValueX * l, j + getValueY * l] == (int)Player.capture)
                            {
                                l = 10;
                            }

                            // Only if a white piece is found, backtace the vector
                            else if (board2D[i + getValueX * l, j + getValueY * l] == (int)Player.white)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    // Mark all pieces in the backtracing vectors way, as captured pieces
                                    board2D[i + getValueX * l, j + getValueY * l] = (int)Player.capture;
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

    // This method is used to check if there are any valid moves in the board 
    public bool CheckForNine(int[,] board2D)
    {
        bool returnValue = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board2D[i, j] == 9)
                {
                    returnValue = false;
                }
            }
        }
        return returnValue;
    }

    // This method is used to prepare the board for MiniMax 
    int[,] boardOnlyResetTurn(int[,] board2D, int playerTurn)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // If theres an available move, change the texture of the current tile
                if (board2D[i, j] == 9)
                {
                    board2D[i, j] = 0;
                }

                // If theres a captured piece, change the piece
                else if (board2D[i, j] == 5)
                {
                    // Change the board value to the current player
                    board2D[i, j] = playerTurn;
                }
            }
        }
        return board2D;
    }

    // This method is used to get the next board state for MiniMax
    public int[,] GetNextBoardState(int[,] board2D, int playerTurn, int board2DX, int board2DY)
    {
        // Clone board
        int[,] boardCopy = (int[,])board2D.Clone();
        // Make board move
        boardCopy[board2DX, board2DY] = playerTurn;
        // Capture pieces
        CaptureEnemyPlayer(boardCopy, board2DY, board2DX, playerTurn);
        // Reset board
        boardCopy = boardOnlyResetTurn(boardCopy, playerTurn);
        // Calculate valid moves
        ValidMove(boardCopy, playerTurn);
        // return board
        return boardCopy;
    }
}
