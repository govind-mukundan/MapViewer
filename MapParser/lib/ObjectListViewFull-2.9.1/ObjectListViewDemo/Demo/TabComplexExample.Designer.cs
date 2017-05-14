namespace ObjectListViewDemo
{
    partial class TabComplexExample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabComplexExample));
            this.label38 = new System.Windows.Forms.Label();
            this.comboBoxHotItemStyle = new System.Windows.Forms.ComboBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.textBoxFilterComplex = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.comboBoxEditable = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.buttonDisable = new System.Windows.Forms.Button();
            this.buttonRebuild = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxGroups = new System.Windows.Forms.CheckBox();
            this.olvComplex = new BrightIdeasSoftware.ObjectListView();
            this.personColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.occupationColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnCookingSkill = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cookingSkillRenderer = new BrightIdeasSoftware.MultiImageRenderer();
            this.birthdayColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.hourlyRateColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.moneyImageColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.salaryRenderer = new BrightIdeasSoftware.MultiImageRenderer();
            this.daysSinceBirthColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvJokeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvMarriedColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupImageList = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label20 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvComplex)).BeginInit();
            this.SuspendLayout();
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(390, 477);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(50, 13);
            this.label38.TabIndex = 33;
            this.label38.Text = "Hot Item:";
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
            this.comboBoxHotItemStyle.Location = new System.Drawing.Point(441, 472);
            this.comboBoxHotItemStyle.Name = "comboBoxHotItemStyle";
            this.comboBoxHotItemStyle.Size = new System.Drawing.Size(86, 21);
            this.comboBoxHotItemStyle.TabIndex = 34;
            this.comboBoxHotItemStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxHotItemStyle_SelectedIndexChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.textBoxFilterComplex);
            this.groupBox10.Location = new System.Drawing.Point(683, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(117, 44);
            this.groupBox10.TabIndex = 32;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Filter";
            // 
            // textBoxFilterComplex
            // 
            this.textBoxFilterComplex.Location = new System.Drawing.Point(7, 20);
            this.textBoxFilterComplex.Name = "textBoxFilterComplex";
            this.textBoxFilterComplex.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilterComplex.TabIndex = 0;
            this.textBoxFilterComplex.TextChanged += new System.EventHandler(this.textBoxFilterComplex_TextChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(547, 472);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(57, 23);
            this.buttonAdd.TabIndex = 28;
            this.buttonAdd.Text = "Add";
            this.toolTip1.SetToolTip(this.buttonAdd, "Click to add a few more rows");
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(612, 472);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(57, 23);
            this.buttonRemove.TabIndex = 29;
            this.buttonRemove.Text = "Remove";
            this.toolTip1.SetToolTip(this.buttonRemove, "Click to remove selected rows");
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // comboBoxEditable
            // 
            this.comboBoxEditable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxEditable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEditable.FormattingEnabled = true;
            this.comboBoxEditable.Items.AddRange(new object[] {
            "No",
            "Single Click",
            "Double Click",
            "F2 Only"});
            this.comboBoxEditable.Location = new System.Drawing.Point(200, 472);
            this.comboBoxEditable.Name = "comboBoxEditable";
            this.comboBoxEditable.Size = new System.Drawing.Size(71, 21);
            this.comboBoxEditable.TabIndex = 25;
            this.comboBoxEditable.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditable_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(281, 476);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "View:";
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
            this.comboBoxView.Location = new System.Drawing.Point(315, 472);
            this.comboBoxView.Name = "comboBoxView";
            this.comboBoxView.Size = new System.Drawing.Size(63, 21);
            this.comboBoxView.TabIndex = 27;
            this.comboBoxView.SelectedIndexChanged += new System.EventHandler(this.comboBoxView_SelectedIndexChanged);
            // 
            // buttonDisable
            // 
            this.buttonDisable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisable.Location = new System.Drawing.Point(677, 472);
            this.buttonDisable.Name = "buttonDisable";
            this.buttonDisable.Size = new System.Drawing.Size(57, 23);
            this.buttonDisable.TabIndex = 30;
            this.buttonDisable.Text = "Disable";
            this.toolTip1.SetToolTip(this.buttonDisable, "Click to disable selected row. Ctrl-click to enable");
            this.buttonDisable.UseVisualStyleBackColor = true;
            this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
            // 
            // buttonRebuild
            // 
            this.buttonRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRebuild.Location = new System.Drawing.Point(742, 472);
            this.buttonRebuild.Name = "buttonRebuild";
            this.buttonRebuild.Size = new System.Drawing.Size(57, 23);
            this.buttonRebuild.TabIndex = 31;
            this.buttonRebuild.Text = "&Rebuild";
            this.buttonRebuild.UseVisualStyleBackColor = true;
            this.buttonRebuild.Click += new System.EventHandler(this.buttonRebuild_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(1, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(676, 48);
            this.label2.TabIndex = 24;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // checkBoxGroups
            // 
            this.checkBoxGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxGroups.Location = new System.Drawing.Point(1, 472);
            this.checkBoxGroups.Name = "checkBoxGroups";
            this.checkBoxGroups.Size = new System.Drawing.Size(62, 21);
            this.checkBoxGroups.TabIndex = 22;
            this.checkBoxGroups.Text = "&Groups";
            this.checkBoxGroups.UseVisualStyleBackColor = true;
            this.checkBoxGroups.CheckedChanged += new System.EventHandler(this.checkBoxGroups_CheckedChanged);
            // 
            // olvComplex
            // 
            this.olvComplex.AllColumns.Add(this.personColumn);
            this.olvComplex.AllColumns.Add(this.occupationColumn);
            this.olvComplex.AllColumns.Add(this.columnCookingSkill);
            this.olvComplex.AllColumns.Add(this.birthdayColumn);
            this.olvComplex.AllColumns.Add(this.hourlyRateColumn);
            this.olvComplex.AllColumns.Add(this.moneyImageColumn);
            this.olvComplex.AllColumns.Add(this.daysSinceBirthColumn);
            this.olvComplex.AllColumns.Add(this.olvJokeColumn);
            this.olvComplex.AllColumns.Add(this.olvMarriedColumn);
            this.olvComplex.AllowColumnReorder = true;
            this.olvComplex.AllowDrop = true;
            this.olvComplex.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.olvComplex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvComplex.BackColor = System.Drawing.SystemColors.Window;
            this.olvComplex.CellEditUseWholeCell = false;
            this.olvComplex.CheckBoxes = true;
            this.olvComplex.CheckedAspectName = "IsActive";
            this.olvComplex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.personColumn,
            this.occupationColumn,
            this.columnCookingSkill,
            this.birthdayColumn,
            this.hourlyRateColumn,
            this.moneyImageColumn,
            this.daysSinceBirthColumn,
            this.olvJokeColumn,
            this.olvMarriedColumn});
            this.olvComplex.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvComplex.EmptyListMsg = "This list is empty. Press \"Add\" to create some items";
            this.olvComplex.FullRowSelect = true;
            this.olvComplex.GroupImageList = this.groupImageList;
            this.olvComplex.GroupWithItemCountFormat = "{0} ({1} people)";
            this.olvComplex.GroupWithItemCountSingularFormat = "{0} ({1} person)";
            this.olvComplex.HeaderWordWrap = true;
            this.olvComplex.HideSelection = false;
            this.olvComplex.LargeImageList = this.imageList2;
            this.olvComplex.Location = new System.Drawing.Point(1, 55);
            this.olvComplex.Name = "olvComplex";
            this.olvComplex.OverlayImage.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.olvComplex.OverlayText.Alignment = System.Drawing.ContentAlignment.TopRight;
            this.olvComplex.OverlayText.BorderColor = System.Drawing.Color.DarkRed;
            this.olvComplex.OverlayText.BorderWidth = 4F;
            this.olvComplex.OverlayText.InsetX = 10;
            this.olvComplex.OverlayText.InsetY = 35;
            this.olvComplex.OverlayText.Rotation = 20;
            this.olvComplex.OverlayText.Text = "";
            this.olvComplex.OverlayText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.olvComplex.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.olvComplex.ShowCommandMenuOnRightClick = true;
            this.olvComplex.ShowGroups = false;
            this.olvComplex.ShowImagesOnSubItems = true;
            this.olvComplex.ShowItemCountOnGroups = true;
            this.olvComplex.ShowItemToolTips = true;
            this.olvComplex.Size = new System.Drawing.Size(799, 409);
            this.olvComplex.SmallImageList = this.imageList1;
            this.olvComplex.TabIndex = 21;
            this.olvComplex.UseAlternatingBackColors = true;
            this.olvComplex.UseCellFormatEvents = true;
            this.olvComplex.UseCompatibleStateImageBehavior = false;
            this.olvComplex.UseFilterIndicator = true;
            this.olvComplex.UseFiltering = true;
            this.olvComplex.UseHotItem = true;
            this.olvComplex.UseHyperlinks = true;
            this.olvComplex.UseSubItemCheckBoxes = true;
            this.olvComplex.View = System.Windows.Forms.View.Details;
            // 
            // personColumn
            // 
            this.personColumn.AspectName = "Name";
            this.personColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.personColumn.HeaderCheckBox = true;
            this.personColumn.HeaderCheckState = System.Windows.Forms.CheckState.Checked;
            this.personColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.personColumn.Text = "Person";
            this.personColumn.ToolTipText = "Tooltip for Person column. This was configurated in the IDE. (Hold down Control t" +
    "o see a different tooltip)";
            this.personColumn.UseInitialLetterForGroup = true;
            this.personColumn.Width = 150;
            // 
            // occupationColumn
            // 
            this.occupationColumn.AspectName = "Occupation";
            this.occupationColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.occupationColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.occupationColumn.Hyperlink = true;
            this.occupationColumn.IsTileViewColumn = true;
            this.occupationColumn.Text = "Occupation";
            this.occupationColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.occupationColumn.Width = 92;
            // 
            // columnCookingSkill
            // 
            this.columnCookingSkill.AspectName = "CulinaryRating";
            this.columnCookingSkill.ButtonPadding = new System.Drawing.Size(10, 10);
            this.columnCookingSkill.GroupWithItemCountFormat = "{0} ({1} candidates)";
            this.columnCookingSkill.GroupWithItemCountSingularFormat = "{0} (only {1} candidate)";
            this.columnCookingSkill.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnCookingSkill.Renderer = this.cookingSkillRenderer;
            this.columnCookingSkill.Text = "Cooking skill";
            this.columnCookingSkill.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnCookingSkill.ToolTipText = "Group on this column to see full group formatting possibilities";
            this.columnCookingSkill.Width = 85;
            // 
            // cookingSkillRenderer
            // 
            this.cookingSkillRenderer.ImageName = "star";
            this.cookingSkillRenderer.MaximumValue = 50;
            this.cookingSkillRenderer.MaxNumberImages = 5;
            // 
            // birthdayColumn
            // 
            this.birthdayColumn.AspectName = "BirthDate";
            this.birthdayColumn.AspectToStringFormat = "{0:D}";
            this.birthdayColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.birthdayColumn.GroupWithItemCountFormat = "{0} has {1} birthdays";
            this.birthdayColumn.GroupWithItemCountSingularFormat = "{0} has only {1} birthday";
            this.birthdayColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.birthdayColumn.IsTileViewColumn = true;
            this.birthdayColumn.Text = "Birthday";
            this.birthdayColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.birthdayColumn.Width = 111;
            // 
            // hourlyRateColumn
            // 
            this.hourlyRateColumn.AspectName = "GetRate";
            this.hourlyRateColumn.AspectToStringFormat = "{0:C}";
            this.hourlyRateColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.hourlyRateColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hourlyRateColumn.IsTileViewColumn = true;
            this.hourlyRateColumn.Text = "Hourly Rate";
            this.hourlyRateColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hourlyRateColumn.Width = 71;
            // 
            // moneyImageColumn
            // 
            this.moneyImageColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.moneyImageColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.moneyImageColumn.IsEditable = false;
            this.moneyImageColumn.Renderer = this.salaryRenderer;
            this.moneyImageColumn.Text = "Salary";
            this.moneyImageColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.moneyImageColumn.Width = 55;
            // 
            // salaryRenderer
            // 
            this.salaryRenderer.ImageName = "tick";
            this.salaryRenderer.MaximumValue = 500000;
            this.salaryRenderer.MaxNumberImages = 5;
            this.salaryRenderer.MinimumValue = 10000;
            // 
            // daysSinceBirthColumn
            // 
            this.daysSinceBirthColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.daysSinceBirthColumn.IsEditable = false;
            this.daysSinceBirthColumn.Text = "Days Since Birth";
            this.daysSinceBirthColumn.Width = 81;
            // 
            // olvJokeColumn
            // 
            this.olvJokeColumn.AspectName = "CanTellJokes";
            this.olvJokeColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvJokeColumn.CheckBoxes = true;
            this.olvJokeColumn.HeaderCheckBox = true;
            this.olvJokeColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvJokeColumn.IsHeaderVertical = true;
            this.olvJokeColumn.Text = "Jokes?";
            this.olvJokeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvJokeColumn.ToolTipText = "Tells Jokes?";
            this.olvJokeColumn.Width = 46;
            // 
            // olvMarriedColumn
            // 
            this.olvMarriedColumn.AspectName = "MaritalStatus";
            this.olvMarriedColumn.ButtonPadding = new System.Drawing.Size(10, 10);
            this.olvMarriedColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvMarriedColumn.IsTileViewColumn = true;
            this.olvMarriedColumn.Text = "Married?";
            this.olvMarriedColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvMarriedColumn.ToolTipText = "Just to show how to do it, if you hold down Control when grouping by this column," +
    " the  groups and items within each group will be sorted by their SECOND letter";
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
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "user");
            this.imageList2.Images.SetKeyName(1, "compass");
            this.imageList2.Images.SetKeyName(2, "down");
            this.imageList2.Images.SetKeyName(3, "find");
            this.imageList2.Images.SetKeyName(4, "folder");
            this.imageList2.Images.SetKeyName(5, "movie");
            this.imageList2.Images.SetKeyName(6, "music");
            this.imageList2.Images.SetKeyName(7, "no");
            this.imageList2.Images.SetKeyName(8, "readonly");
            this.imageList2.Images.SetKeyName(9, "public");
            this.imageList2.Images.SetKeyName(10, "recycle");
            this.imageList2.Images.SetKeyName(11, "spanner");
            this.imageList2.Images.SetKeyName(12, "star");
            this.imageList2.Images.SetKeyName(13, "tick");
            this.imageList2.Images.SetKeyName(14, "archive");
            this.imageList2.Images.SetKeyName(15, "system");
            this.imageList2.Images.SetKeyName(16, "hidden");
            this.imageList2.Images.SetKeyName(17, "temporary");
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
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(151, 475);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 13);
            this.label20.TabIndex = 35;
            this.label20.Text = "Editable:";
            // 
            // TabComplexExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.comboBoxHotItemStyle);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.comboBoxEditable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxView);
            this.Controls.Add(this.buttonDisable);
            this.Controls.Add(this.buttonRebuild);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxGroups);
            this.Controls.Add(this.olvComplex);
            this.Name = "TabComplexExample";
            this.Size = new System.Drawing.Size(800, 499);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvComplex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox comboBoxHotItemStyle;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox textBoxFilterComplex;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.ComboBox comboBoxEditable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxView;
        private System.Windows.Forms.Button buttonDisable;
        private System.Windows.Forms.Button buttonRebuild;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxGroups;
        private BrightIdeasSoftware.ObjectListView olvComplex;
        private BrightIdeasSoftware.OLVColumn personColumn;
        private BrightIdeasSoftware.OLVColumn occupationColumn;
        private BrightIdeasSoftware.OLVColumn columnCookingSkill;
        private BrightIdeasSoftware.OLVColumn birthdayColumn;
        private BrightIdeasSoftware.OLVColumn hourlyRateColumn;
        private BrightIdeasSoftware.OLVColumn moneyImageColumn;
        private BrightIdeasSoftware.OLVColumn daysSinceBirthColumn;
        private BrightIdeasSoftware.OLVColumn olvJokeColumn;
        private BrightIdeasSoftware.OLVColumn olvMarriedColumn;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList groupImageList;
        private BrightIdeasSoftware.MultiImageRenderer cookingSkillRenderer;
        private BrightIdeasSoftware.MultiImageRenderer salaryRenderer;
    }
}
