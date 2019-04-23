using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class MoveStatus
{
    public int row, col, score;

    public MoveStatus(int Row, int Col, int Score)
    {
        row = Row;
        col = Col;
        score = Score;
    }
}
public class MiniMax : BoardRules
{
    public int[,] Evaluation = {
        // A       B      C       D       E     F      G      H
        {10000, -3000,  1000,    800,    800, 1000, -3000, 10000}, // 1
        {-3000, -5000,  -450,   -500,   -500, -450, -5000, -3000}, // 2
        { 1000,  -450,    30,     10,     10,   30,  -450,  1000}, // 3
        {  800,  -500,    10,     50,     50,   10,  -500,   800}, // 4
        {  800,  -500,    10,     50,     50,   10,  -500,   800}, // 5
        { 1000,  -450,    30,     10,     10,   30,  -450,  1000}, // 6
        {-3000, -5000,  -450,   -500,   -500, -450, -5000, -3000}, // 7
        {10000, -3000,  1000,    800,    800, 1000, -3000, 10000}  // 8
    };
    public int EvaluateBoard(byte[,] bitboard, byte playerturn)
    {
        List<Vector2> playerMoves = MoveList(bitboard, playerturn);
        List<Vector2> AIMoves = MoveList(bitboard, playerturn);

        int playerScore = 0;
        int AIScore = 0;

        foreach(Vector2 move in playerMoves)
        {
            playerScore += Evaluation[(int)move.X, (int)move.Y];
        }
        foreach (Vector2 move in playerMoves)
        {
            AIScore += Evaluation[(int)move.X, (int)move.Y];
        }
        int result = 0;

        if(playerturn == 1)
        {
            result = playerScore - AIScore;
        }
        else if(playerturn == 2)
        {
            result = AIScore - playerScore;
        }

        return -1; //PLACEHOLDER
    }
    public List<Vector2> MoveList(byte[,] bitboard, byte playerturn)
    {
        List<Vector2> moveList = new List<Vector2>();
        if (playerturn == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(bitboard[i,j] == 9)
                    {
                        moveList.Add(new Vector2(i, j));
                    }
                }
            }
        }
        else if(playerturn == 2)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == 9)
                    {
                        moveList.Add(new Vector2(i, j));
                    }
                }
            }
        }
        return moveList;
    }
    public MoveStatus MiniMaxAlgorithm(byte[,] bitboard, byte playerturn, int maxDepth, int currentDepth)
    {
        if(currentDepth == maxDepth)
        {
            return new MoveStatus(-1, -1, EvaluateBoard(bitboard, playerturn));
        }
        else
            return new MoveStatus(-1, -1, EvaluateBoard(bitboard, playerturn)); //PLACEHOLDER

    }

    void UpdateMinimaxBoard(byte[,] Bitboard)
    {
        _minimaxBitboard = Bitboard;
    }

    public byte[] ReturnRandomMove(byte[,] bitboard, int playerturn)
    {
        List<byte[]> PossibleMoves = new List<byte[]>();

        _minimaxBitboard = bitboard;
        byte[] CPUBestMove = new byte[2];
        CPUBestMove[0] = 90; CPUBestMove[1] = 90;
        
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (_minimaxBitboard[i, j] == 9)
                {
                    byte[] CPUPossibleMove = new byte[2];
                    CPUPossibleMove[0] = (byte)i; CPUPossibleMove[1] = (byte)j;
                    PossibleMoves.Add(CPUPossibleMove);
                }
            }
        }

        int tempCPUBestMove = 0;

        foreach (byte[] possibleMove in PossibleMoves)
        {
            if (Evaluation[possibleMove[0], possibleMove[1]] > tempCPUBestMove || tempCPUBestMove == 0)
            {
                tempCPUBestMove = Evaluation[possibleMove[0], possibleMove[1]];
                CPUBestMove = possibleMove;
            }
        }

        return CPUBestMove;
        // if [i,j] == 9, sæt en brik
    }
}


/*
public class AIReversi {

	public GameController gameController = new GameController ();

	public int[,] evalFun = {
		{  30, -25, 10, 5, 5, 10, -25,  30 },
		{ -25, -25,  1, 1, 1,  1, -25, -25 },
		{  10,   1,  5, 2, 2,  5,   1,  10 },
		{   5,   1,  2, 1, 1,  2,   1,   5 },
		{   5,   1,  2, 1, 1,  2,   1,   5 },
		{  10,   1,  5, 2, 2,  5,   1,  10 },
		{ -25, -25,  1, 1, 1,  1, -25, -25 },
		{  30, -25, 10, 5, 5, 10, -25,  30 }
	};

	// evaluate the board
	public int EvaluateBoard(int[,] board, int color)
	{
		List<Vector2> playerMoves = gameController.MoveList (board, 1);
		List<Vector2> AIMoves = gameController.MoveList (board, 2);
		int playerScore = 0;
		int AIScore = 0;

		foreach (Vector2 move in playerMoves) {
			playerScore += evalFun [(int)move.x, (int)move.y];
		}
		foreach (Vector2 move in AIMoves) {
			AIScore += evalFun [(int)move.x, (int)move.y];
		}
		//int playerPieces = gameController.CountPieces (board, 1);
		//int AIPieces = gameController.CountPieces (board, 2);
		int result = 0;

		if (color == 1) {
			result = playerScore - AIScore;
		}
		if (color == 2) {
			result = AIScore - playerScore;
		}
		return result;
	}


	// Minimax algorithm with alpha-beta pruning
	public MoveStatus MiniMaxAlphaBeta(int[,] board, int color, int maxDepth, int currentDepth, int alpha, int beta)
	{
		if (gameController.IsGameOver () || currentDepth == maxDepth) {
			return new MoveStatus (-1, -1, EvaluateBoard (board, color));
		}
		MoveStatus ms = new MoveStatus (-1, -1, 0);
		if (color == 2) {
			ms.score = int.MinValue;
		} else
			ms.score = int.MaxValue;
		List<Vector2> allNextMoves;
		// this is the AI turn right now
		if (color == 2) {
			allNextMoves = gameController.MoveList (board, 2);
			foreach (Vector2 move in allNextMoves) {
				int[,] newBoard = gameController.GetNextBoard (board, 2, (int)move.x, (int)move.y);
				int score = MiniMaxAlphaBeta (newBoard, 1, maxDepth, currentDepth + 1, alpha, beta).score;
				if (score > ms.score) {
					ms.row = (int)move.x;
					ms.col = (int)move.y;
					ms.score = score;
					if (ms.score > alpha)
						alpha = ms.score;
					if (alpha >= beta)
						break;

				}
			}
			// this is the human turn right now
		} else {
			allNextMoves = gameController.MoveList (board, 1);
			foreach (Vector2 move in allNextMoves) {
				int[,] newBoard = gameController.GetNextBoard (board, 1, (int)move.x, (int)move.y);
				int score = MiniMaxAlphaBeta (newBoard, 2, maxDepth, currentDepth + 1, alpha, beta).score;
				if (score < ms.score) {
					ms.row = (int)move.x;
					ms.col = (int)move.y;
					ms.score = score;
					if (ms.score < beta)
						beta = ms.score;
					if (beta <= alpha)
						break;
				}
			}
		}
		return ms;
	}
}*/
