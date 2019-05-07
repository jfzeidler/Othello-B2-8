using System;
using System.IO;
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
    string fileName = @"C:\temp\MINIMAX_DEBUG.txt";

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

    readonly int[,] Evaluation2 = {
        {  30, -25, 10, 5, 5, 10, -25,  30 },
        { -25, -25,  1, 1, 1,  1, -25, -25 },
        {  10,   1,  5, 2, 2,  5,   1,  10 },
        {   5,   1,  2, 1, 1,  2,   1,   5 },
        {   5,   1,  2, 1, 1,  2,   1,   5 },
        {  10,   1,  5, 2, 2,  5,   1,  10 },
        { -25, -25,  1, 1, 1,  1, -25, -25 },
        {  30, -25, 10, 5, 5, 10, -25,  30 }
    };

    // This method is used to calculate the score of the branch in Minimax
    public int EvaluateBoard(int[,] bitboard, int playerturn)
    {
        int playerScore = 0;
        int AIScore = 0;
        int result = 0;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bitboard[i, j] == 1)
                {
                    playerScore += Evaluation2[i, j];
                }

                else if (bitboard[i, j] == 2)
                {
                    AIScore += Evaluation2[i, j];
                }
            }
        }

        if (playerturn == 1)
        {
            result = (playerScore - AIScore);
            //File.AppendAllText(fileName, Environment.NewLine + "RESULT FOR BLACK: " + result + Environment.NewLine);
        }

        else if (playerturn == 2)
        {
            result = (AIScore - playerScore);
            //File.AppendAllText(fileName, Environment.NewLine + "RESULT FOR WHITE: " + result + Environment.NewLine);
        }
        return result;
    }

    // This is a list of all possible moves for at given player
    public List<Vector2> MoveList(int[,] bitboard, int playerturn)
    {
        List<Vector2> list = new List<Vector2>();
        if (playerturn == 1)
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

        else if (playerturn == 2)
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

    // This is the MiniMax algorithm
    public MoveStatus MiniMaxAlgorithm(int[,] bitboard, int playerturn, int maxDepth, int currentDepth, int alpha, int beta)
    {
        // If game over or maxDepth is reached
        if (CheckForNine(bitboard) == true || currentDepth == maxDepth)
        {
            File.AppendAllText(fileName, Environment.NewLine + "Return" + Environment.NewLine);
            return new MoveStatus(-1, -1, EvaluateBoard(bitboard, playerturn));
        }

        MoveStatus ms = new MoveStatus(-1, -1, 0);

        // If white turn (Minimizer)
        if (playerturn == 2)
        {
            ms.score = int.MinValue;
        }

        // If black turn (Maximizer)
        else if (playerturn == 1)
        {
            ms.score = int.MaxValue;
        }

        List<Vector2> allMoves;

        if (playerturn == 2)
        {
            allMoves = MoveList(bitboard, 2);

            // Calculate a move for each vector in allMoves
            foreach (Vector2 move in allMoves)
            {
                //File.AppendAllText(fileName, "Chosen VECTOR WHITE: " + (int)move.X + " " + (int)move.Y + Environment.NewLine);
                // Get the next board for MiniMax
                int[,] newBoard = GetNextBoardState(bitboard, playerturn, (int)move.X, (int)move.Y);
                // Get the score from the next MiniMax algorithm
                int score = MiniMaxAlgorithm(newBoard, 1, maxDepth, (currentDepth + 1), alpha, beta).score;

                // Alpha
                if (score > ms.score)
                {
                    ms.row = (int)move.X;
                    ms.col = (int)move.Y;
                    ms.score = score;
                    //File.AppendAllText(fileName, "BEST VECTOR MOVE WHITE FOR NOW:" + ms.row + " " + ms.col + " " + ms.score + Environment.NewLine + Environment.NewLine);
                    if (ms.score > alpha)
                        alpha = ms.score;
                    if (alpha >= beta)
                        break;
                }
            }
        }
        else if (playerturn == 1)
        {
            allMoves = MoveList(bitboard, 1);

            // Calculate a move for each vector in allMoves
            foreach (Vector2 move in allMoves)
            {
                //File.AppendAllText(fileName, "Chosen VECTOR BLACK: " + (int)move.X + " " + (int)move.Y + Environment.NewLine);
                // Get the next board for MiniMax
                int[,] newBoard = GetNextBoardState(bitboard, playerturn, (int)move.X, (int)move.Y);
                // Get the score from the next MiniMax algorithm
                int score = MiniMaxAlgorithm(newBoard, 2, maxDepth, (currentDepth + 1), alpha, beta).score;

                // Beta
                if (score < ms.score)
                {
                    ms.row = (int)move.X;
                    ms.col = (int)move.Y;
                    ms.score = score;
                    //File.AppendAllText(fileName, "BEST VECTOR MOVE BLACK FOR NOW:" + ms.row + " " + ms.col + " " + ms.score + Environment.NewLine + Environment.NewLine);
                    if (ms.score < beta)
                        beta = ms.score;
                    if (beta <= alpha)
                        break;
                }
            }
        }
        return ms;
    }

    // This method is used to run the Minimax algorithm
    public int[] CalculateAIMove(int[,] bitboard, int playerturn, int maxDepth, int currentDepth, int alpha, int beta)
    {
        int[] result = new int[3];
        // Get the best move from MiniMax
        MoveStatus bestMove = MiniMaxAlgorithm(bitboard, playerturn, maxDepth, currentDepth, alpha, beta);
        result[0] = (int)bestMove.row;
        result[1] = (int)bestMove.col;
        result[2] = bestMove.score;
        //File.AppendAllText(fileName, "BEST VECTOR MOVE:" + bestMove.row + bestMove.col + bestMove.score + Environment.NewLine);
        return result;
    }

    // This method is used to prepare MiniMax for debugging
    public void PrepareDebugForMinimax()
    {
        // Check if file already exists. If yes, delete it.     
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        using (StreamWriter sw = File.CreateText(fileName))
        {
            sw.WriteLine("DEBUGGING;");
        }
    }
}
