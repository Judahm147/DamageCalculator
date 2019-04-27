using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DamageCalculator;
using DamageCalculator.Weapons;

namespace DamageCalculatorTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Mk1ParisInit()
        {
            Weapon bow = new Mk1_Paris();
            Assert.AreEqual(bow.base_damage, 120.0);
        }

        [TestMethod]
        public void ElementsCombination()
        {
            Weapon weapon = new Mk1_Paris();
            Hellfire hellfire = new Hellfire();
            Stormbringer stormbringer = new Stormbringer();
            weapon.InstallMod(hellfire, 1);
            weapon.InstallMod(stormbringer, 2);
            DamageType radiation = new RadiationDamage(1.0);
            Assert.IsTrue(weapon.HasDamageType(radiation));
        }
    }
}
