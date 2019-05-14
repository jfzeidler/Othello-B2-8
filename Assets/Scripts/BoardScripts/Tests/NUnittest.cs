using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class NUnittest
{
    enum Player { blank = 0, black = 1, white = 2, capture = 5, validMove = 9 };

    BoardRules BR = new BoardRules();
    Bitboard BB = new Bitboard();
    MiniMax Maximin = new MiniMax();
    // A Test behaves as an ordinary method
    [Test]
    public void NUnitInRangeTest()
    {
        Assert.IsFalse(BR.InRange(8, 8));
        // Use the Assert class to test conditions
    }

    [Test]
    public void NUnitCaptureEnemyPlayerTest()
    {
        int[,] Captureboard2D = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 1, 0, 0, 0}, // 3
        {0, 0, 1, 2, 1, 2, 0, 0}, // 4
        {0, 0, 0, 2, 2, 0, 0, 0}, // 5
        {0, 0, 0, 2, 1, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
        };

        int[,] Capturedboard2D = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 1, 0, 0, 0}, // 3
        {0, 0, 1, 5, 1, 2, 0, 0}, // 4
        {0, 0, 0, 5, 2, 0, 0, 0}, // 5
        {0, 0, 0, 2, 1, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
        };

        Assert.AreEqual(Capturedboard2D, BR.CaptureEnemyPlayer(Captureboard2D, 3, 2, (int)Player.black));
        // Use the Assert class to test conditions
    }

    [Test]
    public void NUnitScoreCounterTest()
    {
        int[,] Captureboard2D = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 1, 0, 0, 0}, // 3
        {0, 0, 1, 1, 1, 2, 0, 0}, // 4
        {0, 0, 0, 1, 2, 0, 0, 0}, // 5
        {0, 0, 0, 2, 1, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
        };

        byte[] Counted = new byte[2];
        Counted[0] = 6;
        Counted[1] = 3;
        Assert.AreEqual(Counted, BB.PieceCounter(Captureboard2D));
    }

    [Test]
    public void NUnitIsGameOverTest()
    {
        int[,] board2D = {
    // A  B  C  D  E  F  G  H
    {0, 0, 0, 0, 0, 0, 0, 0}, // 1
    {0, 0, 0, 0, 0, 0, 0, 0}, // 2
    {0, 0, 0, 9, 0, 0, 0, 0}, // 3
    {0, 0, 9, 2, 1, 0, 0, 0}, // 4
    {0, 0, 0, 1, 2, 9, 0, 0}, // 5
    {0, 0, 0, 0, 9, 0, 0, 0}, // 6
    {0, 0, 0, 0, 0, 0, 0, 0}, // 7
    {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };

        int playerTurn = 1;
        int whitePieces = 2;
        int blackPieces = 2;

        StringAssert.AreEqualIgnoringCase("Not Over", BB.IsGameOver(board2D, playerTurn, whitePieces, blackPieces));
        // Use the Assert class to test conditions

    }

    [Test]
    public void NUnitPassTurnTest()
    {
        int[,] passBoard2D = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 1, 0, 0, 0}, // 3
        {0, 0, 1, 1, 1, 2, 0, 0}, // 4
        {0, 0, 0, 1, 2, 0, 0, 0}, // 5
        {0, 0, 0, 2, 1, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
        };

        Assert.AreEqual((int)Player.white, BB.PassCounter((int[,])passBoard2D.Clone(), (int)Player.black));
        Assert.AreEqual((int)Player.black, BB.PassCounter((int[,])passBoard2D.Clone(), (int)Player.white));
    }

    [Test]
    public void NUnitBoardOnlyResetTurnTest()
    {
        int[,] board2D = {
    // A  B  C  D  E  F  G  H
    {0, 0, 0, 0, 0, 0, 0, 0}, // 1
    {0, 0, 0, 0, 0, 0, 0, 0}, // 2
    {0, 0, 0, 9, 0, 0, 0, 0}, // 3
    {0, 0, 1, 5, 1, 0, 0, 0}, // 4
    {0, 0, 0, 1, 2, 9, 0, 0}, // 5
    {0, 0, 0, 0, 9, 0, 0, 0}, // 6
    {0, 0, 0, 0, 0, 0, 0, 0}, // 7
    {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
        int[,] board2DCorrect = {
    // A  B  C  D  E  F  G  H
    {0, 0, 0, 0, 0, 0, 0, 0}, // 1
    {0, 0, 0, 0, 0, 0, 0, 0}, // 2
    {0, 0, 0, 0, 0, 0, 0, 0}, // 3
    {0, 0, 1, 1, 1, 0, 0, 0}, // 4
    {0, 0, 0, 1, 2, 0, 0, 0}, // 5
    {0, 0, 0, 0, 0, 0, 0, 0}, // 6
    {0, 0, 0, 0, 0, 0, 0, 0}, // 7
    {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
        int playerTurn = 1;
        Assert.AreEqual(board2DCorrect, BR.boardOnlyResetTurn(board2D, playerTurn));
        // Use the Assert class to test conditions

    }

    [Test]
    public void NUnitGetNextBoardStateBlackTest()
    {
        int[,] Nextboard2D = {
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

        int[,] Expectedboard2D = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 0, 0, 0, 0}, // 3
        {0, 0, 0, 2, 1, 9, 0, 0}, // 4
        {0, 0, 0, 1, 1, 0, 0, 0}, // 5
        {0, 0, 0, 9, 1, 9, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
        Assert.AreEqual(Expectedboard2D, BR.GetNextBoardState(Nextboard2D, (int)Player.black, 4, 5));
    }

    [Test]
    public void NUnitGetNextBoardStatewhiteTest()
    {
        int[,] Nextboard2D = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 0, 0, 0, 0}, // 3
        {0, 0, 0, 2, 1, 9, 0, 0}, // 4
        {0, 0, 0, 1, 1, 0, 0, 0}, // 5
        {0, 0, 0, 9, 1, 9, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };

        int[,] Expectedboard2D = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 9, 9, 9, 9, 9, 0}, // 3
        {0, 0, 0, 2, 2, 2, 0, 0}, // 4
        {0, 0, 0, 1, 1, 0, 0, 0}, // 5
        {0, 0, 0, 0, 1, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
        Assert.AreEqual(Expectedboard2D, BR.GetNextBoardState(Nextboard2D, (int)Player.white, 5, 3));
    }

    [Test]
    public void NUnitEvaluateBoardTest()
    {
        int[,] board2D = {
    // A  B  C  D  E  F  G  H
    {0, 0, 0, 0, 0, 0, 0, 0}, // 1
    {0, 1, 0, 0, 0, 0, 0, 0}, // 2
    {0, 0, 1, 1, 0, 0, 0, 0}, // 3
    {0, 0, 2, 1, 1, 0, 0, 0}, // 4
    {0, 0, 2, 2, 2, 0, 0, 0}, // 5
    {0, 0, 0, 0, 0, 0, 0, 0}, // 6
    {0, 0, 0, 0, 0, 0, 0, 0}, // 7
    {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
        int playerTurn = 1;
        int correct = -4980;
        Assert.AreEqual(correct, Maximin.EvaluateBoard(board2D, playerTurn));
        // Use the Assert class to test conditions

    }
}

