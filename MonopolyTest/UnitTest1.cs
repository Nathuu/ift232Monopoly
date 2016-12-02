using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WpfApplication1.sources;

namespace MonopolyTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LanceUnDes()
        {
            List<int> expected = new List<int>()
            {
                1,2,3,4,5,6
            };
            Assert.IsTrue(expected.Contains(Plateau.Instance.LanceUnDes()));
        }
    }
}
