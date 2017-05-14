using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

//TODO: Allow alpha

namespace BrightIdeasSoftware
{
    internal partial class BrushForm : Form
    {
        public BrushForm()
        {
            InitializeComponent();
            this.TopLevel = false;
        }

        public IBrushData GetBrushData()
        {
            return this.propertyGrid1.SelectedObject as IBrushData;
        }

        public Brush GetBrush()
        {
            IBrushData bd = this.GetBrushData();
            if (bd == null)
                return null;
            else
                return bd.GetBrush();
        }

        public void SetBrush(IBrushData value)
        {
            this.rbSolid.Tag = (value is SolidBrushData) ? value : new SolidBrushData();
            this.rbHatch.Tag = (value is HatchBrushData) ? value : new HatchBrushData();
            this.rbGradient.Tag = (value is LinearGradientBrushData) ? value : new LinearGradientBrushData();
            this.TurnOnRadioButton(value);
        }

        protected void TurnOnRadioButton(IBrushData value)
        {
            RadioButton turnedOn = this.rbNone;
            if (value != null) {
                if (value.GetType() == typeof(SolidBrushData))
                    turnedOn = this.rbSolid;
                else if (value.GetType() == typeof(LinearGradientBrushData))
                    turnedOn = this.rbGradient;
                else if (value.GetType() == typeof(HatchBrushData))
                    turnedOn = this.rbHatch;
            }

            turnedOn.Checked = true;
        }

        protected void examplePanel_Paint(object sender, PaintEventArgs e)
        {
            using (BufferedGraphics buffered = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle)) {
                Graphics g = buffered.Graphics;
                g.Clear(((Panel)sender).BackColor);
                this.HandlePaintEvent(g, e.ClipRectangle);
                buffered.Render();
            }
        }

        virtual protected void HandlePaintEvent(Graphics g, Rectangle r)
        {
            using (Brush b = this.GetBrush()) {
                StringFormat fmt = new StringFormat();
                fmt.Alignment = StringAlignment.Center;
                fmt.LineAlignment = StringAlignment.Center;
                if (b == null) {
                    g.DrawString("No brush", new Font("Tahoma", 14), Brushes.DarkGray, r, fmt);
                } else {
                    g.DrawString("Through a mirror darkly", new Font("Tahoma", 14), Brushes.Black, r, fmt);
                    g.FillRectangle(BlockFormat.PrepareBrushForDrawing(b, r), r);
                }
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.examplePanel.Invalidate();
        }

        private void propertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            this.examplePanel.Invalidate();
        }

        private void rbCheckedChanged(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = ((RadioButton)sender).Tag;
        }
    }

    internal class PenForm : BrushForm
    {
        public PenData GetPenData()
        {
            return this.propertyGrid1.SelectedObject as PenData;
        }

        public Pen GetPen()
        {
            PenData data = this.GetPenData();
            if (data == null)
                return null;
            else
                return data.GetPen();
        }

        public void SetPenData(PenData value)
        {
            IBrushData bd = (value == null) ? null : value.Brush;
            this.rbSolid.Tag = (bd is SolidBrushData) ? value : new PenData(new SolidBrushData());
            this.rbGradient.Tag = (bd is LinearGradientBrushData) ? value : new PenData(new LinearGradientBrushData());
            this.rbHatch.Tag = (bd is HatchBrushData) ? value : new PenData(new HatchBrushData());
            this.TurnOnRadioButton(bd);
        }

        protected override void HandlePaintEvent(Graphics g, Rectangle r)
        {
            using (Pen p = this.GetPen()) {
                g.SmoothingMode = ObjectListView.SmoothingMode;
                StringFormat fmt = new StringFormat();
                fmt.Alignment = StringAlignment.Center;
                fmt.LineAlignment = StringAlignment.Center;
                if (p == null) {
                    g.DrawString("No pen", new Font("Tahoma", 14), Brushes.DarkGray, r, fmt);
                } else {
                    g.DrawString("Through a mirror darkly", new Font("Tahoma", 14), Brushes.Black, r, fmt);
                    int inset = (int)Math.Max(1.0, p.Width);
                    r.Inflate(-inset, -inset);
                    Point[] pts = new Point[4];
                    pts[0] = r.Location;
                    pts[1] = new Point(r.X + r.Width / 2, r.Bottom);
                    pts[2] = new Point(r.X + r.Width / 2, r.Top);
                    pts[3] = new Point(r.Right, r.Bottom);
                    g.DrawLines(BlockFormat.PreparePenForDrawing(p, r), pts);
                }
            }
        }
    }
}