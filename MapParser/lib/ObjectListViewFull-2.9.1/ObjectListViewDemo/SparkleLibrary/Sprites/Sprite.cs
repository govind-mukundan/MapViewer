/*
 * Sprite - A graphic item that can be animated on a animation
 * 
 * Author: Phillip Piper
 * Date: 23/10/2009 10:29 PM
 *
 * Change log:
 * 2010-03-01   JPP  - Added FixedLocation and FixedBounds properties
 * 2010-02-08   JPP  - Handle multithreaded access to ImageSprites
 * 2009-10-23   JPP  - Initial version
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
    /// A Sprite is an animated graphic. 
    /// </summary>
    /// <remarks>
    /// <para>A sprite is animated by adding effects, which change its properties between frames.</para>
    /// </remarks>
    public class Sprite : Animateable, ISprite
    {
        #region Life and death

        /// <summary>
        /// Create new do nothing sprite.
        /// </summary>
        public Sprite() {
            this.Init();
            this.ReferenceBoundsLocator = new AnimationBoundsLocator();
            this.ReferenceBoundsLocator.Sprite = this;
        }

        #endregion

        #region Configuration properties

        /// <summary>
        /// Gets or sets where the sprite is located
        /// </summary>
        public virtual Point Location {
            get { return location; }
            set { location = value; }
        }
        private Point location;

        /// <summary>
        /// Gets or sets how transparent the sprite is. 
        /// 0.0 is completely transparent, 1.0 is completely opaque.
        /// </summary>
        public virtual float Opacity {
            get { return opacity; }
            set { opacity = value; }
        }
        private float opacity;

        /// <summary>
        /// Gets or sets the scaling that is applied to the extent of the sprite.
        /// The location of the sprite is not scaled.
        /// </summary>
        public virtual float Scale {
            get { return scale; }
            set { scale = value; }
        }
        private float scale;

        /// <summary>
        /// Gets or sets the size of the sprite
        /// </summary>
        public virtual Size Size {
            get { return size; }
            set { size = value; }
        }
        private Size size;

        /// <summary>
        /// Gets or sets the angle in degrees of the sprite.
        /// 0 means no angle, 90 means right edge lifted vertical.
        /// </summary>
        public virtual float Spin {
            get { return spin; }
            set { spin = value; }
        }
        private float spin;

        /// <summary>
        /// Gets or set if the spinning should be done around the
        /// top left of the sprite. If this is false, the sprite
        /// will spin around the center of the sprite.
        /// </summary>
        public bool SpinAroundOrigin {
            get { return spinAroundOrigin; }
            set { spinAroundOrigin = value; }
        }
        private bool spinAroundOrigin;

        /// <summary>
        /// Gets or sets the point at which this sprite will always be placed.
        /// </summary>
        /// <remarks>
        /// Most sprites play with their location as part of their animation.
        /// But other just want to stay in the same place. 
        /// Do not set this if you use Move or Goto effects on the sprite.
        /// </remarks>
        public IPointLocator FixedLocation {
            get { return fixedLocation; }
            set {
                fixedLocation = value;
                if (fixedLocation != null)
                    fixedLocation.Sprite = this;
            }
        }
        private IPointLocator fixedLocation;

        /// <summary>
        /// Gets or sets the bounds at which this sprite will always be placed.
        /// </summary>
        /// <remarks>See remarks on FixedLocation</remarks>
        public IRectangleLocator FixedBounds {
            get { return fixedBounds; }
            set {
                fixedBounds = value;
                if (fixedBounds != null)
                    fixedBounds.Sprite = this;
            }
        }
        private IRectangleLocator fixedBounds;

        #endregion

        #region Reference properties

        /// <summary>
        /// Gets or sets the bounds of the sprite. This is boundary within which
        /// the sprite will be drawn.
        /// </summary>
        public virtual Rectangle Bounds {
            get { 
                return new Rectangle(this.Location, this.Size); 
            }
            set { 
                this.Location = value.Location;
                this.Size = value.Size; 
            }
        }

        /// <summary>
        /// Gets the outer bounds of this sprite, which is normally the
        /// bounds of the control that is hosting the story board.
        /// Nothing outside of this rectangle will be drawn.
        /// </summary>
        public virtual Rectangle OuterBounds {
            get { return this.Animation.Bounds; }
        }

        /// <summary>
        /// Gets or sets the reference rectangle in relation to which
        /// the sprite will be drawn. This is normal the ClientArea of
        /// the control that is hosting the story board, though it
        /// could be a subarea of that control (e.g. a particular 
        /// cell within a ListView).
        /// </summary>
        /// <remarks>This value is controlled by ReferenceBoundsLocator property.</remarks>
        public virtual Rectangle ReferenceBounds {
            get { return this.ReferenceBoundsLocator.GetRectangle(); }
            set { this.ReferenceBoundsLocator = new FixedRectangleLocator(value); }
        }

        /// <summary>
        /// Gets or sets the locator that will calculate the reference rectangle 
        /// for the sprite.
        /// </summary>
        public virtual IRectangleLocator ReferenceBoundsLocator {
            get { return referenceBoundsLocator; }
            set { referenceBoundsLocator = value; }
        }
        private IRectangleLocator referenceBoundsLocator;

        #endregion

        #region Animation methods

        /// <summary>
        /// Draw the sprite in its current state
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g) {
        }

        /// <summary>
        /// Set the sprite to its initial state
        /// </summary>
        public virtual void Init() {
            this.Location = Point.Empty;
            this.Opacity = 1.0f;
            this.Scale = 1.0f;
            this.Spin = 0.0f;
        }

        /// <summary>
        /// The sprite should advance its state.
        /// </summary>
        /// <param name="elapsed">Milliseconds since Start() was called</param>
        /// <returns>True if Tick() should be called again</returns>
        public override bool Tick(long elapsed) {
            float elapsedAsFloat = (float)elapsed;
            foreach (EffectControlBlock cb in this.ControlBlocks) {
                if (!cb.Stopped && cb.ScheduledStartTick <= elapsed) {
                    if (!cb.Started) {
                        cb.StartTick = elapsed;
                        cb.Effect.Start();
                        cb.Started = true;
                    }
                    if (cb.Duration > 0 && elapsed <= cb.ScheduledEndTick) {
                        float fractionDone = (elapsedAsFloat - cb.StartTick) / cb.Duration;
                        cb.Effect.Apply(fractionDone);
                    } else {
                        cb.Effect.Apply(1.0f);
                        cb.Effect.Stop();
                        cb.Stopped = true;
                    }
                }
            }

            this.ApplyFixedLocations();

            return (this.ControlBlocks.Exists(delegate(EffectControlBlock cb) { return !cb.Stopped; }));
        }

        /// <summary>
        /// Apply any FixedLocation or FixedBounds properties that have been set
        /// </summary>
        protected void ApplyFixedLocations() {
            if (this.FixedBounds != null)
                this.Bounds = this.FixedBounds.GetRectangle();
            else
                if (this.FixedLocation != null)
                    this.Location = this.FixedLocation.GetPoint();
        }

        /// <summary>
        /// Reset the sprite to its neutral state
        /// </summary>
        public override void Reset() {
            this.Init();
            // Reset the effects in reverse order so their side-effects are unwound
            List<EffectControlBlock> sortedBlocks = new List<EffectControlBlock>(this.ControlBlocks);
            sortedBlocks.Sort(delegate(EffectControlBlock b1, EffectControlBlock b2) {
                return b2.ScheduledStartTick.CompareTo(b1.ScheduledStartTick);
            });
            foreach (EffectControlBlock cb in sortedBlocks) {
                cb.Effect.Reset();
                cb.StartTick = 0;
                cb.Started = false;
                cb.Stopped = false;
            }
        }

        /// <summary>
        /// Stop this sprite
        /// </summary>
        public override void Stop() {
            foreach (EffectControlBlock cb in this.ControlBlocks) {
                if (cb.Started && !cb.Stopped) {
                    cb.Effect.Stop();
                    cb.Stopped = true;
                }
            }
        }

        #endregion

        #region Effect methods

        /// <summary>
        /// Add a run-once effect which starts with the sprite
        /// </summary>
        /// <param name="effect"></param>
        public void Add(IEffect effect) {
            this.Add(0, 0, effect);
        }

        /// <summary>
        /// Add a run-once effect
        /// </summary>
        /// <param name="startTick"></param>
        /// <param name="effect"></param>
        public void Add(long startTick, IEffect effect) {
            this.Add(startTick, 0, effect);
        }

        /// <summary>
        /// Add an effect to this sprite
        /// </summary>
        /// <param name="startTick">When should the effect begin in ms since Start()</param>
        /// <param name="duration">For how many milliseconds should the effect continue. 
        /// 0 means the effect will be applied only once.</param>
        /// <param name="effect">The effect to be applied</param>
        public void Add(long startTick, long duration, IEffect effect) {
            EffectControlBlock cb = new EffectControlBlock(startTick, duration, effect);
            effect.Sprite = this;
            this.ControlBlocks.Add(cb);
        }
        private List<EffectControlBlock> ControlBlocks = new List<EffectControlBlock>();

        #endregion

        #region Drawing utility methods

        /// <summary>
        /// Apply any graphic state (translation, rotation, scale) to the given graphic context
        /// </summary>
        /// <remarks>Once the state is applied, the co-ordinates will be translated so that
        /// Location is at (0,0). This is necessary for spinning to work. So when the sprite
        /// draws itself, all its coordinattes should be based on 0,0, not on this.Location.
        /// This means you cannot use this.Bounds when drawing. 
        /// g.DrawRectangle(this.Bounds, Pens.Black); will not draw your rectangle where you want.
        /// g.DrawRectangle(new Rectangle(Point.Empty, this.Size), Pens.Black); will work.</remarks>
        /// <param name="g">The graphic to be configured</param>
        protected virtual void ApplyState(Graphics g) {
            Matrix m = new Matrix();

            if (this.Spin != 0) {
                Rectangle r = this.Bounds;
                // TODO: Make a SpinCentre property
                Point spinCentre = r.Location;
                if (!this.SpinAroundOrigin)
                    spinCentre = new Point(r.X + r.Width / 2, r.Y + r.Height / 2);
                m.RotateAt(this.Spin, spinCentre);
            }

            m.Translate((float)this.Location.X, (float)this.Location.Y);

            g.Transform = m;
        }

        /// <summary>
        /// Remove any graphic state applied by ApplyState().
        /// </summary>
        /// <param name="g">The graphic to be configured</param>
        protected virtual void UnapplyState(Graphics g) {
            g.ResetTransform();
        }

        #endregion

        /// <summary>
        /// Instances of this class hold the state of an effect as it progresses
        /// </summary>
        protected class EffectControlBlock
        {
            public EffectControlBlock(long start, long duration, IEffect effect) {
                this.ScheduledStartTick = start;
                this.Duration = duration;
                this.Effect = effect;
            }

            public long ScheduledStartTick;
            public long Duration ;

            public bool Started ;
            public bool Stopped ;

            public long StartTick ;
            public long ScheduledEndTick {
                get { return this.StartTick + this.Duration; }
            }

            public IEffect Effect ;
        }
    }
}
