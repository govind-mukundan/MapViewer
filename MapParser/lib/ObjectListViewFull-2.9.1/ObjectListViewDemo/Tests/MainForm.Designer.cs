/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/21/2008 11:01 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/21/2008 JPP  Initial Version
 */
namespace BrightIdeasSoftware.Tests
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvcName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOccupation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcCulinaryColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvCanTellJokes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.folvcName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.folvOccupation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.folvCulinaryRating = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.folvCanTellJokes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.tlvcName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlvcOccupation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlvcCulinaryRating = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlvcCanTellJokes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objectListView2 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).BeginInit();
            this.SuspendLayout();
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvcName);
            this.objectListView1.AllColumns.Add(this.olvcOccupation);
            this.objectListView1.AllColumns.Add(this.olvcCulinaryColumn);
            this.objectListView1.AllColumns.Add(this.olvCanTellJokes);
            this.objectListView1.AllColumns.Add(this.olvColumn1);
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcName,
            this.olvcOccupation,
            this.olvcCulinaryColumn,
            this.olvCanTellJokes,
            this.olvColumn1});
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.Location = new System.Drawing.Point(13, 13);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(819, 167);
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvcName
            // 
            this.olvcName.AspectName = "Name";
            this.olvcName.Text = "Name";
            this.olvcName.Width = 176;
            // 
            // olvcOccupation
            // 
            this.olvcOccupation.AspectName = "Occupation";
            this.olvcOccupation.Text = "Occupation";
            this.olvcOccupation.Width = 178;
            // 
            // olvcCulinaryColumn
            // 
            this.olvcCulinaryColumn.AspectName = "CulinaryRating";
            this.olvcCulinaryColumn.Text = "Culinary Rating";
            this.olvcCulinaryColumn.Width = 116;
            // 
            // olvCanTellJokes
            // 
            this.olvCanTellJokes.AspectName = "CanTellJokes";
            this.olvCanTellJokes.Text = "CanTellJokes";
            this.olvCanTellJokes.Width = 117;
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.folvcName);
            this.fastObjectListView1.AllColumns.Add(this.folvOccupation);
            this.fastObjectListView1.AllColumns.Add(this.folvCulinaryRating);
            this.fastObjectListView1.AllColumns.Add(this.folvCanTellJokes);
            this.fastObjectListView1.AllColumns.Add(this.olvColumn2);
            this.fastObjectListView1.CheckBoxes = false;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.folvcName,
            this.folvOccupation,
            this.folvCulinaryRating,
            this.folvCanTellJokes,
            this.olvColumn2});
            this.fastObjectListView1.Location = new System.Drawing.Point(13, 198);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(819, 171);
            this.fastObjectListView1.TabIndex = 1;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            // 
            // folvcName
            // 
            this.folvcName.AspectName = "Name";
            this.folvcName.Text = "Name";
            this.folvcName.Width = 160;
            // 
            // folvOccupation
            // 
            this.folvOccupation.AspectName = "Occupation";
            this.folvOccupation.Text = "Occupation";
            this.folvOccupation.Width = 150;
            // 
            // folvCulinaryRating
            // 
            this.folvCulinaryRating.AspectName = "CulinaryRating";
            this.folvCulinaryRating.Text = "Culinary Rating";
            this.folvCulinaryRating.Width = 138;
            // 
            // folvCanTellJokes
            // 
            this.folvCanTellJokes.AspectName = "CanTellJokes";
            this.folvCanTellJokes.Text = "CanTellJokes";
            this.folvCanTellJokes.Width = 104;
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.tlvcName);
            this.treeListView1.AllColumns.Add(this.tlvcOccupation);
            this.treeListView1.AllColumns.Add(this.tlvcCulinaryRating);
            this.treeListView1.AllColumns.Add(this.tlvcCanTellJokes);
            this.treeListView1.AllColumns.Add(this.olvColumn3);
            this.treeListView1.CheckBoxes = false;
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tlvcName,
            this.tlvcOccupation,
            this.tlvcCulinaryRating,
            this.tlvcCanTellJokes,
            this.olvColumn3});
            this.treeListView1.Location = new System.Drawing.Point(13, 395);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(819, 203);
            this.treeListView1.TabIndex = 2;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            // 
            // tlvcName
            // 
            this.tlvcName.AspectName = "Name";
            this.tlvcName.Text = "Name";
            this.tlvcName.Width = 160;
            // 
            // tlvcOccupation
            // 
            this.tlvcOccupation.AspectName = "Occupation";
            this.tlvcOccupation.Text = "Occupation";
            this.tlvcOccupation.Width = 160;
            // 
            // tlvcCulinaryRating
            // 
            this.tlvcCulinaryRating.AspectName = "CulinaryRating";
            this.tlvcCulinaryRating.Text = "Culinary Rating";
            this.tlvcCulinaryRating.Width = 146;
            // 
            // tlvcCanTellJokes
            // 
            this.tlvcCanTellJokes.AspectName = "CanTellJokes";
            this.tlvcCanTellJokes.Text = "CanTellJokes";
            this.tlvcCanTellJokes.Width = 121;
            // 
            // objectListView2
            // 
            this.objectListView2.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView2.Location = new System.Drawing.Point(13, 604);
            this.objectListView2.Name = "objectListView2";
            this.objectListView2.Size = new System.Drawing.Size(819, 97);
            this.objectListView2.TabIndex = 3;
            this.objectListView2.UseCompatibleStateImageBehavior = false;
            this.objectListView2.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "IsActive";
            this.olvColumn1.CheckBoxes = true;
            this.olvColumn1.Text = "IsActive";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "IsActive";
            this.olvColumn2.CheckBoxes = true;
            this.olvColumn2.Text = "IsActive";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "IsActive";
            this.olvColumn3.CheckBoxes = true;
            this.olvColumn3.Text = "IsActive";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 700);
            this.Controls.Add(this.objectListView2);
            this.Controls.Add(this.treeListView1);
            this.Controls.Add(this.fastObjectListView1);
            this.Controls.Add(this.objectListView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Tests";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).EndInit();
            this.ResumeLayout(false);

        }
        private OLVColumn olvcName;
        private OLVColumn olvcOccupation;
        private OLVColumn folvcName;
        private OLVColumn folvOccupation;
        private OLVColumn tlvcName;
        private OLVColumn tlvcOccupation;
        public TreeListView treeListView1;
        public ObjectListView objectListView1;
        public FastObjectListView fastObjectListView1;
        private OLVColumn olvcCulinaryColumn;
        private OLVColumn folvCulinaryRating;
        private OLVColumn tlvcCulinaryRating;
        private OLVColumn olvCanTellJokes;
        private OLVColumn folvCanTellJokes;
        private OLVColumn tlvcCanTellJokes;
        public ObjectListView objectListView2;
        private OLVColumn olvColumn1;
        private OLVColumn olvColumn2;
        private OLVColumn olvColumn3;
	}
}
