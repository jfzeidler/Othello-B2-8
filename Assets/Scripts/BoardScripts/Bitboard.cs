using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Bitboard : MonoBehaviour
{
    public BoardRules BoardState = new BoardRules();
    public MiniMax MiniMax = new MiniMax();
    public enum Player { blank = 0, black = 1, white = 2};
    public int Blackpieces = 0, Whitepieces = 0;
    public byte playerturn = 1;
    public byte[,] bitboard = new byte[8, 8];

    public GameObject Tiles;
    public GameObject AndTheWinnerIs;
    public GameObject GameOverCanvas;
    public GameObject spawnWhitePlayer;
    public GameObject spawnDarkPlayer;
    public GameObject gameObjectToFlip;
    public GameObject BitboardDisplay;
    public GameObject Blackcountertext;
    public GameObject Whitecountertext;
    public GameObject ScorePanelTurnText;


    // Start is called before the first frame update
    void Start()
    {
        BitboardDisplay.GetComponent<Text>().text = "     AB CD EF GH";
        for (int i = 0; i < 8; i++)
        {
            BitboardDisplay.GetComponent<Text>().text += "\n " + (i + 1) + " [";
            for (int j = 0; j < 8; j++)
            {
                bitboard[i, j] = 0;
                BitboardDisplay.GetComponent<Text>().text += bitboard[i, j] + " ";
            }
            BitboardDisplay.GetComponent<Text>().text += "]";
        }
        bitboard[3, 4] = 1; bitboard[4, 3] = 1; bitboard[3, 3] = 2; bitboard[4, 4] = 2;
        BoardState.ValidMove(bitboard, playerturn);
        bitboardDisplayUpdate();
        ShowValidMoves();
        bitboardDisplayUpdate();
        PieceCounter(bitboard);
        ShowPlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void boardRules(int bitboardX, int bitboardY)
    {
        BoardState.CaptureEnemyPlayer(bitboard, bitboardY, bitboardX, playerturn);
        bitboardResetTurn();
    }

    public void bitboardUpdate()
    {
        PieceCounter(bitboard);
        BoardState.ValidMove(bitboard, playerturn);
        playerturn = PassCounter(bitboard, playerturn);
        bitboardDisplayUpdate();
        ShowValidMoves();
        ShowPlayerTurn();
        IsGameOver(bitboard, playerturn, Whitepieces, Blackpieces);
        
        if (playerturn == (int)Player.white)
        {
            byte[] CPUBestMove = new byte[2];
            CPUBestMove = MiniMax.ReturnRandomMove(bitboard, playerturn);
            CPUTurn(CPUBestMove);
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
                if (bitboard[i, j] == 9)
                {
                    int temp = j + 65;
                    char c = (char)temp;
                    string s = c + System.Convert.ToString(i + 1 + "/TileVisual");

                    GameObject.Find(s).GetComponent<SwapTextures>().TextureSwap();
                    bitboard[i, j] = 0;
                }

                else if (bitboard[i, j] == 5)
                {
                    flipIt(i, j);
                    bitboard[i, j] = playerturn;
                }
            }
        }
    }

    void flipIt(int i, int j)
    {
        string playerToFlip = "Player-" + j + i;
        Vector3 vectorPos = new Vector3(i, -0.85f, j);
        gameObjectToFlip = GameObject.Find(playerToFlip);
        Debug.Log(playerToFlip);
        Destroy(gameObjectToFlip);

        if (playerturn == 2)
        {
            var newObject = Instantiate(spawnWhitePlayer, vectorPos, Quaternion.identity);
            newObject.name = "Player-" + j + i;
        }

        if (playerturn == 1)
        {
            var newObject = Instantiate(spawnDarkPlayer, vectorPos, Quaternion.identity);
            newObject.name = "Player-" + j + i;
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
                    string s = c + System.Convert.ToString(i + 1 + "/TileVisual");
                    GameObject.Find(s).GetComponent<SwapTextures>().TextureSwap();
                }
            }
        }
    }

    void IsGameOver(byte[,] bitboard, byte playerturn, int Whitepieces, int Blackpieces)
    {
        //Debug.Log("W:" + Whitepieces + "| B:" + Blackpieces);
        if (Blackpieces > Whitepieces)
        {
            AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Player Black Won";
        }

        else if(Whitepieces > Blackpieces)
        {
            AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Player White Won";
        }

        else if (Whitepieces == Blackpieces)
        {
            AndTheWinnerIs.GetComponent<TextMeshProUGUI>().text = "Draw";
        }

        if (BoardState.CheckForNine(bitboard) == true)
        {
            Debug.Log("Game over");
            GameOverCanvas.SetActive(true);
        }
    }

    public byte PassCounter(byte[,] bitboard, byte playerturn)
    {
        if (BoardState.CheckForNine(bitboard) == true)
        {
            if (playerturn == 1)
            {
                playerturn = 2;
                Debug.Log("No valid moves for black player");
            }

            else if (playerturn == 2)
            {
                playerturn = 1;
                Debug.Log("No valid moves for white player");
            }
            Debug.Log("Made it here");
            BoardState.ValidMove(bitboard, playerturn);
        }
        return playerturn;
    }
    
    void CPUTurn(byte[] CPUBestMove)
    {
        if (CPUBestMove[0] < 8 && CPUBestMove[1] < 8)
        {
            int temp = CPUBestMove[1] + 65;
            char c = (char)temp;
            Debug.Log("CPU: " + (CPUBestMove[0] + 1) + " " + c);
            Tiles.GetComponent<tileScript>().MakeMove(CPUBestMove[1], CPUBestMove[0], playerturn);
        }
    }
}
