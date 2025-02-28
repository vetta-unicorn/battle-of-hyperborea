using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoH.Models;

namespace GameTest
{
    [TestClass]
    public class PlayersTest
    {
        [TestMethod]
        public void TestMethodPlayer()
        {
            string team = "Some team";
            Player player = new Player(team);
            Assert.AreEqual(team, player.Team);
        }


    }
}
