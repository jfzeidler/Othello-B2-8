using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRules : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void captureRule(int Tilex, int Tiley)
    {

        int[,] Tempbitboard = GameObject.Find("The-Board").GetComponent<Bitboard>().bitboard;

        if (GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn == 1)
        {

        }

        else if (GameObject.Find("The-Board").GetComponent<Bitboard>().playerturn == 2)
        {

        }
    }

}
