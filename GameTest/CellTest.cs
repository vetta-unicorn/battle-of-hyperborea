using BoH.Models;

namespace GameTest
{
    [TestClass]
    public sealed class CellTest
    {
        [TestMethod]
        public void TestMethodCell()
        {
            Cell cell = new Cell((2, 1));
            Cell cell2 = new Cell(2, 1);
            int x = 2;
            int y = 1;
            Assert.AreEqual(x, cell.Position.X);
            Assert.AreEqual(y, cell.Position.Y);
            Assert.AreEqual(x, cell2.Position.X);
            Assert.AreEqual(y, cell2.Position.Y);
            Assert.AreEqual((x, y), cell.Position);

        }
    }
}
