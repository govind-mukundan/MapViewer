using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ObjectListViewDemo.Models;

namespace ObjectListViewDemo
{
    public partial class TabTreeListView : OlvDemoTab {
        private byte[] treeListViewViewState;

        public TabTreeListView()
        {
            InitializeComponent();
            this.ListView = treeListView;
            this.ListView.CellClick += (sender, args) => Debug.WriteLine("CellClicked: {0}", args);
        }

        protected override void InitializeTab() {

            // Setup the controls on the tab
            // On other tabs, we add "Vista" hot item style. But that only works when NOT in
            // owner draw mode, and TreeListViews *requires* OwnerDraw. So we can't use Vista style hot item
            // with TreeListView, so we don't give the option
            this.comboBoxHotItemStyle.SelectedIndex = 0; // None
            this.treeListView.HierarchicalCheckboxes = this.checkBoxHierarchicalCheckboxes.Checked;
            this.comboBoxExpanders.SelectedIndex = 2; // triangles

            SetupColumns();
            SetupDragAndDrop();
            SetupTree();

            // You can change the way the connection lines are drawn by changing the pen
            TreeListView.TreeRenderer renderer = this.treeListView.TreeColumnRenderer;
            renderer.LinePen = new Pen(Color.Firebrick, 0.5f);
            renderer.LinePen.DashStyle = DashStyle.Dot;
        }

        private void SetupDragAndDrop() {

            // Setup the tree so that it can drop and drop.

            // Dropping doesn't do anything, but it does show how it works

            treeListView.IsSimpleDragSource = true;
            treeListView.IsSimpleDropSink = true;

            treeListView.ModelCanDrop += delegate(object sender, ModelDropEventArgs e) {
                e.Effect = DragDropEffects.None;
                if (e.TargetModel == null)
                    return;

                if (e.TargetModel is DirectoryInfo)
                    e.Effect = e.StandardDropActionFromKeys;
                else
                    e.InfoMessage = "Can only drop on directories";
            };

            treeListView.ModelDropped += delegate(object sender, ModelDropEventArgs e) {
                String msg = String.Format("{2} items were dropped on '{1}' as a {0} operation.",
                    e.Effect, ((DirectoryInfo) e.TargetModel).Name, e.SourceModels.Count);
                Coordinator.ShowMessage(msg);
            };
        }

