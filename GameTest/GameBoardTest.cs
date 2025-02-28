using BoH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    [TestClass]
    public sealed class GameBoardTest
    {
        [TestMethod]
        public void TestMethodGameBoard()
        {
            int w = 5;
            int h = 5;
            GameBoard gameBoard = new GameBoard(w, h);
            Assert.AreEqual(w, gameBoard.Width);
            Assert.AreEqual(h, gameBoard.Height);
        }

        [TestMethod]
        public void TestMethodIsCellAvailable()
        {
            int w = 5;
            int h = 5;
            GameBoard gameBoard = new GameBoard(w, h);
            int x = 2;
            int y = 3;
            Assert.AreEqual(true, gameBoard.IsCellAvailable(x, y));
        }
    }
}
