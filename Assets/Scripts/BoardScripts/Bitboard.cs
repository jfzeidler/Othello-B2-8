using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bitboard : MonoBehaviour
{
    public BoardRules boardRules = new BoardRules();

    public int playerturn = 1;
    public int[,] bitboard = new int[8, 8];

    public GameObject BitboardDisplay;
    public GameObject Blackcountertext;
    public GameObject Whitecountertext;

    // Start is called before the first frame update
    void Start() 
    {
        BitboardDisplay.GetComponent<Text>().text = "     AB CD EF GH";
        for (int i = 0; i < 8; i++)
        {
            BitboardDisplay.GetComponent<Text>().text += "\n " + (i + 1) + " [";
            for (int j = 0; j < 8; j++)
            {
                bitboard[i, j] = 0;
                BitboardDisplay.GetComponent<Text>().text += bitboard[i, j] + " ";
            }
            BitboardDisplay.GetComponent<Text>().text += "]";
        }
        bitboard[3, 4] = 1; bitboard[4, 3] = 1; bitboard[3, 3] = 2; bitboard[4, 4] = 2;
        bitboardDisplayUpdate();
        pieceCounter(bitboard);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void bitboardUpdate(string Tile)
    {
        var Char = Tile[0];
        int bitboardX = char.ToUpper(Char) - 65;
        int bitboardY = Tile[1] - '0';
        bitboard[bitboardY - 1, bitboardX] = playerturn;
        bitboardDisplayUpdate();
        pieceCounter(bitboard);
    }

    void bitboardDisplayUpdate()
    {
        BitboardDisplay.GetComponent<Text>().text = "     AB CD EF GH";
        for (int i = 0; i < 8; i++)
        {
            BitboardDisplay.GetComponent<Text>().text +="\n " + (i+1) + " [";
            for (int j = 0; j < 8; j++)
            {
                BitboardDisplay.GetComponent<Text>().text += bitboard[i, j] + " ";
            }
            BitboardDisplay.GetComponent<Text>().text += "]";
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
        boardRules.ValidMove();
        Blackcountertext.GetComponent<Text>().text = $"{Blackpieces}";
        Whitecountertext.GetComponent<Text>().text = $"{Whitepieces}";
    }
}
