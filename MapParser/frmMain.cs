//#define TEST

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
        bool DEBUG = false;
        string BINUTIL_NM;
        string BINUTIL_READ_ELF;
        SymParser _syms;
        Settings _settings;
        bool _UIUpdateInProgress; // disable filtering while UI is being updated

        System.Timers.Timer _timer = new System.Timers.Timer(1000);

        Cref cref;

        public MapViewer()
        {
            InitializeComponent();
            _UIUpdateInProgress = false;
            _settings = new Settings();
            _settings.LoadAppSettings();
            RefreshSettings();
            cref = new Cref();
        }

        #region Form UI Handlers

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoOnQuit();
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

        private void btn_BrowseElfFile_Click(object sender, EventArgs e)
        {
            OFD.Filter = "ELF files|*.elf";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                txtBx_ElfFilepath.Text = OFD.FileName;
                _settings.ElfPath = OFD.FileName;
            }
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
                Debug.WriteLineIf(DEBUG, "Selected:" + ((Module)x).ModuleName);
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

        private void btn_ResetSyms_Click(object sender, EventArgs e)
        {
            if (_syms == null) return;
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
#if TEST
                
                cref.Build(txtBx_MapFilepath.Text);
                tlv_Init();

#else
                Button_status(false);
                try
                {
                    Button_status(false);
                    AnalyzeSymbols();
                    cref.Build(txtBx_MapFilepath.Text);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error analyzing Map file!\n" + ex.ToString());
                }
                finally
                {
                    Button_status(true);
                    Button_status_text("Analyze");
                }
#endif
            });
        }

        private void olv_ModuleView_DoubleClick(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Stream file = sfd.OpenFile();
                byte[] headers = Encoding.ASCII.GetBytes("TEXT,BSS,DATA,Module\n");
                file.Write(headers, 0, headers.Length);
                foreach (var x in olv_ModuleView.FilteredObjects.Cast<Module>().ToList())
                {
                    byte[] line = Encoding.ASCII.GetBytes(x.TextSize + "," + x.BSSSize + "," + x.DataSize + "," + x.ModuleName + "\n");
                    file.Write(line, 0, line.Length);
                }
                file.Close();
                MessageBox.Show("File saved");
            }
        }

        public void Button_status(bool val)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(Button_status), val);
                return;
            }
            else
            {
                this.btn_Analyze.Enabled = val;
            }
        }

        public void Button_status_text(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Button_status_text), text);
                return;
            }
            else
            {
                this.btn_Analyze.Text = text;
            }
        }

        #endregion

        #region Form UI Helpers

        void RefreshSettings()
        {
            txtBx_MapFilepath.Text = _settings.MapPath;
            txtBx_ElfFilepath.Text = _settings.ElfPath;
            BINUTIL_NM = _settings.NMPath;
            BINUTIL_READ_ELF = _settings.ReadElfPath;
            MAPParser.Instance.C_BSS_ID = _settings.BSSSeg2SecMap.ToArray();
            MAPParser.Instance.C_DATA_ID = _settings.DataSeg2SecMap.ToArray();
            MAPParser.Instance.C_TEXT_ID = _settings.TextSeg2SecMap.ToArray();
        }

        /// <summary>
        /// This is the main work horse task.
        /// 1. The MAP file is parsed and the output is bound to the module list view
        /// 2. The DWARF information and NM output is parsed next and bound with the symbol list view
        /// </summary>
        void AnalyzeSymbols()
        {
            _UIUpdateInProgress = true;
            // Parse the MAP file alone
            if (!MAPParser.Instance.Run(txtBx_MapFilepath.Text)) return;
            // Update the ListView for Modules
            AddSumRow(MAPParser.Instance.ModuleMap);
            PopulateModuleLV(MAPParser.Instance.ModuleMap);

            // Analyze Symbols
            if (txtBx_ElfFilepath.Text == "" || !File.Exists(txtBx_ElfFilepath.Text))
            {
                MessageBox.Show("Please enter a valid ELF file path for symbol analysis!");
                //hide_sym_column();
                //return;
            }
            if (BINUTIL_READ_ELF == "" || !File.Exists(BINUTIL_READ_ELF) ||
                BINUTIL_NM == "" || !File.Exists(BINUTIL_NM))
            {
                MessageBox.Show("Please enter a valid ObjectDump and NM path for symbol analysis!\n Click the Settings button!");
                //return;
            }

            // Parse the dwarf information to get all the compilation units
            DwarfParser.Instance.Run(BINUTIL_READ_ELF, txtBx_ElfFilepath.Text);
            // Extract the symbols using NM
            _syms = SymParser.Instance;
            _syms.Run(BINUTIL_NM, txtBx_ElfFilepath.Text, this);

            // Update symbols present in MAP but missing in NM output
            // FIXME: The size of these "hidden" symbols may not be accurate as of now..
            foreach (Symbol s in MAPParser.Instance.MapSymbols)
            {
                if (!_syms.Symbols.Exists(x => x.SymbolName == s.SymbolName))
                {
                    s.GlobalScope = Symbol.TYPE_HIDDEN;
                    _syms.Symbols.Add(s);
                    Debug.WriteLineIf(DEBUG, s.SymbolName + " : " + s.Size.ToString());
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
            _settings.MapPath = txtBx_MapFilepath.Text;
            _settings.ElfPath = txtBx_ElfFilepath.Text;
            _settings.SaveAppSettings(); // persist all modified settings
        }

        #endregion

        #region Object List View Handlers

        private void olv_SymbolView_SelectionChanged(object sender, EventArgs e)
        {
            Debug.WriteLineIf(DEBUG, "selected changed");
        }

        // string - /cygdrive/c/Program Files (x86)/FTDI/FT90x Toolchain/Toolchain/tools/bin/../lib/gcc/ft32-elf/5.0.0/../../../../ft32-elf/lib/libc.a(lib_a-vfiprintf.o)
        // regex - [^\/(]+(?=.o\)$)  ==> match result = lib_a-vfiprintf
        // [^\/(]+(?=.o\)?$) => extracts lib_a-vfiprintf from both (lib_a-vfiprintf.o) and lib_a-vfiprintf.o
        private void olv_ModuleView_SelectionChanged(object sender, EventArgs e)
        {
            if (_syms == null || _UIUpdateInProgress) return;

            PopulateSymbolLV(FilterSymbols(olv_ModuleView.SelectedObjects.Cast<Module>().ToList()));
#if TEST
            tlv_Cref.CanExpandGetter = x => { return ((CrefNode)x).Children.Count > 0; };
            tlv_Cref.ChildrenGetter = x => { return ((CrefNode)x).Children; };
            var cref_tree = new List<CrefNode>();
            cref_tree.Add(new CrefNode(olv_ModuleView.SelectedObjects.Cast<Module>().FirstOrDefault().ModuleName));
            Build(cref_tree[0], 5);
            tlv_Cref.SetObjects(cref_tree);
#endif
        }

        private void hide_sym_column()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                // Hide Symbol view if there's no elf file
                tlp_Main.ColumnStyles[1].Width = 0;
            }));
        }

