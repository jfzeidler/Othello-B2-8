using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


    public class NUnittest
    {
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
        Assert.AreEqual(,);
            // Use the Assert class to test conditions
        }

        [Test]
        public void NUnitPlaceNineTest(){

            
        }

        [Test]
        public void NUnitTest()
        {
            Assert.IsFalse(BR.InRange(8, 8));
            // Use the Assert class to test conditions
        }
}

