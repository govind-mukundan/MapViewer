﻿#region copyright
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
using System.Text.RegularExpressions;


// test: weak syms, static syms with same name, bss, data, sym = var, subprogram
namespace MapViewer
{

    // Parse the symbol files generated by NM
    // NM indicates static symbols with lower case and global symbols with upper case
    // If you use the --line-numbers option along with -g3 (debugging info), NM can give you the line numbers of most functions both static and global from the debugging info
    // The info for variables seems not available, so we use DWARF info to locate the file corresponding to each symbol
    // For more info on options: https://sourceware.org/binutils/docs/binutils/nm.html
    class SymParser
    {
        bool DEBUG = true;
        public List<Symbol> Symbols;
        public List<Symbol> StaticSymbols
        { get { return Symbols.Where(x => x.GlobalScope == Symbol.TYPE_STATIC).ToList(); } }
        public List<Symbol> GlobalSymbols
        { get { return Symbols.Where(x => x.GlobalScope == Symbol.TYPE_GLOBAL).ToList(); } }

        public UInt32 RAM_ADDRESS_MASK = 0x800000;
        public string SEC_NAME_TEXT = ".text";
        public string SEC_NAME_DATA = ".data";
        public string SEC_NAME_BSS = ".bss";
        // Symbols gathered from .o files - we use this to find the modules of static symbols
        public List<Symbol> UnresolvedSymols;

        public bool UseDWARF = true;

        static readonly SymParser _instance = new SymParser();

        public static SymParser Instance
        {
            get { return _instance; }
        }
        
        public void Run(string nmPath, string elfPath, Action<bool> prog_ind)
        {
            string result = "";
            Symbols = new List<Symbol>();
            if (nmPath == "" || elfPath == "" || !File.Exists(nmPath) || !File.Exists(elfPath)) return;
            ProcessAdapter.Execute(ref result, nmPath, "--demangle --print-size --size-sort " + Quote(elfPath)); //--line-numbers

            // Parse the resultant table
            // 00800744 00000004 b LoaderIPMode  --> b means static BSS symbol, B beans global BSS symbol, same for d/D data
            // 00006364 00000018 T vTaskSuspendAll	/cygdrive/d/Freelance/Study/FTDI/VFWLoaderDemo/lib/FreeRTOS/Source/tasks.c:1632
            string[] symTable = result.Split(new[] { '\r', '\n' });
            bool flip = false;
            foreach (string line in symTable)
            {
                prog_ind?.Invoke(false);

                // Split using spaces - note that the module path may itself contain spaces
                string[] entries = line.Split(new char[0], 4, StringSplitOptions.RemoveEmptyEntries);
                if (entries.Length < 4) continue;
                string path = "";
                int type = Symbol.TYPE_STATIC;
                string secName = "unknown";
                // Extract the module path, we also take into account paths with spaces in between

                entries[3] = entries[3].TrimEnd(' '); // trim spaces from the end of function name

                 if (UseDWARF)
                {
                    // Extract module path from DARF info
                    if (entries[2].ToLower() == "t")
                    {
                        // Subroutines
                        path = DwarfParser.Instance.FindSubRoutineCUnit(entries[3], Convert.ToUInt32(entries[0], 16));
                    }
                    else if (entries[2].ToLower() == "w" || entries[2].ToLower() == "v") // Weak symbols could be either vars or subroutines
                    {
                        Debug.WriteLineIf(DEBUG,"Found a weak symbol " + entries[3]);
                        path = DwarfParser.Instance.FindSubRoutineCUnit(entries[3], Convert.ToUInt32(entries[0], 16));
                        if (path != String.Empty)
                        {
                            Debug.WriteLineIf(DEBUG,"Is a subroutine " + path);
                        }
                        else
                        {
                            path = DwarfParser.Instance.FindSymbolCUnit(entries[3], Convert.ToUInt32(entries[0], 16) & (~RAM_ADDRESS_MASK));
                            if (path != String.Empty)
                            {
                                Debug.WriteLineIf(DEBUG,"Is a variable " + path);
                                // FIXME: for now just assume all weak variable syms are in BSS
                                entries[2] = "b";
                            }
                            else
                            {
                                // Couldn't find this symbol, lets apply some hurestics to at least identify if its a subroutine or not
                                // Check if the symbol contains () ==> C++ subroutine
                                if (entries[3].Contains("(")) entries[2] = "t";
                            }
                        }
                    }
                    else
                    {
                        // variables
                        path = DwarfParser.Instance.FindSymbolCUnit(entries[3], Convert.ToUInt32(entries[0], 16) & (~RAM_ADDRESS_MASK));
                    }
                }
                else
                {
                    if (entries.Length > 4)
                    {
                        int end = line.IndexOf(':');
                        int j = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            // Find the index of 4th space
                            if (line[i] == ' ' || line[i] == '\t')
                            {
                                j++;
                                if (j == 4)
                                {
                                    j = i + 1; break;
                                }
                            }
                        }
                        path = line.Substring(j, end - j);
                    }
                }

                if (Regex.IsMatch(entries[2], @"[TDBGSR]")) type = Symbol.TYPE_GLOBAL;
                // Get the section
                if (entries[2].ToLower() == "t") secName = SEC_NAME_TEXT;
                else if (entries[2].ToLower() == "d" || entries[2].ToLower() == "g" || entries[2].ToLower() == "r") secName = SEC_NAME_DATA;
                else if (entries[2].ToLower() == "b" || entries[2].ToLower() == "s" || entries[2].ToLower() == "c") secName = SEC_NAME_BSS;
                else
                {
                    Debug.WriteLineIf(DEBUG, "Unknown section");
                }
                if (path != String.Empty) {
                    Symbol sym = new Symbol(entries[3], path, Convert.ToUInt32(entries[0], 16), Convert.ToUInt32(entries[1], 16), secName); sym.GlobalScope = type;
                    Symbols.Add(sym);
                }
            }