#endregion

#region OLV Helpers

        /// <summary>
        /// Builds the list of items for the Module View
        /// The appearance of the list elements is adjusted here, and the list view data is bound here.
        /// Mapping between the list elements and the elemenst of <Module> class is preset in the Form designer.
        /// </summary>
        /// <param name="m"></param>
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
            // Extra: Update the actuals also
            this.BeginInvoke(new MethodInvoker(() =>
            {
                lbl_TextSizeActual.Text = "Text: " + MAPParser.Instance.TextSegSize.ToString() + " (" + (MAPParser.Instance.TextSegSize / 1024f).ToString("##.##") + " KB)";
                lbl_BssSizeActual.Text = "Bss: " + MAPParser.Instance.BssSize.ToString() + " (" + (MAPParser.Instance.BssSize / 1024f).ToString("##.##") + " KB)";
                lbl_DataSizeActual.Text = "Data: " + MAPParser.Instance.DataSize.ToString() + " (" + (MAPParser.Instance.DataSize / 1024f).ToString("##.##") + " KB)";
            }));

        }

        /// <summary>
        /// Builds the list of items for the Symbol View
        /// Custom aspect getters are used to fine-tune the appearance of the address column
        /// </summary>
        /// <param name="s"></param>
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
            this.symAddrColumn.AspectGetter = (x) => { return ((Symbol)x).LoadAddress.ToString("X6"); };
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

        /// <summary>
        /// Given a list of selected modules, return the list of symbols associated with them
        /// </summary>
        /// <param name="mods"></param>
        /// <returns></returns>
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
                    //Debug.WriteLineIf(DEBUG,"Match: " + m1.ToString());
                    return true;
                }
                else return false;
            })).ToList();

            return syms;
        }

