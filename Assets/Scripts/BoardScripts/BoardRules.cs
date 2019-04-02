using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRules
{
    public Bitboard boardState = new Bitboard();

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

    // Start is called before the first frame update
    void Start()
    {
        ValidMove();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValidMove()
    {
        if (boardState.playerturn == (int)Player.black)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardState.bitboard[i, j] == (int)Player.white && CheckForAdjacent(boardState.bitboard, i, j, boardState.playerturn))
                    {

                    }
                }
            }
        }

        else if (boardState.playerturn == (int)Player.white)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (boardState.bitboard[i, j] == (int)Player.black && CheckForAdjacent(boardState.bitboard, i, j, boardState.playerturn))
                    {

                    }
                }
            }
        }
    }

    bool CheckForAdjacent(int[,] bitboard, int i, int j, int playerturn)
    {
        if (playerturn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].x;
                int getValueY = (int)vectors[k].y;
                Vector2 vector = vectors[k];
                if (bitboard[i + getValueX, j + getValueY] == (int)Player.black)
                {
                    //smid over i liste
                    Debug.Log("TRUE");
                }
                else
                    Debug.Log("FALSE");
            }
        }
        return true;
    }
}
