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
using System.Linq;
using System.Text;

namespace MapViewer
{
    class Log
    {
        /// <summary>
        /// Delegate that the main form can attach to
        /// </summary>
        public Action<string, byte> Print;
        public const byte C_LOG_FT900 = 1;
        public const byte C_LOG_ERROR = 2;
        public const byte C_LOG_APPLICATION = 3;

        static readonly Log _instance = new Log();

        public static Log Instance
        {
            get { return _instance; }
        }

        public Log()
        {
            Print = DoNothing;
        }

        void DoNothing(string s, byte type) { }

    }
}
