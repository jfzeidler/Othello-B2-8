using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{
    public Bitboard bitboard = new Bitboard();


    void Awake() // Kaldes før Start, men efter initalisering af variabler
    {

    }

    public Transform spawnPos;
    public GameObject spawnWhitePlayer;
    public GameObject spawnDarkPlayer;

    int Isoccupied = 0;

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

        if (Isoccupied == 0)
        {
            Debug.Log("Debug: " + bitboard.playerturn);
            Debug.Log("Clicked on " + selectedTile);
            placePlayer(bitboard.playerturn);
            Debug.Log("Debug: " + bitboard.playerturn);
        }
        else
            Debug.Log("Tile " + selectedTile + " occupied!");
    }

    // Place on tile and change turn to next player
    void placePlayer(int x)
    {
        //If black turn
        if (x == 1)
        {
            Instantiate(spawnDarkPlayer, spawnPos.position, spawnPos.rotation);
            Isoccupied++;
            bitboard.playerturn++;
        }

        //If white turn
        else if (x == 2)
        {
            Instantiate(spawnWhitePlayer, spawnPos.position, spawnPos.rotation);
            Isoccupied++;
            bitboard.playerturn--;
        }
    }
}