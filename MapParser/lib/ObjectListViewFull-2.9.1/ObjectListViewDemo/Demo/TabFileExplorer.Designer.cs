namespace ObjectListViewDemo
{
    partial class TabFileExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabFileExplorer));
            this.label37 = new System.Windows.Forms.Label();
            this.comboBoxHotItemStyle = new System.Windows.Forms.ComboBox();
            this.checkBoxTooltips = new System.Windows.Forms.CheckBox();
            this.buttonSaveState = new System.Windows.Forms.Button();
            this.buttonRestoreState = new System.Windows.Forms.Button();
            this.buttonColumns = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.checkBoxGroups = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.olvFiles = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCreated = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnModified = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFileType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAttributes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuOfCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appropriateToTheClickedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whichOnlyAppearsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whenYouClickOnColumn0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.comboBoxNagLevel = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.olvFiles)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(209, 477);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(50, 13);
            this.label37.TabIndex = 31;
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
            this.comboBoxHotItemStyle.Location = new System.Drawing.Point(260, 472);
            this.comboBoxHotItemStyle.Name = "comboBoxHotItemStyle";
            this.comboBoxHotItemStyle.Size = new System.Drawing.Size(86, 21);
            this.comboBoxHotItemStyle.TabIndex = 32;
            this.comboBoxHotItemStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxHotItemStyle_SelectedIndexChanged);
            // 
            // checkBoxTooltips
            // 
            this.checkBoxTooltips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxTooltips.Location = new System.Drawing.Point(97, 474);
            this.checkBoxTooltips.Name = "checkBoxTooltips";
            this.checkBoxTooltips.Size = new System.Drawing.Size(65, 19);
            this.checkBoxTooltips.TabIndex = 30;
            this.checkBoxTooltips.Text = "Tooltips";
            this.checkBoxTooltips.UseVisualStyleBackColor = true;
            this.checkBoxTooltips.CheckedChanged += new System.EventHandler(this.checkBoxTooltips_CheckedChanged);
            // 
            // buttonSaveState
            // 
            this.buttonSaveState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveState.Location = new System.Drawing.Point(545, 471);
            this.buttonSaveState.Name = "buttonSaveState";
            this.buttonSaveState.Size = new System.Drawing.Size(87, 23);
            this.buttonSaveState.TabIndex = 26;
            this.buttonSaveState.Text = "Save State";
            this.buttonSaveState.UseVisualStyleBackColor = true;
            this.buttonSaveState.Click += new System.EventHandler(this.buttonSaveState_Click);
            // 
            // buttonRestoreState
            // 
            this.buttonRestoreState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestoreState.Enabled = false;
            this.buttonRestoreState.Location = new System.Drawing.Point(638, 471);
            this.buttonRestoreState.Name = "buttonRestoreState";
            this.buttonRestoreState.Size = new System.Drawing.Size(83, 23);
            this.buttonRestoreState.TabIndex = 27;
            this.buttonRestoreState.Text = "Restore State";
            this.buttonRestoreState.UseVisualStyleBackColor = true;
            this.buttonRestoreState.Click += new System.EventHandler(this.buttonRestoreState_Click);
            // 
            // buttonColumns
            // 
            this.buttonColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonColumns.Location = new System.Drawing.Point(727, 471);
            this.buttonColumns.Name = "buttonColumns";
            this.buttonColumns.Size = new System.Drawing.Size(75, 23);
            this.buttonColumns.TabIndex = 28;
            this.buttonColumns.Text = "&Columns...";
            this.buttonColumns.UseVisualStyleBackColor = true;
            this.buttonColumns.Click += new System.EventHandler(this.buttonColumns_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUp.Location = new System.Drawing.Point(577, 52);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(75, 23);
            this.buttonUp.TabIndex = 20;
            this.buttonUp.Text = "&Up";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(496, 52);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 19;
            this.buttonGo.Text = "&Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolderPath.Location = new System.Drawing.Point(53, 54);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.Size = new System.Drawing.Size(437, 20);
            this.textBoxFolderPath.TabIndex = 18;
            this.textBoxFolderPath.TextChanged += new System.EventHandler(this.textBoxFolderPath_TextChanged);
            this.textBoxFolderPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFolderPath_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "&Folder:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(352, 477);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "View:";
            // 
            // comboBoxView
            // 
            this.comboBoxView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxView.FormattingEnabled = true;
            this.comboBoxView.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBoxView.Location = new System.Drawing.Point(386, 472);
            this.comboBoxView.Name = "comboBoxView";
            this.comboBoxView.Size = new System.Drawing.Size(86, 21);
            this.comboBoxView.TabIndex = 25;
            this.comboBoxView.SelectedIndexChanged += new System.EventHandler(this.comboBoxView_SelectedIndexChanged);
            // 
            // checkBoxGroups
            // 
            this.checkBoxGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxGroups.Location = new System.Drawing.Point(3, 471);
            this.checkBoxGroups.Name = "checkBoxGroups";
            this.checkBoxGroups.Size = new System.Drawing.Size(64, 24);
            this.checkBoxGroups.TabIndex = 21;
            this.checkBoxGroups.Text = "&Groups";
            this.checkBoxGroups.UseVisualStyleBackColor = true;
            this.checkBoxGroups.CheckedChanged += new System.EventHandler(this.checkBoxGroups_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(799, 46);
            this.label5.TabIndex = 22;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // olvFiles
            // 
            this.olvFiles.AllColumns.Add(this.olvColumnName);
            this.olvFiles.AllColumns.Add(this.olvColumnCreated);
            this.olvFiles.AllColumns.Add(this.olvColumnModified);
            this.olvFiles.AllColumns.Add(this.olvColumnSize);
            this.olvFiles.AllColumns.Add(this.olvColumnFileType);
            this.olvFiles.AllColumns.Add(this.olvColumnAttributes);
            this.olvFiles.AllowColumnReorder = true;
            this.olvFiles.AllowDrop = true;
            this.olvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvFiles.CellEditUseWholeCell = false;
            this.olvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnCreated,
            this.olvColumnModified,
            this.olvColumnSize,
            this.olvColumnFileType,
            this.olvColumnAttributes});
            this.olvFiles.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvFiles.EmptyListMsg = "This folder is completely empty!";
            this.olvFiles.EmptyListMsgFont = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvFiles.HideSelection = false;
            this.olvFiles.Location = new System.Drawing.Point(3, 80);
            this.olvFiles.Name = "olvFiles";
            this.olvFiles.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.olvFiles.ShowCommandMenuOnRightClick = true;
            this.olvFiles.ShowGroups = false;
            this.olvFiles.ShowItemToolTips = true;
            this.olvFiles.Size = new System.Drawing.Size(799, 385);
            this.olvFiles.TabIndex = 29;
            this.olvFiles.UseCompatibleStateImageBehavior = false;
            this.olvFiles.UseFilterIndicator = true;
            this.olvFiles.UseFiltering = true;
            this.olvFiles.View = System.Windows.Forms.View.Details;
            this.olvFiles.ItemActivate += new System.EventHandler(this.olvFiles_ItemActivate);
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumnName.IsTileViewColumn = true;
            this.olvColumnName.Text = "Name";
            this.olvColumnName.UseInitialLetterForGroup = true;
            this.olvColumnName.Width = 180;
            // 
            // olvColumnCreated
            // 
            this.olvColumnCreated.AspectName = "CreationTime";
            this.olvColumnCreated.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumnCreated.DisplayIndex = 4;
            this.olvColumnCreated.Text = "Created";
            this.olvColumnCreated.Width = 131;
            // 
            // olvColumnModified
            // 
            this.olvColumnModified.AspectName = "LastWriteTime";
            this.olvColumnModified.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumnModified.DisplayIndex = 1;
            this.olvColumnModified.IsTileViewColumn = true;
            this.olvColumnModified.Text = "Modified";
            this.olvColumnModified.Width = 127;
            // 
            // olvColumnSize
            // 
            this.olvColumnSize.AspectName = "Extension";
            this.olvColumnSize.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumnSize.DisplayIndex = 2;
            this.olvColumnSize.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnSize.Text = "Size";
            this.olvColumnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnSize.Width = 80;
            // 
            // olvColumnFileType
            // 
            this.olvColumnFileType.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumnFileType.DisplayIndex = 3;
            this.olvColumnFileType.IsTileViewColumn = true;
            this.olvColumnFileType.Text = "File Type";
            this.olvColumnFileType.Width = 148;
            // 
            // olvColumnAttributes
            // 
            this.olvColumnAttributes.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumnAttributes.FillsFreeSpace = true;
            this.olvColumnAttributes.IsEditable = false;
            this.olvColumnAttributes.MinimumWidth = 20;
            this.olvColumnAttributes.Text = "Attributes";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOfCommandsToolStripMenuItem,
            this.appropriateToTheClickedFileToolStripMenuItem,
            this.whichOnlyAppearsToolStripMenuItem,
            this.whenYouClickOnColumn0ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(231, 92);
            // 
            // menuOfCommandsToolStripMenuItem
            // 
            this.menuOfCommandsToolStripMenuItem.Name = "menuOfCommandsToolStripMenuItem";
            this.menuOfCommandsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.menuOfCommandsToolStripMenuItem.Text = "Menu of commands";
            // 
            // appropriateToTheClickedFileToolStripMenuItem
            // 
            this.appropriateToTheClickedFileToolStripMenuItem.Name = "appropriateToTheClickedFileToolStripMenuItem";
            this.appropriateToTheClickedFileToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.appropriateToTheClickedFileToolStripMenuItem.Text = "Appropriate to the clicked file";
            // 
            // whichOnlyAppearsToolStripMenuItem
            // 
            this.whichOnlyAppearsToolStripMenuItem.Name = "whichOnlyAppearsToolStripMenuItem";
            this.whichOnlyAppearsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.whichOnlyAppearsToolStripMenuItem.Text = "Which only appears";
            // 
            // whenYouClickOnColumn0ToolStripMenuItem
            // 
            this.whenYouClickOnColumn0ToolStripMenuItem.Name = "whenYouClickOnColumn0ToolStripMenuItem";
            this.whenYouClickOnColumn0ToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.whenYouClickOnColumn0ToolStripMenuItem.Text = "When you click on column 0";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "compass");
            this.imageList1.Images.SetKeyName(1, "down");
            this.imageList1.Images.SetKeyName(2, "user");
            this.imageList1.Images.SetKeyName(3, "find");
            this.imageList1.Images.SetKeyName(4, "folder");
            this.imageList1.Images.SetKeyName(5, "movie");
            this.imageList1.Images.SetKeyName(6, "music");
            this.imageList1.Images.SetKeyName(7, "no");
            this.imageList1.Images.SetKeyName(8, "readonly");
            this.imageList1.Images.SetKeyName(9, "public");
            this.imageList1.Images.SetKeyName(10, "recycle");
            this.imageList1.Images.SetKeyName(11, "spanner");
            this.imageList1.Images.SetKeyName(12, "star");
            this.imageList1.Images.SetKeyName(13, "tick");
            this.imageList1.Images.SetKeyName(14, "archive");
            this.imageList1.Images.SetKeyName(15, "system");
            this.imageList1.Images.SetKeyName(16, "hidden");
            this.imageList1.Images.SetKeyName(17, "temporary");
            // 
            // comboBoxNagLevel
            // 
            this.comboBoxNagLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNagLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNagLevel.FormattingEnabled = true;
            this.comboBoxNagLevel.Items.AddRange(new object[] {
            "Slight",
            "Expired",
            "Extreme"});
            this.comboBoxNagLevel.Location = new System.Drawing.Point(718, 53);
            this.comboBoxNagLevel.Name = "comboBoxNagLevel";
            this.comboBoxNagLevel.Size = new System.Drawing.Size(83, 21);
            this.comboBoxNagLevel.TabIndex = 36;
            this.comboBoxNagLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxNagLevel_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(664, 57);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(55, 13);
            this.label36.TabIndex = 35;
            this.label36.Text = "Nag level:";
            // 
            // TabFileExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxNagLevel);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.comboBoxHotItemStyle);
            this.Controls.Add(this.checkBoxTooltips);
            this.Controls.Add(this.buttonSaveState);
            this.Controls.Add(this.buttonRestoreState);
            this.Controls.Add(this.buttonColumns);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.textBoxFolderPath);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBoxView);
            this.Controls.Add(this.checkBoxGroups);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.olvFiles);
            this.Name = "TabFileExplorer";
            this.Size = new System.Drawing.Size(804, 499);
            ((System.ComponentModel.ISupportInitialize)(this.olvFiles)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox comboBoxHotItemStyle;
        private System.Windows.Forms.CheckBox checkBoxTooltips;
        private System.Windows.Forms.Button buttonSaveState;
        private System.Windows.Forms.Button buttonRestoreState;
        private System.Windows.Forms.Button buttonColumns;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxView;
        private System.Windows.Forms.CheckBox checkBoxGroups;
        private System.Windows.Forms.Label label5;
        private BrightIdeasSoftware.ObjectListView olvFiles;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnCreated;
        private BrightIdeasSoftware.OLVColumn olvColumnModified;
        private BrightIdeasSoftware.OLVColumn olvColumnSize;
        private BrightIdeasSoftware.OLVColumn olvColumnFileType;
        private BrightIdeasSoftware.OLVColumn olvColumnAttributes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menuOfCommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appropriateToTheClickedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whichOnlyAppearsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whenYouClickOnColumn0ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox comboBoxNagLevel;
        private System.Windows.Forms.Label label36;

    }
}
