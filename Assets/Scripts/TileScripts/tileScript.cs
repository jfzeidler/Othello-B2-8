using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{
    byte DEBUG = 0;

    public Transform spawnPos;
    public GameObject The_Board;
    public GameObject spawnWhitePlayer;
    public GameObject spawnDarkPlayer;

    public byte isoccupied = 0;

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

        if (The_Board.GetComponent<Bitboard>().bitboard[bitboardY, bitboardX] == 9 && isoccupied == 0)
        {
            Debug.Log("Clicked on " + selectedTile);
            byte playerturn = The_Board.GetComponent<Bitboard>().playerturn;
            The_Board.GetComponent<Bitboard>().boardRules(selectedTile);
            placePlayer(playerturn, bitboardX, bitboardY);
            The_Board.GetComponent<Bitboard>().bitboard[bitboardY, bitboardX] = playerturn;
            //byte[,] bitboard = The_Board.GetComponent<Bitboard>().bitboard;
            //byte playerturn2 = The_Board.GetComponent<Bitboard>().PassCounter(bitboard, playerturn);

            //if (playerturn != playerturn2)
            //{
            //    playerturn = playerturn2;
            //}

            The_Board.GetComponent<Bitboard>().bitboardUpdate();
            isoccupied++;
        }
    }

    void placePlayer(int x, int y, int z)
    {
        //If black turn
        if (x == 1)
        {
            var newObject = Instantiate(spawnDarkPlayer, spawnPos.position, spawnPos.rotation);
            newObject.name = "Player-" + y + z;
            The_Board.GetComponent<Bitboard>().playerturn += 1;
        }

        //If white turn
        else if (x == 2)
        {
            var newObject = Instantiate(spawnWhitePlayer, spawnPos.position, spawnPos.rotation);
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
}