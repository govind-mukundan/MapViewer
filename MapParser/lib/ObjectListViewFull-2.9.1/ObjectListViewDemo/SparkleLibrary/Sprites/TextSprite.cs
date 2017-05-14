/*
 * TextSprite - A sprite that draws text
 * 
 * Author: Phillip Piper
 * Date: 08/02/2010 6:18 PM
 *
 * Change log:
 * 2010-03-31   JPP  - Correctly calculate the height of wrapped text
 *                   - Cleaned up
 * 2010-02-29   JPP  - Add more formatting options (wrap, border, background)
 * 2010-02-08   JPP  - Initial version
 *
 * To do:
 *
 * Copyright (C) 2010 Phillip Piper
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
    /// A TextSprite is animated text. Like all Sprites, animation is achieved
    /// by adding animations to it.
    /// </summary>
    public class TextSprite : Sprite
    {
        #region Life and death

        public TextSprite() {
            this.Font = new Font("Tahoma", 12);
            this.ForeColor = Color.Blue;
            this.BackColor = Color.Empty;
        }

        public TextSprite(string text) {
            this.Text = text;
        }

        public TextSprite(string text, Font font, Color foreColor) {
            this.Text = text;
            this.Font = font;
            this.ForeColor = foreColor;
        }

        public TextSprite(string text, Font font, Color foreColor, Color backColor, Color borderColor, float borderWidth) {
            this.Text = text;
            this.Font = font;
            this.ForeColor = foreColor;
            this.BackColor = backColor;
            this.BorderColor = borderColor;
            this.BorderWidth = borderWidth;
        }

        #endregion

        #region Configuration properties

        /// <summary>
        /// Gets or sets the text that will be rendered by the sprite
        /// </summary>
        public string Text { 
            get { return text; } 
            set { text = value; } 
        }
        private string text;

        /// <summary>
        /// Gets or sets the font in which the text will be rendered.
        /// This will be scaled before being used to draw the text.
        /// </summary>
        public Font Font {
            get { return this.font; }
            set { this.font = value; }
        }
        private Font font;

        /// <summary>
        /// Gets or sets the color of the text
        /// </summary>
        public Color ForeColor {
            get { return this.foreColor; }
            set { this.foreColor = value; }
        }
        private Color foreColor = Color.Empty;

        /// <summary>
        /// Gets or sets the background color of the text
        /// Set this to Color.Empty to not draw a background
        /// </summary>
        public Color BackColor {
            get { return this.backColor; }
            set { this.backColor = value; }
        }
        private Color backColor = Color.Empty;

        /// <summary>
        /// Gets or sets the color of the border around the billboard.
        /// Set this to Color.Empty to remove the border
        /// </summary>
        public Color BorderColor {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }
        private Color borderColor = Color.Empty;

        /// <summary>
        /// Gets or sets the width of the border around the text
        /// </summary>
        public float BorderWidth {
            get { return this.borderWidth; }
            set { this.borderWidth = value; }
        }
        private float borderWidth;

        /// <summary>
        /// How rounded should the corners of the border be? 0 means no rounding.
        /// </summary>
        /// <remarks>If this value is too large, the edges of the border will appear odd.</remarks>
        public float CornerRounding {
            get { return this.cornerRounding; }
            set { this.cornerRounding = value; }
        }
        private float cornerRounding = 16.0f;

        /// <summary>
        /// Gets the font that will be used to draw the text or a reasonable default
        /// </summary>
        public Font FontOrDefault {
            get {
                return this.Font ?? new Font("Tahoma", 16);
            }
        }

        /// <summary>
        /// Does this text have a background?
        /// </summary>
        public bool HasBackground {
            get {
                return this.BackColor != Color.Empty;
            }
        }

        /// <summary>
        /// Does this overlay have a border?
        /// </summary>
        public bool HasBorder {
            get {
                return this.BorderColor != Color.Empty && this.BorderWidth > 0;
            }
        }

        /// <summary>
        /// Gets or sets the maximum width of the text. Text longer than this will wrap.
        /// 0 means no maximum.
        /// </summary>
        public int MaximumTextWidth {
            get { return this.maximumTextWidth; }
            set { this.maximumTextWidth = value; }
        }
        private int maximumTextWidth = 0;

        /// <summary>
        /// Gets or sets the formatting that should be used on the text
        /// </summary>
        public StringFormat StringFormat {
            get {
                if (this.stringFormat == null) {
                    this.stringFormat = new StringFormat();
                    this.stringFormat.Alignment = StringAlignment.Center;
                    this.stringFormat.LineAlignment = StringAlignment.Center;
                    this.stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                    if (!this.Wrap)
                        this.stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                }
                return this.stringFormat;
            }
            set { this.stringFormat = value; }
        }
        private StringFormat stringFormat;

        /// <summary>
        /// Gets or sets whether the text will wrap when it exceeds its bounds
        /// </summary>
        public bool Wrap {
            get { return wrap; }
            set { wrap = value; }
        }
        private bool wrap;

        #endregion

        #region Sprite properties

        /// <summary>
        /// Gets the size of the drawn text. This is always calculated from the 
        /// natural size of the text when drawn in the correctly scaled font.
        /// It cannot be set directly.
        /// </summary>
        public override Size Size {
            get {
                if (String.IsNullOrEmpty(this.Text))
                    return Size.Empty;

                // This is a stupid hack to get a Graphics object. How should this be done?
                lock (dummyImageLock) {
                    using (Graphics g = Graphics.FromImage(this.dummyImage)) {
                        return g.MeasureString(this.Text, this.ActualFont, this.CalcMaxLineWidth(), this.StringFormat).ToSize();
                    }
                }
            }
            set { }
        }
        private Image dummyImage = new Bitmap(1, 1);
        private object dummyImageLock = new object();

        #endregion

        #region Implementation properties

        /// <summary>
        /// Gets the font that will be used to draw the text. This takes
        /// scaling into account.
        /// </summary>
        protected Font ActualFont {
            get {
                if (this.Scale == 1.0f)
                    return this.Font;
                else
                    // TODO: Cache this font and discard it when either Font or Scale changed.
                    return new Font(this.Font.FontFamily, this.Font.SizeInPoints * this.Scale, this.Font.Style);
            }
        }

        #endregion

        #region Sprite methods

        public override void Draw(Graphics g) {
            if (String.IsNullOrEmpty(this.Text) || this.Opacity <= 0.0f)
                return;

            this.ApplyState(g);
            this.DrawText(g, this.Text, this.Opacity);
            this.UnapplyState(g);
        }

        #endregion

        #region Drawing methods

        protected void DrawText(Graphics g, string s, float opacity) {
            Font f = this.ActualFont;
            SizeF textSize = g.MeasureString(s, f, this.CalcMaxLineWidth(), this.StringFormat);
            this.DrawBorderedText(g, new Rectangle(0, 0, 1+(int)textSize.Width, 1+(int)textSize.Height), s, f, opacity);
        }

        protected int CalcMaxLineWidth() {
            return this.MaximumTextWidth > 0 ? (int)(this.MaximumTextWidth * this.Scale) : Int32.MaxValue;
        }

        protected Brush GetTextBrush(float opacity) {
            if (opacity < 1.0f)
                return new SolidBrush(Color.FromArgb((int)(opacity * 255), this.ForeColor));
            else
                return new SolidBrush(this.ForeColor);
        }

        protected Brush GetBackgroundBrush(float opacity) {
            if (opacity < 1.0f)
                return new SolidBrush(Color.FromArgb((int)(opacity * 255), this.BackColor));
            else
                return new SolidBrush(this.BackColor);
        }

        protected Pen GetBorderPen(float opacity) {
            if (opacity < 1.0f)
                return new Pen(Color.FromArgb((int)(opacity * 255), this.BorderColor), this.BorderWidth);
            else
                return new Pen(this.BorderColor, this.BorderWidth);
        }

        /// <summary>
        /// Draw the text with a border
        /// </summary>
        /// <param name="g">The Graphics used for drawing</param>
        /// <param name="textRect">The bounds within which the text should be drawn</param>
        /// <param name="text">The text to draw</param>
        protected void DrawBorderedText(Graphics g, Rectangle textRect, string text, Font font, float opacity) {
            Rectangle borderRect = textRect;
            if (this.BorderWidth > 0.0f)
                borderRect.Inflate((int)this.BorderWidth / 2, (int)this.BorderWidth / 2);

            borderRect.Y -= 1; // Looks better a little higher

            using (GraphicsPath path = ShapeSprite.GetRoundedRect(borderRect, this.CornerRounding * this.Scale)) {
                if (this.HasBackground) {
                    using (Brush brush = this.GetBackgroundBrush(opacity)) {
                        g.FillPath(brush, path);
                    }
                }

                using (Brush textBrush = this.GetTextBrush(opacity)) {
                    g.DrawString(text, font, textBrush, textRect, this.StringFormat);
                }

                if (this.HasBorder) {
                    using (Pen pen = this.GetBorderPen(opacity)) {
                        g.DrawPath(pen, path);
                    }
                }
            }

        }


        #endregion
    }
}
