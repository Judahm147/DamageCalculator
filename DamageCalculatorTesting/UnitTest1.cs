using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DamageCalculator;

namespace DamageCalculatorTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Mk1ParisInit()
        {
            Weapon bow = new Mk1_Paris();
            Assert.AreEqual(bow.BaseDamage(), 120.0);
        }
    }
}
