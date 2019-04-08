﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class BoardRules
{
    private byte[,] bitboard;
    enum Player { blank = 0, black = 1, white = 2 };

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

    public byte[,] Bitboard { get => bitboard; set => bitboard = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
                if (getValueX + i >= 0 && getValueX + i <= 7 && getValueY + j >= 0 && getValueY + j <= 7)
                {
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.white)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (i - getValueX * l >= 0 && i - getValueX * l <= 7 && j - getValueY * l >= 0 && j - getValueY * l <= 7)
                            {
                                if (bitboard[i - getValueX * l, j - getValueY * l] == 9 || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.white)
                                {
                                    l = 8;
                                }

                                else if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    bitboard[i - getValueX * l, j - getValueY * l] = 9;
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
                if (getValueX + i >= 0 && getValueX + i <= 7 && getValueY + j >= 0 && getValueY + j <= 7)
                {
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.black)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (i - getValueX * l >= 0 && i - getValueX * l <= 7 && j - getValueY * l >= 0 && j - getValueY * l <= 7)
                            {
                                if (bitboard[i - getValueX * l, j - getValueY * l] == 9 || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.black)
                                {
                                    l = 8;
                                }

                                else if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    bitboard[i - getValueX * l, j - getValueY * l] = 9;
                                    l = 8;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void captureEnemyPlayer(byte[,] bitboard, int i, int j, byte playerturn)
    {
        if (playerturn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];
                if (getValueX + i >= 0 && getValueX + i <= 7 && getValueY + j >= 0 && getValueY + j <= 7)
                {
                    for (int l = 1; l < 8; l++)
                    {
                        if (i + getValueX * l >= 0 && i + getValueX * l <= 7 && j + getValueY * l >= 0 && j + getValueY * l <= 7)
                        {
                            if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == 9 || bitboard[i + getValueX * l, j + getValueY * l] == 5)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.black)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    bitboard[i + getValueX * l, j + getValueY * l] = 5;
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
                if (getValueX + i >= 0 && getValueX + i <= 7 && getValueY + j >= 0 && getValueY + j <= 7)
                {
                    for (int l = 1; l < 8; l++)
                    {
                        if (i + getValueX * l >= 0 && i + getValueX * l <= 7 && j + getValueY * l >= 0 && j + getValueY * l <= 7)
                        {
                            if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == 9 || bitboard[i + getValueX * l, j + getValueY * l] == 5)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.white)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    bitboard[i + getValueX * l, j + getValueY * l] = 5;
                                }
                                l = 10;
                            }
                        }
                    }
                }
            }
        }
    }
}
