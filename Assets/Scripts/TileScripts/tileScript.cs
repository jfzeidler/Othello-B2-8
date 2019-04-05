using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{

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
        string selectedTile = gameObject.ToString();
        var Char = selectedTile[0];
        var bitboardX = char.ToUpper(Char) - 65;
        var bitboardY = selectedTile[1] - '1';
        DEBUG(The_Board.GetComponent<Bitboard>().bitboard);

        if (The_Board.GetComponent<Bitboard>().bitboard[bitboardY, bitboardX] == 9 && isoccupied == 0)
        {
            Debug.Log("Clicked on " + selectedTile);
            byte playerturn = The_Board.GetComponent<Bitboard>().playerturn;
            The_Board.GetComponent<Bitboard>().bitboard[bitboardY, bitboardX] = playerturn;
            placePlayer(playerturn, selectedTile);
            The_Board.GetComponent<Bitboard>().bitboardUpdate(selectedTile);
            isoccupied++;
        }
    }

    void placePlayer(int x, string selectedTile)
    {
        //If black turn
        if (x == 1)
        {
            var newObject = Instantiate(spawnDarkPlayer, spawnPos.position, spawnPos.rotation);
            newObject.name = "Player-" + selectedTile;
            The_Board.GetComponent<Bitboard>().playerturn += 1;
        }

        //If white turn
        else if (x == 2)
        {
            var newObject = Instantiate(spawnWhitePlayer, spawnPos.position, spawnPos.rotation);
            newObject.name = "Player-" + selectedTile;
            The_Board.GetComponent<Bitboard>().playerturn -= 1;
        }
    }
    void DEBUG(byte[,] bitboard)
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log(bitboard[i, 0] + " " + bitboard[i, 1] + " " + bitboard[i, 2] + " " + bitboard[i, 3] + " " + bitboard[i, 4] + " " + bitboard[i, 5] + " " + bitboard[i, 6] + " " + bitboard[i, 7]);
        }
    }
}