using System.Drawing;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{

    [TestFixture]
    public class TestAdornments
    {
        [Test]
        public void TestCalculateAlignedPosition() {
            GraphicAdornment ga = new GraphicAdornment();
            Rectangle r = new Rectangle(10, 20, 30, 40);
            Point pt = new Point(100, 200);
            Size sz = new Size(50, 60);
            Assert.AreEqual(pt, ga.CalculateAlignedPosition(pt, sz, ContentAlignment.TopLeft));
            Assert.AreEqual(pt - sz, ga.CalculateAlignedPosition(pt, sz, ContentAlignment.BottomRight));
            Assert.AreEqual(new Point(75, 170), ga.CalculateAlignedPosition(pt, sz, ContentAlignment.MiddleCenter));
        }

        [Test]
        public void TestCalculateCorner() {
            GraphicAdornment ga = new GraphicAdornment();
            Rectangle r = new Rectangle(10, 20, 30, 40);
            Assert.AreEqual(r.Location, ga.CalculateCorner(r, ContentAlignment.TopLeft));
            Assert.AreEqual(new Point(40, 20), ga.CalculateCorner(r, ContentAlignment.TopRight));
            Assert.AreEqual(new Point(25, 40), ga.CalculateCorner(r, ContentAlignment.MiddleCenter));
            Assert.AreEqual(new Point(10, 60), ga.CalculateCorner(r, ContentAlignment.BottomLeft));
            Assert.AreEqual(r.Location + r.Size, ga.CalculateCorner(r, ContentAlignment.BottomRight));
        }

        [Test]
        public void TestCreateAlignedRectangle() {
            GraphicAdornment ga = new GraphicAdornment();
            Rectangle r = new Rectangle(10, 20, 30, 40);
            Size sz = new Size(50, 60);
            Size offset = new Size(1, 1);
            Assert.AreEqual(new Rectangle(r.Location + offset, sz), 
                ga.CreateAlignedRectangle(r, sz, ContentAlignment.TopLeft, ContentAlignment.TopLeft, offset));
        }
    }
}
