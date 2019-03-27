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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void bitboardUpdate(string Tile)
    {
        Debug.Log(Tile[0]);
        Debug.Log(Tile[1]);
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
}
