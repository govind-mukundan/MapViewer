namespace ObjectListViewDemo
{
    partial class TabTreeListView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabTreeListView));
            this.checkBoxHierarchicalCheckboxes = new System.Windows.Forms.CheckBox();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSaveState = new System.Windows.Forms.Button();
            this.buttonRestoreState = new System.Windows.Forms.Button();
            this.buttonColumns = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.treeListView = new BrightIdeasSoftware.TreeListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCreated = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModified = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFileType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAttributes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonDisable = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.comboBoxHotItemStyle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxExpanders = new System.Windows.Forms.ComboBox();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxHierarchicalCheckboxes
            // 
            this.checkBoxHierarchicalCheckboxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxHierarchicalCheckboxes.AutoSize = true;
            this.checkBoxHierarchicalCheckboxes.Location = new System.Drawing.Point(3, 474);
            this.checkBoxHierarchicalCheckboxes.Name = "checkBoxHierarchicalCheckboxes";
            this.checkBoxHierarchicalCheckboxes.Size = new System.Drawing.Size(85, 17);
            this.checkBoxHierarchicalCheckboxes.TabIndex = 32;
            this.checkBoxHierarchicalCheckboxes.Text = "Checkboxes";
            this.toolTip1.SetToolTip(this.checkBoxHierarchicalCheckboxes, "Show hierarchical checkboxes");
            this.checkBoxHierarchicalCheckboxes.UseVisualStyleBackColor = true;
            this.checkBoxHierarchicalCheckboxes.CheckedChanged += new System.EventHandler(this.checkBoxHierarchicalCheckboxes_CheckedChanged);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCheck.Location = new System.Drawing.Point(516, 470);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(71, 23);
            this.buttonCheck.TabIndex = 31;
            this.buttonCheck.Text = "Check";
            this.toolTip1.SetToolTip(this.buttonCheck, "Toggle checkedness of the selected items");
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.textBoxFilter);
            this.groupBox12.Location = new System.Drawing.Point(685, 3);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(117, 44);
            this.groupBox12.TabIndex = 30;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Filter";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(7, 20);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilter.TabIndex = 0;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRefresh.Location = new System.Drawing.Point(445, 470);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(71, 23);
            this.buttonRefresh.TabIndex = 29;
            this.buttonRefresh.Text = "Refresh";
            this.toolTip1.SetToolTip(this.buttonRefresh, "Refresh the selected items");
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonSaveState
            // 
            this.buttonSaveState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveState.Location = new System.Drawing.Point(587, 470);
            this.buttonSaveState.Name = "buttonSaveState";
            this.buttonSaveState.Size = new System.Drawing.Size(71, 23);
            this.buttonSaveState.TabIndex = 25;
            this.buttonSaveState.Text = "Save State";
            this.toolTip1.SetToolTip(this.buttonSaveState, "Save the width and arrangement of columns");
            this.buttonSaveState.UseVisualStyleBackColor = true;
            this.buttonSaveState.Click += new System.EventHandler(this.buttonSaveState_Click);
            // 
            // buttonRestoreState
            // 
            this.buttonRestoreState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestoreState.Enabled = false;
            this.buttonRestoreState.Location = new System.Drawing.Point(658, 470);
            this.buttonRestoreState.Name = "buttonRestoreState";
            this.buttonRestoreState.Size = new System.Drawing.Size(71, 23);
            this.buttonRestoreState.TabIndex = 26;
            this.buttonRestoreState.Text = "Restore";
            this.toolTip1.SetToolTip(this.buttonRestoreState, "Restore the width and arrangement of columns");
            this.buttonRestoreState.UseVisualStyleBackColor = true;
            this.buttonRestoreState.Click += new System.EventHandler(this.buttonRestoreState_Click);
            // 
            // buttonColumns
            // 
            this.buttonColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonColumns.Location = new System.Drawing.Point(731, 470);
            this.buttonColumns.Name = "buttonColumns";
            this.buttonColumns.Size = new System.Drawing.Size(71, 23);
            this.buttonColumns.TabIndex = 27;
            this.buttonColumns.Text = "&Columns...";
            this.toolTip1.SetToolTip(this.buttonColumns, "Choose which columns are visible");
            this.buttonColumns.UseVisualStyleBackColor = true;
            this.buttonColumns.Click += new System.EventHandler(this.buttonColumns_Click);
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Location = new System.Drawing.Point(3, 4);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(676, 46);
            this.label32.TabIndex = 24;
            this.label32.Text = "This is like the File Explorer tab, except that it shows the directory structure," +
    " rooted on the available disks.";
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.olvColumnName);
            this.treeListView.AllColumns.Add(this.olvColumnCreated);
            this.treeListView.AllColumns.Add(this.olvColumnModified);
            this.treeListView.AllColumns.Add(this.olvColumnSize);
            this.treeListView.AllColumns.Add(this.olvColumnFileType);
            this.treeListView.AllColumns.Add(this.olvColumnAttributes);
            this.treeListView.AllowColumnReorder = true;
            this.treeListView.AllowDrop = true;
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView.CellEditUseWholeCell = false;
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnCreated,
            this.olvColumnModified,
            this.olvColumnSize,
            this.olvColumnFileType,
            this.olvColumnAttributes});
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.EmptyListMsg = "This folder is completely empty!";
            this.treeListView.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.HideSelection = false;
            this.treeListView.IsSimpleDragSource = true;
            this.treeListView.IsSimpleDropSink = true;
            this.treeListView.Location = new System.Drawing.Point(3, 53);
            this.treeListView.Name = "treeListView";
            this.treeListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.treeListView.ShowCommandMenuOnRightClick = true;
            this.treeListView.ShowGroups = false;
            this.treeListView.ShowImagesOnSubItems = true;
            this.treeListView.ShowItemToolTips = true;
            this.treeListView.Size = new System.Drawing.Size(799, 413);
            this.treeListView.SmallImageList = this.imageListSmall;
            this.treeListView.TabIndex = 28;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.UseFilterIndicator = true;
            this.treeListView.UseFiltering = true;
            this.treeListView.UseHotItem = true;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            this.treeListView.ItemActivate += new System.EventHandler(this.treeListView_ItemActivate);
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.IsTileViewColumn = true;
            this.olvColumnName.Text = "Name";
            this.olvColumnName.UseInitialLetterForGroup = true;
            this.olvColumnName.Width = 180;
            this.olvColumnName.WordWrap = true;
            // 
            // olvColumnCreated
            // 
            this.olvColumnCreated.AspectName = "CreationTime";
            this.olvColumnCreated.DisplayIndex = 4;
            this.olvColumnCreated.Text = "Created";
            this.olvColumnCreated.Width = 131;
            // 
            // olvColumnModified
            // 
            this.olvColumnModified.AspectName = "LastWriteTime";
            this.olvColumnModified.DisplayIndex = 1;
            this.olvColumnModified.IsTileViewColumn = true;
            this.olvColumnModified.Text = "Modified";
            this.olvColumnModified.Width = 145;
            // 
            // olvColumnSize
            // 
            this.olvColumnSize.AspectName = "Extension";
            this.olvColumnSize.DisplayIndex = 2;
            this.olvColumnSize.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnSize.Text = "Size";
            this.olvColumnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnSize.Width = 80;
            // 
            // olvColumnFileType
            // 
            this.olvColumnFileType.DisplayIndex = 3;
            this.olvColumnFileType.IsTileViewColumn = true;
            this.olvColumnFileType.Text = "File Type";
            this.olvColumnFileType.Width = 148;
            // 
            // olvColumnAttributes
            // 
            this.olvColumnAttributes.FillsFreeSpace = true;
            this.olvColumnAttributes.IsEditable = false;
            this.olvColumnAttributes.MinimumWidth = 20;
            this.olvColumnAttributes.Text = "Attributes";
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "compass");
            this.imageListSmall.Images.SetKeyName(1, "down");
            this.imageListSmall.Images.SetKeyName(2, "user");
            this.imageListSmall.Images.SetKeyName(3, "find");
            this.imageListSmall.Images.SetKeyName(4, "folder");
            this.imageListSmall.Images.SetKeyName(5, "movie");
            this.imageListSmall.Images.SetKeyName(6, "music");
            this.imageListSmall.Images.SetKeyName(7, "no");
            this.imageListSmall.Images.SetKeyName(8, "readonly");
            this.imageListSmall.Images.SetKeyName(9, "public");
            this.imageListSmall.Images.SetKeyName(10, "recycle");
            this.imageListSmall.Images.SetKeyName(11, "spanner");
            this.imageListSmall.Images.SetKeyName(12, "star");
            this.imageListSmall.Images.SetKeyName(13, "tick");
            this.imageListSmall.Images.SetKeyName(14, "archive");
            this.imageListSmall.Images.SetKeyName(15, "system");
            this.imageListSmall.Images.SetKeyName(16, "hidden");
            this.imageListSmall.Images.SetKeyName(17, "temporary");
            // 
            // buttonDisable
            // 
            this.buttonDisable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDisable.Location = new System.Drawing.Point(374, 470);
            this.buttonDisable.Name = "buttonDisable";
            this.buttonDisable.Size = new System.Drawing.Size(71, 23);
            this.buttonDisable.TabIndex = 35;
            this.buttonDisable.Text = "Disable";
            this.toolTip1.SetToolTip(this.buttonDisable, "Disable the selected items. Cntl-click to re-enable all rows");
            this.buttonDisable.UseVisualStyleBackColor = true;
            this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(222, 475);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(50, 13);
            this.label37.TabIndex = 33;
            this.label37.Text = "Hot Item:";
            // 
            // comboBoxHotItemStyle
            // 
            this.comboBoxHotItemStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxHotItemStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHotItemStyle.FormattingEnabled = true;
            this.comboBoxHotItemStyle.Items.AddRange(new object[] {
            "None",
            "Text Color",
            "Border",
            "Translucent",
            "Lightbox"});
            this.comboBoxHotItemStyle.Location = new System.Drawing.Point(273, 471);
            this.comboBoxHotItemStyle.Name = "comboBoxHotItemStyle";
            this.comboBoxHotItemStyle.Size = new System.Drawing.Size(86, 21);
            this.comboBoxHotItemStyle.TabIndex = 34;
            this.comboBoxHotItemStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxHotItemStyle_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 475);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Expander:";
            // 
            // comboBoxExpanders
            // 
            this.comboBoxExpanders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxExpanders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExpanders.FormattingEnabled = true;
            this.comboBoxExpanders.Items.AddRange(new object[] {
            "None",
            "Plus/Minus",
            "Triangles"});
            this.comboBoxExpanders.Location = new System.Drawing.Point(142, 471);
            this.comboBoxExpanders.Name = "comboBoxExpanders";
            this.comboBoxExpanders.Size = new System.Drawing.Size(79, 21);
            this.comboBoxExpanders.TabIndex = 37;
            this.comboBoxExpanders.SelectedIndexChanged += new System.EventHandler(this.comboBoxExpanders_SelectedIndexChanged);
            // 
            // TabTreeListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxExpanders);
            this.Controls.Add(this.buttonDisable);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.comboBoxHotItemStyle);
            this.Controls.Add(this.checkBoxHierarchicalCheckboxes);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonSaveState);
            this.Controls.Add(this.buttonRestoreState);
            this.Controls.Add(this.buttonColumns);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.treeListView);
            this.Name = "TabTreeListView";
            this.Size = new System.Drawing.Size(804, 499);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxHierarchicalCheckboxes;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSaveState;
        private System.Windows.Forms.Button buttonRestoreState;
        private System.Windows.Forms.Button buttonColumns;
        private System.Windows.Forms.Label label32;
        private BrightIdeasSoftware.TreeListView treeListView;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnCreated;
        private BrightIdeasSoftware.OLVColumn olvColumnModified;
        private BrightIdeasSoftware.OLVColumn olvColumnSize;
        private BrightIdeasSoftware.OLVColumn olvColumnFileType;
        private BrightIdeasSoftware.OLVColumn olvColumnAttributes;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox comboBoxHotItemStyle;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.Button buttonDisable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxExpanders;
    }
}
