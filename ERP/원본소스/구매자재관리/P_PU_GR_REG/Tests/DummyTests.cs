using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace pur.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void FloorTest()
        {
            Assert.AreEqual(12, Math.Floor(12.34));
            Assert.AreEqual(12, Math.Floor(12.78));
            Assert.AreEqual(12, Math.Floor(12.01));
        }

        [Test]
        public void RoundTest()
        {
            Assert.AreEqual(12, Math.Round(12.34));
            Assert.AreEqual(13, Math.Round(12.6));
            Assert.AreEqual(13, Math.Round(12.5, MidpointRounding.AwayFromZero));
            Assert.AreEqual(12, Math.Round(12.34, MidpointRounding.AwayFromZero));
            Assert.AreEqual(13, Math.Round(12.6, MidpointRounding.AwayFromZero));
        }

        [Test]
        public void RoundTest_2()
        {
            Assert.AreEqual(12.5, Math.Round(12.54, 1, MidpointRounding.AwayFromZero));
            Assert.AreEqual(12.6, Math.Round(12.55, 1, MidpointRounding.AwayFromZero));
            Assert.AreEqual(12.6, Math.Round(12.57, 1, MidpointRounding.AwayFromZero));
        }

        [Test]
        public void RoundTest_3()
        {
            Assert.AreEqual(12.5M, Math.Round(12.54M, 1, MidpointRounding.AwayFromZero));
            Assert.AreEqual(12.6M, Math.Round(12.55M, 1, MidpointRounding.AwayFromZero));
            Assert.AreEqual(12.6M, Math.Round(12.57M, 1, MidpointRounding.AwayFromZero));
        }

        [Test]
        public void Test_1()
        {
            string str = "0|123|456";

            int lastindex = str.LastIndexOf('|');
            Assert.AreEqual("456", str.Substring(lastindex + 1, 3));

            string strs = "하나|둘|셋";
        }

        [Test]
        public void Split_1()
        {
            string str = "하나|둘|셋";

            string[] arr = str.Split('|');

            Assert.AreEqual(3, arr.Length);
        }

        [Test]
        public void Split_2()
        {
            string str = "하나|둘|셋|";

            string[] arr = str.Split('|');
            Assert.AreEqual(4, arr.Length);     

            arr = str.Split(new char[] {'|'},  StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(3, arr.Length); 
        }

    }
}
