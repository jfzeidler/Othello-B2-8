using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class BoardRules
{
    private byte[,] bitboard;
    enum Player { blank = 0, black = 1, white = 2, capture = 5, validMove = 9};

    Vector2[] vectors = new Vector2[]
        {
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(-1, 0),
        new Vector2(0, -1),
        new Vector2(1, 1),
        new Vector2(-1, -1),
        new Vector2(-1, 1),
        new Vector2(1, -1)
        };

    public byte[,] _minimaxBitboard { get => bitboard; set => bitboard = value; }

    public void ValidMove(byte[,] bitboard, byte playerturn)
    {
        if (playerturn == (int)Player.black)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == (int)Player.white)
                    {
                        CheckForAdjacent(bitboard, i, j, playerturn);
                    }
                }
            }
        }

        else if (playerturn == (int)Player.white)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (bitboard[i, j] == (int)Player.black)
                    {
                        CheckForAdjacent(bitboard, i, j, playerturn);
                    }
                }
            }
        }
    }

    void CheckForAdjacent(byte[,] bitboard, int i, int j, byte playerturn)
    {
        if (playerturn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                if (InRange(getValueX + i, getValueY + j))
                {
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.white)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.white)
                                {
                                    l = 8;
                                }

                                else if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    bitboard[i - getValueX * l, j - getValueY * l] = (int)Player.validMove;
                                    l = 8;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (playerturn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                if (InRange(getValueX + i, getValueY + j))
                {
                    if (bitboard[i + getValueX, j + getValueY] == (int)Player.black)
                    {
                        for (int l = 1; l < 8; l++)
                        {
                            if (InRange(i - getValueX * l, j - getValueY * l))
                            {
                                if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.validMove || bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.black)
                                {
                                    l = 8;
                                }

                                else if (bitboard[i - getValueX * l, j - getValueY * l] == (int)Player.blank)
                                {
                                    bitboard[i - getValueX * l, j - getValueY * l] = (int)Player.validMove;
                                    l = 8;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void CaptureEnemyPlayer(byte[,] bitboard, int i, int j, byte playerturn)
    {
        if (playerturn == (int)Player.black)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                if (InRange(getValueX + i, getValueY + j))
                {
                    for (int l = 1; l < 8; l++)
                    {
                        if (InRange(i + getValueX * l, j + getValueY * l))
                        {
                            if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.validMove || bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.capture)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.black)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    bitboard[i + getValueX * l, j + getValueY * l] = (int)Player.capture;
                                }
                                l = 10;
                            }
                        }
                    }
                }
            }
        }

        if (playerturn == (int)Player.white)
        {
            for (int k = 0; k < 8; k++)
            {
                int getValueX = (int)vectors[k].X;
                int getValueY = (int)vectors[k].Y;
                Vector2 vector = vectors[k];

                if (InRange(getValueX + i, getValueY + j))
                {
                    for (int l = 1; l < 8; l++)
                    {
                        if (InRange(i + getValueX * l, j + getValueY * l))
                        {
                            if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.blank)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.validMove || bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.capture)
                            {
                                l = 10;
                            }

                            else if (bitboard[i + getValueX * l, j + getValueY * l] == (int)Player.white)
                            {
                                l--;
                                for (; l >= 1; l--)
                                {
                                    bitboard[i + getValueX * l, j + getValueY * l] = (int)Player.capture;
                                }
                                l = 10;
                            }
                        }
                    }
                }
            }
        }
    }

    bool InRange (int x, int y)
    {
        if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            return true;
        else
            return false;
    }

    public bool CheckForNine(byte[,] bitboard)
    {
        bool returnValue = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (bitboard[i, j] == 9)
                {
                    returnValue = false;
                }
            }
        }
        return returnValue;
    }

}

//Dette Skal over og så i sin egen .cs fil
public class MiniMax : BoardRules
{

