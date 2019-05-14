using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System;

public class Move
{
    public int row, col, score;

    public Move(int x, int y, int Score)
    {
        row = x;
        col = y;
        score = Score;
    }
}

public class MiniMax : BoardRules
{
    string fileName = @"C:\temp\MINIMAX_DEBUG.txt";
    readonly bool alphaBetaOn = true;

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

    // This method is used to calculate the score of the branch in Minimax
    public int EvaluateBoard(int[,] board2D, int playerTurn)
    {
        int Minimizer = 0;
        int Maximizer = 0;
        int result = 0;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board2D[i, j] == 1)
                    Minimizer += evaluation[i, j];

                else if (board2D[i, j] == 2)
                    Maximizer += evaluation[i, j];
            }
        }

        //if (playerTurn == 1)
        //    result = (Minimizer - Maximizer);

        //else if (playerTurn == 2)
            result = (Maximizer - Minimizer);

        return result;
    }

    // This is a list of all possible moves for at given player
    public List<Vector2> ValidMoves(int[,] board2D, int playerTurn)
    {
        List<Vector2> list = new List<Vector2>();

        for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
            {
                if (board2D[i, j] == 9)
                {
                    list.Add(new Vector2(i, j));
                }
            }
        }
        return list;
    }

    // This is the MiniMax algorithm
    public Move MiniMaxAlgorithm(int[,] board2D, int playerTurn, int maxDepth, int currentDepth, int alpha, int beta)
    {
        // If game over or maxDepth is reached
        if (CheckForNine(board2D) == true || currentDepth == maxDepth)
        {
            //File.AppendAllText(fileName, Environment.NewLine + "Return" + Environment.NewLine);
            return new Move(-1, -1, EvaluateBoard(board2D, playerTurn));
        }

        Move selectedMove = new Move(-1, -1, 0);

        // If white turn (Maximizer)
        if (playerTurn == 2)
            selectedMove.score = int.MinValue;

        // If black turn (Minimizer)
        else if (playerTurn == 1)
            selectedMove.score = int.MaxValue;

        List<Vector2> allMoves;

        if (playerTurn == 2)
        {
            allMoves = ValidMoves(board2D, 2);

            // Calculate a move for each vector in allMoves
            foreach (Vector2 move in allMoves)
            {
                //File.AppendAllText(fileName, "Chosen VECTOR WHITE: " + (int)move.X + " " + (int)move.Y + Environment.NewLine);
                // Get the next board for MiniMax
                int[,] newBoard = GetNextBoardState(board2D, playerTurn, (int)move.X, (int)move.Y);
                // Get the score from the next MiniMax algorithm
                int score = MiniMaxAlgorithm(newBoard, 1, maxDepth, (currentDepth + 1), alpha, beta).score;

                if (score > selectedMove.score)
                {
                    selectedMove.row = (int)move.X;
                    selectedMove.col = (int)move.Y;
                    selectedMove.score = score;
                    //File.AppendAllText(fileName, "BEST VECTOR MOVE WHITE FOR NOW:" + (int)move.X + " " + (int)move.Y + " " + score + Environment.NewLine + Environment.NewLine);

                    // Alpha
                    if (alphaBetaOn == true)
                    {
                        if (selectedMove.score > alpha)
                            alpha = selectedMove.score;
                        if (alpha >= beta)
                            break;
                    }
                }
            }
        }
        else if (playerTurn == 1)
        {
            allMoves = ValidMoves(board2D, 1);

            // Calculate a move for each vector in allMoves
            foreach (Vector2 move in allMoves)
            {
                //File.AppendAllText(fileName, "Chosen VECTOR BLACK: " + (int)move.X + " " + (int)move.Y + Environment.NewLine);
                // Get the next board for MiniMax
                int[,] newBoard = GetNextBoardState(board2D, playerTurn, (int)move.X, (int)move.Y);
                // Get the score from the next MiniMax algorithm
                int score = MiniMaxAlgorithm(newBoard, 2, maxDepth, (currentDepth + 1), alpha, beta).score;

                if (score < selectedMove.score)
                {
                    selectedMove.row = (int)move.X;
                    selectedMove.col = (int)move.Y;
                    selectedMove.score = score;
                    //File.AppendAllText(fileName, "BEST VECTOR MOVE BLACK FOR NOW:" + (int)move.X + " " + (int)move.Y + " " + score + Environment.NewLine + Environment.NewLine);
                    // Beta
                    if (alphaBetaOn == true)
                    {
                        if (selectedMove.score < beta)
                            beta = selectedMove.score;
                        if (beta <= alpha)
                            break;
                    }
                }
            }
        }
        return selectedMove;
    }

    // This method is used to run the Minimax algorithm
    public int[] CalculateAIMove(int[,] board2D, int playerTurn, int maxDepth, int currentDepth, int alpha, int beta)
    {
        int[] result = new int[3];
        // Get the best move from MiniMax
        //File.AppendAllText(fileName, Environment.NewLine + "Run Minimax" + Environment.NewLine);
        Move bestMove = MiniMaxAlgorithm(board2D, playerTurn, maxDepth, currentDepth, alpha, beta);
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
            File.Delete(fileName);

        using (StreamWriter sw = File.CreateText(fileName))
            sw.WriteLine("DEBUGGING;");
    }
}