        private void SetupTree() {

            // TreeListView require two delegates:
            // 1. CanExpandGetter - Can a particular model be expanded?
            // 2. ChildrenGetter - Once the CanExpandGetter returns true, ChildrenGetter should return the list of children

            // CanExpandGetter is called very often! It must be very fast.

            this.treeListView.CanExpandGetter = delegate(object x) {
                return ((MyFileSystemInfo) x).IsDirectory;
            };

            // We just want to get the children of the given directory.
            // This becomes a little complicated when we can't (for whatever reason). We need to report the error 
            // to the user, but we can't just call MessageBox.Show() directly, since that would stall the UI thread
            // leaving the tree in a potentially undefined state (not good). We also don't want to keep trying to
            // get the contents of the given directory if the tree is refreshed. To get around the first problem,
            // we immediately return an empty list of children and use BeginInvoke to show the MessageBox at the 
            // earliest opportunity. We get around the second problem by collapsing the branch again, so it's children
            // will not be fetched when the tree is refreshed. The user could still explicitly unroll it again --
            // that's their problem :)
            this.treeListView.ChildrenGetter = delegate(object x) {
                try {
                    return ((MyFileSystemInfo) x).GetFileSystemInfos();
                }
                catch (UnauthorizedAccessException ex) {
                    this.BeginInvoke((MethodInvoker)delegate() {
                        this.treeListView.Collapse(x);
                        MessageBox.Show(this, ex.Message, "ObjectListViewDemo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                    });
                    return new ArrayList();
                }
            };

            // Once those two delegates are in place, the TreeListView starts working
            // after setting the Roots property.

            // List all drives as the roots of the tree
            ArrayList roots = new ArrayList();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.IsReady)
                    roots.Add(new MyFileSystemInfo(new DirectoryInfo(di.Name)));
            }
            this.treeListView.Roots = roots;
        }

        private void SetupColumns() {
            // The column setup here is identical to the File Explorer example tab --
            // nothing specific to the TreeListView. 

            // The only difference is that we don't setup anything to do with grouping,
            // since TreeListViews can't show groups.

            SysImageListHelper helper = new SysImageListHelper(this.treeListView);
            this.olvColumnName.ImageGetter = delegate(object x) {
                return helper.GetImageIndex(((MyFileSystemInfo) x).FullName);
            };

            // Get the size of the file system entity. 
            // Folders and errors are represented as negative numbers
            this.olvColumnSize.AspectGetter = delegate(object x) {
                MyFileSystemInfo myFileSystemInfo = (MyFileSystemInfo) x;

                if (myFileSystemInfo.IsDirectory)
                    return (long) -1;

                try {
                    return myFileSystemInfo.Length;
                }
                catch (System.IO.FileNotFoundException) {
                    // Mono 1.2.6 throws this for hidden files
                    return (long) -2;
                }
            };

            // Show the size of files as GB, MB and KBs. By returning the actual
            // size in the AspectGetter, and doing the conversion in the 
            // AspectToStringConverter, sorting on this column will work off the
            // actual sizes, rather than the formatted string.
            this.olvColumnSize.AspectToStringConverter = delegate(object x) {
                long sizeInBytes = (long) x;
                if (sizeInBytes < 0) // folder or error
                    return "";
                return Coordinator.FormatFileSize(sizeInBytes);
            };

            // Show the system description for this object
            this.olvColumnFileType.AspectGetter = delegate(object x) {
                return ShellUtilities.GetFileType(((MyFileSystemInfo) x).FullName);
            };

            // Show the file attributes for this object
            // A FlagRenderer masks off various values and draws zero or images based 
            // on the presence of individual bits.
            this.olvColumnAttributes.AspectGetter = delegate(object x) {
                return ((MyFileSystemInfo) x).Attributes;
            };
            FlagRenderer attributesRenderer = new FlagRenderer();
            attributesRenderer.ImageList = imageListSmall;
            attributesRenderer.Add(FileAttributes.Archive, "archive");
            attributesRenderer.Add(FileAttributes.ReadOnly, "readonly");
            attributesRenderer.Add(FileAttributes.System, "system");
            attributesRenderer.Add(FileAttributes.Hidden, "hidden");
            attributesRenderer.Add(FileAttributes.Temporary, "temporary");
            this.olvColumnAttributes.Renderer = attributesRenderer;

            // Tell the filtering subsystem that the attributes column is a collection of flags
            this.olvColumnAttributes.ClusteringStrategy = new FlagClusteringStrategy(typeof (FileAttributes));
        }

        #region UI Event handlers

        private void treeListView_ItemActivate(object sender, EventArgs e)
        {
            Object model = this.treeListView.SelectedObject;
            if (model != null)
                this.treeListView.ToggleExpansion(model);
        }

        private void checkBoxHierarchicalCheckboxes_CheckedChanged(object sender, EventArgs e) {
            this.treeListView.HierarchicalCheckboxes = ((CheckBox) sender).Checked;
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            Coordinator.TimedFilter(this.ListView, ((TextBox)sender).Text);
        }

        private void comboBoxHotItemStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Coordinator.ChangeHotItemStyle(this.ListView, (ComboBox)sender);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.treeListView.RefreshObjects(this.treeListView.SelectedObjects);
        }

        private void buttonCheck_Click(object sender, EventArgs e) {
            this.treeListView.ToggleSelectedRowCheckBoxes();
        }

        private void buttonSaveState_Click(object sender, EventArgs e)
        {
            // SaveState() returns a byte array that holds the current state of the columns.
            // For this demo, we just hold onto that value in an instance variable. For your
            // application, you should persist it some more permanent fashion than this.
            this.treeListViewViewState = this.treeListView.SaveState();
            this.buttonRestoreState.Enabled = true;
        }

        private void buttonRestoreState_Click(object sender, EventArgs e)
        {
            this.treeListView.RestoreState(this.treeListViewViewState);
        }

        private void buttonColumns_Click(object sender, EventArgs e)
        {
            ColumnSelectionForm form = new ColumnSelectionForm();
            form.OpenOn(this.treeListView);
        }

        private void buttonDisable_Click(object sender, EventArgs e)
        {
            bool isControlKeyDown = ((Control.ModifierKeys & Keys.Control) == Keys.Control);
            if (isControlKeyDown)
                this.ListView.EnableObjects(this.ListView.DisabledObjects);
            else
                this.ListView.DisableObjects(this.ListView.SelectedObjects);
        }

        #endregion

        private void comboBoxExpanders_SelectedIndexChanged(object sender, EventArgs e) {
            TreeListView.TreeRenderer treeColumnRenderer = this.treeListView.TreeColumnRenderer;
            ComboBox cb = (ComboBox)sender;
            switch (cb.SelectedIndex)
            {
                case 0:
                    treeColumnRenderer.IsShowGlyphs = false;
                    break;
                case 1:
                    treeColumnRenderer.IsShowGlyphs = true;
                    treeColumnRenderer.UseTriangles = false;
                    break;
                case 2:
                    treeColumnRenderer.IsShowGlyphs = true;
                    treeColumnRenderer.UseTriangles = true;
                    break;
            }

            // Cause a redraw so that the changes to the renderer take effect
            this.treeListView.Refresh();
        }

    }
}
