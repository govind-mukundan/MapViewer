/*
 * ShapeSprite - A sprite that draws a shape
 * 
 * Author: Phillip Piper
 * Date: 08/02/2010 6:18 PM
 *
 * Change log:
 * 2010-02-08   JPP  - Initial version
 *
 * To do:
 * 2010-02-08   Given TextSprite more formatting options
 * 2010-01-18   Change ShapeSprite so it can use arbitrary Pens and Brushes
 *
 * Copyright (C) 2009-2014 Phillip Piper
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * If you wish to use this code in a closed source application, please contact phillip.piper@gmail.com.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Instances of ShapeSprite draw a geometric shape within their bounds
    /// </summary>
    public class ShapeSprite : Sprite
    {
        protected enum Shapes
        {
            None,
            Rectangle,
            Triangle,
            RoundedRectangle,
            Square,
            Circle,
            Oval,
            IsoTriangle,
            EquiTriangle
        }


        #region Factories

        public static ShapeSprite Rectangle(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.Rectangle, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite RoundedRectangle(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.RoundedRectangle, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite Circle(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.Circle, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite Oval(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.Oval, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite Square(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.Square, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite Triangle(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.Triangle, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite IsoTriangle(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.IsoTriangle, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite EquiTriangle(float penWidth, Color foreColor) {
            return new ShapeSprite(Shapes.EquiTriangle, penWidth, foreColor, Color.Empty);
        }

        public static ShapeSprite FilledRectangle(Color backColor) {
            return new ShapeSprite(Shapes.Rectangle, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite FilledRoundedRectangle(Color backColor) {
            return new ShapeSprite(Shapes.RoundedRectangle, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite FilledCircle(Color backColor) {
            return new ShapeSprite(Shapes.Circle, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite FilledOval(Color backColor) {
            return new ShapeSprite(Shapes.Oval, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite FilledSquare(Color backColor) {
            return new ShapeSprite(Shapes.Square, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite FilledTriangle(Color backColor) {
            return new ShapeSprite(Shapes.Triangle, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite FilledIsoTriangle(Color backColor) {
            return new ShapeSprite(Shapes.IsoTriangle, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite FilledEquiTriangle(Color backColor) {
            return new ShapeSprite(Shapes.EquiTriangle, 0.0f, Color.Empty, backColor);
        }

        public static ShapeSprite Rectangle(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.Rectangle, penWidth, foreColor, backColor);
        }

        public static ShapeSprite RoundedRectangle(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.RoundedRectangle, penWidth, foreColor, backColor);
        }

        public static ShapeSprite Circle(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.Circle, penWidth, foreColor, backColor);
        }

        public static ShapeSprite Oval(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.Oval, penWidth, foreColor, backColor);
        }

        public static ShapeSprite Square(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.Square, penWidth, foreColor, backColor);
        }

        public static ShapeSprite Triangle(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.Triangle, penWidth, foreColor, backColor);
        }

        public static ShapeSprite IsoTriangle(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.IsoTriangle, penWidth, foreColor, backColor);
        }

        public static ShapeSprite EquiTriangle(float penWidth, Color foreColor, Color backColor) {
            return new ShapeSprite(Shapes.EquiTriangle, penWidth, foreColor, backColor);
        }

        #endregion

        #region Life and death

        public ShapeSprite() :
            this(Shapes.Rectangle, 2.0f, Color.DarkBlue, Color.CornflowerBlue)
        {
        }

        protected ShapeSprite(Shapes shape, float penWidth, Color foreColor, Color backColor) {
            this.Shape = shape;
            this.PenWidth = penWidth;
            this.ForeColor = foreColor;
            this.BackColor = backColor;
            this.CornerRounding = 16;
        }

        #endregion

        #region Public properties

        protected Shapes Shape ;
        public float PenWidth ;
        public Color ForeColor ;
        public Color BackColor ;

        /// <summary>
        /// How rounded should the corners of a rounded rectangle be?
        /// Has no impact on other shapes
        /// </summary>
        public float CornerRounding ;

        #endregion

        #region Implementation properties

        /// <summary>
        /// Gets whether the shape should be drawn filled in
        /// </summary>
        protected bool Filled {
            get {
                return !this.BackColor.IsEmpty;
            }
        }

        /// <summary>
        /// Gets whether the shape should be drawn with an outline
        /// </summary>
        protected bool Lined {
            get {
                return this.PenWidth > 0.0f && !this.ForeColor.IsEmpty;
            }
        }

        #endregion

        #region Sprite methods

        public override void Draw(Graphics g) {
            this.ApplyState(g);

            // We don't draw the shape into Bounds, since the location of the
            // shape is handled by a co-ordinate transformation. So we draw the
            // shape as if it was at origin 0,0
            this.DrawShape(g, new Rectangle(Point.Empty, this.Size), this.Opacity);
            this.UnapplyState(g);
        }

        #endregion

        #region Implementation methods

        protected virtual void DrawShape(Graphics g, Rectangle r, float opacity) {
            using (GraphicsPath path = this.GetGraphicsPath(r)) {
                if (this.Filled) {
                    using (Brush b = this.GetFillBrush(r, opacity)) {
                        g.FillPath(b, path);
                    }
                }
                if (this.Lined) {
                    using (Pen p = this.GetLinePen(r, opacity)) {
                        g.DrawPath(p, path);
                    }
                }
            }
        }

        protected virtual Pen GetLinePen(Rectangle r, float opacity) {
            return new Pen(this.ApplyOpacityToColor(opacity, this.ForeColor), this.PenWidth);
        }

        protected virtual Brush GetFillBrush(Rectangle r, float opacity) {
            return new SolidBrush(this.ApplyOpacityToColor(opacity, this.BackColor));
        }

        protected Color ApplyOpacityToColor(float opacity, Color c) {
            if (opacity < 1.0f)
                return Color.FromArgb((int)(opacity * c.A), this.BackColor);
            else
                return c;
        }

        protected virtual GraphicsPath GetGraphicsPath(Rectangle r) {
            GraphicsPath path = new GraphicsPath();
            int minimumAxis = Math.Min(r.Width, r.Height);
            Point midPoint = new Point(r.X + r.Width / 2, r.Y + r.Height / 2);

            switch (this.Shape) {
                case Shapes.Rectangle:
                    path.AddRectangle(new Rectangle(Point.Empty, r.Size));
                    break;
                case Shapes.RoundedRectangle:
                    path = ShapeSprite.GetRoundedRect(r, this.CornerRounding);
                    break;
                case Shapes.Circle:
                    path.AddEllipse(midPoint.X - minimumAxis / 2, midPoint.Y - minimumAxis / 2, minimumAxis, minimumAxis);
                    break;
                case Shapes.Oval:
                    path.AddEllipse(r);
                    break;
                case Shapes.Square:
                    path.AddRectangle(new Rectangle(midPoint.X - minimumAxis / 2, midPoint.Y - minimumAxis / 2,
                        minimumAxis, minimumAxis));
                    break;
                case Shapes.Triangle:
                    path.AddLines(new Point[] {
                        new Point(r.Left, r.Top),
                        new Point(r.Left, r.Bottom),
                        new Point(r.Right, r.Top),
                        new Point(r.Left, r.Top)
                    });
                    break;
                case Shapes.IsoTriangle:
                    path.AddLines(new Point[] {
                        new Point(r.Left, r.Bottom),
                        new Point(r.Right, r.Bottom),
                        new Point(midPoint.X, r.Top),
                        new Point(r.Left, r.Bottom)
                    });
                    break;
                case Shapes.EquiTriangle:
                    //TODO
                    break;
                default:
                    break;
            }

            return path;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Return a GraphicPath that is a round cornered rectangle
        /// </summary>
        /// <param name="rect">The rectangle</param>
        /// <param name="diameter">The diameter of the corners</param>
        /// <returns>A round cornered rectagle path</returns>
        /// <remarks>If I could rely on people using C# 3.0+, this should be
        /// an extension method of GraphicsPath.</remarks>
        public static GraphicsPath GetRoundedRect(Rectangle rect, float diameter) {
            GraphicsPath path = new GraphicsPath();

            if (diameter > 0) {
                RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);
                path.AddArc(arc, 180, 90);
                arc.X = rect.Right - diameter;
                path.AddArc(arc, 270, 90);
                arc.Y = rect.Bottom - diameter;
                path.AddArc(arc, 0, 90);
                arc.X = rect.Left;
                path.AddArc(arc, 90, 90);
                path.CloseFigure();
            } else {
                path.AddRectangle(rect);
            }

            return path;
        }

        #endregion
    }

    public class ParallelogramSprite : ShapeSprite
    {
        public ParallelogramSprite(int horizontalSize, bool forwardSlope) {
            this.HorizontalSide = horizontalSize;
            this.SlopeForward = forwardSlope;
        }

        /// <summary>
        /// Gets or sets the length of the parallel size of the shape
        /// </summary>
        public int HorizontalSide ;

        /// <summary>
        /// Gets or sets if the slope of the parallelogram is forward
        /// (left edge of bottom is left of the left edge of the top
        /// </summary>
        public bool SlopeForward ;

        protected override GraphicsPath GetGraphicsPath(Rectangle r) {
            GraphicsPath path = new GraphicsPath();

            if (this.SlopeForward) {
                path.AddLines(new Point[] {
                    new Point(r.Left, r.Bottom),
                    new Point(Math.Min(r.Left + this.HorizontalSide, r.Right), r.Bottom),
                    new Point(r.Right, r.Top),
                    new Point(Math.Max(r.Right - this.HorizontalSide, r.Left), r.Top),
                    new Point(r.Left, r.Bottom)
                });
            } else {
                path.AddLines(new Point[] {
                    new Point(r.Left, r.Top),
                    new Point(Math.Min(r.Left + this.HorizontalSide, r.Right), r.Top),
                    new Point(r.Right, r.Bottom),
                    new Point(Math.Max(r.Right - this.HorizontalSide, r.Left), r.Bottom),
                    new Point(r.Left, r.Top)
                });
            }

            return path;
        }
    }
}
