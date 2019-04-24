using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class BoardRules
{
    private byte[,] bitboard;
    enum Player { blank = 0, black = 1, white = 2, capture = 5, validMove = 9};

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

    public byte[,] _minimaxBitboard { get => bitboard; set => bitboard = value; }

    public void ValidMove(byte[,] bitboard, byte playerturn)
    {
        if (playerturn == (int)Player.black)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == (int)Player.white)
                    {
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
                        CheckForAdjacent(bitboard, i, j, playerturn);
                    }
                }
            }
        }
    }

    void CheckForAdjacent(byte[,] bitboard, int i, int j, byte playerturn)
    {
        if (playerturn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                if (InRange(getValueX + i, getValueY + j))
                {
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.white)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.white)
                                {
                                    l = 8;
                                }

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

                if (InRange(getValueX + i, getValueY + j))
                {
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.black)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.black)
                                {
                                    l = 8;
                                }

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

    public void CaptureEnemyPlayer(byte[,] bitboard, int i, int j, byte playerturn)
    {
        if (playerturn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                if (InRange(getValueX + i, getValueY + j))
                {
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

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.black)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
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
                Vector2 vector = vectors[k];

                if (InRange(getValueX + i, getValueY + j))
                {
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

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.white)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
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

    bool InRange (int x, int y)
    {
        if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            return true;
        else
            return false;
    }

    public bool CheckForNine(byte[,] bitboard)
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

    byte [,] bitboardOnlyResetTurn(byte[,] bitboard, byte playerturn)
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

    public byte[,] GetNextBoardState(byte[,] bitboard, byte playerturn, int bitboardX, int bitboardY)
    {
        CaptureEnemyPlayer(bitboard, bitboardY, bitboardX, playerturn);
        bitboardOnlyResetTurn(bitboard, playerturn);
        ValidMove(bitboard, playerturn);
        return bitboard;
    }
}
