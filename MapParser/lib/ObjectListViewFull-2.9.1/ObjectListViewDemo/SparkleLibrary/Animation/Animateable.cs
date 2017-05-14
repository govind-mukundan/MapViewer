/*
 * Animateable - A item that can be placed in an animation
 * 
 * Author: Phillip Piper
 * Date: 23/10/2009 10:39 PM
 *
 * Change log:
 * 2009-10-23   JPP  - Initial version
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
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
    public interface IAnimateable
    {
        /// <summary>
        /// Gets or sets the animation that this component belongs to
        /// </summary>
        Animation Animation { get; set; }

        /// <summary>
        /// This component is being started. It should acquire any resources that it needs
        /// </summary>
        void Start();

        /// <summary>
        /// A unit of time has passed and the animation component should advance its state
        /// if sufficient time has passed.
        /// </summary>
        /// <param name="elapsed">The number of milliseconds since Start() was called.</param>
        /// <returns>True if Tick() should be called again</returns>
        bool Tick(long elapsed);

        /// <summary>
        /// Revert this component to its initial state. 
        /// </summary>
        void Reset();

        /// <summary>
        /// This component has been stopped. It should release any resources acquired in Start().
        /// </summary>
        void Stop();
    }

    /// <summary>
    /// A Animateable is the base class for any item that can be 
    /// placed within an Animation.
    /// </summary>
    public class Animateable : IAnimateable
    {
        /// <summary>
        /// Gets or sets the animation that this component belongs to
        /// </summary>
        public Animation Animation {
            get { return animation; }
            set { animation = value; }
        }
        private Animation animation;

        /// <summary>
        /// This component is being started. It should acquire any resources that it needs
        /// </summary>
        public virtual void Start() {
        }

        /// <summary>
        /// A unit of time has passed and the animation component should advance its state
        /// if sufficient time has passed.
        /// </summary>
        /// <param name="elapsed">The number of milliseconds since Start() was called.</param>
        /// <returns>True if Tick() should be called again</returns>
        public virtual bool Tick(long elapsed) {
            return false;
        }

        /// <summary>
        /// Revert this component to its initial state. 
        /// </summary>
        public virtual void Reset() {
        }

        /// <summary>
        /// This component has been stopped. It should release any resources acquired in Start().
        /// </summary>
        public virtual void Stop() {
        }
    }
}
