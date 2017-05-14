/*
 * ObjectListViewDemo - A simple demo to show the ObjectListView control
 *
 * User: Phillip Piper
 * Date: 15/10/2006 11:15 AM
 *
 * Change log:
 * 2015-06-12  JPP  COMPLETE REWRITE. Goal of rewrite is to make the code much easier to follow
 * 
 * 2009-07-04  JPP  Added ExampleVirtualDataSource for virtual list demo
 * [lots of stuff]
 * 2006-10-20  JPP  Added DataSet tab page
 * 2006-10-15  JPP  Initial version
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ObjectListViewDemo {

    public partial class MainForm {

        [STAThread]
        public static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        ///
        /// </summary>
        public MainForm() {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            InitializeExamples();
        }

        void InitializeExamples() {
            // Use different font under Vista
            if (ObjectListView.IsVistaOrLater)
                this.Font = new Font("Segoe UI", 9);
             
            OLVDemoCoordinator coordinator = new OLVDemoCoordinator(this);

            this.tabSimple.Coordinator = coordinator;
            this.tabComplex.Coordinator = coordinator;
            this.tabDataSet.Coordinator = coordinator;
            this.tabFileExplorer1.Coordinator = coordinator;
            this.tabFastList1.Coordinator = coordinator;
            this.tabTreeListView1.Coordinator = coordinator;
            this.tabDataTreeListView1.Coordinator = coordinator;
            this.tabDragAndDrop1.Coordinator = coordinator;
            this.tabDescribedTask1.Coordinator = coordinator;

            // Printing tab is slightly different, since it needs to know about the ObjectListViews from the other tabs
            this.tabPrinting1.SimpleView = this.tabSimple.ListView;
            this.tabPrinting1.ComplexView = this.tabComplex.ListView;
            this.tabPrinting1.DataListView = this.tabDataSet.ListView;
            this.tabPrinting1.FileExplorerView = this.tabFileExplorer1.ListView;
            this.tabPrinting1.TreeListView = this.tabTreeListView1.ListView;
            this.tabPrinting1.Coordinator = coordinator;

            //this.tabControl1.SelectTab(this.tabDescribedTasks);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.TabPages[e.TabPageIndex].Name == "tabPagePrinting")
                this.tabPrinting1.UpdatePrintPreview();
        }
    }
}