            prog_ind?.Invoke(true);
        }


        public void FilterDuplicates()
        {
            // Sometimes there are duplicate symbols with different names but same Load Address and Size. Mark such syms as .duplicate
            foreach(Symbol sym in Symbols)
            {
                if(Symbols.Where(x => x.LoadAddress == sym.LoadAddress && x.Size == sym.Size && !sym.SymbolName.Contains(".duplicate")).Count() > 1)
                {
                    sym.SymbolName += ".duplicate";
                    sym.Size = 0;
                }
            }

            // Sort all symbols according to size
            //Symbols.OrderBy(x => x.SectionName).ThenBy(y => y.Size);

        }

        string Quote(string ip)
        {
            return "\"" + ip + "\"";
        }

        /// <summary>
        /// Input the search path for .o files. We extract all the resolved symbols inside each .o file and store them for future reference
        /// </summary>
        /// <param name="nmPath"></param>
        /// <param name="objSearchPaths"></param>
        public void GatherStaticSyms(string nmPath, string[] objSearchPaths)
        {
            string result = "";
            UnresolvedSymols = new List<Symbol>();

            // 1. Extract all the object files from the various directories
            // Note select() doesnt execute the where() filter, but selectmany works!
            var objs = objSearchPaths.SelectMany(x => Directory.GetFiles(x).Where(y =>
            {
                if ((Path.GetExtension(y).ToLower() == ".o" || Path.GetExtension(y).ToLower() == ".obj"))
                    return true;
                else
                    return false;
            }));
            // 2. Run NM on them and get their symbols
            foreach (var obj in objs)
            {
                ProcessAdapter.Execute(ref result, nmPath, "--print-size --size-sort " + Quote(obj.ToString()));
                string[] symTable = result.Split(new[] { '\r', '\n' });
                string secName = "unknown";

                foreach (string line in symTable)
                {
                    string[] entries = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    if (entries.Length < 3) continue;

                    // We are only interested in local symbols -> lower case stuff except for C
                    // https://sourceware.org/binutils/docs/binutils/nm.html
                    if (entries[2] == "t") secName = SEC_NAME_TEXT;
                    else if (Regex.IsMatch(entries[2], @"[dg]")) secName = SEC_NAME_DATA;
                    else if (Regex.IsMatch(entries[2], @"[bCs]")) secName = SEC_NAME_BSS;
                    else continue;

                    Symbol sym = new Symbol(entries[3], obj.ToString(), Convert.ToUInt32(entries[0], 16), Convert.ToUInt32(entries[1], 16), secName); sym.GlobalScope = Symbol.TYPE_STATIC;
                    UnresolvedSymols.Add(sym);
                }
            }

        }

    }

}
