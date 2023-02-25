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
    public static class helper
    {
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
       
    /// <summary>
    /// The DWARF debug_info section contains a lot of information about each of the "compilation units" = .o files
    /// We try to parse that to find the compilation units corresponding to static symbols
    /// </summary>
    class DwarfParser
    {

        List<string> DwarfInfo;
        List<string[]> CompilationUnits;
        List<string> CompilationUnitsName;
        List<string[]> CompilationUnitsByName;
        List<string[]> CompilationUnitsBySubprogAddress;
        List<string[]> CompilationUnitsBySymbolAddress;
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
        public void Run(string readElfPath, string elfPath)
        {
            string result = "";
            if (elfPath == "" || readElfPath == "") return;
            ProcessAdapter.Execute(ref result, readElfPath, "--debug-dump=info " + Quote(elfPath));
            string[] lines = result.Split(new[] { '\r', '\n' });
            DwarfInfo = lines.ToList();
            CompilationUnits = new List<string[]>();
            CompilationUnitsName = new List<string>();
            CompilationUnitsByName = new List<string[]>();
            CompilationUnitsBySubprogAddress = new List<string[]>();
            CompilationUnitsBySymbolAddress = new List<string[]>();

            // Select the index of all lines that contain the compilation unit tag
            var ci = DwarfInfo.Select((s, i) => new { s, i }).Where(x => x.s.Contains(COMPILE_UNIT)).Select(x => x.i).ToList();
            ci.Add(DwarfInfo.Count - 1); // last index
            for (int i = 0; i < ci.Count - 1; i++)
            {
                CompilationUnits.Add(DwarfInfo.GetRange(ci[i], ci[i + 1] - ci[i]).ToArray());
            }
            foreach(string[] itemList in CompilationUnits)
            {
                List<string> ByName = new List<string>();
                List<string> BySymbolAddress = new List<string>();
                List<string> BySubprogAddress = new List<string>();
                CompilationUnitsName.Add(GetCUName(itemList));
                foreach (string item in itemList)
                {
                    if (item.Contains(ATTRIB_NAME))
                    {
                        string[] parts = item.Split(':');
                        ByName.Add(helper.RemoveWhitespace(parts[parts.Length - 1]));
                    }
                    if (item.Contains(AT_SUBPROGRAM_ADDRESS))
                    {
                        BySubprogAddress.Add(item);
                    }
                    if (item.Contains(ATTRIB_LOCATION) && item.Contains(LOC_ADDRESS))
                    {
                        BySymbolAddress.Add(item);
                    }
                }
                CompilationUnitsByName.Add(ByName.ToArray());
                CompilationUnitsBySubprogAddress.Add(BySubprogAddress.ToArray());
                CompilationUnitsBySymbolAddress.Add(BySymbolAddress.ToArray());
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

            //if (name.Contains("StaticFunc"))
            //    Debug.WriteLine("aaa");

            string[] mName;
            mName = name.Split('.'); // .postfix is added by compiler/linker.. we ignore that
            name = mName[0].Trim('_');

            if(name.Contains("::"))
            {
                // CPP
                string[] parts = name.Split(':');
                name = parts[parts.Length - 1];
                parts = name.Split('(');
                name = parts[0];
            }

            int filteredCUNameindex = CompilationUnitsByName.FindIndex(cu =>
            {
                // Name is case sensitive, address is not
                // XC16 compiler mangles all names with a leading underscore, so we trim that
                if (cu.Where(s => String.Equals(s, name)).Count() > 0)
                        return true;
                else return false;
            });

            int filteredCUSubindex = CompilationUnitsBySubprogAddress.FindIndex(cu =>
            {
                // Name is case sensitive, address is not
                if (cu.Where(s => s.ToLower().Contains(ads)).Count() > 0)
                    return true;
                else return false;
            });

            if(filteredCUNameindex != -1)
            {
                return CompilationUnitsName[filteredCUNameindex];
            }
            else if (filteredCUSubindex != -1)
            {
                return CompilationUnitsName[filteredCUSubindex];
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

            int filteredCUNameindex = CompilationUnitsByName.FindIndex(cu =>
            {
                // Name is case sensitive, address is not
                // XC16 compiler mangles all names with a leading underscore, so we trim that
                if (cu.Where(s => s.Contains(mName[0].Trim('_'))).Count() > 0)
                    return true;
                else return false;
            });
            int filteredCUSymbolindex = CompilationUnitsBySymbolAddress.FindIndex(cu =>
            {
                // Name is case sensitive, address is not
                // XC16 compiler mangles all names with a leading underscore, so we trim that
                if (cu.Where(s => s.ToLower().Contains(ads)).Count() > 0)
                    return true;
                else return false;
            });

            if (filteredCUNameindex != -1)
            {
                return CompilationUnitsName[filteredCUNameindex];
            }
            else if (filteredCUSymbolindex != -1)
            {
                return CompilationUnitsName[filteredCUSymbolindex];
            }
            else return String.Empty;

            //filteredCU = CompilationUnits.FirstOrDefault(cu =>
            //{
            //    if (cu.Where(s => s.Contains(ATTRIB_NAME) && s.Contains(mName[0].Trim('_'))).Count() > 0 &&
            //        cu.Where(s => s.Contains(ATTRIB_LOCATION) && s.Contains(LOC_ADDRESS) && s.ToLower().Contains(ads)).Count() > 0)
            //        return true;
            //    else return false;
            //});

            //if (filteredCU != null && filteredCU.Count() > 0)
            //{
            //    return (GetCUName(filteredCU));
            //}
            //else return String.Empty;
        }

        public string GetCUName(string[] cu)
        {
            string name = "";
            //Debug.WriteLine(cu[COMPILATION_UNIT_NAME_INDEX]);
            // name string looks like this: 
            // <192b2>   DW_AT_name        : (indirect string, offset: 0x372f): ../src/main.c
            // <15>   DW_AT_comp_dir    : (indirect string, offset: 0xb4c): /d/Debug
            // Paths are relative to the folder where the .elf file is generated
            string[] ele = cu[COMPILATION_UNIT_NAME_INDEX].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string fullPath = ele[ele.Length - 1];
            string[] e2 = cu[COMPILATION_UNIT_NAME_INDEX + 1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string compilePath = e2[e2.Length - 1];

            if (!Path.IsPathRooted(fullPath)) // Don't do anything if we already have the full path
                name = Path.GetFullPath(compilePath + fullPath); // GetFullPath(baseDir, relativePath);
            else
                name = fullPath;

            return name;
        }

    }
}
