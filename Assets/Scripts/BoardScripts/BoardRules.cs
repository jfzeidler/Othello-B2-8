using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelloWorld
{
    public class BoardRules : Bitboard
    {
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
         //   ValidMove();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ValidMove()
        {
            int[,] Tempbitboard = bitboard;

            if (playerturn == (int)Player.black)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Tempbitboard[i, j] == (int)Player.white && CheckForAdjacent(Tempbitboard, i, j, playerturn))
                        {

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
                        if (Tempbitboard[i, j] == (int)Player.black && CheckForAdjacent(Tempbitboard, i, j, playerturn))
                        {

                        }
                    }
                }
            }


        }

        public void captureRule(int Tilex, int Tiley)
        {

        }

        bool CheckForAdjacent(int[,] Tempbitboard, int i, int j, int playerturn)
        {

            if (playerturn == (int)Player.white)
            {
                for (int k = 0; k < 8; k++)
                {
                    int getValueX = (int)vectors[k].x;
                    int getValueY = (int)vectors[k].y;
                    Vector2 vector = vectors[k];
                    if (Tempbitboard[i + getValueX, j + getValueY] == (int)Player.black)
                    {
                        //smid over i liste
                        Debug.Log("TRUE");
                    }
                    else
                        Debug.Log("FALSE");
                }

            }
            return false;
        }
    }
}
