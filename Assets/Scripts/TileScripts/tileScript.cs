﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class tileScript : MonoBehaviour
{
    enum Player { blank = 0, black = 1, white = 2};
    byte DEBUG = 0;

    public GameObject TheBoard;
    public GameObject SpawnWhitePlayer;
    public GameObject SpawnDarkPlayer;

    int playMode = 0;

    void Awake()
    {
        playMode = PlayerPrefs.GetInt("playMode", 0);
    }

    // What to do when clicking on a tile on the board
    void OnMouseUp()
    {

        // Get the name of hte currently sellected tile
        string selectedTile = gameObject.ToString();
        var letter = selectedTile[0];

        // Convert, to compare with bitboard - C4 = [3 , 5]
        var bitboardX = char.ToUpper(letter) - 65;
        var bitboardY = selectedTile[1] - '1';

        // If playingmode is Player Vs. Player or on menu scene
        if (playMode == 1 || SceneManager.GetActiveScene().buildIndex == 0)
        {
            // If the tile sellected, has a possible move
            if (TheBoard.GetComponent<Bitboard>().board2D[bitboardY, bitboardX] == 9)
            {
                // Get playerturn to tell which turn it is
                int playerTurn = TheBoard.GetComponent<Bitboard>().playerTurn;
                MakeMove(bitboardX, bitboardY, playerTurn);
            }
        }

        // Else if playingmode is Player Vs. CPU
        else if (playMode == 0)
        {
            // Get playerturn to tell which turn it is
            int playerTurn = TheBoard.GetComponent<Bitboard>().playerTurn;

            if (playerTurn == (int)Player.black)
            {
                // If the tile sellected, has a possible move
                if (TheBoard.GetComponent<Bitboard>().board2D[bitboardY, bitboardX] == 9)
                    MakeMove(bitboardX, bitboardY, playerTurn);
            }
        }
    }

    void PlacePlayer(int playerTurn, int y, int z)
    {
        // Make position vector for Instantiate
        Vector3 vectorPos = new Vector3(z, -0.85f, y);

        //If black turn
        if (playerTurn == (int)Player.black)
        {
            // Places black piece on the board
            var newObject = Instantiate(SpawnDarkPlayer, vectorPos, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + y + z;
            // Update playerturn
            TheBoard.GetComponent<Bitboard>().playerTurn += 1;
        }

        //If white turn
        else if (playerTurn == (int)Player.white)
        {
            // Places white piece on the board
            var newObject = Instantiate(SpawnWhitePlayer, vectorPos, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + y + z;
            // Update playerturn
            TheBoard.GetComponent<Bitboard>().playerTurn -= 1;
        }
    }

    void DEBUGLOG(int[,] board2D)
    {
        for (int i = 0; i < 8; i++)
            Debug.Log(board2D[i, 0] + " " + board2D[i, 1] + " " + board2D[i, 2] + " " + board2D[i, 3] + " " + board2D[i, 4] + " " + board2D[i, 5] + " " + board2D[i, 6] + " " + board2D[i, 7]);
    }

    public void MakeMove (int xPos, int yPos, int playerTurn)
    {
        if (DEBUG == 1)
            DEBUGLOG(TheBoard.GetComponent<Bitboard>().board2D);

        // Call boardRules from Bitboard.cs
        TheBoard.GetComponent<Bitboard>().boardRules(xPos, yPos);
        // Place a piece on the sellected tile
        PlacePlayer(playerTurn, xPos, yPos);
        // Update the bitboard with the new value
        TheBoard.GetComponent<Bitboard>().board2D[yPos, xPos] = playerTurn;
        // Call bitboardUpdate from Bitboard.cs
        TheBoard.GetComponent<Bitboard>().boardUpdate();
        //The_Board.GetComponent<Bitboard>().bitboardResetTurn();

    }
}
