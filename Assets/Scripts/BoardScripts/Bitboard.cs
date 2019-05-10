using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Bitboard : MonoBehaviour
{
    public BoardRules BoardRules = new BoardRules();
    public MiniMax MiniMax = new MiniMax();
    public enum Player { blank = 0, black = 1, white = 2 };
    public int playerTurn = 1;
    int DEBUG = 0;
    int cpuPoints = 0;
    int blackPieces = 0; int whitePieces = 0;
    int maxDepth = 2;
    int playMode = 0; // 0 = Player vs. CPU, 1 = Player vs. Player, 2 = CPU vs. CPU
    int moveGuide = 0; // 0 = No Moves, 1 = Show Moves
    int startHelp = 0; // 0 = Show help at start, 1 = No help is shown

    readonly int[,] evaluation = {
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

    public int[,] board2D = {
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
    public GameObject SpawnWhitePlayer;
    public GameObject SpawnDarkPlayer;
    public GameObject BitboardDisplay;
    public GameObject BlackCounterText;
    public GameObject WhiteCounterText;
    public GameObject ScorePanelTurnText;
    public GameObject HelpBoard;
    public GameObject ScoreBoard;
    public GameObject MainMenuButton;
    public GameObject BackButton;
    public GameObject HelpButton;
    public GameObject Text2Image;

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
                    BitboardDisplay.GetComponent<Text>().text += board2D[i, j] + " ";
                }
                BitboardDisplay.GetComponent<Text>().text += "]";
            }
        }
        // Call ValidMove from BoardRules.cs
        BoardRules.ValidMove(board2D, playerTurn);
        // Show current player
        ShowPlayerTurn();
        // Play in accordance with the current playing mode
        PlayingMode();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void boardRules(int boardX, int boardY)
    {
        // Call CaptureEnemyPlayer from BoardRules.cs
        BoardRules.CaptureEnemyPlayer(board2D, boardY, boardX, playerTurn);
        // Reset the board for the next turn
        boardResetTurn();
    }

    public void boardUpdate()
    {
        // Update the scoreboard
        PieceCounter(board2D);
        // Call ValidMove from BoardRules.cs
        BoardRules.ValidMove(board2D, playerTurn);
        // Check if there are available moves for the current player
        playerTurn = PassCounter(board2D, playerTurn);

        // For Debugging
        if (DEBUG == 1)
            boardDisplayUpdate();

        // Show current player
        ShowPlayerTurn();
        // Check if theres a Game Over Senario
        IsGameOver(board2D, playerTurn, whitePieces, blackPieces);
        // Play in accordance with the current playing mode
        PlayingMode();
    }

    // This method is for debugging and show the current board to the game overlay
    void boardDisplayUpdate()
    {
        BitboardDisplay.GetComponent<Text>().text = "     AB CD EF GH";
        for (int i = 0; i < 8; i++)
        {
            BitboardDisplay.GetComponent<Text>().text += "\n " + (i + 1) + " [";

            for (int j = 0; j < 8; j++)
                BitboardDisplay.GetComponent<Text>().text += board2D[i, j] + " ";

            BitboardDisplay.GetComponent<Text>().text += "]";
        }
    }

    // A counter for counting pieces
    public void PieceCounter(int[,] board2D)
    {
        blackPieces = 0; whitePieces = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board2D[i, j] == 1)
                    blackPieces++;

                else if (board2D[i, j] == 2)
                    whitePieces++;
            }
        }
        // Update the visual scoreboard
        PieceCounterUpdate(blackPieces, whitePieces);
    }

    // This method updates the number of white and black pieces, to keep count of the score
    void PieceCounterUpdate(int blackPieces, int whitePieces)
    {
        BlackCounterText.GetComponent<TextMeshProUGUI>().text = $"{blackPieces}";
        WhiteCounterText.GetComponent<TextMeshProUGUI>().text = $"{whitePieces}";
    }

    // This method resets the board, to remove unnecessary values
    void boardResetTurn()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // If theres an available move, change the texture of the current tile
                if (board2D[i, j] == 9)
                {
                    int temp = j + 65;
                    char c = (char)temp;
                    string tileGameObject = c + System.Convert.ToString(i + 1 + "/TileVisual");

                    if (moveGuide == 1)
                        // Call TextureSwap from the gameobject
                        GameObject.Find(tileGameObject).GetComponent<SwapTextures>().TextureSwap();

                    // reset the board value to 0
                    board2D[i, j] = 0;
                }

                // If theres a captured piece, change the piece
                else if (board2D[i, j] == 5)
                {
                    // Change the pieces thats captured
                    FlipIt(i, j);
                    // Change the board value to the current player
                    board2D[i, j] = playerTurn;
                }
            }
        }
    }

    // This method is used to flip the captured pieces
    void FlipIt(int i, int j)
    {
        string playerToFlip = "Player_" + j + i;
        Vector3 piecePosition = new Vector3(i, -0.85f, j);
        // Find the piece to capture, and destroy it
        DestroyImmediate(GameObject.Find(playerToFlip));
        //UnityEngine.Debug.Log(playerToFlip);

        if (playerTurn == (byte)Player.white)
        {
            // Place a white piece on the board
            var newObject = Instantiate(SpawnWhitePlayer, piecePosition, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + j + i;
        }

        if (playerTurn == (byte)Player.black)
        {
            // Place a black piece on the board
            var newObject = Instantiate(SpawnDarkPlayer, piecePosition, Quaternion.identity) as GameObject;
            // Naming the piece for the capture rule
            newObject.name = "Player_" + j + i;
        }
    }

    // This method is used to show whose turn it is
    void ShowPlayerTurn()
    {
        if (playerTurn == (byte)Player.black)
            ScorePanelTurnText.GetComponent<TextMeshProUGUI>().text = "Black";

        else if (playerTurn == (byte)Player.white)
            ScorePanelTurnText.GetComponent<TextMeshProUGUI>().text = "White";
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
                    if (board2D[i, j] == 9)
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
    void IsGameOver(int[,] board2D, int playerTurn, int whitePieces, int blackPieces)
    {
        // Only use this method when on the main scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // If black player has the most pieces, show "Player Black Won"
            if (blackPieces > whitePieces)
                AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Player Black Won";

            // If black player has the most pieces, show "Player White Won"
            else if (whitePieces > blackPieces)
                AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Player White Won";

            // If black player has the most pieces, show "Draw"
            else if (whitePieces == blackPieces)
                AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Draw";

            // Call CheckForNine from BoardRules.cs
            if (BoardRules.CheckForNine(board2D) == true)
            {
                UnityEngine.Debug.Log("Game over");
                // Show the Game Over canvas, to show who won
                GameOverCanvas.SetActive(true);
            }
        }
    }

    // This method is used to pass the turn on to the opposite player, if no moves are available
    public int PassCounter(int[,] board2D, int playerTurn)
    {
        // Call CheckForNine from BoardRules.cs
        if (BoardRules.CheckForNine(board2D) == true)
        {
            if (playerTurn == (byte)Player.black)
            {
                // Give the turn to the opposite player
                playerTurn = (byte)Player.white;
                UnityEngine.Debug.Log("No valid moves for black player");
            }

            else if (playerTurn == (byte)Player.white)
            {
                // Give the turn to the opposite player
                playerTurn = (byte)Player.black;
                UnityEngine.Debug.Log("No valid moves for white player");
            }
            // Call ValidMove from BoardRules.cs
            BoardRules.ValidMove(board2D, playerTurn);
        }
        // return the new playerturn
        return playerTurn;
    }

    // This method is used to perform the best move from MiniMax
    void CPUTurn(int[] cpuBestMove)
    {
        // If the move is inside the board
        if (BoardRules.InRange(cpuBestMove[0], cpuBestMove[1]))
        {
            int temp = cpuBestMove[1] + 65;
            char c = (char)temp;
            UnityEngine.Debug.Log("CPU: " + c + (cpuBestMove[0] + 1));
            // Call MakeMove from tileScript.cs
            Tiles.GetComponent<tileScript>().MakeMove(cpuBestMove[1], cpuBestMove[0], playerTurn);
        }
    }

    // This method is used to debug how many points the CPU have
    void ShowAIPoints(int[] cpuBestMove)
    {
        if (BoardRules.InRange(cpuBestMove[0], cpuBestMove[1]))
        {
            cpuPoints += evaluation[cpuBestMove[0], cpuBestMove[1]];
            UnityEngine.Debug.Log("CPU Points: " + cpuPoints);
        }
    }

    // This method is used to change how you play the game
    void PlayingMode()
    {
        // If on main scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playMode == 0)
            {
                if (playerTurn == (int)Player.black)
                    ShowValidMoves();

                if (playerTurn == (int)Player.white)
                    // Invoke is used to run the method CPU(), after 1 sec. delay
                    Invoke("CPU", 1);
            }

            else if (playMode == 1)
                ShowValidMoves();

            else if (playMode == 2)
                Invoke("CPU", 1);
        }

        // If on main menu scene
        else if (SceneManager.GetActiveScene().buildIndex == 0)
            ShowValidMoves();
    }

    // This method is used to calculate the best move for the CPU turn
    void CPU()
    {
        int currentDepth = 0;
        // Active AI if the playerturn is White
        Stopwatch stopWatch = new Stopwatch();
        int[] cpuBestMove = new int[2];
        // Start a stopwatch to calculate the runtime of the MiniMax method
        stopWatch.Start();
        // Get the best move from CalculateAIMove from MiniMax.cs
        cpuBestMove = MiniMax.CalculateAIMove(board2D, playerTurn, maxDepth, currentDepth, int.MinValue, int.MaxValue);
        // Stop the stopwatch to calculate the runtime of the MiniMax method
        stopWatch.Stop();
        // Remove the red tiles, since the AI doesn't need them
        ShowValidMoves();
        // Perform the move
        CPUTurn(cpuBestMove);
        // Calculate runtime of MiniMax
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("M{1:00}:S{2:00}.Mil{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        // Both method below is for debugging
        UnityEngine.Debug.Log(elapsedTime);
        ShowAIPoints(cpuBestMove);
    }

    // This method is used to show the help menu when the "HELP" button is pressed
    void ShowHelp()
    {
        HelpBoard.SetActive(true);
        ScoreBoard.SetActive(false);
        MainMenuButton.SetActive(false);
        BitboardDisplay.SetActive(false);
        BackButton.SetActive(false);
        HelpButton.SetActive(false);
        Text2Image.SetActive(false);
    }
}
