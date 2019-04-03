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

        if (The_Board.GetComponent<Bitboard>().bitboard[bitboardX, bitboardY] == 9)
        {
                Debug.Log("Clicked on " + selectedTile);
                The_Board.GetComponent<Bitboard>().bitboardUpdate(selectedTile);
                int playerturn = The_Board.GetComponent<Bitboard>().playerturn;
                placePlayer(playerturn);
        }
    }

    void placePlayer (int x)
    {
        //If black turn
        if (x == 1)
        {
            Instantiate(spawnDarkPlayer, spawnPos.position, spawnPos.rotation);
            GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn += 1;
        }

        //If white turn
        else if (x == 2)
        {
            Instantiate(spawnWhitePlayer, spawnPos.position, spawnPos.rotation);
            GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn -= 1;
        }
    }
}