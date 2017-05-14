/*
 * PointLocator - Generalized mechanism to calculate points 
 * 
 * Author: Phillip Piper
 * Date: 18/01/2010 5:48 PM
 *
 * Change log:
 * 2010-02-05   JPP  - Added RectangleWalker and PointWalker
 * 2010-01-20   JPP  - Use proportions rather than just fixed corners
 * 2010-01-18   JPP  - Initial version
 *
 * To do:
 * - Replace Corner with Drawing.ContentAlignment
 * - Add circles: ICircleLocator, PointOnCircleLocator, CircleWalker
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

using System;
using System.Drawing;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Corner defines the nine commonly used locations within a rectangle.
    /// Technically, they are not all "corners".
    /// </summary>
    public enum Corner
    {
        None = 0,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    /// <summary>
    /// This defines whether a walk proceeds in a clockwise or anticlockwise
    /// direction.
    /// </summary>
    public enum WalkDirection
    {
        Clockwise,
        Anticlockwise
    }

    /// <summary>
    /// A IPointLocator calculates a point relative to a given sprite.
    /// </summary>
    public interface IPointLocator
    {
        ISprite Sprite { get; set; }
        Point GetPoint();
    }
    
    /// <summary>
    /// A useful base class for point locators
    /// </summary>
    public class AbstractPointLocator : IPointLocator
    {
        #region Properties

        public Point Offset ;

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

        /// <summary>
        /// Gets the point from the locator
        /// </summary>
        /// <returns></returns>
        public virtual Point GetPoint() {
            return Point.Empty;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// The sprite associate with this locator has changed. 
        /// Make sure any dependent locators are updated
        /// </summary>
        protected virtual void InitializeSublocators() {
        }

        /// <summary>
        /// Offset a point by our Offset property
        /// </summary>
        /// <param name="pt">The point to be offset</param>
        /// <returns></returns>
        protected Point OffsetBy(Point pt) {
            if (this.Offset == Point.Empty)
                return pt;

            Point pt2 = pt;
            pt2.Offset(this.Offset);
            return pt2;
        }

        /// <summary>
        /// Initialize the given locator so it refers to our sprite,
        /// unless it already refers to another one
        /// </summary>
        /// <param name="locator">The locator to be initialized</param>
        protected void InitializeLocator(IPointLocator locator) {
            if (locator != null && locator.Sprite == null)
                locator.Sprite = this.Sprite;
        }

        /// <summary>
        /// Initialize the given locator so it refers to our sprite,
        /// unless it already refers to another one
        /// </summary>
        /// <param name="locator">The locator to be initialized</param>
        protected void InitializeLocator(IRectangleLocator locator) {
            if (locator != null && locator.Sprite == null)
                locator.Sprite = this.Sprite;
        }

        #endregion
    }

    /// <summary>
    /// A FixedPointLocator simply returns the point with which it is initialized.
    /// </summary>
    public class FixedPointLocator : AbstractPointLocator
    {
        public FixedPointLocator(Point pt) {
            this.Point = pt;
        }

        protected Point Point ;

        public override Point GetPoint() {
            return this.OffsetBy(this.Point);
        }
    }

    /// <summary>
    /// A PointOnRectangleLocator calculate a point relative to rectangle.
    /// </summary>
    /// <remarks>
    /// <para>
    /// "Relative to a rectangle" can be indicated through a Corner, or through a fraction of
    /// the width/heights. Giving (0.5f, 0.5f) indicates the middle of the rectangle.
    /// </para>
    /// <para>
    /// The reference rectangle defaults to the bounds
    /// of the animation
    /// </para>
    /// </remarks>
    public class PointOnRectangleLocator : AbstractPointLocator
    {
        #region Life and death

        public PointOnRectangleLocator() 
            : this(Corner.TopLeft) {
        }

        public PointOnRectangleLocator(Corner corner)
            : this(new AnimationBoundsLocator(), corner, Point.Empty) {
        }

        public PointOnRectangleLocator(Corner corner, Point offset)
            : this(new AnimationBoundsLocator(), corner, offset) {
        }

        public PointOnRectangleLocator(IRectangleLocator locator, Corner corner)
            : this(locator, corner, Point.Empty) {
        }

        public PointOnRectangleLocator(IRectangleLocator locator, Corner corner, Point offset) {
            this.RectangleLocator = locator;
            this.PointProportions = this.ConvertCornerToProportion(corner);
            this.Offset = offset;
        }

        public PointOnRectangleLocator(IRectangleLocator locator, float horizontal, float vertical)
            : this(locator, horizontal, vertical, Point.Empty) {
        }

        public PointOnRectangleLocator(IRectangleLocator locator, float horizontal, float vertical, Point offset) {
            this.RectangleLocator = locator;
            this.PointProportions = new SizeF(horizontal, vertical);
            this.Offset = offset;
        }

        #endregion

        #region Configuration properties

        protected IRectangleLocator RectangleLocator ;
        protected SizeF PointProportions ;

        #endregion

        #region Public interface

        public override Point GetPoint() {
            Rectangle r = this.RectangleLocator.GetRectangle();
            Point pt = this.CalculateProportionalPosition(r, this.PointProportions);

            return this.OffsetBy(pt);
        }

        #endregion

        protected override void InitializeSublocators() {
            this.InitializeLocator(this.RectangleLocator);
        }

        #region Calculations

        protected Point CalculateProportionalPosition(Rectangle r, SizeF proportions) {
            return new Point(
                r.X + (int)(r.Width * proportions.Width),
                r.Y + (int)(r.Height * proportions.Height));
        }

        protected SizeF ConvertCornerToProportion(Corner corner) {
            switch (corner) {
                case Corner.TopLeft:
                    return new SizeF(0.0f, 0.0f);
                case Corner.TopCenter:
                    return new SizeF(0.5f, 0.0f);
                case Corner.TopRight:
                    return new SizeF(1.0f, 0.0f);
                case Corner.MiddleLeft:
                    return new SizeF(0.0f, 0.5f);
                case Corner.MiddleCenter:
                    return new SizeF(0.5f, 0.5f);
                case Corner.MiddleRight:
                    return new SizeF(1.0f, 0.5f);
                case Corner.BottomLeft:
                    return new SizeF(0.0f, 1.0f);
                case Corner.BottomCenter:
                    return new SizeF(0.5f, 1.0f);
                case Corner.BottomRight:
                    return new SizeF(1.0f, 1.0f);
            }

            // Should never reach here
            return new SizeF(0.0f, 0.0f);
        }

        #endregion
    }

    /// <summary>
    /// An DifferenceLocator is simply the difference between
    /// two point locators
    /// </summary>
    /// <remarks>I can't think of a case where this would actually
    /// be useful. It might disappear. JPP 2010/02/05</remarks>
    public class DifferenceLocator : AbstractPointLocator
    {
        #region Life and death

        public DifferenceLocator(IPointLocator locator1, IPointLocator locator2) 
            : this(locator1, locator2, Point.Empty) {
        }

        public DifferenceLocator(IPointLocator locator1, IPointLocator locator2, Point offset) {
            this.Locator1 = locator1;
            this.Locator2 = locator2;
            this.Offset = offset;
        }

        #endregion

        #region Configuration properties

        protected IPointLocator Locator1 ;
        protected IPointLocator Locator2 ;

        #endregion

        #region Public interface

        public override Point GetPoint() {
            Point pt1 = this.Locator1.GetPoint();
            Point pt2 = this.Locator2.GetPoint();

            return this.OffsetBy(new Point(pt1.X - pt2.X, pt1.Y - pt2.Y));
        }

        #endregion

        protected override void InitializeSublocators() {
            this.InitializeLocator(this.Locator1);
            this.InitializeLocator(this.Locator2);
        }
    }


    /// <summary>
    /// Instances of this locator return the location that a sprite must 
    /// move to so that one of its points (SpritePointLocator) is directly 
    /// over another point (ReferencePointLocator).
    /// </summary>
    public class AlignedSpriteLocator : AbstractPointLocator
    {
        #region Life and death

        public AlignedSpriteLocator() {
        }

        public AlignedSpriteLocator(IPointLocator referencePointLocator, IPointLocator spritePointLocator) :
            this(referencePointLocator, spritePointLocator, Point.Empty) {
        }

        public AlignedSpriteLocator(IPointLocator referencePointLocator, IPointLocator spritePointLocator, Point offset) {
            this.ReferencePointLocator = referencePointLocator;
            this.SpritePointLocator = spritePointLocator;
            this.Offset = offset;
        }

        #endregion

        #region Configuration properties

        protected IPointLocator ReferencePointLocator ;
        protected IPointLocator SpritePointLocator ;

        #endregion

        #region Public interface

        public override Point GetPoint() {
            Point location = this.Sprite.Location;
            Point spritePoint = this.SpritePointLocator.GetPoint();
            spritePoint.Offset(-location.X, -location.Y);
            Point referencePoint = this.ReferencePointLocator.GetPoint();
            referencePoint.Offset(-spritePoint.X, -spritePoint.Y);
            return this.OffsetBy(referencePoint);
        }

        #endregion

        #region Initializations

        protected override void InitializeSublocators() {
            this.InitializeLocator(this.ReferencePointLocator);
            this.InitializeLocator(this.SpritePointLocator);
        }

        #endregion
    }

    /// <summary>
    /// A BaseWalker provides useful methods to classes that walk between
    /// a series of point.
    /// </summary>
    public class BaseWalker : AbstractPointLocator
    {
        public float WalkProgress ;

        /// <summary>
        /// Walk the given distance between the given points, starting at pt.
        /// Return the point where the walk ends. The walk will loop through
        /// the point multiple times if necessary to exhaust the distance.
        /// </summary>
        /// <param name="distance">How far to walk?</param>
        /// <param name="pt">Where to start.</param>
        /// <param name="targetPoints">The control points in order</param>
        /// <returns>The point where the walk ends</returns>
        protected Point Walk(int distance, Point pt, Point[] targetPoints) {
            int i = 0;
            double remaining = distance;
            while (true) {
                double distanceToPoint = this.CalculateDistance(pt, targetPoints[i]);
                if (remaining <= distanceToPoint)
                    return this.CalculateEndPoint(pt, targetPoints[i], (int)remaining);

                pt = targetPoints[i];
                remaining -= distanceToPoint;
                i = (i + 1) % targetPoints.Length;
            }
        }

        /// <summary>
        /// Given a start and end points, calculate the point that is
        /// distance along that line.
        /// </summary>
        /// <param name="pt1">Line start point</param>
        /// <param name="pt2">Line end point</param>
        /// <param name="distance">Distance from start</param>
        /// <returns>The point that is 'distance' from the start</returns>
        protected Point CalculateEndPoint(Point pt1, Point pt2, int distance) {
            int dx = pt2.X - pt1.X;
            int dy = pt2.Y - pt1.Y;

            if (dx == 0)
                return new Point(pt1.X, pt1.Y + (distance * Math.Sign(dy)));

            if (dy == 0)
                return new Point(pt1.X + (distance * Math.Sign(dx)), pt1.Y);

            double inverseMagnitude = 1.0 / Math.Sqrt((dx * dx) + (dy * dy));

            return new Point(
                pt1.X + (int)(dx * inverseMagnitude * distance),
                pt1.Y + (int)(dy * inverseMagnitude * distance));
        }

        /// <summary>
        /// Calculate the distance between the two points
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        protected double CalculateDistance(Point pt1, Point pt2) {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;

            if (dx == 0)
                return Math.Abs(dy);

            if (dy == 0)
                return Math.Abs(dx);

            return Math.Sqrt((dx * dx) + (dy * dy));
        }
    }
    
    /// <summary>
    /// A PointWalker generates points as if walking between a series of control points.
    /// The progress through the walk is controlled by the WalkProgress property.
    /// </summary>
    public class PointWalker : BaseWalker
    {
        #region Life and death

        public PointWalker() {
            this.Locators = new List<IPointLocator>();
        }

        public PointWalker(IEnumerable<Point> points) {
            List<IPointLocator> locators = new List<IPointLocator>();
            foreach (Point pt in points)
                locators.Add(new FixedPointLocator(pt));
            this.Locators = locators;
        }

        public PointWalker(IEnumerable<Point> points, Point offset)
            : this(points) {
            this.Offset = offset;
        }

        public PointWalker(IEnumerable<IPointLocator> locators) {
            this.Locators = locators;
        }

        public PointWalker(IEnumerable<IPointLocator> locators, Point offset)
            : this(locators) {
            this.Offset = offset;
        }

        #endregion

        #region Configuration properties

        protected IEnumerable<IPointLocator> Locators ;

        #endregion
                
        #region Public interface

        public override Point GetPoint() {
            Point[] points = this.GetPoints();
            int totalDistance = this.CalculateTotalDistance(points);
            return this.OffsetBy(this.Walk((int)(totalDistance * this.WalkProgress), points[0], points));
        }

        #endregion
        
        #region Initializations

        protected override void InitializeSublocators() {
            foreach (IPointLocator locator in this.Locators)
                this.InitializeLocator(locator);
        }

        #endregion

        #region Calculations

        private Point[] GetPoints() {
            List<Point> points = new List<Point>();
            foreach (IPointLocator locator in this.Locators)
                points.Add(locator.GetPoint());
            return points.ToArray();
        }

        private int CalculateTotalDistance(Point[] points) {
            double distance = 0;
            for (int i = 1; i < points.Length; i++)
                distance += this.CalculateDistance(points[i - 1], points[i]);
            return (int)Math.Ceiling(distance + 1);
        }

        #endregion
    }

    /// <summary>
    /// Instances of this locator return points on the perimeter of
    /// a rectangle.
    /// </summary>
    public class RectangleWalker : BaseWalker
    {
        #region Life and death

        public RectangleWalker() {
        }

        public RectangleWalker(IRectangleLocator rectangleLocator)
            : this(rectangleLocator, WalkDirection.Clockwise) {
        }

        public RectangleWalker(IRectangleLocator rectangleLocator, WalkDirection direction)
            : this(rectangleLocator, direction, null) {

        }

        public RectangleWalker(IRectangleLocator rectangleLocator, WalkDirection direction, IPointLocator startPointLocator) {
            this.RectangleLocator = rectangleLocator;
            this.Direction = direction;
            this.StartPointLocator = startPointLocator;
        }

        #endregion

        #region Configuration properties

        public IRectangleLocator RectangleLocator ;
        public IPointLocator StartPointLocator ;
        public WalkDirection Direction ;

        #endregion

        #region Public interface

        public override Point GetPoint() {
            Rectangle r = this.RectangleLocator.GetRectangle();
            int totalLength = r.Width * 2 + r.Height * 2;
            int distanceToWalk = (int)(totalLength * this.WalkProgress);

            Point pt = this.RationalizeStartPoint(r, this.StartPointLocator.GetPoint());
            int segment = this.DecideSegment(r, pt);

            Point[] targetPoints = new Point[] {};

            if (this.Direction == WalkDirection.Clockwise) {
                switch (segment) {
                    case 0:
                        targetPoints = new Point[] { r.Location, new Point(r.Right, r.Top), new Point(r.Right, r.Bottom), new Point(r.Left, r.Bottom) };
                        break;
                    case 1:
                        targetPoints = new Point[] { new Point(r.Right, r.Top), new Point(r.Right, r.Bottom), new Point(r.Left, r.Bottom), r.Location, };
                        break;
                    case 2:
                        targetPoints = new Point[] { new Point(r.Right, r.Bottom), new Point(r.Left, r.Bottom), r.Location, new Point(r.Right, r.Top) };
                        break;
                    case 3:
                        targetPoints = new Point[] { new Point(r.Left, r.Bottom), r.Location, new Point(r.Right, r.Top), new Point(r.Right, r.Bottom) };
                        break;
                }
            } else {
                switch (segment) {
                    case 0:
                        targetPoints = new Point[] { new Point(r.Left, r.Bottom), new Point(r.Right, r.Bottom), new Point(r.Right, r.Top), new Point(r.Left, r.Top) };
                        break;
                    case 1:
                        targetPoints = new Point[] { new Point(r.Right, r.Bottom), new Point(r.Right, r.Top), new Point(r.Left, r.Top), new Point(r.Left, r.Bottom) };
                        break;
                    case 2:
                        targetPoints = new Point[] { new Point(r.Right, r.Top), new Point(r.Left, r.Top), new Point(r.Left, r.Bottom), new Point(r.Right, r.Bottom) };
                        break;
                    case 3:
                        targetPoints = new Point[] { new Point(r.Left, r.Top), new Point(r.Left, r.Bottom), new Point(r.Right, r.Bottom), new Point(r.Right, r.Top) };
                        break;
                }
            }

            return this.OffsetBy(this.Walk(distanceToWalk, pt, targetPoints));
        }

        protected Point RationalizeStartPoint(Rectangle r, Point point) {
            if (point == Point.Empty)
                return r.Location;

            return new Point(
                Math.Min(r.Right, Math.Max(r.Left, point.X)),
                Math.Min(r.Bottom, Math.Max(r.Top, point.Y)));
        }

        protected int DecideSegment(Rectangle rectangleToWalk, Point point) {
            // Segments (numbers are clockwise):
            // 0 - left
            // 1 - top
            // 2 - right
            // 3 - bottom

            if (rectangleToWalk.Location == point) {
                 return this.Direction == WalkDirection.Clockwise ? 1 : 0;
            }

            if (point.X == rectangleToWalk.Left)
                return 0;
            if (point.Y == rectangleToWalk.Top)
                return 1;
            if (point.X == rectangleToWalk.Right)
                return 2;
            if (point.X == rectangleToWalk.Bottom)
                return 3;

            // Point not on perimeter
            //TODO: Do something clever here
            return this.Direction == WalkDirection.Clockwise ? 1 : 0;
        }

        #endregion

        #region Initializations

        protected override void InitializeSublocators() {
            this.InitializeLocator(this.RectangleLocator);
            this.InitializeLocator(this.StartPointLocator);
        }

        #endregion
    }
}
