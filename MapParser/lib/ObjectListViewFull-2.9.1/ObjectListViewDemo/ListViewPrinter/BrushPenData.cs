using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// PenData represents the data required to create a pen.
    /// </summary>
    /// <remarks>Pens cannot be edited directly within the IDE (is this VCS EE only?)
    /// These objects allow pen characters to be edited within the IDE and then real
    /// Pen objects created.</remarks>
    [Editor(typeof(PenDataEditor), typeof(UITypeEditor)),
    TypeConverter(typeof(PenDataConverter))]
    public class PenData
    {
        public PenData() : this(new SolidBrushData())
        {
        }

        public PenData(IBrushData brush)
        {
            this.Brush = brush;
        }

        public Pen GetPen()
        {
            Pen p = new Pen(this.Brush.GetBrush(), this.Width);
            p.SetLineCap(this.StartCap, this.EndCap, this.DashCap);
            p.DashStyle = this.DashStyle;
            p.LineJoin = this.LineJoin;
            return p;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IBrushData Brush
        {
            get { return brushData; }
            set { brushData = value; }
        }
        private IBrushData brushData;

        [DefaultValue(typeof(DashCap), "Round")]
        public DashCap DashCap
        {
            get { return dashCap; }
            set { dashCap = value; }
        }
        private DashCap dashCap = DashCap.Round;

        [DefaultValue(typeof(DashStyle), "Solid")]
        public DashStyle DashStyle
        {
            get { return dashStyle; }
            set { dashStyle = value; }
        }
        private DashStyle dashStyle = DashStyle.Solid;

        [DefaultValue(typeof(LineCap), "NoAnchor")]
        public LineCap EndCap
        {
            get { return endCap; }
            set { endCap = value; }
        }
        private LineCap endCap = LineCap.NoAnchor;

        [DefaultValue(typeof(LineJoin), "Round")]
        public LineJoin LineJoin
        {
            get { return lineJoin; }
            set { lineJoin = value; }
        }
        private LineJoin lineJoin = LineJoin.Round;

        [DefaultValue(typeof(LineCap), "NoAnchor")]
        public LineCap StartCap
        {
            get { return startCap; }
            set { startCap = value; }
        }
        private LineCap startCap = LineCap.NoAnchor;

        [DefaultValue(1.0f)]
        public float Width
        {
            get { return width; }
            set { width = value; }
        }
        private float width = 1.0f;
    }

    [Editor(typeof(BrushDataEditor), typeof(UITypeEditor)),
    TypeConverter(typeof(BrushDataConverter))]
    public interface IBrushData
    {
        Brush GetBrush();
    }

    public class SolidBrushData : IBrushData
    {
        public Brush GetBrush()
        {
            if (this.Alpha < 255)
                return new SolidBrush(Color.FromArgb(this.Alpha, this.Color));
            else
                return new SolidBrush(this.Color);
        }

        [DefaultValue(typeof(Color), "")]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color color = Color.Empty;

        [DefaultValue(255)]
        public int Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        private int alpha = 255;
    }

    public class LinearGradientBrushData : IBrushData
    {
        public Brush GetBrush()
        {
            return new LinearGradientBrush(new Rectangle(0, 0, 100, 100), this.FromColor, this.ToColor, this.GradientMode);
        }

        public Color FromColor
        {
            get { return fromColor; }
            set { fromColor = value; }
        }
        private Color fromColor = Color.Aqua;

        public Color ToColor
        {
            get { return toColor; }
            set { toColor = value; }
        }
        private Color toColor = Color.Pink;

        public LinearGradientMode GradientMode
        {
            get { return gradientMode; }
            set { gradientMode = value; }
        }
        private LinearGradientMode gradientMode = LinearGradientMode.Horizontal;
    }

    public class HatchBrushData : IBrushData
    {
        public Brush GetBrush()
        {
            return new HatchBrush(this.HatchStyle, this.ForegroundColor, this.BackgroundColor);
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        private Color backgroundColor = Color.AliceBlue;

        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; }
        }
        private Color foregroundColor = Color.Aqua;

        public HatchStyle HatchStyle
        {
            get { return hatchStyle; }
            set { hatchStyle = value; }
        }
        private HatchStyle hatchStyle = HatchStyle.Cross;
    }

    public class TextureBrushData : IBrushData
    {
        public Brush GetBrush()
        {
            if (this.Image == null)
                return null;
            else
                return new TextureBrush(this.Image, this.WrapMode);
        }

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
        private Image image;

        public WrapMode WrapMode
        {
            get { return wrapMode; }
            set { wrapMode = value; }
        }
        private WrapMode wrapMode = WrapMode.Tile;
    }
}
