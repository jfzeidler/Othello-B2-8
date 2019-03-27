using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bitboard : MonoBehaviour
{

    public int playerturn = 1;

    public int[,] bitboard = new int[8, 8];

    
    // Start is called before the first frame update
    void Start() 
    {
        GameObject.Find("BitboardDisplay").GetComponent<Text>().text = "     AB CD EF GH";
        for (int i = 0; i < 8; i++)
        {
            GameObject.Find("BitboardDisplay").GetComponent<Text>().text += "\n " + (i + 1) + " [";
            for (int j = 0; j < 8; j++)
            {
                bitboard[i, j] = 0;
                GameObject.Find("BitboardDisplay").GetComponent<Text>().text += bitboard[i, j] + " ";
            }
            GameObject.Find("BitboardDisplay").GetComponent<Text>().text += "]";
        }
        bitboard[3, 4] = 1; bitboard[4, 3] = 1; bitboard[3, 3] = 2; bitboard[4, 4] = 2;
        bitboardDisplayUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void bitboardUpdate(string Tile)
    {
        var Char = Tile[0];
        int bitboardX = 0;
        switch (Char)
        {
            case 'A':
                bitboardX = 0;
                break;
            case 'B':
                bitboardX = 1;
                break;
            case 'C':
                bitboardX = 2;
                break;
            case 'D':
                bitboardX = 3;
                break;
            case 'E':
                bitboardX = 4;
                break;
            case 'F':
                bitboardX = 5;
                break;
            case 'G':
                bitboardX = 6;
                break;
            case 'H':
                bitboardX = 7;
                break;

        }
        int bitboardY = Tile[1] - '0';
        bitboard[bitboardY - 1, bitboardX] = playerturn;
        bitboardDisplayUpdate();
        pieceCounter(bitboard);
        GameObject.Find("The-Board").GetComponent<BoardRules>().captureRule((bitboardY - 1), bitboardX);
    }

    void bitboardDisplayUpdate()
    {
        GameObject.Find("BitboardDisplay").GetComponent<Text>().text = "     AB CD EF GH";
        for (int i = 0; i < 8; i++)
        {
            GameObject.Find("BitboardDisplay").GetComponent<Text>().text +="\n " + (i+1) + " [";
            for (int j = 0; j < 8; j++)
            {
                GameObject.Find("BitboardDisplay").GetComponent<Text>().text += bitboard[i, j] + " ";
            }
            GameObject.Find("BitboardDisplay").GetComponent<Text>().text += "]";
        }
    }

    public void pieceCounter(int[,] bitboard)
    {
        int Blackpieces = 0, Whitepieces = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bitboard[i, j] == 1)
                {
                    Blackpieces++;
                }
                else if (bitboard[i, j] == 2)
                {
                    Whitepieces++;
                }
            }
        }
        pieceCounterUpdate(Blackpieces, Whitepieces);
    }

    void pieceCounterUpdate(int Blackpieces, int Whitepieces)
    {
        GameObject.Find("PieceCounter").GetComponent<Text>().text = "Black = " + Blackpieces + "White = " + Whitepieces;
    }
}
