using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ObjectListViewDemo {
    /// <summary>
    /// This simple class just shows how an overlay can be drawn when the hot item changes.
    /// </summary>
    internal class BusinessCardOverlay : AbstractOverlay
    {
        public BusinessCardOverlay()
        {
            this.businessCardRenderer.HeaderBackBrush = Brushes.DarkBlue;
            this.businessCardRenderer.BorderPen = new Pen(Color.DarkBlue, 2);
            this.Transparency = 255;
        }
        #region IOverlay Members

        public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
        {
            if (olv.HotRowIndex < 0)
                return;

            if (olv.View == View.Tile)
                return;

            OLVListItem item = olv.GetItem(olv.HotRowIndex);
            if (item == null)
                return;

            Size cardSize = new Size(250, 120);
            Rectangle cardBounds = new Rectangle(
                r.Right - cardSize.Width - 8, r.Bottom - cardSize.Height - 8, cardSize.Width, cardSize.Height);
            this.businessCardRenderer.DrawBusinessCard(g, cardBounds, item.RowObject, olv, item);
        }

        #endregion

        private readonly BusinessCardRenderer businessCardRenderer = new BusinessCardRenderer();
    }
}