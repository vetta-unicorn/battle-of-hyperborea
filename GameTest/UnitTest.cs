using BoH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    [TestClass]
    public sealed class UnitTest
    {
        [TestMethod]
        public void TestMethodLizardArcher()
        {

            LizardArcher a = new LizardArcher();
            string Name = "Lizard-Archer";
            string Icon = "2";
            string Team = "Lizard";
            int HP = 12;
            int AttackRange = 4;
            int Defence = 2;
            int DamageDices = 3;
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(Icon, a.Icon);
            Assert.AreEqual(Team, a.Team);
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(HP, a.Hp);
            Assert.AreEqual(AttackRange, a.AttackRange);
            Assert.AreEqual(Defence, a.Defence);
            Assert.AreEqual(DamageDices, a.DamageDices);

            a.TakeDamage(10);
            Assert.AreEqual(4, a.Hp);
            a.Heal(8);
            Assert.AreEqual(HP, a.Hp);

        }

        [TestMethod]
        public void TestMethodBaseUnit()
        {
            string Name = "Something";
            BaseUnit a = new BaseUnit(Name);
            string Icon = "T";
            string Team = "Dev";
            int HP = 100;
            int Defence = 0;
            int DamageDices = 3;
            int Speed = 3;
            int Range = 1;
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(Icon, a.Icon);
            Assert.AreEqual(Team, a.Team);
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(HP, a.Hp);
            Assert.AreEqual(Defence, a.Defence);
            Assert.AreEqual(DamageDices, a.DamageDices);
            Assert.AreEqual(Speed, a.Speed);
            Assert.AreEqual(Range, a.Range);
            Assert.AreEqual(false, a.IsDead);
            Assert.AreEqual(false, a.IsStunned);
            a.TakeDamage(10);
            Assert.AreEqual(90, a.Hp);
            a.Heal(10);
            Assert.AreEqual(HP, a.Hp);
        }

        [TestMethod]
        public void TestMethodLizardWarrion()
        {
            LizardWarrior a = new LizardWarrior();
            string Name = "Lizard-Warrior";
            string Icon = "S";
            string Team = "Lizard";
            int HP = 10;
            int AttackRange = 4;
            int Defence = 8;
            int DamageDices = 2;
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(Icon, a.Icon);
            Assert.AreEqual(Team, a.Team);
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(HP, a.Hp);
            Assert.AreEqual(Defence, a.Defence);
            Assert.AreEqual(DamageDices, a.DamageDices);
            a.TakeDamage(10);
            Assert.AreEqual(8, a.Hp);
            a.Heal(2);
            Assert.AreEqual(HP, a.Hp);
        }

        [TestMethod]
        public void TestMethodRusWarrion()
        {
            RusWarrior a = new RusWarrior();
            string Name = "Rus-Warrior";
            string Icon = "R";
            string Team = "Rus";
            int HP = 15;
            int Defence = 8;
            int DamageDices = 2;
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(Icon, a.Icon);
            Assert.AreEqual(Team, a.Team);
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(HP, a.Hp);
            Assert.AreEqual(Defence, a.Defence);
            Assert.AreEqual(DamageDices, a.DamageDices);
            a.TakeDamage(10);
            Assert.AreEqual(13, a.Hp);
            a.Heal(2);
            Assert.AreEqual(HP, a.Hp);
        }

        [TestMethod]
        public void TestMethodRusArcher()
        {
            RusArcher a = new RusArcher();
            string Name = "Rus-Archer";
            string Icon = "Я";
            string Team = "Rus";
            int HP = 10;
            int Defence = 2;
            int DamageDices = 3;
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(Icon, a.Icon);
            Assert.AreEqual(Team, a.Team);
            Assert.AreEqual(Name, a.UnitName);
            Assert.AreEqual(HP, a.Hp);
            Assert.AreEqual(Defence, a.Defence);
            Assert.AreEqual(DamageDices, a.DamageDices);
            a.TakeDamage(10);
            Assert.AreEqual(2, a.Hp);
            a.Heal(8);
            Assert.AreEqual(HP, a.Hp);
        }
    }
}
