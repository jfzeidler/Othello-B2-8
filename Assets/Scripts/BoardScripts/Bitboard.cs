using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bitboard : MonoBehaviour
{

    public BoardRules BoardState = new BoardRules();

    public byte playerturn = 1;
    public byte[,] bitboard = new byte[8, 8];

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
        BoardState.ValidMove(bitboard, playerturn);
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
        var bitboardX = char.ToUpper(Char) - 65;
        var bitboardY = Tile[1] - '1';
        BoardState.captureEnemyPlayer(bitboard, bitboardY, bitboardX, playerturn);
        bitboardResetTurn(playerturn);
        pieceCounter(bitboard);
        BoardState.ValidMove(bitboard, playerturn);
        bitboardDisplayUpdate();
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

    public void pieceCounter(byte[,] bitboard)
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
        Blackcountertext.GetComponent<Text>().text = $"{Blackpieces}";
        Whitecountertext.GetComponent<Text>().text = $"{Whitepieces}";
    }

    public void bitboardResetTurn(byte playerturn)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bitboard[i, j] == 9)
                {
                    bitboard[i, j] = 0;
                }

                else if (bitboard[i, j] == 5)
                {
                    if (playerturn == 1)
                    {
                        bitboard[i, j] = 2;
                    }
                    
                    else if (playerturn == 2)
                    {
                        bitboard[i, j] = 1;
                    }
                }
            }
        }
    }
}
