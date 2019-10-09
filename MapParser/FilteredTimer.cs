#region copyright
/*
Copyright 2015 Govind Mukundan

This file is part of MapViewer.

MapViewer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MapViewer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MapViewer.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MapViewer
{
    /// <summary>
    /// Class to filter events so that you only get an indication if there has not been any new event in the last X mS
    /// Also works as a auto-retriggerable timer that will never execute a callback after it has been stopped
    /// </summary>
    class FilteredTimer
    {
        System.Timers.Timer _timer = new System.Timers.Timer();
        ElapsedEventHandler _cb;
        double _period;
        Stopwatch _sysTime;
        long _startTime;
        long _interval;
        bool _autoReset;

        bool _started;
        bool DEBUG = false;

        public FilteredTimer(double period)
        {
            _timer = new System.Timers.Timer(period);
            _timer.Elapsed += ExecuteCB;
            _cb = null;
            _started = false;
            _period = period;
            _sysTime = new Stopwatch();
        }

        public void Restart(bool autoReset, ElapsedEventHandler cb)
        {
            Debug.WriteLineIf(DEBUG, "Reset! " + _sysTime.ElapsedMilliseconds.ToString());
            _sysTime.Restart();

            if (!_started)
            {
                _timer.Stop(); // restart the timer
                Debug.WriteLineIf(DEBUG, "RE-Starting!");
            }
            else
            {
                return;
            }

            _cb = cb;
            _timer.AutoReset = true;
            _timer.Interval = 200;
            _timer.Enabled = true;
            _timer.Start();
            _started = true;
            _interval = (long)_period;
            _autoReset = autoReset;

            Debug.WriteLineIf(DEBUG, "Started! " + _sysTime.ElapsedMilliseconds.ToString());
        }

        private void ExecuteCB(object s, ElapsedEventArgs e1)
        {
            if (!_started) return;

            Debug.WriteLineIf(DEBUG, "CB! " + _sysTime.ElapsedMilliseconds.ToString());
            if (_sysTime.ElapsedMilliseconds >= _period)
            {
                _cb(s, e1);
                if(!_autoReset)
                    Stop();
            }
            else
            {
                // continue
            }

            
        }

        public void Stop()
        {
            _started = false;
            _timer.Enabled = false;
            _timer.Stop();
            _sysTime.Stop();
            _cb = null;

            Debug.WriteLineIf(DEBUG, "Stopped" + _sysTime.ElapsedMilliseconds.ToString());
        }

    }
}
