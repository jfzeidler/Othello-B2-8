using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRules : MonoBehaviour
{
    public Vector2 up = new Vector2(0, 1);
    public Vector2 right = new Vector2(1, 0);
    public Vector2 left = new Vector2(-1, 0);
    public Vector2 down = new Vector2(0, -1);
    public Vector2 upright = new Vector2(1, 1);
    public Vector2 downleft = new Vector2(-1, -1);
    public Vector2 upleft = new Vector2(-1, 1);
    public Vector2 downright = new Vector2(1, -1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ValidMove()
    {
        int[,] Tempbitboard = GameObject.Find("The-Board").GetComponent<Bitboard>().bitboard;
        int playerturn = GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn;

        if (playerturn == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                }
            }
        }
        else if (playerturn == 2)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Tempbitboard[i,j] == 2 && CheckForAdjacent(Tempbitboard, i, j, playerturn))
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
        int upHDir = (int)up.x; int upVDir = (int)up.y;
        int downHDir = (int)down.x; int downVDir = (int)down.y;
        int leftHDir = (int)left.x; int leftVDir = (int)left.y;
        int rightHDir = (int)right.x; int rightVDir = (int)right.y;
        int uprightHDir = (int)upright.x; int uprightVDir = (int)upright.y;
        int upleftHDir = (int)upleft.x; int upleftVDir = (int)upleft.y;
        int downrightHDir = (int)downright.x; int downrightVDir = (int)downright.y;
        int downleftHDir = (int)downleft.x; int downleftVDir = (int)downleft.y;

        
        int result1 = Tempbitboard[upHDir + i, upVDir + j];
        int result2 = Tempbitboard[downHDir + i, downVDir + j];
        int result3 = Tempbitboard[leftHDir + i, leftVDir + j];
        int result4 = Tempbitboard[rightHDir + i, rightVDir + j];
        int result5 = Tempbitboard[uprightHDir + i, uprightVDir + j];
        int result6 = Tempbitboard[upleftHDir + i, upleftVDir + j];
        int result7 = Tempbitboard[downrightHDir + i, downrightHDir + j];
        int result8 = Tempbitboard[downleftHDir + i, downleftHDir + j];

        if (playerturn == 2)
        {
            if (result1 == 1 || result2 == 1 || result3 == 1 || result4 == 1 || result5 == 1 || result6 == 1 || result7 == 1 || result8 == 1)
            {
                return true;
            }
        }
        else if (playerturn == 1)
        {
            if (result1 == 2 || result2 == 2 || result3 == 2 || result4 == 2 || result5 == 2 || result6 == 2 || result7 == 2 || result8 == 2)
            {
                return true;
            }
        }




            if (result1 != 0)
            return true;
        else
            return false;
    }
}
