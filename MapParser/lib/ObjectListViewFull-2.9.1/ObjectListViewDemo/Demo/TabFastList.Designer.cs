namespace ObjectListViewDemo
{
    partial class TabFastList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabFastList));
            this.label24 = new System.Windows.Forms.Label();
            this.checkBoxCheckboxes = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.textBoxFilterFast = new System.Windows.Forms.TextBox();
            this.checkBoxGroups = new System.Windows.Forms.CheckBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.comboBoxEditable = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.buttonDisable = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.olvFast = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn18 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn19 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn26 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn27 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn28 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn29 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn31 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn32 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn33 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupImageList = new System.Windows.Forms.ImageList(this.components);
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvFast)).BeginInit();
            this.SuspendLayout();
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(211, 478);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 24;
            this.label24.Text = "Editable:";
            // 
            // checkBoxCheckboxes
            // 
            this.checkBoxCheckboxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCheckboxes.Checked = true;
            this.checkBoxCheckboxes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckboxes.Location = new System.Drawing.Point(73, 474);
            this.checkBoxCheckboxes.Name = "checkBoxCheckboxes";
            this.checkBoxCheckboxes.Size = new System.Drawing.Size(87, 21);
            this.checkBoxCheckboxes.TabIndex = 35;
            this.checkBoxCheckboxes.Text = "Check&boxes";
            this.checkBoxCheckboxes.UseVisualStyleBackColor = true;
            this.checkBoxCheckboxes.CheckedChanged += new System.EventHandler(this.checkBoxCheckboxes_CheckedChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.comboBoxFilterType);
            this.groupBox11.Controls.Add(this.textBoxFilterFast);
            this.groupBox11.Location = new System.Drawing.Point(588, 4);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(214, 44);
            this.groupBox11.TabIndex = 34;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Filter";
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "Any text",
            "Prefix",
            "Regex"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(114, 18);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(94, 21);
            this.comboBoxFilterType.TabIndex = 1;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // textBoxFilterFast
            // 
            this.textBoxFilterFast.Location = new System.Drawing.Point(7, 18);
            this.textBoxFilterFast.Name = "textBoxFilterFast";
            this.textBoxFilterFast.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilterFast.TabIndex = 0;
            this.textBoxFilterFast.TextChanged += new System.EventHandler(this.textBoxFilterFast_TextChanged);
            // 
            // checkBoxGroups
            // 
            this.checkBoxGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxGroups.Location = new System.Drawing.Point(2, 474);
            this.checkBoxGroups.Name = "checkBoxGroups";
            this.checkBoxGroups.Size = new System.Drawing.Size(60, 21);
            this.checkBoxGroups.TabIndex = 33;
            this.checkBoxGroups.Text = "&Groups";
            this.checkBoxGroups.UseVisualStyleBackColor = true;
            this.checkBoxGroups.CheckedChanged += new System.EventHandler(this.checkBoxGroups_CheckedChanged);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(498, 472);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(90, 23);
            this.buttonCopy.TabIndex = 32;
            this.buttonCopy.Text = "Copy Chec&ked";
            this.toolTip1.SetToolTip(this.buttonCopy, "Copy the checked rows to the clipboard");
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(665, 472);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(65, 23);
            this.buttonRemove.TabIndex = 29;
            this.buttonRemove.Text = "Remove";
            this.toolTip1.SetToolTip(this.buttonRemove, "Remove selected people. Ctrl-Click to clear the list");
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Location = new System.Drawing.Point(3, 4);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(579, 45);
            this.label26.TabIndex = 31;
            this.label26.Text = resources.GetString("label26.Text");
            // 
            // comboBoxEditable
            // 
            this.comboBoxEditable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEditable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEditable.FormattingEnabled = true;
            this.comboBoxEditable.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBoxEditable.Location = new System.Drawing.Point(260, 473);
            this.comboBoxEditable.Name = "comboBoxEditable";
            this.comboBoxEditable.Size = new System.Drawing.Size(83, 21);
            this.comboBoxEditable.TabIndex = 25;
            this.comboBoxEditable.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditable_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(357, 478);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(33, 13);
            this.label25.TabIndex = 26;
            this.label25.Text = "View:";
            // 
            // comboBoxView
            // 
            this.comboBoxView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxView.FormattingEnabled = true;
            this.comboBoxView.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "List",
            "Tile",
            "Details"});
            this.comboBoxView.Location = new System.Drawing.Point(392, 473);
            this.comboBoxView.Name = "comboBoxView";
            this.comboBoxView.Size = new System.Drawing.Size(83, 21);
            this.comboBoxView.TabIndex = 27;
            this.comboBoxView.SelectedIndexChanged += new System.EventHandler(this.comboBoxView_SelectedIndexChanged);
            // 
            // buttonDisable
            // 
            this.buttonDisable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisable.Location = new System.Drawing.Point(594, 472);
            this.buttonDisable.Name = "buttonDisable";
            this.buttonDisable.Size = new System.Drawing.Size(65, 23);
            this.buttonDisable.TabIndex = 28;
            this.buttonDisable.Text = "&Disable";
            this.toolTip1.SetToolTip(this.buttonDisable, "Disable the selected rows. Ctrl-click to re-enable all rows");
            this.buttonDisable.UseVisualStyleBackColor = true;
            this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(736, 472);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(65, 23);
            this.buttonAdd.TabIndex = 30;
            this.buttonAdd.Text = "&Add 1000";
            this.toolTip1.SetToolTip(this.buttonAdd, "Add 1000 people to the list");
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // olvFast
            // 
            this.olvFast.AllColumns.Add(this.olvColumn18);
            this.olvFast.AllColumns.Add(this.olvColumn19);
            this.olvFast.AllColumns.Add(this.olvColumn26);
            this.olvFast.AllColumns.Add(this.olvColumn27);
            this.olvFast.AllColumns.Add(this.olvColumn28);
            this.olvFast.AllColumns.Add(this.olvColumn29);
            this.olvFast.AllColumns.Add(this.olvColumn31);
            this.olvFast.AllColumns.Add(this.olvColumn32);
            this.olvFast.AllColumns.Add(this.olvColumn33);
            this.olvFast.AllowColumnReorder = true;
            this.olvFast.AllowDrop = true;
            this.olvFast.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.olvFast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvFast.BackgroundImageTiled = true;
            this.olvFast.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.olvFast.CellEditEnterChangesRows = true;
            this.olvFast.CellEditTabChangesRows = true;
            this.olvFast.CellEditUseWholeCell = false;
            this.olvFast.CheckBoxes = true;
            this.olvFast.CheckedAspectName = "";
            this.olvFast.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn18,
            this.olvColumn19,
            this.olvColumn26,
            this.olvColumn27,
            this.olvColumn28,
            this.olvColumn29,
            this.olvColumn31,
            this.olvColumn32,
            this.olvColumn33});
            this.olvFast.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvFast.EmptyListMsg = "This fast list is empty";
            this.olvFast.FullRowSelect = true;
            this.olvFast.GroupImageList = this.groupImageList;
            this.olvFast.HideSelection = false;
            this.olvFast.LargeImageList = this.imageListLarge;
            this.olvFast.Location = new System.Drawing.Point(3, 55);
            this.olvFast.Name = "olvFast";
            this.olvFast.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.olvFast.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.olvFast.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.olvFast.ShowCommandMenuOnRightClick = true;
            this.olvFast.ShowGroups = false;
            this.olvFast.ShowImagesOnSubItems = true;
            this.olvFast.ShowItemToolTips = true;
            this.olvFast.Size = new System.Drawing.Size(799, 414);
            this.olvFast.SmallImageList = this.imageListSmall;
            this.olvFast.SpaceBetweenGroups = 20;
            this.olvFast.TabIndex = 22;
            this.olvFast.TintSortColumn = true;
            this.olvFast.TriStateCheckBoxes = true;
            this.olvFast.UseAlternatingBackColors = true;
            this.olvFast.UseCompatibleStateImageBehavior = false;
            this.olvFast.UseFilterIndicator = true;
            this.olvFast.UseFiltering = true;
            this.olvFast.UseHyperlinks = true;
            this.olvFast.View = System.Windows.Forms.View.Details;
            this.olvFast.VirtualMode = true;
            // 
            // olvColumn18
            // 
            this.olvColumn18.AspectName = "Name";
            this.olvColumn18.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn18.Text = "Person";
            this.olvColumn18.UseInitialLetterForGroup = true;
            this.olvColumn18.Width = 132;
            // 
            // olvColumn19
            // 
            this.olvColumn19.AspectName = "Occupation";
            this.olvColumn19.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn19.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn19.Hyperlink = true;
            this.olvColumn19.IsTileViewColumn = true;
            this.olvColumn19.Text = "Occupation";
            this.olvColumn19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn19.Width = 107;
            // 
            // olvColumn26
            // 
            this.olvColumn26.AspectName = "CulinaryRating";
            this.olvColumn26.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn26.GroupWithItemCountFormat = "{0} ({1} candidates)";
            this.olvColumn26.GroupWithItemCountSingularFormat = "{0} (only {1} candidate)";
            this.olvColumn26.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn26.Text = "Cooking skill";
            this.olvColumn26.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn26.Width = 90;
            // 
            // olvColumn27
            // 
            this.olvColumn27.AspectName = "YearOfBirth";
            this.olvColumn27.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn27.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn27.Text = "Year Of Birth";
            this.olvColumn27.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn27.Width = 80;
            // 
            // olvColumn28
            // 
            this.olvColumn28.AspectName = "BirthDate";
            this.olvColumn28.AspectToStringFormat = "{0:D}";
            this.olvColumn28.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn28.GroupWithItemCountFormat = "{0} has {1} birthdays";
            this.olvColumn28.GroupWithItemCountSingularFormat = "{0} has only {1} birthday";
            this.olvColumn28.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn28.IsTileViewColumn = true;
            this.olvColumn28.Text = "Birthday";
            this.olvColumn28.Width = 111;
            // 
            // olvColumn29
            // 
            this.olvColumn29.AspectName = "GetRate";
            this.olvColumn29.AspectToStringFormat = "{0:C}";
            this.olvColumn29.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn29.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn29.IsTileViewColumn = true;
            this.olvColumn29.Text = "Hourly Rate";
            this.olvColumn29.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn29.Width = 71;
            // 
            // olvColumn31
            // 
            this.olvColumn31.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn31.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn31.IsEditable = false;
            this.olvColumn31.Text = "Salary";
            this.olvColumn31.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn31.Width = 55;
            // 
            // olvColumn32
            // 
            this.olvColumn32.AspectToStringFormat = "{0:#,##0}";
            this.olvColumn32.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn32.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.olvColumn32.IsEditable = false;
            this.olvColumn32.Text = "Days Since Birth";
            this.olvColumn32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn32.Width = 98;
            // 
            // olvColumn33
            // 
            this.olvColumn33.AspectName = "CanTellJokes";
            this.olvColumn33.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvColumn33.CheckBoxes = true;
            this.olvColumn33.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn33.Text = "Tells Jokes?";
            this.olvColumn33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn33.Width = 74;
            // 
            // groupImageList
            // 
            this.groupImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("groupImageList.ImageStream")));
            this.groupImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.groupImageList.Images.SetKeyName(0, "beef");
            this.groupImageList.Images.SetKeyName(1, "chef");
            this.groupImageList.Images.SetKeyName(2, "toast");
            this.groupImageList.Images.SetKeyName(3, "hamburger");
            this.groupImageList.Images.SetKeyName(4, "not");
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "user");
            this.imageListLarge.Images.SetKeyName(1, "compass");
            this.imageListLarge.Images.SetKeyName(2, "down");
            this.imageListLarge.Images.SetKeyName(3, "find");
            this.imageListLarge.Images.SetKeyName(4, "folder");
            this.imageListLarge.Images.SetKeyName(5, "movie");
            this.imageListLarge.Images.SetKeyName(6, "music");
            this.imageListLarge.Images.SetKeyName(7, "no");
            this.imageListLarge.Images.SetKeyName(8, "readonly");
            this.imageListLarge.Images.SetKeyName(9, "public");
            this.imageListLarge.Images.SetKeyName(10, "recycle");
            this.imageListLarge.Images.SetKeyName(11, "spanner");
            this.imageListLarge.Images.SetKeyName(12, "star");
            this.imageListLarge.Images.SetKeyName(13, "tick");
            this.imageListLarge.Images.SetKeyName(14, "archive");
            this.imageListLarge.Images.SetKeyName(15, "system");
            this.imageListLarge.Images.SetKeyName(16, "hidden");
            this.imageListLarge.Images.SetKeyName(17, "temporary");
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
            // TabFastList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label24);
            this.Controls.Add(this.checkBoxCheckboxes);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.checkBoxGroups);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.comboBoxEditable);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.comboBoxView);
            this.Controls.Add(this.buttonDisable);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.olvFast);
            this.Name = "TabFastList";
            this.Size = new System.Drawing.Size(804, 499);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvFast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.CheckBox checkBoxCheckboxes;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.TextBox textBoxFilterFast;
        private System.Windows.Forms.CheckBox checkBoxGroups;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox comboBoxEditable;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox comboBoxView;
        private System.Windows.Forms.Button buttonDisable;
        private System.Windows.Forms.Button buttonAdd;
        private BrightIdeasSoftware.FastObjectListView olvFast;
        private BrightIdeasSoftware.OLVColumn olvColumn18;
        private BrightIdeasSoftware.OLVColumn olvColumn19;
        private BrightIdeasSoftware.OLVColumn olvColumn26;
        private BrightIdeasSoftware.OLVColumn olvColumn27;
        private BrightIdeasSoftware.OLVColumn olvColumn28;
        private BrightIdeasSoftware.OLVColumn olvColumn29;
        private BrightIdeasSoftware.OLVColumn olvColumn31;
        private BrightIdeasSoftware.OLVColumn olvColumn32;
        private BrightIdeasSoftware.OLVColumn olvColumn33;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.ImageList groupImageList;
    }
}
