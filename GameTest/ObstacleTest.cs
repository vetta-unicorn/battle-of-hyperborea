using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoH.Models;

namespace GameTest
{
    [TestClass]
    public sealed class ObstacleTest
    {
        [TestMethod]
        public void TestMethodObstacle()
        {
            string icon = "B";
            Obstacle obst = new Obstacle();
            Assert.AreEqual(icon, obst.Icon);
        }
    }
}
