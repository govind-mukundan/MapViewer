using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapViewer
{
    // A cross reference table entry is of the form:
    // SymbolName ....      ModuleName (where the symbol was defined in)
    //                      User Module 1
    //                      User Module 2 ..
    class CrefEntry
    {
        string SymbolName;
        string SouceModule;
        List<string> Users;
    }

    class Cref
    {
        string CrefHeader = "Cross Reference Table";
        List<CrefEntry> CrefTable;
        enum Parser
        {
            FindSym,
            FindUsers
        }

        public bool Build(string mapFile)
        {
            List<string> Map = File.ReadAllLines(mapFile).ToList();
            int Cref_index = Map.FindIndex(x => String.Equals(CrefHeader, x));
            int Map_end = Map.Count;
            if ((Cref_index == -1) || (Map_end == -1))
            {
                MessageBox.Show("Couldn't find Cross Reference Table in map file! Can't proceed!", "Oops!", MessageBoxButtons.OK); return false;
            }
            Debug.WriteLine("Found Cref at index :" + Cref_index.ToString());

            Parser parser = Parser.FindSym;
            foreach (string line in Map.GetRange(Cref_index, Map_end - Cref_index))
            {
                switch (parser)
                {
                    // You get both the symbol namd and the source module
                    case Parser.FindSym:
                        if (!Char.IsLetterOrDigit(line[0])) continue; // Ignore invalid chars

                        break;

                    case Parser.FindUsers:

                        break;
                }

            }

            return false;
        }
    }
}
