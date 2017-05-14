/*
 * OlvDecorationAdapter - Put an animation as a decoration on an ObjectListView
 *
 * Author: Phillip Piper
 * Date: 24/02/2010 8:18 PM
 *
 * Change log:
 * 2010-02-24   JPP  - Initial version
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

using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Support running an animation as a decoration on an ObjectListView
    /// </summary>
    class AnimatedDecoration : AbstractDecoration
    {
        #region Life and death

        /// <summary>
        /// Create a decoration that will draw an animation onto the given ObjectListView
        /// </summary>
        /// <param name="olv"></param>
        public AnimatedDecoration(ObjectListView olv) {
            this.ListView = olv;
            this.Animation = new Animation();
            this.SubscribeEvents();
        }

        /// <summary>
        /// Create a decoration that will draw an animation around the row of the given model
        /// </summary>
        /// <param name="ModelObject">The model that identifies the row on which the 
        /// animation will draw. If this is null, the decoration will drawn around the 
        /// whole ListView.</param>
        public AnimatedDecoration(ObjectListView olv, object modelObject)
            : this(olv) {
            this.ModelObject = modelObject;
        }

        /// <summary>
        /// Create a decoration that will draw an animation around a cell of the given model
        /// </summary>
        /// <param name="ModelObject"></param>
        public AnimatedDecoration(ObjectListView olv, object modelObject, OLVColumn column)
            : this(olv) {
            this.ModelObject = modelObject;
            this.Column = column;
        }

        /// <summary>
        /// Create a decoration that will draw an animation around a cell of the given model
        /// </summary>
        /// <param name="ModelObject"></param>
        public AnimatedDecoration(ObjectListView olv, OLVListItem item, OLVListSubItem subItem)
            : this(olv) {
            this.ModelObject = item.RowObject;
            this.Column = olv.GetColumn(item.SubItems.IndexOf(subItem));
        }

        #endregion

        #region Public properties

        public ObjectListView ListView { get; protected set; }
        public Animation Animation { get; protected set; }
        public object ModelObject { get; protected set; }
        public OLVColumn Column { get; protected set; }

        #endregion

        #region Subscriptions

        protected void SubscribeEvents() {
            this.ListView.Disposed += new EventHandler(ListView_Disposed);
            this.Animation.Started += new EventHandler<StartAnimationEventArgs>(Animation_Started);
            this.Animation.Stopped += new EventHandler<StopAnimationEventArgs>(Animation_Stopped);
            this.Animation.Redraw += new EventHandler<RedrawEventArgs>(Animation_Redraw);
            this.Animation.Ticked += new EventHandler<TickEventArgs>(Animation_Ticked);
        }

        protected void UnsubscribeEvents() {
            this.ListView.Disposed -= new EventHandler(ListView_Disposed);
            this.Animation.Started -= new EventHandler<StartAnimationEventArgs>(Animation_Started);
            this.Animation.Stopped -= new EventHandler<StopAnimationEventArgs>(Animation_Stopped);
            this.Animation.Redraw -= new EventHandler<RedrawEventArgs>(Animation_Redraw);
            this.Animation.Ticked -= new EventHandler<TickEventArgs>(Animation_Ticked);
        }

        #endregion

        #region IOverlay implementation

        public override void Draw(ObjectListView olv, Graphics g, Rectangle r) {
            if (!this.Animation.Running)
                return;

            if (this.ModelObject != null) {
                this.ListItem = this.ListView.ModelToItem(this.ModelObject);
                if (this.ListItem == null)
                    return;
                if (this.Column != null) {
                    this.Animation.Bounds = this.ListItem.GetSubItemBounds(this.Column.Index);
                } else 
                    this.Animation.Bounds = this.RowBounds;
            } else {
                this.Animation.Bounds = r;
            } 
            
            this.Animation.Draw(g);
        }

        #endregion

        #region Event handlers

        protected virtual void ListView_Disposed(object sender, EventArgs e) {
            this.Animation.Stop();
        }

        protected virtual void Animation_Started(object sender, StartAnimationEventArgs e) {
            this.Animation.Bounds = this.ListView.ContentRectangle;
            this.ListView.AddDecoration(this);
        }

        protected virtual void Animation_Stopped(object sender, StopAnimationEventArgs e) {
            if (!this.ListView.IsDisposed && this.ListView.IsHandleCreated) {
                this.ListView.Invoke((MethodInvoker)delegate {
                    this.UnsubscribeEvents();
                    this.ListView.RemoveDecoration(this);
                });
            }
        }

        protected virtual void Animation_Redraw(object sender, RedrawEventArgs e) {
            if (!this.ListView.IsDisposed && this.ListView.IsHandleCreated) {
                this.ListView.Invoke((MethodInvoker)delegate {
                    this.ListView.Invalidate();
                });
            }
        }

        protected virtual void Animation_Ticked(object sender, TickEventArgs e) {
        }

        #endregion
    }
}
