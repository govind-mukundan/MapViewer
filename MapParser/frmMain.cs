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

using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MapViewer
{
    public partial class MapViewer : Form
    {

        string BINUTIL_NM; //= Environment.GetEnvironmentVariable("FT90X_TOOLCHAIN") + "/tools/bin/" + "ft32-elf-nm.exe";
        string BINUTIL_READ_ELF; //= Environment.GetEnvironmentVariable("FT90X_TOOLCHAIN") + "/tools/bin/" + "ft32-elf-objdump.exe";

        /*
        
        string BINUTIL_NM = @"C:\Program Files (x86)\Microchip\xc32\v1.34\bin\" + "xc32-nm.exe";
        string BINUTIL_OBJ_DUMP = @"C:\Program Files (x86)\Microchip\xc32\v1.34\bin\" + "xc32-objdump.exe";
        */
        SymParser _syms;
        Settings _settings;
        bool _UIUpdateInProgress; // disable filtering while UI is being updated

        System.Timers.Timer _timer = new System.Timers.Timer(1000);

        public MapViewer()
        {
            InitializeComponent();
            _UIUpdateInProgress = false;
            //Log.Instance.Print += PrintLog;
            _settings = new Settings();
            _settings.LoadAppSettings();
            RefreshSettings();
            //string val = @"/cygdrive/c/Program Files (x86)/FTDI/FT90x Toolchain/Toolchain/tools/bin/../lib/gcc/ft32-elf/5.0.0/../../../../ft32-elf/lib/libc.a(lib_a-locale.o)";
            //val = val.Replace("lib_a-", String.Empty);
            //Debug.Write(val);
        }

        void RefreshSettings()
        {
            txtBx_MapFilepath.Text = _settings.MapPath;//Properties.Settings.Default.MapPath.ToString();
            txtBx_ElfFilepath.Text = _settings.ElfPath;//Properties.Settings.Default.ElfPath.ToString();
            BINUTIL_NM = _settings.NMPath;
            BINUTIL_READ_ELF = _settings.ReadElfPath;
            MAPParser.Instance.C_BSS_ID = _settings.BSSSeg2SecMap.ToArray();
            MAPParser.Instance.C_DATA_ID = _settings.DataSeg2SecMap.ToArray();
            MAPParser.Instance.C_TEXT_ID = _settings.TextSeg2SecMap.ToArray();
        }

        private void btn_Analyze_Click(object sender, EventArgs e)
        {
            if (txtBx_MapFilepath.Text == String.Empty || !File.Exists(txtBx_MapFilepath.Text))
            {
                MessageBox.Show("Can't proceed! Please enter a valid MAP file path!");
                return;
            }

            if (MAPParser.Instance.C_BSS_ID.Count() == 0 || MAPParser.Instance.C_TEXT_ID.Count() == 0 || MAPParser.Instance.C_DATA_ID.Count() == 0)
            {
                MessageBox.Show("Can't proceed! Please enter valid Segment to Section mappings for TEXT, DATA and BSS!\nClick Settings button!");
                return;
            }

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    AnalyzeSymbols();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error analyzing Map file!\n" + ex.ToString());
                }
            });
        }

        void PopulateModuleLV(List<Module> m)
        {
            // Make the decoration
            RowBorderDecoration rbd = new RowBorderDecoration();
            rbd.BorderPen = new Pen(Color.FromArgb(128, Color.LightSeaGreen), 2);
            rbd.BoundsPadding = new Size(1, 1);
            rbd.CornerRounding = 4.0f;

            // Put the decoration onto the hot item
            olv_ModuleView.HotItemStyle = new HotItemStyle();
            olv_ModuleView.HotItemStyle.Decoration = rbd;
            olv_ModuleView.UseHotItem = true;

            olv_ModuleView.SetObjects(m);
            olv_ModuleView.Sort("TEXT");

            this.BeginInvoke(new MethodInvoker(() =>
            {
                lbl_TextSizeActual.Text = "Text: " + MAPParser.Instance.TextSegSize.ToString() + " (" + (MAPParser.Instance.TextSegSize / 1024f).ToString("##.##") + " KB)";
                lbl_BssSizeActual.Text = "Bss: " + MAPParser.Instance.BssSize.ToString() + " (" + (MAPParser.Instance.BssSize / 1024f).ToString("##.##") + " KB)";
                lbl_DataSizeActual.Text = "Data: " + MAPParser.Instance.DataSize.ToString() + " (" + (MAPParser.Instance.DataSize / 1024f).ToString("##.##") + " KB)";
            }));

        }

        void PopulateSymbolLV(List<Symbol> s)
        {
            // Make the decoration
            RowBorderDecoration rbd = new RowBorderDecoration();
            rbd.BorderPen = new Pen(Color.FromArgb(128, Color.LightSeaGreen), 2);
            rbd.BoundsPadding = new Size(1, 1);
            rbd.CornerRounding = 4.0f;

            // Put the decoration onto the hot item
            olv_SymbolView.HotItemStyle = new HotItemStyle();
            olv_SymbolView.HotItemStyle.Decoration = rbd;
            olv_SymbolView.UseHotItem = true;

            // Special aspect getter for address coz we want to see that in hex
            this.symAddrColumn.AspectGetter = (x) => { return ((Symbol)x).LoadAddress.ToString("X6"); };// (x) => { return x.ToString("X4"); };
            colSection.AspectGetter = x => { return ((Symbol)x).SectionName[1].ToString().ToUpper(); };
            columnGlobal.AspectGetter = x => { 
                int type = ((Symbol)x).GlobalScope;
                if (type == Symbol.TYPE_GLOBAL) return "G";
                else if (type == Symbol.TYPE_STATIC) return "S";
                else if (type == Symbol.TYPE_HIDDEN) return "H";
                else return "X";
            };

            olv_SymbolView.SetObjects(s);

            olv_SymbolView.SecondarySortColumn = olv_SymbolView.AllColumns[3];
            olv_SymbolView.PrimarySortColumn = olv_SymbolView.AllColumns[0];
            olv_SymbolView.PrimarySortOrder = SortOrder.Descending;
            olv_SymbolView.SecondarySortOrder = SortOrder.Descending;
            olv_SymbolView.Sort("SEC");
            AddSumRow(s); // Update the sum over currently displayed symbols
        }

        private void textBoxFilterSimple_TextChanged(object sender, EventArgs e)
        {
            TextMatchFilter filter = null;

            if (rb_RegexFilter.Checked)
                filter = TextMatchFilter.Regex(olv_ModuleView, textBoxFilterSimple.Text);
            else
                filter = TextMatchFilter.Contains(olv_ModuleView, textBoxFilterSimple.Text);

            olv_ModuleView.AdditionalFilter = filter;

            AddSumRow(olv_ModuleView.FilteredObjects);
            foreach (var x in olv_ModuleView.FilteredObjects)
            {
                Debug.WriteLine("Selected:" + ((Module)x).ModuleName);
            }

            // Use a timer to have a single update for more than "text changed" event, better UX

            _timer.Stop(); // Stop an existing timer
            _timer.AutoReset = false; // one shot
            _timer.Enabled = true;
            _timer.Elapsed += (object s, ElapsedEventArgs e1) =>
            {
                if (!_UIUpdateInProgress)
                {
                    PopulateSymbolLV(FilterSymbols(olv_ModuleView.FilteredObjects.Cast<Module>().ToList()));
                    olv_ModuleView.Invalidate();
                }
            };
            _timer.Start();


        }

        void AddSumRow(System.Collections.IEnumerable k)
        {
            List<Module> m = k.Cast<Module>().ToList();

            List<Module> s = new List<Module>();
            Module sum = new Module("Summary", (UInt32)m.Sum(x => x.TextSize), (UInt32)m.Sum(x => x.BSSSize), (UInt32)m.Sum(x => x.DataSize));
            s.Add(sum);

            olv_ModuleSum.SetObjects(s);
        }

        void AddSumRow(List<Symbol> syms)
        {
            // Sum text syms
            long tSum = syms.Where(s => s.SectionName == SymParser.Instance.SEC_NAME_TEXT).Sum(x => x.Size);
            long dSum = syms.Where(s => s.SectionName == SymParser.Instance.SEC_NAME_DATA).Sum(x => x.Size);
            long bSum = syms.Where(s => s.SectionName == SymParser.Instance.SEC_NAME_BSS).Sum(x => x.Size);

            List<Module> l = new List<Module>();
            Module sum = new Module("Summary", (UInt32)tSum, (UInt32)bSum, (UInt32)dSum);
            l.Add(sum);

            olv_SymbolSum.SetObjects(l);
        }

        public void DoOnQuit()
        {
            //Properties.Settings.Default.MapPath = txtBx_MapFilepath.Text;
            //Properties.Settings.Default.ElfPath = txtBx_ElfFilepath.Text;
            //Properties.Settings.Default.Save(); // persist all modified settings

            _settings.MapPath = txtBx_MapFilepath.Text;
            _settings.ElfPath = txtBx_ElfFilepath.Text;
            //_settings.ObjFilesSearchPath.Add(@"D:\Freelance\Study\C#\MapViewer");
            _settings.SaveAppSettings(); // persist all modified settings
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoOnQuit();
        }

        private void btn_BrowseMapFile_Click(object sender, EventArgs e)
        {
            OFD.Filter = "MAP files|*.map";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                txtBx_MapFilepath.Text = OFD.FileName;
                _settings.MapPath = OFD.FileName;
            }
        }

        private void olv_SymbolView_SelectionChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("selected changed");
        }



        // string - /cygdrive/c/Program Files (x86)/FTDI/FT90x Toolchain/Toolchain/tools/bin/../lib/gcc/ft32-elf/5.0.0/../../../../ft32-elf/lib/libc.a(lib_a-vfiprintf.o)
        // regex - [^\/(]+(?=.o\)$)  ==> match result = lib_a-vfiprintf
        // [^\/(]+(?=.o\)?$) => extracts lib_a-vfiprintf from both (lib_a-vfiprintf.o) and lib_a-vfiprintf.o
        private void olv_ModuleView_SelectionChanged(object sender, EventArgs e)
        {
            if (_syms == null || _UIUpdateInProgress) return;
            PopulateSymbolLV(FilterSymbols(olv_ModuleView.SelectedObjects.Cast<Module>().ToList()));
            return;
        }

        List<Symbol> FilterSymbols(List<Module> mods)
        {
            if (_syms == null) return null;
            bool filtGlobal = !chkBx_ShowStatic.Checked;

            // Go through all the symbols and select those that match the currently selected modules
            List<Symbol> syms = mods.SelectMany(m => _syms.Symbols.Where(s =>
                {
                    string mod = m.ModuleName;
                    // Special for newlib, module name turns out to be of the form ..lib/libc.a(lib_a-rget.o), while dwarf file name = rget.c, so we takeout all "lib_a" prefix
                    if (m.ModuleName.Contains("lib_a-"))
                        mod = mod.Replace("lib_a-", String.Empty);

                    Match m2 = Regex.Match(mod, @"[^\/\\(]+(?=.o\)?$)");
                    Match m1 = Regex.Match(s.ModuleName, @"[^\/\\(]+(?=.c\)?$)");

                    if (m1.ToString() == m2.ToString() && ((filtGlobal == false) || (Symbol.TYPE_GLOBAL == s.GlobalScope)))
                    {
                        //Debug.WriteLine("Match: " + m1.ToString());
                        return true;
                    }
                    else return false;
                })).ToList();

            return syms;
        }

        private void chkBx_ShowStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (_syms == null) return;
            if (olv_ModuleView.SelectedObjects.Count == 0)
            {
                if (!chkBx_ShowStatic.Checked)
                    PopulateSymbolLV(_syms.GlobalSymbols);
                else
                    PopulateSymbolLV(_syms.Symbols);
            }
            else
            {
                olv_ModuleView_SelectionChanged(null, null);
            }

        }


        void AnalyzeSymbols()
        {
            _UIUpdateInProgress = true;
            // Parse the MAP file alone
            if (!MAPParser.Instance.Run(txtBx_MapFilepath.Text)) return;
            // Update the ListView for Modules
            AddSumRow(MAPParser.Instance.ModuleMap);
            PopulateModuleLV(MAPParser.Instance.ModuleMap);

            // Analyze Symbols
            if (txtBx_ElfFilepath.Text == "" || !File.Exists(txtBx_MapFilepath.Text))
            {
                MessageBox.Show("Please enter a valid ELF file path for symbol analysis!");
                hide_sym_column();
                return;
            }
            if (BINUTIL_READ_ELF == "" || !File.Exists(BINUTIL_READ_ELF) ||
                BINUTIL_NM == "" || !File.Exists(BINUTIL_NM))
            {
                MessageBox.Show("Please enter a valid ObjectDump and NM path for symbol analysis!\n Click the Settings button!");
                return;
            }
            // Parse the dwarf information to get all the compilation units
            DwarfParser.Instance.Run(BINUTIL_READ_ELF, txtBx_ElfFilepath.Text);
            // Extract the symbols using NM
            _syms = SymParser.Instance;
            _syms.Run(BINUTIL_NM, txtBx_ElfFilepath.Text);

            // Update symbols present in MAP but missing in NM output
            // FIXME: The size of these "hidden" symbols may not be accurate as of now..
            foreach (Symbol s in MAPParser.Instance.MapSymbols)
            {
                if (!_syms.Symbols.Exists(x => x.SymbolName == s.SymbolName))
                {
                    s.GlobalScope = Symbol.TYPE_HIDDEN;
                    _syms.Symbols.Add(s);
                    Debug.WriteLine(s.SymbolName + " : " + s.Size.ToString());
                }
            }

            // Filter out any duplicate symbols - same Load address + size but same/different names
            _syms.FilterDuplicates();
            // Finally update the symbol view
            if (!chkBx_ShowStatic.Checked)
                PopulateSymbolLV(_syms.GlobalSymbols);
            else
                PopulateSymbolLV(_syms.Symbols);

            //PrintLog("\n-------------------------------------------------\n", C_LOG_APPLICATION);
            //foreach (Section s in MAPParser.Instance.Sections)
            //{
            //    PrintLog("\n++++++++++++++++++++++++++++++++++++++++++++++\n", C_LOG_APPLICATION);
            //    PrintLog("SECTION    " + s.Name + "\t\t" + s.Size.ToString() + "\t\t" + s.Address + "\n", C_LOG_APPLICATION);
            //    foreach (Module m in s.Modules)
            //    {
            //        PrintLog("\n============================================\n", C_LOG_APPLICATION);
            //        PrintLog("MODULE    " + m.ModuleName + "\t\t" + m.Size + "\n", C_LOG_APPLICATION);
            //        foreach (Symbol sm in m.Symbols)
            //        {
            //            //  syms.Add(sm);
            //            Symbol s2 = _syms.Symbols.Where(x => x.SymbolName == sm.SymbolName).FirstOrDefault();
            //            if (s2 != null)
            //            {
            //                sm.Size = s2.Size;
            //                sm.GlobalScope = s2.GlobalScope;
            //                PrintLog(sm.SymbolName + "\t\t" + sm.LoadAddress + "\t\t" + sm.Size + "\n", C_LOG_APPLICATION);

            //            }
            //            else
            //            {
            //                PrintLog(sm.SymbolName + "\t\t" + sm.LoadAddress + "\n", C_LOG_APPLICATION);
            //            }

            //        }
            //    }

            //}
            
            _UIUpdateInProgress = false;
        }

        private void btn_BrowseElfFile_Click(object sender, EventArgs e)
        {
            OFD.Filter = "ELF files|*.elf";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                txtBx_ElfFilepath.Text = OFD.FileName;
                _settings.ElfPath = OFD.FileName;
            }
        }

        private void txtBx_SymFilter_TextChanged(object sender, EventArgs e)
        {
            TextMatchFilter filter = null;

            if (rb_RegexFilter2.Checked)
                filter = TextMatchFilter.Regex(olv_SymbolView, txtBx_SymFilter.Text);
            else
                filter = TextMatchFilter.Contains(olv_SymbolView, txtBx_SymFilter.Text);

            olv_SymbolView.AdditionalFilter = filter;

            // Use a timer to have a single update for more than "text changed" event, better UX

            _timer.Stop(); // Stop an existing timer
            _timer.AutoReset = false; // one shot
            _timer.Enabled = true;
            _timer.Elapsed += (object s, ElapsedEventArgs e1) =>
            {
                if (!_UIUpdateInProgress)
                {
                    AddSumRow(olv_SymbolView.FilteredObjects.Cast<Symbol>().ToList());
                    olv_SymbolView.Invalidate();
                }
            };
            _timer.Start();


        }

        private void btn_ResetSyms_Click(object sender, EventArgs e)
        {
            // Show all symbols
            chkBx_ShowStatic.Checked = true;
            olv_SymbolView.ClearObjects();
            PopulateSymbolLV(_syms.Symbols);
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            Form fm = new frmSettings(_settings);
            fm.Show();
            // reload settings on close of settings form
            fm.FormClosed += (object s, FormClosedEventArgs x) => RefreshSettings();
        }

        private void hide_sym_column()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            { 
           // Hide Symbol view if there's no elf file
           tlp_Main.ColumnStyles[1].Width = 0;
            }));
        }

    }
}
