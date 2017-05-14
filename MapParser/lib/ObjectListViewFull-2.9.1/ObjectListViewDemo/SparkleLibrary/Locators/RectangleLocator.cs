/*
 * RectangleLocator - Generalized mechanism to calculate rectangles 
 * 
 * Author: Phillip Piper
 * Date: 18/01/2010 5:48 PM
 *
 * Change log:
 * 2010-01-18   JPP  - Initial version
 *
 * To do:
 *
 * Copyright (C) 2009 Phillip Piper
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

using System.Drawing;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// A IRectangleLocator calculates a rectangle
    /// </summary>
    public interface IRectangleLocator
    {
        ISprite Sprite { get; set; }
        Rectangle GetRectangle();
    }

    /// <summary>
    /// A safe do-nothing implementation of IRectangleLocator plus some useful utilities
    /// </summary>
    public class AbstractRectangleLocator : IRectangleLocator
    {
        #region Properties

        public Point Expansion ;

        public ISprite Sprite {
            get { return this.sprite; }
            set {
                this.sprite = value;
                this.InitializeSublocators();
            }
        }
        private ISprite sprite; 

        #endregion

        #region Public interface

        public virtual Rectangle GetRectangle() {
            return Rectangle.Empty;
        }

        #endregion

        #region Utilities

        protected Rectangle Expand(Rectangle r) {
            if (this.Expansion == Point.Empty)
                return r;

            Rectangle r2 = r;
            r2.Inflate(this.Expansion.X, this.Expansion.Y);
            return r2;
        }

        /// <summary>
        /// The sprite associate with this locator has changed. 
        /// Make sure any dependent locators are updated
        /// </summary>
        protected virtual void InitializeSublocators() {
        }

        protected void InitializeLocator(IPointLocator locator) {
            if (locator != null && locator.Sprite == null)
                locator.Sprite = this.Sprite;
        }

        protected void InitializeLocator(IRectangleLocator locator) {
            if (locator != null && locator.Sprite == null)
                locator.Sprite = this.Sprite;
        }

        #endregion
    }

    /// <summary>
    /// A SpritePointLocator calculates a point relative to
    /// the reference bound of sprite.
    /// </summary>
    public class SpriteBoundsLocator : AbstractRectangleLocator
    {
        public SpriteBoundsLocator() {
        }

        public SpriteBoundsLocator(ISprite sprite) {
            this.Sprite = sprite;
        }

        public SpriteBoundsLocator(int expandX, int expandY) {
            this.Expansion = new Point(expandX, expandY);
        }

        public override Rectangle GetRectangle() {
            return this.Expand(this.Sprite.Bounds);
        }
    }

    /// <summary>
    /// A AnimationBoundsLocator calculates a point 
    /// on the bounds of whole animation.
    /// </summary>
    public class AnimationBoundsLocator : AbstractRectangleLocator
    {
        public AnimationBoundsLocator() {
        }

        public AnimationBoundsLocator(int expandX, int expandY) {
            this.Expansion = new Point(expandX, expandY);
        }

        public override Rectangle GetRectangle() {
            return this.Expand(this.Sprite.OuterBounds);
        }
    }

    /// <summary>
    /// A RectangleFromCornersLocator calculates its rectangle through two point locators,
    /// one for the top left, the other for the bottom right. The rectangle
    /// can also be expanded by a fixed amount.
    /// </summary>
    public class RectangleFromCornersLocator : AbstractRectangleLocator
    {
        #region Life and death

        public RectangleFromCornersLocator(IPointLocator topLeftLocator, IPointLocator bottomRightLocator) :
            this(topLeftLocator, bottomRightLocator, Point.Empty) {
        }

        public RectangleFromCornersLocator(IPointLocator topLeftLocator, IPointLocator bottomRightLocator, Point expand) {
            this.TopLeftLocator = topLeftLocator;
            this.BottomRightLocator = bottomRightLocator;
            this.Expansion = expand;
        }

        #endregion

        #region Configuration properties

        protected IPointLocator TopLeftLocator ;
        protected IPointLocator BottomRightLocator ;

        #endregion

        #region Public methods

        public override Rectangle GetRectangle() {
            Point topLeft = this.TopLeftLocator.GetPoint();
            Point bottomRight = this.BottomRightLocator.GetPoint();
            return this.Expand(Rectangle.FromLTRB(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y));
        }

        #endregion

        protected override void InitializeSublocators() {
            this.InitializeLocator(this.TopLeftLocator);
            this.InitializeLocator(this.BottomRightLocator);
        }
    }

    /// <summary>
    /// A FixedRectangleLocator simply returns the rectangle with which it was initialized
    /// </summary>
    public class FixedRectangleLocator : AbstractRectangleLocator
    {
        public FixedRectangleLocator(Rectangle r) {
            this.Rectangle = r;
        }

        public FixedRectangleLocator(int x, int y, int width, int height) {
            this.Rectangle = new Rectangle(x, y, width, height);
        }

        protected Rectangle Rectangle ;

        public override Rectangle GetRectangle() {
            return this.Expand(this.Rectangle);
        }
    }
}
