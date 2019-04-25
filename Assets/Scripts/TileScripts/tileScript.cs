using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{
    enum Player { blank = 0, black = 1, white = 2};
    byte DEBUG = 0;

    public GameObject The_Board;
    public GameObject spawnWhitePlayer;
    public GameObject spawnDarkPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // What to do when clicking on a tile on the board
    void OnMouseUp()
    {

        // Get the name of hte currently sellected tile
        string selectedTile = gameObject.ToString();
        var Char = selectedTile[0];

        // Convert, to compare with bitboard - C4 = [3 , 4]
        var bitboardX = char.ToUpper(Char) - 65;
        var bitboardY = selectedTile[1] - '1';

        // If the tile sellected, has a possible move
        if (The_Board.GetComponent<Bitboard>().bitboard[bitboardY, bitboardX] == 9)
        {
            // Get playerturn to tell which turn it is
            int playerturn = The_Board.GetComponent<Bitboard>().playerturn;
            MakeMove(bitboardX, bitboardY, playerturn);
        }
    }

    void placePlayer(byte playerturn, int y, int z)
    {
        // Make position vector for Instantiate
        Vector3 vectorPos = new Vector3(z, -0.85f, y);

        //If black turn
        if (playerturn == (byte)Player.black)
        {
            // Places black piece on the board
            var newObject = Instantiate(spawnDarkPlayer, vectorPos, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + y + z;
            // Update playerturn
            The_Board.GetComponent<Bitboard>().playerturn += 1;
        }

        //If white turn
        else if (playerturn == (byte)Player.white)
        {
            // Places white piece on the board
            var newObject = Instantiate(spawnWhitePlayer, vectorPos, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + y + z;
            // Update playerturn
            The_Board.GetComponent<Bitboard>().playerturn -= 1;
        }
    }

    void DEBUGLOG(int[,] bitboard)
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log(bitboard[i, 0] + " " + bitboard[i, 1] + " " + bitboard[i, 2] + " " + bitboard[i, 3] + " " + bitboard[i, 4] + " " + bitboard[i, 5] + " " + bitboard[i, 6] + " " + bitboard[i, 7]);
        }
    }

    public void MakeMove (int xPos, int yPos, int playerturn)
    {
        if (DEBUG == 1)
        {
            DEBUGLOG(The_Board.GetComponent<Bitboard>().bitboard);
        }
        // Call boardRules from Bitboard.cs
        The_Board.GetComponent<Bitboard>().boardRules(xPos, yPos);
        // Place a piece on the sellected tile
        placePlayer(playerturn, xPos, yPos);
        // Update the bitboard with the new value
        The_Board.GetComponent<Bitboard>().bitboard[yPos, xPos] = playerturn;
        // Call bitboardUpdate from Bitboard.cs
        The_Board.GetComponent<Bitboard>().bitboardUpdate();
        //The_Board.GetComponent<Bitboard>().bitboardResetTurn();

    }
}
