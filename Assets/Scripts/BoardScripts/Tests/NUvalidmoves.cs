using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Numerics;

public class NUvalidmoves
{
    MiniMax MM = new MiniMax();
    public int[,] testboardnull = {
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
    public int[,]testboard = {
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
    [Test]
    public void NunitvalidMovesTest_null1board()
    {
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboardnull, 1);
        Assert.IsEmpty(testset);
    }

    [Test]
    public void NunitvalidMovesTest_notnull1()
    {
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboard, 1);
        Assert.IsNotNull(testset);
    }
    [Test]
    public void NunitvalidMovesTest_notnull2()
    {
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboard, 2);
        Assert.IsNotNull(testset);
    }
    [Test]
   public void NunitvalidMovesTest_correctlistsize1()
    {
        int sizeoftestset = 4;
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboard, 1);
        Assert.AreEqual(sizeoftestset, testset.Count);
    }
    [Test]
    public void NunitvalidMovesTest_correctlistsize1a()
    {
        int sizeoftestset = 4;
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboard, 2);
        Assert.AreEqual(sizeoftestset, testset.Count);
    }

    [Test]
    public void NunitvalidMovesTest_correctcoordinates1()
    {
        List<Vector2> excepted = new List<Vector2>();
        excepted.Add(new Vector2(2, 3));
        excepted.Add(new Vector2(3, 2));
        excepted.Add(new Vector2(4, 5));
        excepted.Add(new Vector2(5, 4));
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboard, 1);
        Assert.AreEqual(excepted, testset);
        
    }

    public int[,] testboard2 = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 9, 0, 0, 1, 0, 0}, // 2
        {0, 0, 0, 1, 1, 0, 0, 0}, // 3
        {0, 0, 0, 1, 1, 0, 0, 0}, // 4
        {0, 0, 9, 1, 1, 2, 0, 0}, // 5
        {0, 0, 0, 0, 1, 0, 2, 0}, // 6
        {0, 0, 0, 9, 1, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
    [Test]
    public void NunitvalidMovesTest_correctlistsize2()
    {
        int sizeoftestset = 3;
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboard2, 1);
        Assert.AreEqual(sizeoftestset, testset.Count);
    }

    public void NunitvalidMovesTest_correctcoordinates2()
    {
        List<Vector2> excepted = new List<Vector2>();
        excepted.Add(new Vector2(2, 1));
        excepted.Add(new Vector2(2, 4));
        excepted.Add(new Vector2(3, 6));
        List<Vector2> testset = new List<Vector2>();
        testset = MM.ValidMoves(testboard2, 1);
        Assert.AreEqual(excepted, testset);

    }
}
