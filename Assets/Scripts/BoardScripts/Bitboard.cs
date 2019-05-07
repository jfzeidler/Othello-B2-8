using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Bitboard : MonoBehaviour
{
    public BoardRules BoardState = new BoardRules();
    public MiniMax MiniMax = new MiniMax();
    public enum Player { blank = 0, black = 1, white = 2 };
    public int playerturn = 1;
    int DEBUG = 0;
    int CPUPoints = 0;
    int Blackpieces = 0; int Whitepieces = 0;
    int maxDepth = 2;
    int playMode = 0; // 0 = Player vs. CPU, 1 = Player vs. Player, 2 = CPU vs. CPU
    int moveGuide = 0; // 0 = No Moves, 1 = Show Moves
    int startHelp = 0; // 0 = Show help at start, 1 = No help is shown

    readonly int[,] Evaluation = {
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

    public int[,] bitboard = {
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
    public GameObject HelpBoard;
    public GameObject Scoreboard;
    public GameObject Menu_Button;
    public GameObject BackButton;
    public GameObject HelpButton;
    public GameObject Text2image;

    public void MenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Awake()
    {
        // Get values from PlayerPrefs, else default to \/ 
        maxDepth =       PlayerPrefs.GetInt("maxDepth", 2);
        playMode =       PlayerPrefs.GetInt("playMode", 0);
        startHelp =     PlayerPrefs.GetInt("startHelp", 0);
        moveGuide =     PlayerPrefs.GetInt("moveGuide", 0);
        PlayerPrefs.Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        // If it's the first time launching the game
        if (startHelp == 0)
        {
            // Show the help menu
            ShowHelp();

            PlayerPrefs.SetInt("startHelp", 1);
            PlayerPrefs.Save();
        }

        // For Debugging
        if (DEBUG == 1)
        {
            MiniMax.PrepareDebugForMinimax();

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
        // Show current player
        ShowPlayerTurn();
        // Play in accordance with the current playing mode
        PlayingMode();
    }

    // Update is called once per frame
    void Update()
    {

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

        // For Debugging
        if (DEBUG == 1)
        {
            bitboardDisplayUpdate();
        }

        // Show current player
        ShowPlayerTurn();
        // Check if theres a Game Over Senario
        IsGameOver(bitboard, playerturn, Whitepieces, Blackpieces);
        // Play in accordance with the current playing mode
        PlayingMode();
    }

    // This method is for debugging and show the current bitboard to the game overlay
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

    // A counter for counting pieces
    public void PieceCounter(int[,] bitboard)
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

    // This method updates the number of white and black pieces, to keep count of the score
    void pieceCounterUpdate(int Blackpieces, int Whitepieces)
    {
        Blackcountertext.GetComponent<TextMeshProUGUI>().text = $"{Blackpieces}";
        Whitecountertext.GetComponent<TextMeshProUGUI>().text = $"{Whitepieces}";
    }

    // This method resets the bitboard, to remove unnecessary values
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

                    if (moveGuide == 1)
                    {
                        // Call TextureSwap from the gameobject
                        GameObject.Find(tileGameObject).GetComponent<SwapTextures>().TextureSwap();
                    }
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

    // This method is used to flip the captured pieces
    void flipIt(int i, int j)
    {
        string playerToFlip = "Player_" + j + i;
        Vector3 vectorPos = new Vector3(i, -0.85f, j);
        // Find the piece to capture, and destroy it
        DestroyImmediate(GameObject.Find(playerToFlip));
        //UnityEngine.Debug.Log(playerToFlip);

        if (playerturn == (byte)Player.white)
        {
            // Place a white piece on the board
            var newObject = Instantiate(spawnWhitePlayer, vectorPos, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + j + i;
        }

        if (playerturn == (byte)Player.black)
        {
            // Place a black piece on the board
            var newObject = Instantiate(spawnDarkPlayer, vectorPos, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + j + i;
        }
    }

    // This method is used to show whose turn it is
    void ShowPlayerTurn()
    {
        if (playerturn == (byte)Player.black)
        {
            ScorePanelTurnText.GetComponent<TextMeshProUGUI>().text = "Black";
        }

        else if (playerturn == (byte)Player.white)
        {
            ScorePanelTurnText.GetComponent<TextMeshProUGUI>().text = "White";
        }
    }

    // This method is used to show which tiles follow the rules, and are valid moves
    void ShowValidMoves()
    {
        // Only show valid moves, if the player has toggled this option on in the menu
        if (moveGuide == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == 9)
                    {
                        int temp = j + 65;
                        char c = (char)temp;
                        string tileGameObject = c + System.Convert.ToString((i + 1) + "/TileVisual");
                        // Call TextureSwap from the gameobject
                        GameObject.Find(tileGameObject).GetComponent<SwapTextures>().TextureSwap();
                    }
                }
            }
        }
    }

    // This method is used to check if there are any valid moves left
    void IsGameOver(int[,] bitboard, int playerturn, int Whitepieces, int Blackpieces)
    {
        // Only use this method when on the main scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
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
    }

    // This method is used to pass the turn on to the opposite player, if no moves are available
    public int PassCounter(int[,] bitboard, int playerturn)
    {
        // Call CheckForNine from BoardRules.cs
        if (BoardState.CheckForNine(bitboard) == true)
        {
            if (playerturn == (byte)Player.black)
            {
                // Give the turn to the opposite player
                playerturn = (byte)Player.white;
                UnityEngine.Debug.Log("No valid moves for black player");
            }

            else if (playerturn == (byte)Player.white)
            {
                // Give the turn to the opposite player
                playerturn = (byte)Player.black;
                UnityEngine.Debug.Log("No valid moves for white player");
            }
            // Call ValidMove from BoardRules.cs
            BoardState.ValidMove(bitboard, playerturn);
        }
        // return the new playerturn
        return playerturn;
    }

    // This method is used to perform the best move from MiniMax
    void CPUTurn(int[] CPUBestMove)
    {
        // If the move is inside the bitboard
        if (BoardState.InRange(CPUBestMove[0], CPUBestMove[1]))
        {
            int temp = CPUBestMove[1] + 65;
            char c = (char)temp;
            UnityEngine.Debug.Log("CPU: " + c + (CPUBestMove[0] + 1));
            // Call MakeMove from tileScript.cs
            Tiles.GetComponent<tileScript>().MakeMove(CPUBestMove[1], CPUBestMove[0], playerturn);
        }
    }

    // This method is used to debug how many points the CPU have
    void ShowAIPoints(int[] CPUBestMove)
    {
        if (BoardState.InRange(CPUBestMove[0], CPUBestMove[1]))
        {
            CPUPoints += Evaluation[CPUBestMove[0], CPUBestMove[1]];
            UnityEngine.Debug.Log("CPU Points: " + CPUPoints);
        }
    }

    // This method is used to change how you play the game
    void PlayingMode()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playMode == 0)
            {
                if (playerturn == (int)Player.black)
                {
                    ShowValidMoves();
                }

                if (playerturn == (int)Player.white)
                {
                    // Invoke is used to run the method CPU(), after 1 sec. delay
                    Invoke("CPU", 1);
                }
            }

            else if (playMode == 1)
            {
                ShowValidMoves();
            }

            else if (playMode == 2)
            {
                Invoke("CPU", 1);
            }
        }

        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ShowValidMoves();
        }
    }

    // This method is used to calculate the best move for the CPU turn
    void CPU()
    {
        int currentDepth = 0;
        // Active AI if the playerturn is White
        Stopwatch stopWatch = new Stopwatch();
        int[] CPUBestMove = new int[2];
        // Start a stopwatch to calculate the runtime of the MiniMax method
        stopWatch.Start();
        // Get the best move from CalculateAIMove from MiniMax.cs
        CPUBestMove = MiniMax.CalculateAIMove(bitboard, playerturn, maxDepth, currentDepth, int.MinValue, int.MaxValue);
        // Stop the stopwatch to calculate the runtime of the MiniMax method
        stopWatch.Stop();
        // Remove the red tiles, since the AI doesn't need them
        ShowValidMoves();
        // Perform the move
        CPUTurn(CPUBestMove);
        // Calculate runtime of MiniMax
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("M{1:00}:S{2:00}.Mil{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        // Both method below is for debugging
        UnityEngine.Debug.Log(elapsedTime);
        ShowAIPoints(CPUBestMove);
    }

    // This method is used to show the help menu when the "HELP" button is pressed
    void ShowHelp()
    {
        HelpBoard.SetActive(true);
        Scoreboard.SetActive(false);
        Menu_Button.SetActive(false);
        BitboardDisplay.SetActive(false);
        BackButton.SetActive(false);
        HelpButton.SetActive(false);
        Text2image.SetActive(false);
    }
}
