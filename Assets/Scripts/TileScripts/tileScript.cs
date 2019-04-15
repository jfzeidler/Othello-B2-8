using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{
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
        if (DEBUG == 1)
        {
            DEBUGLOG(The_Board.GetComponent<Bitboard>().bitboard);
        }

        string selectedTile = gameObject.ToString();
        var Char = selectedTile[0];
        var bitboardX = char.ToUpper(Char) - 65;
        var bitboardY = selectedTile[1] - '1';

        if (The_Board.GetComponent<Bitboard>().bitboard[bitboardY, bitboardX] == 9)
        {
            byte playerturn = The_Board.GetComponent<Bitboard>().playerturn;
            MakeMove(bitboardX, bitboardY, playerturn);
        }
    }

    void placePlayer(int x, int y, int z)
    {
        Vector3 vectorPos = new Vector3(z, -0.85f, y);

        //If black turn
        if (x == 1)
        {
            var newObject = Instantiate(spawnDarkPlayer, vectorPos, Quaternion.identity);
            newObject.name = "Player-" + y + z;
            The_Board.GetComponent<Bitboard>().playerturn += 1;
        }

        //If white turn
        else if (x == 2)
        {
            var newObject = Instantiate(spawnWhitePlayer, vectorPos, Quaternion.identity);
            newObject.name = "Player-" + y + z;
            The_Board.GetComponent<Bitboard>().playerturn -= 1;
        }
    }

    void DEBUGLOG(byte[,] bitboard)
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log(bitboard[i, 0] + " " + bitboard[i, 1] + " " + bitboard[i, 2] + " " + bitboard[i, 3] + " " + bitboard[i, 4] + " " + bitboard[i, 5] + " " + bitboard[i, 6] + " " + bitboard[i, 7]);
        }
    }

    public void MakeMove (int xPos, int yPos, byte playerturn)
    {
        The_Board.GetComponent<Bitboard>().boardRules(xPos, yPos);
        placePlayer(playerturn, xPos, yPos);
        The_Board.GetComponent<Bitboard>().bitboard[yPos, xPos] = playerturn;
        The_Board.GetComponent<Bitboard>().bitboardUpdate();
    }

}