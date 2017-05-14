/*
 * Sprite - A graphic item that can be animated on a animation
 * 
 * Author: Phillip Piper
 * Date: 2/3/2010 9:36 AM
 *
 * Change log:
 * 2010-03-02   JPP  - Initial version (Separated from Sprite.cs)
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
    public interface ISprite : IAnimateable
    {
        /// <summary>
        /// Gets or sets where the sprite is located
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Gets or sets how transparent the sprite is. 
        /// 0.0 is completely transparent, 1.0 is completely opaque.
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        /// Gets or sets the scaling that is applied to the extent of the sprite.
        /// The location of the sprite is not scaled.
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// Gets or sets the size of the sprite
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Gets or sets the angle in degrees of the sprite.
        /// 0 means no angle, 90 means right edge lifted vertical.
        /// </summary>
        float Spin { get; set; }

        /// <summary>
        /// Gets or sets the bounds of the sprite. This is boundary within which
        /// the sprite will be drawn.
        /// </summary>
        Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets the outer bounds of this sprite, which is normally the
        /// bounds of the control that is hosting the story board.
        /// Nothing outside of this rectangle will be drawn.
        /// </summary>
        Rectangle OuterBounds { get; }

        /// <summary>
        /// Gets or sets the reference rectangle in relation to which
        /// the sprite will be drawn. This is normal the ClientArea of
        /// the control that is hosting the story board, though it
        /// could be a subarea of that control (e.g. a particular 
        /// cell within a ListView).
        /// </summary>
        /// <remarks>This value is controlled by ReferenceBoundsLocator property.</remarks>
        Rectangle ReferenceBounds { get; set; }

        /// <summary>
        /// Gets or sets the locator that will calculate the reference rectangle 
        /// for the sprite.
        /// </summary>
        IRectangleLocator ReferenceBoundsLocator { get; set; }

        /// <summary>
        /// Gets or sets the point at which this sprite will always be placed.
        /// </summary>
        /// <remarks>
        /// Most sprites play with their location as part of their animation.
        /// But other just want to stay in the same place. 
        /// Do not set this if you use Move or Goto effects on the sprite.
        /// </remarks>
        IPointLocator FixedLocation { get; set; }

        /// <summary>
        /// Gets or sets the bounds at which this sprite will always be placed.
        /// </summary>
        /// <remarks>See remarks on FixedLocation</remarks>
        IRectangleLocator FixedBounds { get; set; }

        /// <summary>
        /// Draw the sprite in its current state
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);

        /// <summary>
        /// Add an Effect to this sprite. This effect will run at the beginning of
        /// the sprite and will have 0 duration.
        /// </summary>
        /// <param name="effect">The effect to be applied to the sprite</param>
        void Add(IEffect effect);

        /// <summary>
        /// Add an Effect to this sprite. This effect will commences startTick's
        /// after the sprite begins and will have 0 duration
        /// </summary>
        /// <param name="startTick">When will the effect begins?</param>
        /// <param name="effect">What effect will be applied?</param>
        void Add(long startTick, IEffect effect);

        /// <summary>
        /// The main entry point for adding effects to Sprites.
        /// </summary>
        /// <param name="startTick">When will the effect begin?</param>
        /// <param name="duration">For how long will it last?</param>
        /// <param name="effect">What effect will be applied?</param>
        void Add(long startTick, long duration, IEffect effect);
    }
}