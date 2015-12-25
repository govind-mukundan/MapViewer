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
using System.IO;
using System.Linq;
using System.Text;

namespace MapViewer
{

    /// <summary>
    /// The DWARF debug_info section contains a lot of information about each of the "compilation units" = .o files
    /// We try to parse that to find the compilation units corresponding to static symbols
    /// </summary>
    class DwarfParser
    {

        List<string> DwarfInfo;
        List<string[]> CompilationUnits;
        string COMPILE_UNIT = "Compilation Unit";
        string ATTRIB_NAME = "DW_AT_name";
        string ATTRIB_LOCATION = "DW_AT_location";
        string LOC_ADDRESS = "DW_OP_addr:";
        string AT_SUBPROGRAM_ADDRESS = "DW_AT_low_pc";
        int COMPILATION_UNIT_NAME_INDEX = 8;

        static readonly DwarfParser _instance = new DwarfParser();

        public static DwarfParser Instance
        {
            get { return _instance; }
        }

        string Quote(string ip)
        {
            return "\"" + ip + "\"";
        }

        /// <summary>
        /// A function to extract the debug_info and store it for future reference
        /// </summary>
        /// <param name="nmPath"></param>
        /// <param name="elfPath"></param>
        public void Run(string objDumpPath, string elfPath)
        {
            string result = "";
            if (elfPath == "" || objDumpPath == "") return;
            ProcessAdapter.Execute(ref result, objDumpPath, "--dwarf=info " + Quote(elfPath));
            string[] lines = result.Split(new[] { '\r', '\n' });
            DwarfInfo = lines.ToList();
            CompilationUnits = new List<string[]>();
            // Select the index of all lines that contain the compilation unit tag
            var ci = DwarfInfo.Select((s, i) => new { s, i }).Where(x => x.s.Contains(COMPILE_UNIT)).Select(x => x.i).ToList();
            for (int i = 0; i < ci.Count - 1; i++)
            {
                CompilationUnits.Add(DwarfInfo.GetRange(ci[i], ci[i + 1] - ci[i]).ToArray());
            }

            // test
            //FindSymbolCUnit("gRecipeFwd", Convert.ToUInt32("1074", 16));
            //FindSubRoutineCUnit("vSPIM_Task", Convert.ToUInt32("4ecc", 16));
            //FindSubRoutineCUnit("gRecipeFwd", Convert.ToUInt32("1075", 16));
        }

        /// <summary>
        /// The address of the subroutine is stored in the attribute DW_AT_low_pc
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public string FindSubRoutineCUnit(string name, UInt32 address)
        {
            string ads = address.ToString("X").ToLower();

            string[] mName = name.Split('.'); // .postfix is added by compiler/linker.. we ignore that

            var filteredCU = CompilationUnits.FirstOrDefault(cu =>
            {
                // Name is case sensitive, address is not
                if (cu.Where(s => s.Contains(ATTRIB_NAME) && s.Contains(mName[0])).Count() > 0 &&
                    cu.Where(s => s.Contains(AT_SUBPROGRAM_ADDRESS) && s.ToLower().Contains(ads)).Count() > 0)
                    return true;
                else return false;
            });

            if (filteredCU != null && filteredCU.Count() > 0)
            {
                return (GetCUName(filteredCU));
            }
            else return String.Empty;

        }
        public string FindSymbolCUnit(string name, UInt32 address)
        {
            // 1. Find the CU with matching name attribute
            // 2. Check if the address location also matches, repeat if no match
            string ads = address.ToString("X").ToLower();
            string[] filteredCU = null;

//            if (name.Contains("FUNCTION"))
//                Debug.Write("test");
            string[] mName = name.Split('.'); // .postfix is added by compiler/linker.. we ignore that

            filteredCU = CompilationUnits.FirstOrDefault(cu =>
            {
                if (cu.Where(s => s.Contains(ATTRIB_NAME) && s.Contains(mName[0])).Count() > 0 &&
                    cu.Where(s => s.Contains(ATTRIB_LOCATION) && s.Contains(LOC_ADDRESS) && s.ToLower().Contains(ads)).Count() > 0)
                    return true;
                else return false;
            });

            if (filteredCU != null && filteredCU.Count() > 0)
            {
                return (GetCUName(filteredCU));
            }
            else return String.Empty;
        }

        public string GetCUName(string[] cu)
        {
            // name string looks like this: 
            // <192b2>   DW_AT_name        : (indirect string, offset: 0x372f): ../src/main.c
            // <15>   DW_AT_comp_dir    : (indirect string, offset: 0xb4c): /d/Debug
            // Paths are relative to the folder where the .elf file is generated
            string name = "";


            //Debug.WriteLine(cu[COMPILATION_UNIT_NAME_INDEX]);
            name = cu[COMPILATION_UNIT_NAME_INDEX];
            string[] ele = name.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string[] e2 = cu[9].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            name = Path.GetFullPath(e2[e2.Length - 1] + ele[ele.Length - 1]); // GetFullPath(baseDir, relativePath);

            return name;
        }

    }
}
