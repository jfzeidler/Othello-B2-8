using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class Bitboard : MonoBehaviour
{
    public BoardRules BoardState = new BoardRules();
    public MiniMax MiniMax = new MiniMax();
    public enum Player { blank = 0, black = 1, white = 2};
    public byte playerturn = 1;
    byte DEBUG = 1;
    int CPUPoints = 0;
    int Blackpieces = 0; int Whitepieces = 0;

    public int[,] Evaluation = {
        // A       B     C     D     E     F      G      H
        {10000, -3000, 1000,  800,  800, 1000, -3000, 10000}, // 1
        {-3000, -5000, -450, -500, -500, -450, -5000, -3000}, // 2
        { 1000,  -450,   30,   10,   10,   30,  -450,  1000}, // 3
        {  800,  -500,   10,   50,   50,   10,  -500,   800}, // 4
        {  800,  -500,   10,   50,   50,   10,  -500,   800}, // 5
        { 1000,  -450,   30,   10,   10,   30,  -450,  1000}, // 6
        {-3000, -5000, -450, -500, -500, -450, -5000, -3000}, // 7
        {10000, -3000, 1000,  800,  800, 1000, -3000, 10000}  // 8
    };

    public byte[,] bitboard = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 0, 0, 0, 0}, // 3
        {0, 0, 0, 2, 1, 0, 0, 0}, // 4
        {0, 0, 0, 1, 2, 0, 0, 0}, // 5
        {0, 0, 0, 0, 0, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };

    public GameObject Tiles;
    public GameObject AndTheWinnerIs;
    public GameObject GameOverCanvas;
    public GameObject spawnWhitePlayer;
    public GameObject spawnDarkPlayer;
    public GameObject BitboardDisplay;
    public GameObject Blackcountertext;
    public GameObject Whitecountertext;
    public GameObject ScorePanelTurnText;


    // Start is called before the first frame update
    void Start()
    {
        if (DEBUG == 1)
        {
            BitboardDisplay.GetComponent<Text>().text = "     AB CD EF GH";
            for (int i = 0; i < 8; i++)
            {
                BitboardDisplay.GetComponent<Text>().text += "\n " + (i + 1) + " [";
                for (int j = 0; j < 8; j++)
                {
                    BitboardDisplay.GetComponent<Text>().text += bitboard[i, j] + " ";
                }
                BitboardDisplay.GetComponent<Text>().text += "]";
            }
        }
        // Call ValidMove from BoardRules.cs
        BoardState.ValidMove(bitboard, playerturn);
        ShowValidMoves();
        // Show current player
        ShowPlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses "ESC" on the keyboard
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Go back to the main menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void boardRules(int bitboardX, int bitboardY)
    {
        // Call CaptureEnemyPlayer from BoardRules.cs
        BoardState.CaptureEnemyPlayer(bitboard, bitboardY, bitboardX, playerturn);
        // Reset the bitboard for the next turn
        bitboardResetTurn();
    }

    public void bitboardUpdate()
    {
        // Update the scoreboard
        PieceCounter(bitboard);
        // Call ValidMove from BoardRules.cs
        BoardState.ValidMove(bitboard, playerturn);
        // Check if there are available moves for the current player
        playerturn = PassCounter(bitboard, playerturn);
        if (DEBUG == 1)
        {
            bitboardDisplayUpdate();

        }

        // Show current player
        ShowPlayerTurn();
        // Check if theres a Game Over Senario
        IsGameOver(bitboard, playerturn, Whitepieces, Blackpieces);
        // Give the current player the right to make a move
        if (playerturn == 1)
        {
            // Show valid moves on board
            ShowValidMoves();
        }

        else if (playerturn == 2)
        {
            PlayerToMakeMove(playerturn);
        }
    }

    void bitboardDisplayUpdate()
    {
        BitboardDisplay.GetComponent<Text>().text = "     AB CD EF GH";
        for (int i = 0; i < 8; i++)
        {
            BitboardDisplay.GetComponent<Text>().text += "\n " + (i + 1) + " [";
            for (int j = 0; j < 8; j++)
            {
                BitboardDisplay.GetComponent<Text>().text += bitboard[i, j] + " ";
            }
            BitboardDisplay.GetComponent<Text>().text += "]";
        }
    }

    public void PieceCounter(byte[,] bitboard)
    {
        Blackpieces = 0; Whitepieces = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bitboard[i, j] == 1)
                {
                    Blackpieces++;
                }
                else if (bitboard[i, j] == 2)
                {
                    Whitepieces++;
                }
            }
        }
        // Update the visual scoreboard
        pieceCounterUpdate(Blackpieces, Whitepieces);
    }

    void pieceCounterUpdate(int Blackpieces, int Whitepieces)
    {
        Blackcountertext.GetComponent<TextMeshProUGUI>().text = $"{Blackpieces}";
        Whitecountertext.GetComponent<TextMeshProUGUI>().text = $"{Whitepieces}";
    }

    void bitboardResetTurn()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // If theres an available move, change the texture of the current tile
                if (bitboard[i, j] == 9)
                {
                    int temp = j + 65;
                    char c = (char)temp;
                    string tileGameObject = c + System.Convert.ToString(i + 1 + "/TileVisual");

                    // Call TextureSwap from the gameobject
                    GameObject.Find(tileGameObject).GetComponent<SwapTextures>().TextureSwap();
                    // reset the bitboard value to 0
                    bitboard[i, j] = 0;
                }

                // If theres a captured piece, change the piece
                else if (bitboard[i, j] == 5)
                {
                    // Change the pieces thats captured
                    flipIt(i, j);
                    // Change the bitboard value to the current player
                    bitboard[i, j] = playerturn;
                }
            }
        }
    }

    void flipIt(int i, int j)
    {

        string playerToFlip = "Player_" + j + i;
        Vector3 vectorPos = new Vector3(i, -0.85f, j);
        // Find the piece to capture, and destroy it
        DestroyImmediate(GameObject.Find(playerToFlip));
        //UnityEngine.Debug.Log(playerToFlip);

        if (playerturn == 2)
        {
            // Place a white piece on the board
            var newObject = Instantiate(spawnWhitePlayer, vectorPos, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + j + i;
        }

        if (playerturn == 1)
        {
            // Place a black piece on the board
            var newObject = Instantiate(spawnDarkPlayer, vectorPos, Quaternion.identity)as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + j + i;
        }
    }

    void ShowPlayerTurn()
    {
        if (playerturn == (int)Player.black)
        {
            ScorePanelTurnText.GetComponent<TextMeshProUGUI>().text = "Black";
        }

        else if (playerturn == (int)Player.white)
        {
            ScorePanelTurnText.GetComponent<TextMeshProUGUI>().text = "White";
        }
    }

    void ShowValidMoves()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bitboard[i, j] == 9)
                {
                    int temp = j + 65;
                    char c = (char)temp;
                    string tileGameObject = c + System.Convert.ToString(i + 1 + "/TileVisual");
                    // Call TextureSwap from the gameobject
                    GameObject.Find(tileGameObject).GetComponent<SwapTextures>().TextureSwap();
                }
            }
        }
    }

    void IsGameOver(byte[,] bitboard, byte playerturn, int Whitepieces, int Blackpieces)
    {
        // If black player has the most pieces, show "Player Black Won"
        if (Blackpieces > Whitepieces)
        {
            AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Player Black Won";
        }

        // If black player has the most pieces, show "Player White Won"
        else if (Whitepieces > Blackpieces)
        {
            AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Player White Won";
        }

        // If black player has the most pieces, show "Draw"
        else if (Whitepieces == Blackpieces)
        {
            AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Draw";
        }

        // Call CheckForNine from BoardRules.cs
        if (BoardState.CheckForNine(bitboard) == true)
        {
            UnityEngine.Debug.Log("Game over");
            // Show the Game Over canvas, to show who won
            GameOverCanvas.SetActive(true);
        }
    }

    public byte PassCounter(byte[,] bitboard, byte playerturn)
    {
        // Call CheckForNine from BoardRules.cs
        if (BoardState.CheckForNine(bitboard) == true)
        {
            if (playerturn == 1)
            {
                // Give the turn to the opposite player
                playerturn = 2;
                UnityEngine.Debug.Log("No valid moves for black player");
            }

            else if (playerturn == 2)
            {
                // Give the turn to the opposite player
                playerturn = 1;
                UnityEngine.Debug.Log("No valid moves for white player");
            }
            // Call ValidMove from BoardRules.cs
            BoardState.ValidMove(bitboard, playerturn);
        }
        // return the new playerturn
        return playerturn;
    }

    void CPUTurn(byte[] CPUBestMove)
    {
        // If the move is inside the bitboard
        if (CPUBestMove[0] < 8 && CPUBestMove[1] < 8)
        {
            int temp = CPUBestMove[1] + 65;
            char c = (char)temp;
            UnityEngine.Debug.Log("CPU: " + c + (CPUBestMove[0] + 1));
            // Call MakeMove from tileScript.cs
            Tiles.GetComponent<tileScript>().MakeMove(CPUBestMove[1], CPUBestMove[0], playerturn);

        }
    }

    void PlayerToMakeMove(byte playerturn)
    {
        int maxDepth = 10;
        int currentDepth = 1;
        // Active AI if the playerturn is White
        if (playerturn == (int)Player.white)
        {
            Stopwatch stopWatch = new Stopwatch();

            byte[] CPUBestMove = new byte[2];
            // Get the best move from ReturnRandomMove from MiniMax.cs
            stopWatch.Start();
            CPUBestMove = MiniMax.CalculateAIMove(bitboard, playerturn, maxDepth, currentDepth, int.MaxValue, int.MinValue);
            stopWatch.Stop();
            // Remove the red tiles, since the AI doesn't need them
            ShowValidMoves();
            // Remove unintended 5's
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == 5)
                    {
                        bitboard[i, j] = 1;
                    }
                }
            }
            // Perform the move
            CPUTurn(CPUBestMove);
            //ShowAIPoints(CPUBestMove);
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            UnityEngine.Debug.Log(elapsedTime);
            //ShowAIPoints(CPUBestMove);
        }
    }

    /*void ShowAIPoints(byte[] CPUBestMove)
    {
        if (GameOverCanvas.isActiveAndEnabled)
        {

        }
        else
        {
            CPUPoints += Evaluation[CPUBestMove[0], CPUBestMove[1]];
            UnityEngine.Debug.Log("CPU Points: " + CPUPoints);
        }
    } */
}
