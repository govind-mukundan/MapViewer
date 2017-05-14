/*
 * Events - All events triggerable by an animation
 * 
 * Author: Phillip Piper
 * Date: 8/02/2010 17:35
 *
 * Change log:
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
using System.ComponentModel;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
    public class StartAnimationEventArgs : EventArgs
    {
    }

    public class TickEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets if the tick event was completely handled
        /// </summary>
        public bool Handled;
    }

    public class RedrawEventArgs : EventArgs
    {
        public RedrawEventArgs() {
            this.Damage = new Rectangle(-100000, -100000, 200000, 200000);
        }

        public RedrawEventArgs(Rectangle r) {
            this.Damage = r;
        }

        /// <summary>
        /// Gets the area of the animation that was damaged
        /// </summary>
        public Rectangle Damage;
    }

    public class StopAnimationEventArgs : EventArgs
    {
    }
}
