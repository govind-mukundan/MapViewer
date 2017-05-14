namespace ObjectListViewDemo
{
    partial class TabSimpleExample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabSimpleExample));
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBoxFilterSimple = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.checkBoxGroups = new System.Windows.Forms.CheckBox();
            this.checkBoxHotItem = new System.Windows.Forms.CheckBox();
            this.comboBoxEditable = new System.Windows.Forms.ComboBox();
            this.checkBoxItemCount = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRebuild = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonListAnimation = new System.Windows.Forms.Button();
            this.buttonRowAnimation = new System.Windows.Forms.Button();
            this.buttonCellAnimation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.olvSimple = new BrightIdeasSoftware.ObjectListView();
            this.columnHeader11 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnHeader12 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSimpleCookingColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnHeader14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnHeader15 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnHeaderSalaryRate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn34 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvSimple)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.textBoxFilterSimple);
            this.groupBox9.Location = new System.Drawing.Point(683, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(117, 44);
            this.groupBox9.TabIndex = 35;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Filter";
            // 
            // textBoxFilterSimple
            // 
            this.textBoxFilterSimple.Location = new System.Drawing.Point(7, 20);
            this.textBoxFilterSimple.Name = "textBoxFilterSimple";
            this.textBoxFilterSimple.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilterSimple.TabIndex = 0;
            this.textBoxFilterSimple.TextChanged += new System.EventHandler(this.textBoxFilterSimple_TextChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.label21);
            this.groupBox8.Controls.Add(this.checkBoxGroups);
            this.groupBox8.Controls.Add(this.checkBoxHotItem);
            this.groupBox8.Controls.Add(this.comboBoxEditable);
            this.groupBox8.Controls.Add(this.checkBoxItemCount);
            this.groupBox8.Location = new System.Drawing.Point(1, 445);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(334, 48);
            this.groupBox8.TabIndex = 34;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Settings";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(223, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(28, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "&Edit:";
            // 
            // checkBoxGroups
            // 
            this.checkBoxGroups.Location = new System.Drawing.Point(6, 19);
            this.checkBoxGroups.Name = "checkBoxGroups";
            this.checkBoxGroups.Size = new System.Drawing.Size(62, 24);
            this.checkBoxGroups.TabIndex = 1;
            this.checkBoxGroups.Text = "&Groups";
            this.checkBoxGroups.UseVisualStyleBackColor = true;
            this.checkBoxGroups.CheckedChanged += new System.EventHandler(this.checkBoxGroups_CheckedChanged);
            // 
            // checkBoxHotItem
            // 
            this.checkBoxHotItem.Checked = true;
            this.checkBoxHotItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHotItem.Location = new System.Drawing.Point(153, 20);
            this.checkBoxHotItem.Name = "checkBoxHotItem";
            this.checkBoxHotItem.Size = new System.Drawing.Size(66, 24);
            this.checkBoxHotItem.TabIndex = 10;
            this.checkBoxHotItem.Text = "Hot &Item";
            this.checkBoxHotItem.UseVisualStyleBackColor = true;
            this.checkBoxHotItem.CheckedChanged += new System.EventHandler(this.checkBoxHotItem_CheckedChanged);
            // 
            // comboBoxEditable
            // 
            this.comboBoxEditable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEditable.FormattingEnabled = true;
            this.comboBoxEditable.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBoxEditable.Location = new System.Drawing.Point(253, 21);
            this.comboBoxEditable.Name = "comboBoxEditable";
            this.comboBoxEditable.Size = new System.Drawing.Size(80, 21);
            this.comboBoxEditable.TabIndex = 4;
            this.comboBoxEditable.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditable_SelectedIndexChanged);
            // 
            // checkBoxItemCount
            // 
            this.checkBoxItemCount.Location = new System.Drawing.Point(68, 20);
            this.checkBoxItemCount.Name = "checkBoxItemCount";
            this.checkBoxItemCount.Size = new System.Drawing.Size(83, 24);
            this.checkBoxItemCount.TabIndex = 2;
            this.checkBoxItemCount.Text = "Item &Count";
            this.checkBoxItemCount.UseVisualStyleBackColor = true;
            this.checkBoxItemCount.CheckedChanged += new System.EventHandler(this.checkBoxItemCount_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.buttonAdd);
            this.groupBox7.Controls.Add(this.buttonRebuild);
            this.groupBox7.Controls.Add(this.buttonCopy);
            this.groupBox7.Controls.Add(this.buttonRemove);
            this.groupBox7.Location = new System.Drawing.Point(541, 445);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(254, 48);
            this.groupBox7.TabIndex = 33;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Commands";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(8, 19);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(56, 23);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "&Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRebuild
            // 
            this.buttonRebuild.Location = new System.Drawing.Point(189, 19);
            this.buttonRebuild.Name = "buttonRebuild";
            this.buttonRebuild.Size = new System.Drawing.Size(56, 23);
            this.buttonRebuild.TabIndex = 8;
            this.buttonRebuild.Text = "&Rebuild";
            this.buttonRebuild.UseVisualStyleBackColor = true;
            this.buttonRebuild.Click += new System.EventHandler(this.buttonRebuild_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(128, 19);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(56, 23);
            this.buttonCopy.TabIndex = 7;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(68, 19);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(56, 23);
            this.buttonRemove.TabIndex = 6;
            this.buttonRemove.Text = "Re&move";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.buttonListAnimation);
            this.groupBox6.Controls.Add(this.buttonRowAnimation);
            this.groupBox6.Controls.Add(this.buttonCellAnimation);
            this.groupBox6.Location = new System.Drawing.Point(341, 445);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(194, 48);
            this.groupBox6.TabIndex = 32;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Animations";
            // 
            // buttonListAnimation
            // 
            this.buttonListAnimation.Location = new System.Drawing.Point(6, 19);
            this.buttonListAnimation.Name = "buttonListAnimation";
            this.buttonListAnimation.Size = new System.Drawing.Size(56, 23);
            this.buttonListAnimation.TabIndex = 11;
            this.buttonListAnimation.Text = "ListView";
            this.buttonListAnimation.UseVisualStyleBackColor = true;
            this.buttonListAnimation.Click += new System.EventHandler(this.buttonListAnimation_Click);
            // 
            // buttonRowAnimation
            // 
            this.buttonRowAnimation.Location = new System.Drawing.Point(68, 19);
            this.buttonRowAnimation.Name = "buttonRowAnimation";
            this.buttonRowAnimation.Size = new System.Drawing.Size(56, 23);
            this.buttonRowAnimation.TabIndex = 12;
            this.buttonRowAnimation.Text = "Row";
            this.buttonRowAnimation.UseVisualStyleBackColor = true;
            this.buttonRowAnimation.Click += new System.EventHandler(this.buttonRowAnimation_Click);
            // 
            // buttonCellAnimation
            // 
            this.buttonCellAnimation.Location = new System.Drawing.Point(130, 19);
            this.buttonCellAnimation.Name = "buttonCellAnimation";
            this.buttonCellAnimation.Size = new System.Drawing.Size(56, 23);
            this.buttonCellAnimation.TabIndex = 13;
            this.buttonCellAnimation.Text = "Cell";
            this.buttonCellAnimation.UseVisualStyleBackColor = true;
            this.buttonCellAnimation.Click += new System.EventHandler(this.buttonCellAnimation_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(675, 46);
            this.label1.TabIndex = 31;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // olvSimple
            // 
            this.olvSimple.AllColumns.Add(this.columnHeader11);
            this.olvSimple.AllColumns.Add(this.columnHeader12);
            this.olvSimple.AllColumns.Add(this.olvSimpleCookingColumn);
            this.olvSimple.AllColumns.Add(this.columnHeader14);
            this.olvSimple.AllColumns.Add(this.columnHeader15);
            this.olvSimple.AllColumns.Add(this.columnHeaderSalaryRate);
            this.olvSimple.AllColumns.Add(this.olvColumn34);
            this.olvSimple.AllowColumnReorder = true;
            this.olvSimple.AllowDrop = true;
            this.olvSimple.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvSimple.CellEditUseWholeCell = false;
            this.olvSimple.CheckBoxes = true;
            this.olvSimple.CheckedAspectName = "";
            this.olvSimple.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.olvSimpleCookingColumn,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeaderSalaryRate,
            this.olvColumn34});
            this.olvSimple.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvSimple.FullRowSelect = true;
            this.olvSimple.HeaderWordWrap = true;
            this.olvSimple.HideSelection = false;
            this.olvSimple.IncludeColumnHeadersInCopy = true;
            this.olvSimple.Location = new System.Drawing.Point(1, 56);
            this.olvSimple.Name = "olvSimple";
            this.olvSimple.OverlayImage.Image = global::ObjectListViewDemo.Properties.Resources.limeleaf;
            this.olvSimple.OverlayText.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.olvSimple.OverlayText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.olvSimple.OverlayText.BorderWidth = 2F;
            this.olvSimple.OverlayText.Rotation = -20;
            this.olvSimple.OverlayText.Text = "";
            this.olvSimple.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.olvSimple.ShowCommandMenuOnRightClick = true;
            this.olvSimple.ShowGroups = false;
            this.olvSimple.ShowHeaderInAllViews = false;
            this.olvSimple.ShowItemToolTips = true;
            this.olvSimple.Size = new System.Drawing.Size(799, 383);
            this.olvSimple.SortGroupItemsByPrimaryColumn = false;
            this.olvSimple.TabIndex = 30;
            this.olvSimple.TriStateCheckBoxes = true;
            this.olvSimple.UseAlternatingBackColors = true;
            this.olvSimple.UseCellFormatEvents = true;
            this.olvSimple.UseCompatibleStateImageBehavior = false;
            this.olvSimple.UseFilterIndicator = true;
            this.olvSimple.UseFiltering = true;
            this.olvSimple.UseHotItem = true;
            this.olvSimple.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.AspectName = "Name";
            this.columnHeader11.HeaderCheckBox = true;
            this.columnHeader11.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.MaximumWidth = 200;
            this.columnHeader11.MinimumWidth = 100;
            this.columnHeader11.Text = "Person";
            this.columnHeader11.ToolTipText = "This is a long tooltip text that should appear when the mouse is over this column" +
    " header but contains absolutely no useful information :)";
            this.columnHeader11.UseInitialLetterForGroup = true;
            this.columnHeader11.Width = 140;
            // 
            // columnHeader12
            // 
            this.columnHeader12.AspectName = "Occupation";
            this.columnHeader12.Hyperlink = true;
            this.columnHeader12.MaximumWidth = 180;
            this.columnHeader12.MinimumWidth = 50;
            this.columnHeader12.Text = "Occupation";
            this.columnHeader12.Width = 112;
            // 
            // olvSimpleCookingColumn
            // 
            this.olvSimpleCookingColumn.AspectName = "CulinaryRating";
            this.olvSimpleCookingColumn.HeaderForeColor = System.Drawing.Color.Green;
            this.olvSimpleCookingColumn.Text = "Cooking Skill";
            this.olvSimpleCookingColumn.Width = 74;
            // 
            // columnHeader14
            // 
            this.columnHeader14.AspectName = "YearOfBirth";
            this.columnHeader14.HeaderForeColor = System.Drawing.Color.Black;
            this.columnHeader14.MaximumWidth = 81;
            this.columnHeader14.MinimumWidth = 81;
            this.columnHeader14.Text = "Year of birth";
            this.columnHeader14.Width = 81;
            // 
            // columnHeader15
            // 
            this.columnHeader15.AspectName = "BirthDate";
            this.columnHeader15.AspectToStringFormat = "{0:d}";
            this.columnHeader15.Text = "Birthday";
            this.columnHeader15.Width = 121;
            // 
            // columnHeaderSalaryRate
            // 
            this.columnHeaderSalaryRate.AspectName = "GetRate";
            this.columnHeaderSalaryRate.AspectToStringFormat = "{0:C}";
            this.columnHeaderSalaryRate.IsEditable = false;
            this.columnHeaderSalaryRate.Text = "Hourly Rate";
            this.columnHeaderSalaryRate.Width = 93;
            // 
            // olvColumn34
            // 
            this.olvColumn34.AspectName = "Comments";
            this.olvColumn34.FillsFreeSpace = true;
            this.olvColumn34.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn34.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumn34.IsTileViewColumn = true;
            this.olvColumn34.MinimumWidth = 30;
            this.olvColumn34.Text = "Comments";
            this.olvColumn34.ToolTipText = "This is the tool tip for the Comments column. This is configured through the Tool" +
    "TipText property.";
            this.olvColumn34.UseInitialLetterForGroup = true;
            // 
            // TabSimpleExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.olvSimple);
            this.Name = "TabSimpleExample";
            this.Size = new System.Drawing.Size(800, 499);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvSimple)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox textBoxFilterSimple;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox checkBoxGroups;
        private System.Windows.Forms.CheckBox checkBoxHotItem;
        private System.Windows.Forms.ComboBox comboBoxEditable;
        private System.Windows.Forms.CheckBox checkBoxItemCount;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRebuild;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonListAnimation;
        private System.Windows.Forms.Button buttonRowAnimation;
        private System.Windows.Forms.Button buttonCellAnimation;
        private System.Windows.Forms.Label label1;
        private BrightIdeasSoftware.ObjectListView olvSimple;
        private BrightIdeasSoftware.OLVColumn columnHeader11;
        private BrightIdeasSoftware.OLVColumn columnHeader12;
        private BrightIdeasSoftware.OLVColumn olvSimpleCookingColumn;
        private BrightIdeasSoftware.OLVColumn columnHeader14;
        private BrightIdeasSoftware.OLVColumn columnHeader15;
        private BrightIdeasSoftware.OLVColumn columnHeaderSalaryRate;
        private BrightIdeasSoftware.OLVColumn olvColumn34;
    }
}
