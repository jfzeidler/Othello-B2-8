using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class NUnitCheckfornine
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

    [Test]
    public void NUnitCheckforNineTest1()
    {
        bool exceptedvalue = true;
        bool actualvalue;
        actualvalue = BR.CheckForNine(testboard1);

        Assert.AreEqual(exceptedvalue, actualvalue);
    }
    [Test]
    public void NUnitCheckforNineTest2()
    {
        bool exceptedvalue = false;
        bool actualvalue;
        actualvalue = BR.CheckForNine(testboard1);

        Assert.AreNotEqual(exceptedvalue, actualvalue);
    }

    public int[,] testboard2 = {
      // A  B  C  D  E  F  G  H
        {0, 0, 0, 0, 0, 0, 0, 9}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 0, 0, 0, 0}, // 3
        {0, 0, 0, 2, 1, 0, 0, 0}, // 4
        {0, 0, 0, 1, 2, 0, 0, 0}, // 5
        {0, 0, 0, 0, 0, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };

    [Test]
    public void NUnitCheckforNineTest3()
    {
        bool exceptedvalue = false;
        bool actualvalue;
        actualvalue = BR.CheckForNine(testboard2);

        Assert.AreEqual(exceptedvalue, actualvalue);
    }
    [Test]
    public void NUnitCheckforNineTest4()
    {
        bool exceptedvalue = true;
        bool actualvalue;
        actualvalue = BR.CheckForNine(testboard2);

        Assert.AreNotEqual(exceptedvalue, actualvalue);
    }
    public int[,] testboard3 = {
      // A  B  C  D  E  F  G  H
        {9, 0, 0, 0, 0, 0, 0, 0}, // 1
        {0, 0, 0, 0, 0, 0, 0, 0}, // 2
        {0, 0, 0, 0, 0, 0, 0, 0}, // 3
        {0, 0, 0, 2, 1, 0, 0, 0}, // 4
        {0, 0, 0, 1, 2, 0, 0, 0}, // 5
        {0, 0, 0, 0, 0, 0, 0, 0}, // 6
        {0, 0, 0, 0, 0, 0, 0, 0}, // 7
        {0, 0, 0, 0, 0, 0, 0, 0}  // 8
    };
    [Test]
    public void NUnitCheckforNineTest5()
    {
        bool exceptedvalue = false;
        bool actualvalue;
        actualvalue = BR.CheckForNine(testboard3);

        Assert.AreEqual(exceptedvalue, actualvalue);
    }

}
