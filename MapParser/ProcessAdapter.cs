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

namespace MapViewer
{
    static class ProcessAdapter
    {

        /// <summary>
        /// Executes a process with given args. The output of the process is returned to the caller, which is either stdout or stderr depending on whether it was successful or not.
        /// </summary>
        /// <param name="output"></param>
        /// <param name="PID"></param>
        /// <param name="Args"></param>
        /// <returns></returns>
        static public bool Execute(ref string output, string PID, string Args)
        {

            StringBuilder stdout, stderr;

            Log.Instance.Print("Running: " + PID + " " + Args, Log.C_LOG_APPLICATION);

            stdout = new StringBuilder();
            stderr = new StringBuilder();
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(PID);
            startInfo.Arguments = Args;
            startInfo.UseShellExecute = false;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true; // hides the console window

            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            startInfo.StandardErrorEncoding = Encoding.UTF8;
            // handlers to receive output
            process.OutputDataReceived += (sender2, args) =>
            {
               // Debug.WriteLine("{0}", args.Data);
                stdout.Append(args.Data + " \n"); // It's important to add the space here for later matching of symbols
            };

            process.ErrorDataReceived += (sender2, args) =>
            {
                Debug.WriteLine("stderr: {0}", args.Data);
                stderr.Append(args.Data);
            };

            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            // string error = process.StandardError.ReadToEnd(); // Hangs here if there's no data
            //string op = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (stderr.Length == 0)
            {
                output = stdout.ToString();
                Log.Instance.Print(output, Log.C_LOG_FT900);
                return true;
            }
            else
            {
                output = stderr.ToString();
                Log.Instance.Print(output, Log.C_LOG_ERROR);
                return false;
            }
        }

    }
}
