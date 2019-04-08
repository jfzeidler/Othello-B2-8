using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bitboard : MonoBehaviour
{

    public BoardRules BoardState = new BoardRules();

    public byte playerturn = 1;
    public byte[,] bitboard = new byte[8, 8];

    public GameObject spawnWhitePlayer;
    public GameObject spawnDarkPlayer;
    public GameObject gameObjectToFlip;
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

    public void boardRules(string Tile)
    {
        var Char = Tile[0];
        var bitboardX = char.ToUpper(Char) - 65;
        var bitboardY = Tile[1] - '1';
        BoardState.captureEnemyPlayer(bitboard, bitboardY, bitboardX, playerturn);
        bitboardResetTurn();
    }

    public void bitboardUpdate()
    {
        pieceCounter(bitboard);
        BoardState.ValidMove(bitboard, playerturn);
        bitboardDisplayUpdate();
        Debug.Log(playerturn);
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

    public void bitboardResetTurn()
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
                    flipIt(i, j);
                    bitboard[i, j] = playerturn;
                }
            }
        }
    }

    public void flipIt(int i, int j)
    {
        string playerToFlip = "Player-" + j + i;
        Debug.Log(playerToFlip);
        Vector3 vectorRot = new Vector3(0.0f, 0.0f, 180f);
        Vector3 vectorPos = new Vector3(i, 1.05f, j);
        gameObjectToFlip = GameObject.Find(playerToFlip);
        byte [,] bitboard = new byte [8, 8];

       
        if (playerturn == 2)
        {
            gameObjectToFlip.transform.Rotate(0, 0, 180, Space.Self);
            if(bitboard[i, j] == 0)
            {
            gameObjectToFlip.transform.position = new Vector3(i, 1.05f, j);
            bitboard[i, j] = 1;
            }

        }

        if (playerturn == 1)
        {
            gameObjectToFlip.transform.Rotate(0, 0, 180, Space.Self);
            if(bitboard[i, j] == 0)
            {
            gameObjectToFlip.transform.position = new Vector3(i, 1.05f, j);
            bitboard[i, j] = 1;
            }
            

        }
         /*     
        if (playerturn == 2)
        {
            Destroy(gameObjectToFlip);
            Instantiate(spawnWhitePlayer, vectorPos, vectorRot);
        }

        if (playerturn == 1)
        {
            Destroy(gameObjectToFlip);
            Instantiate(spawnWhitePlayer, vectorPos, vectorRot);
        }*/
    }
}

