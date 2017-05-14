/*
 * Locators - A factory to create useful locators
 * 
 * Author: Phillip Piper
 * Date: 19/10/2009 1:02 AM
 *
 * Change log:
 * 2010-01-18   JPP  - Split out PointLocator.cs and RectangleFromCornersLocator.cs
 * 2009-10-19   JPP  - Initial version
 * 
 * To do:
 *
 * Copyright (C) 20009-2014 Phillip Piper
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
    /// Locators is a Factory that simplifies the creation of common locators.
    /// </summary>
    /// <remarks>Obviously, locators can still be created directly, but this class
    /// makes the creation of common locators easy.</remarks>
    public static class Locators
    {
        /// <summary>
        /// Create a PointLocator for the given fixed point
        /// </summary>
        public static IPointLocator At(int x, int y) {
            return new FixedPointLocator(new Point(x, y));
        }

        public static IPointLocator At(Point pt) {
            return new FixedPointLocator(pt);
        }

        /// <summary>
        /// Create a PointLocator that will align the given corner of a sprite
        /// at the corresponding corner of the animation.
        /// </summary>
        /// <remarks>For example,
        /// Locators.SpriteAligned(Corners.BottomRight) means the bottom right corner
        /// of the sprite will be placed at the bottom right corner of the animation.
        /// </remarks>
        /// <param name="corner">The corner to be aligned AND the corner at which it will be aligned</param>
        /// <returns>A point locator</returns>
        public static IPointLocator SpriteAligned(Corner corner) {
            return Locators.SpriteAligned(corner, corner, Point.Empty);
        }

        /// <summary>
        /// The same as SpriteAligned(Corner) but offset by a constant amount.
        /// </summary>
        /// <param name="corner"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static IPointLocator SpriteAligned(Corner corner, Point offset) {
            return Locators.SpriteAligned(corner, corner, offset);
        }

        /// <summary>
        /// Create a PointLocator that will align the given corner of a sprite
        /// at the given corner of the animation.
        /// </summary>
        /// <remarks>For example,
        /// Locators.SpriteAligned(Corners.MiddleCenter, Corner.BottomRight) means the center
        /// of the sprite will be placed at the bottom right corner of the animation.
        /// </remarks>
        /// <param name="spriteCorner">The corner of the sprite to be aligned</param>
        /// <param name="corner">The corner at which it will be aligned</param>
        /// <returns>A point locator</returns>
        public static IPointLocator SpriteAligned(Corner spriteCorner, Corner animationCorner) {
            return Locators.SpriteAligned(spriteCorner, animationCorner, Point.Empty);
        }

        /// <summary>
        /// The same as SpriteAligned(Corner, Corner) but offset by a constant amount.
        /// </summary>
        /// <param name="spriteCorner"></param>
        /// <param name="animationCorner"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static IPointLocator SpriteAligned(Corner spriteCorner, Corner animationCorner, Point offset) {
            return new AlignedSpriteLocator(
                Locators.AnimationBoundsPoint(animationCorner),
                Locators.SpriteBoundsPoint(spriteCorner),
                offset);
        }

        /// <summary>
        /// Create a PointLocator that will align the given corner of a sprite
        /// at the proportional location of bounds of the animation.
        /// </summary>
        /// <remarks>For example,
        /// Locators.SpriteAligned(Corners.MiddleCenter, 0.6f, 0.7f) means the center
        /// of the sprite will be placed 60% across and 70% down the animation.
        /// </remarks>
        /// <param name="spriteCorner">The corner of the sprite to be aligned</param>
        /// <param name="proportionX">The x axis proportion</param>
        /// <param name="proportionY">The y axis proportion</param>
        /// <returns>A point locator</returns>
        public static IPointLocator SpriteAligned(Corner spriteCorner, float proportionX, float proportionY) {
            return Locators.SpriteAligned(spriteCorner, proportionX, proportionY, Point.Empty);
        }

        /// <summary>
        /// The same as SpriteAligned(Corner, float, float) but offset by a constant amount.
        /// </summary>
        /// <param name="spriteCorner"></param>
        /// <param name="proportionX"></param>
        /// <param name="proportionY"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static IPointLocator SpriteAligned(Corner spriteCorner, float proportionX, float proportionY, Point offset) {
            return new AlignedSpriteLocator(
                Locators.AnimationBoundsPoint(proportionX, proportionY),
                Locators.SpriteBoundsPoint(spriteCorner),
                offset);
        }

        /// <summary>
        /// Create a FixedRectangleLocator for the given fixed co-ordinates.
        /// </summary>
        public static IRectangleLocator At(int x, int y, int width, int height) {
            return new FixedRectangleLocator(new Rectangle(x, y, width, height));
        }

        public static IRectangleLocator At(Rectangle r) {
            return new FixedRectangleLocator(r);
        }

        /// <summary>
        /// Create a Locator that gives the bounds of the animation
        /// </summary>
        /// <returns></returns>
        public static IRectangleLocator AnimationBounds() {
            return new AnimationBoundsLocator();
        }

        /// <summary>
        /// Create a Locator that gives the bounds of the animation inset by a fixed amount
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static IRectangleLocator AnimationBounds(int x, int y) {
            return new AnimationBoundsLocator(x, y);
        }

        /// <summary>
        /// Create a Locator that gives the bounds of the sprite
        /// </summary>
        /// <returns></returns>
        public static IRectangleLocator SpriteBounds() {
            return new SpriteBoundsLocator();
        }

        /// <summary>
        /// Create a Locator that gives the bounds of the sprite inset by a fixed amount
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static IRectangleLocator SpriteBounds(int x, int y) {
            return new SpriteBoundsLocator(x, y);
        }

        /// <summary>
        /// Returns a Locator that gives a point on the bounds of a sprite
        /// </summary>
        /// <param name="corner"></param>
        /// <returns></returns>
        public static IPointLocator SpriteBoundsPoint(Corner corner) {
            return new PointOnRectangleLocator(new SpriteBoundsLocator(), corner);
        }

        /// <summary>
        /// Returns a Locator that gives a point proportional to the bounds of a sprite
        /// </summary>
        /// <param name="proportionX"></param>
        /// <param name="proportionY"></param>
        /// <returns></returns>
        public static IPointLocator SpriteBoundsPoint(float proportionX, float proportionY) {
            return new PointOnRectangleLocator(new SpriteBoundsLocator(), proportionX, proportionY);
        }

        /// <summary>
        /// Returns a Locator which gives a corner of the bounds of the animation
        /// </summary>
        /// <param name="corner"></param>
        /// <returns></returns>
        public static IPointLocator AnimationBoundsPoint(Corner corner) {
            return new PointOnRectangleLocator(new AnimationBoundsLocator(), corner);
        }

        /// <summary>
        /// Returns a Locator which gives a corner of the bounds of the animation inset by a fixed amount
        /// </summary>
        /// <param name="corner"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <returns></returns>
        public static IPointLocator AnimationBoundsPoint(Corner corner, int xOffset, int yOffset) {
            return new PointOnRectangleLocator(new AnimationBoundsLocator(), corner, new Point(xOffset, yOffset));
        }

        /// <summary>
        /// Returns a Locator that gives a point proportional to the bounds of the animation.
        /// </summary>
        /// <param name="proportionX"></param>
        /// <param name="proportionY"></param>
        /// <returns></returns>
        public static IPointLocator AnimationBoundsPoint(float proportionX, float proportionY) {
            return new PointOnRectangleLocator(new AnimationBoundsLocator(), proportionX, proportionY);
        }
    }
}
