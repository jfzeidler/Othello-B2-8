using System;
using System.Collections;
using System.Collections.Generic;

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

// C++ program to find the next optimal move for 
// a player 
#include<bits/stdc++.h> 
using namespace std; 
  
struct Move 
{ 
    int row, col; 
}; 
  
char player = 'x', opponent = 'o'; 
  
// This function returns true if there are moves 
// remaining on the board. It returns false if 
// there are no moves left to play. 
bool isMovesLeft(char board[3][3]) 
{ 
    for (int i = 0; i<3; i++) 
        for (int j = 0; j<3; j++) 
            if (board[i][j]=='_') 
                return true; 
    return false; 
}
  
// Driver code 
int main() 
{ 
    char board[3][3] = 
    { 
        { 'x', 'o', 'x' }, 
        { 'o', 'o', 'x' }, 
        { '_', '_', '_' } 
    }; 
  
    Move bestMove = findBestMove(board); 
  
    printf("The Optimal Move is :\n"); 
    printf("ROW: %d COL: %d\n\n", bestMove.row, 
                                  bestMove.col ); 
    return 0; 
} 

    // This will return the best possible move for the player 
    return findBestMove(char board[8][8])
    {
        int bestVal = -1000;
        bestMove;
        bestMove.row = -1;
        bestMove.col = -1;

        // Traverse all cells, evaluate minimax function for 
        // all empty cells. And return the cell with optimal 
        // value. 
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Check if cell is empty 
                if (board[i][j] == '_')
                {
                    // Make the move 
                    board[i][j] = player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(board, 0, false);

                    // Undo the move 
                    board[i][j] = '_';

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        printf("The value of the best Move is : %d\n\n",
                bestVal);

        return bestMove;
    }

    // This is the minimax function. It considers all 
    // the possible ways the game can go and returns 
    // the value of the board 
    int minimax(char board[3][3], int depth, bool isMax)
    {
        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i][j] == '_')
                    {
                        // Make the move 
                        board[i][j] = player;

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = max(best,
                            minimax(board, depth + 1, !isMax));

                        // Undo the move 
                        board[i][j] = '_';
                    }
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i][j] == '_')
                    {
                        // Make the move 
                        board[i][j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = min(best,
                               minimax(board, depth + 1, !isMax));

                        // Undo the move 
                        board[i][j] = '_';
                    }
                }
            }
            return best;
        }
    }

    */
