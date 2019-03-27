using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{

    public int Isoccupied = 0;

    public Transform spawnPos;
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

        if (Isoccupied == 0)
        {
            Debug.Log("Clicked on " + selectedTile);
            GameObject.Find("The-Board").GetComponent<Bitboard>().bitboardUpdate(selectedTile);
            int playerturn = GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn;
            placePlayer(playerturn);
        }
        else
            Debug.Log("Tile " + selectedTile + " occupied!");
    }

    void placePlayer (int x)
    {
        //If black turn
        if (x == 1)
        {
            Instantiate(spawnDarkPlayer, spawnPos.position, spawnPos.rotation);
            Isoccupied++;
            GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn += 1;
            Debug.Log("DEBUG");
        }

        //If white turn
        else if (x == 2)
        {
            Instantiate(spawnWhitePlayer, spawnPos.position, spawnPos.rotation);
            Isoccupied++;
            GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn -= 1;
            Debug.Log("DEBUG");
        }
    }
}