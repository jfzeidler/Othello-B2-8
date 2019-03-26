using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tileScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int x = 0;
    public int Isoccupied = 0;

    public Transform spawnPos;
    public GameObject spawnWhitePlayer;
    public GameObject spawnDarkPlayer;

    // Update is called once per frame
    void Update()
    {

    }

    // What to do when clicking on a tile on the board
    void OnMouseUp()
    {
        if (Isoccupied == 0)
        {
            placePlayer(x);
        }
        else
            Debug.Log("Tile " + gameObject.ToString() + " occupied!");
    }

    void placePlayer (int x)
    {
        Debug.Log("Clicked on " + gameObject.ToString());
        if (x == 0)
        {
            Instantiate(spawnWhitePlayer, spawnPos.position, spawnPos.rotation);
            Isoccupied++;
        }

        else if (x == 1)
        {
            Instantiate(spawnDarkPlayer, spawnPos.position, spawnPos.rotation);
            Isoccupied++;
        }
    }
}