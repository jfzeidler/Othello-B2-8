using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HelloWorld
{
    public class tileScript : Bitboard
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
                Debug.Log("Debug: " + playerturn);
                Debug.Log("Clicked on " + selectedTile);
                placePlayer(playerturn);
                Debug.Log("Debug: " + playerturn);
            }
            else
                Debug.Log("Tile " + selectedTile + " occupied!");
        }

        void placePlayer(int x)
        {
            //If black turn
            if (x == 1)
            {
                Instantiate(spawnDarkPlayer, spawnPos.position, spawnPos.rotation);
                Isoccupied++;
                playerturn++;
            }

            //If white turn
            else if (x == 2)
            {
                Instantiate(spawnWhitePlayer, spawnPos.position, spawnPos.rotation);
                Isoccupied++;
                playerturn--;
            }
        }
    }
}