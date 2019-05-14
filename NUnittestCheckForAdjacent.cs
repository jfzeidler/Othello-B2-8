using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

class NUnittestCheckForAdjacent
{
    BoardRules BR = new BoardRules();
    public int[,] testboard = {
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
    public int[,] expectedboard = {
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
    public void NUnitAdjacentTest()
    {
        //Arrange
        testboard = BR.ValidMove(testboard, 1);

        Assert.AreEqual(expectedboard, testboard);


        // Use the Assert class to test conditions
    }
}

