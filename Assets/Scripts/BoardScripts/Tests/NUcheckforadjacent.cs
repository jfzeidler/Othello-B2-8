using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

class NUnittestCheckForAdjacent
{
    BoardRules BR = new BoardRules();
    public int[,] testboard1 = {
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
    public int[,] expectedboard1 = {
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
    public void NUnitAdjacentTest1()
    {
        //Arrange
        testboard1 = BR.ValidMove(testboard1, 1);

        Assert.AreEqual(expectedboard1, testboard1);
        // Use the Assert class to test conditions
    }
    [Test]
    public void NUnitAdjacentTest1a()
    {
        //Arrange
        testboard1 = BR.ValidMove(testboard1, 2);

        Assert.AreNotEqual(expectedboard1, testboard2);
        // Use the Assert class to test conditions
    }

    public int[,] testboard2 = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 1, 0, 0, 0, 0}, // 3
        {0, 0, 0, 1, 1, 0, 0, 0}, // 4
        {0, 0, 0, 1, 2, 0, 0, 0}, // 5
        {0, 0, 0, 0, 0, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
    public int[,] expectedboard2 = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 9, 1, 9, 0, 0, 0}, // 3
        {0, 0, 0, 1, 1, 0, 0, 0}, // 4
        {0, 0, 9, 1, 2, 0, 0, 0}, // 5
        {0, 0, 0, 0, 0, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
    [Test]
    public void NUnitAdjacentTest2()
    {
        //Arrange
        testboard2 = BR.ValidMove(testboard2, 2);

        Assert.AreEqual(expectedboard2, testboard2);
        // Use the Assert class to test conditions
    }
    [Test]
    public void NUnitAdjacentTest2a()
    {
        //Arrange
        testboard2 = BR.ValidMove(testboard2, 2);

        Assert.AreNotEqual(expectedboard1, testboard2);
        // Use the Assert class to test conditions
    }

    public int[,] testboard3 = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 1, 0, 0}, // 2
        {0, 0, 0, 1, 1, 0, 0, 0}, // 3
        {0, 0, 0, 1, 1, 0, 0, 0}, // 4
        {0, 0, 0, 1, 1, 2, 0, 0}, // 5
        {0, 0, 0, 0, 1, 0, 2, 0}, // 6
        {0, 0, 0, 0, 1, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
    public int[,] expectedboard3 = {
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
    public void NUnitAdjacentTest3()
    {
        //Arrange
        testboard3 = BR.ValidMove(testboard3, 2);

        Assert.AreEqual(expectedboard3, testboard3);
        // Use the Assert class to test conditions
    }
    [Test]
    public void NUnitAdjacentTest3a()
    {
        //Arrange
        testboard3 = BR.ValidMove(testboard3, 2);

        Assert.AreNotEqual(null, testboard3);
        // Use the Assert class to test conditions
    }
}