#endregion

        private void InvokeEx(Func<object, object> p)
        {
            throw new NotImplementedException();
        }


#region     TREE LIST VIEW

        int Depth;
        private void tlv_Init()
        {
            tlv_Cref.CanExpandGetter = x => { return ((CrefNode)x).Children.Count > 0; };
            tlv_Cref.ChildrenGetter = x => { return ((CrefNode)x).Children; };

            //var MyClasses = new List<CrefNode>();
            //MyClasses.Add(new CrefNode("Bob"));
            //MyClasses.Add(new CrefNode("John"));
            //var myClass = new CrefNode("Mike");
            //myClass.Children.Add(new CrefNode("Joe"));
            //MyClasses.Add(myClass);

            //tlv_Cref.SetObjects(MyClasses);
            // findfp

            /* First of all we need to know the root node module name */
            string root_node_module = "impure.o";
            CrefEntry cr = cref.CrefTable.Where(x => x.SouceModule.Contains(root_node_module)).ToList().FirstOrDefault();
            
            var cref_tree = new List<CrefNode>();
            cref_tree.Add(new CrefNode(cr.SouceModule));

            Build(cref_tree[0]);
            Depth = 0;
            /*
            bool end = false;
            int depth = 10;
            for(int i=0; i < 1; i++)
            {
                List<CrefNode> c = FindChildren(cref_tree[i]);
                cref_tree[i].Children = c;
                
                for (int j=0; j < c.Count; j++)
                {
                    c[j].Children = FindChildren(c[j]);
                }
            }
            */
            tlv_Cref.SetObjects(cref_tree);
        }


        void Build(CrefNode n, int depth)
        {
            //if (Depth++ > 2000) return;
            depth--;
            if ((depth == 0)) return;
            Debug.WriteLineIf(DEBUG, "depth: " + depth.ToString());
            /* If the node already exists in the tree, forget it */
            if (!IsUnique(n, n.Parent)) return;
            List<CrefNode> c = FindChildren(n);
            n.Children = c;
            foreach (CrefNode k in c)
            {
                k.Parent = n;
                Build(k, depth);
            }
        }

        void Build(CrefNode n)
        {
            if (Depth++ > 2000) return;
            /* If the node already exists in the tree, forget it */
            if (!IsUnique(n, n.Parent)) return;
            List<CrefNode> c = FindChildren(n);
            n.Children = c;
            foreach(CrefNode k in c)
            {
                k.Parent = n;
                Build(k);
            }
        }

        /// <summary>
        /// Walk the tree and see if any node contains this element 
        /// </summary>
        /// <param name="n"> The node which could have been duplicated in the tree </param>
        /// <param name="p"> The next parent </param>
        /// <returns></returns>
        bool IsUnique(CrefNode n, CrefNode p)
        {
            if (p == null) return true;
            if (n.Module == p.Module)
                return false;
            else return IsUnique(n, p.Parent);
        }

        List<CrefNode> FindChildren(CrefNode n)
        {
            List<string> nodes = cref.FindUsers(n.Module);
            List<CrefNode> childs = new List<CrefNode>();
            for (int i = 0; i < nodes.Count; i++)
            {
                childs.Add(new CrefNode(nodes[i]));
            }
            return childs;
        }

#endregion
    }
}