    public int[,] Evaluation = {
        {10000, -3000,  1000,    800,    800, 1000, -3000, 10000},
        {-3000, -5000,  -450,   -500,   -500, -450, -5000, -3000},
        { 1000,  -450,    30,     10,     10,   30,  -450,  1000},
        {  800,  -500,    10,     50,     50,   10,  -500,   800},
        {  800,  -500,    10,     50,     50,   10,  -500,   800},
        { 1000,  -450,    30,     10,     10,   30,  -450,  1000},
        {-3000, -5000,  -450,   -500,   -500, -450, -5000, -3000},
        {10000, -3000,  1000,    800,    800, 1000, -3000, 10000}
    };

    void UpdateMinimaxBoard(byte[,] Bitboard)
    {
        _minimaxBitboard = Bitboard;
    }

    // Dette skal ikke være void, hvis den skal retunere et felt, hvor en brik skal placeres
    public byte[] ReturnRandomMove(byte[,] bitboard, int playerturn)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // If theres an available move, change the texture of the current tile
                if (bitboard[i, j] == 9)
                {
                    bitboard[i, j] = 0;
                }

                // If theres a captured piece, change the piece
                else if (bitboard[i, j] == 5)
                {
                    // Change the bitboard value to the current player
                    bitboard[i, j] = playerturn;
                }
            }
        }
        return bitboard;
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

// This is the evaluation function as discussed
// in the previous article ( http://goo.gl/sJgv68 )
int evaluate(char b[3][3])
{
    // Checking for Rows for X or O victory.
    for (int row = 0; row<3; row++)
    {
        if (b[row][0]==b[row][1] &&
            b[row][1]==b[row][2])
        {
            if (b[row][0]==player)
                return +10;
            else if (b[row][0]==opponent)
                return -10;
        }
    }

    // Checking for Columns for X or O victory.
    for (int col = 0; col<3; col++)
    {
        if (b[0][col]==b[1][col] &&
            b[1][col]==b[2][col])
        {
            if (b[0][col]==player)
                return +10;

            else if (b[0][col]==opponent)
                return -10;
        }
    }

    // Checking for Diagonals for X or O victory.
    if (b[0][0]==b[1][1] && b[1][1]==b[2][2])
    {
        if (b[0][0]==player)
            return +10;
        else if (b[0][0]==opponent)
            return -10;
    }

    if (b[0][2]==b[1][1] && b[1][1]==b[2][0])
    {
        if (b[0][2]==player)
            return +10;
        else if (b[0][2]==opponent)
            return -10;
    }

    // Else if none of them have won then return 0
    return 0;
}

// This is the minimax function. It considers all
// the possible ways the game can go and returns
// the value of the board
int minimax(char board[3][3], int depth, bool isMax)
{
    int score = evaluate(board);

    // If Maximizer has won the game return his/her
    // evaluated score
    if (score == 10)
        return score;

    // If Minimizer has won the game return his/her
    // evaluated score
    if (score == -10)
        return score;

    // If there are no more moves and no winner then
    // it is a tie
    if (isMovesLeft(board)==false)
        return 0;

    // If this maximizer's move
    if (isMax)
    {
        int best = -1000;

        // Traverse all cells
        for (int i = 0; i<3; i++)
        {
            for (int j = 0; j<3; j++)
            {
                // Check if cell is empty
                if (board[i][j]=='_')
                {
                    // Make the move
                    board[i][j] = player;

                    // Call minimax recursively and choose
                    // the maximum value
                    best = max( best,
                        minimax(board, depth+1, !isMax) );

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
        for (int i = 0; i<3; i++)
        {
            for (int j = 0; j<3; j++)
            {
                // Check if cell is empty
                if (board[i][j]=='_')
                {
                    // Make the move
                    board[i][j] = opponent;

                    // Call minimax recursively and choose
                    // the minimum value
                    best = min(best,
                           minimax(board, depth+1, !isMax));

                    // Undo the move
                    board[i][j] = '_';
                }
            }
        }
        return best;
    }
}

// This will return the best possible move for the player
Move findBestMove(char board[3][3])
{
    int bestVal = -1000;
    Move bestMove;
    bestMove.row = -1;
    bestMove.col = -1;

    // Traverse all cells, evaluate minimax function for
    // all empty cells. And return the cell with optimal
    // value.
    for (int i = 0; i<3; i++)
    {
        for (int j = 0; j<3; j++)
        {
            // Check if cell is empty
            if (board[i][j]=='_')
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

}
