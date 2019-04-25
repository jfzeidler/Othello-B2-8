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
    public int EvaluateBoard(int[,] bitboard, int playerturn)
    {
        List<Vector2> playerMoves = MoveList(bitboard, 1);
        List<Vector2> AIMoves = MoveList(bitboard, 2);

        int playerScore = 0;
        int AIScore = 0;
        int result = 0;

        foreach(Vector2 move in playerMoves)
        {
            playerScore += Evaluation[(int)move.X, (int)move.Y];
        }
        foreach (Vector2 move in AIMoves)
        {
            AIScore += Evaluation[(int)move.X, (int)move.Y];
        }

        if (playerturn == 1)
        {
            result = (playerScore - AIScore);
        }
        else if (playerturn == 2)
        {
            result = (AIScore - playerScore);
        }
        return result;
    }

    public List<Vector2> MoveList(int[,] bitboard, int playerturn)
    {
        List<Vector2> list = new List<Vector2>();
        if (playerturn == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(bitboard[i,j] == 9)
                    {
                        list.Add(new Vector2(i, j));
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
                        list.Add(new Vector2(i, j));
                    }
                }
            }
        }
        return list;
    }

    public MoveStatus MiniMaxAlgorithm(int[,] bitboard, int playerturn, int maxDepth, int currentDepth, int alpha, int beta)
    {
        if (currentDepth == maxDepth )
        {
            return new MoveStatus(-1, -1, EvaluateBoard(bitboard, playerturn));
        }

        MoveStatus ms = new MoveStatus(-1, -1, 0);

        if(playerturn == 2)
        {
            ms.score = int.MinValue;
        }
        else if (playerturn == 1)
        {
            ms.score = int.MaxValue;
        }
        List<Vector2> allMoves;

        if(playerturn == 2)
        {
            allMoves = MoveList(bitboard, 2);
            foreach(Vector2 move in allMoves)
            {
                int[,] newBoard = GetNextBoardState(bitboard, playerturn, (int)move.X, (int)move.Y);
                int score = MiniMaxAlgorithm(newBoard, 1, maxDepth, (currentDepth + 1), alpha, beta).score;
                if (score > ms.score)
                {
                    ms.row = (int)move.X;
                    ms.col = (int)move.Y;
                    ms.score = score;
                    if (ms.score > alpha)
                        alpha = ms.score;
                    if (alpha >= beta)
                        break;
                }
            }
        }
        else if(playerturn == 1)
        {
            allMoves = MoveList(bitboard, 1);
            foreach (Vector2 move in allMoves)
            {
                int[,] newBoard = GetNextBoardState(bitboard, playerturn, (int)move.X, (int)move.Y);
                int score = MiniMaxAlgorithm(newBoard, 2, maxDepth, (currentDepth + 1), alpha, beta).score;
                if (score < ms.score)
                {
                    ms.row = (int)move.X;
                    ms.col = (int)move.Y;
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

    public int[] CalculateAIMove(int[,] bitboard, int playerturn, int maxDepth, int currentDepth, int alpha, int beta)
    {
        int[] result = new int[3];
        MoveStatus bestMove = MiniMaxAlgorithm(bitboard, playerturn, maxDepth, currentDepth, alpha, beta);
        result[0] = (int)bestMove.row;
        result[1] = (int)bestMove.col;
        result[2] = bestMove.score;
        return result;
    }
}
