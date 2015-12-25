namespace MapViewer
{
    partial class MapViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewer));
            this.chkBx_ShowStatic = new System.Windows.Forms.CheckBox();
            this.btn_BrowseMapFile = new System.Windows.Forms.Button();
            this.btn_Analyze = new System.Windows.Forms.Button();
            this.txtBx_MapFilepath = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBoxFilterSimple = new System.Windows.Forms.TextBox();
            this.olv_ModuleSum = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rb_SimpleFilter = new System.Windows.Forms.RadioButton();
            this.rb_RegexFilter = new System.Windows.Forms.RadioButton();
            this.olv_ModuleView = new BrightIdeasSoftware.ObjectListView();
            this.olvText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvBSS = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvData = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvModName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olv_SymbolView = new BrightIdeasSoftware.ObjectListView();
            this.colSection = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnGlobal = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.symAddrColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colFileName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_BrowseElfFile = new System.Windows.Forms.Button();
            this.txtBx_ElfFilepath = new System.Windows.Forms.TextBox();
            this.btn_Settings = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_DataSizeActual = new System.Windows.Forms.Label();
            this.olv_SymbolSum = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lbl_TextSizeActual = new System.Windows.Forms.Label();
            this.lbl_BssSizeActual = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_ResetSyms = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBx_SymFilter = new System.Windows.Forms.TextBox();
            this.rb_SimpleFilter2 = new System.Windows.Forms.RadioButton();
            this.rb_RegexFilter2 = new System.Windows.Forms.RadioButton();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_ModuleSum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_ModuleView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_SymbolView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_SymbolSum)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkBx_ShowStatic
            // 
            this.chkBx_ShowStatic.AutoSize = true;
            this.chkBx_ShowStatic.Checked = true;
            this.chkBx_ShowStatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBx_ShowStatic.Location = new System.Drawing.Point(130, 8);
            this.chkBx_ShowStatic.Name = "chkBx_ShowStatic";
            this.chkBx_ShowStatic.Size = new System.Drawing.Size(122, 17);
            this.chkBx_ShowStatic.TabIndex = 31;
            this.chkBx_ShowStatic.Text = "Show Static Syms";
            this.chkBx_ShowStatic.UseVisualStyleBackColor = true;
            this.chkBx_ShowStatic.CheckedChanged += new System.EventHandler(this.chkBx_ShowStatic_CheckedChanged);
            // 
            // btn_BrowseMapFile
            // 
            this.btn_BrowseMapFile.Location = new System.Drawing.Point(474, 3);
            this.btn_BrowseMapFile.Name = "btn_BrowseMapFile";
            this.btn_BrowseMapFile.Size = new System.Drawing.Size(37, 22);
            this.btn_BrowseMapFile.TabIndex = 30;
            this.btn_BrowseMapFile.Text = "...";
            this.btn_BrowseMapFile.UseVisualStyleBackColor = true;
            this.btn_BrowseMapFile.Click += new System.EventHandler(this.btn_BrowseMapFile_Click);
            // 
            // btn_Analyze
            // 
            this.btn_Analyze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Analyze.Location = new System.Drawing.Point(536, 3);
            this.btn_Analyze.Name = "btn_Analyze";
            this.btn_Analyze.Size = new System.Drawing.Size(67, 22);
            this.btn_Analyze.TabIndex = 29;
            this.btn_Analyze.Text = "Analyze";
            this.btn_Analyze.UseVisualStyleBackColor = true;
            this.btn_Analyze.Click += new System.EventHandler(this.btn_Analyze_Click);
            // 
            // txtBx_MapFilepath
            // 
            this.txtBx_MapFilepath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBx_MapFilepath.Location = new System.Drawing.Point(3, 4);
            this.txtBx_MapFilepath.Name = "txtBx_MapFilepath";
            this.txtBx_MapFilepath.Size = new System.Drawing.Size(465, 20);
            this.txtBx_MapFilepath.TabIndex = 28;
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.textBoxFilterSimple);
            this.groupBox9.Controls.Add(this.olv_ModuleSum);
            this.groupBox9.Controls.Add(this.rb_SimpleFilter);
            this.groupBox9.Controls.Add(this.rb_RegexFilter);
            this.groupBox9.Location = new System.Drawing.Point(3, 65);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(606, 60);
            this.groupBox9.TabIndex = 27;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Filter";
            // 
            // textBoxFilterSimple
            // 
            this.textBoxFilterSimple.Location = new System.Drawing.Point(7, 19);
            this.textBoxFilterSimple.Name = "textBoxFilterSimple";
            this.textBoxFilterSimple.Size = new System.Drawing.Size(230, 20);
            this.textBoxFilterSimple.TabIndex = 0;
            this.textBoxFilterSimple.TextChanged += new System.EventHandler(this.textBoxFilterSimple_TextChanged);
            // 
            // olv_ModuleSum
            // 
            this.olv_ModuleSum.AllColumns.Add(this.olvColumn1);
            this.olv_ModuleSum.AllColumns.Add(this.olvColumn2);
            this.olv_ModuleSum.AllColumns.Add(this.olvColumn3);
            this.olv_ModuleSum.AllowColumnReorder = true;
            this.olv_ModuleSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_ModuleSum.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.olv_ModuleSum.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_ModuleSum.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olv_ModuleSum.Location = new System.Drawing.Point(387, 9);
            this.olv_ModuleSum.Name = "olv_ModuleSum";
            this.olv_ModuleSum.SelectColumnsMenuStaysOpen = false;
            this.olv_ModuleSum.ShowGroups = false;
            this.olv_ModuleSum.Size = new System.Drawing.Size(213, 48);
            this.olv_ModuleSum.SortGroupItemsByPrimaryColumn = false;
            this.olv_ModuleSum.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.olv_ModuleSum.TabIndex = 34;
            this.olv_ModuleSum.TintSortColumn = true;
            this.olv_ModuleSum.UseAlternatingBackColors = true;
            this.olv_ModuleSum.UseCompatibleStateImageBehavior = false;
            this.olv_ModuleSum.UseFilterIndicator = true;
            this.olv_ModuleSum.UseFiltering = true;
            this.olv_ModuleSum.UseHotItem = true;
            this.olv_ModuleSum.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "TextSize";
            this.olvColumn1.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn1.Text = "TEXT";
            this.olvColumn1.Width = 83;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "BSSSize";
            this.olvColumn2.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvColumn2.Text = "BSS";
            this.olvColumn2.Width = 62;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "DataSize";
            this.olvColumn3.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvColumn3.Text = "DATA";
            this.olvColumn3.Width = 64;
            // 
            // rb_SimpleFilter
            // 
            this.rb_SimpleFilter.AutoSize = true;
            this.rb_SimpleFilter.Checked = true;
            this.rb_SimpleFilter.Location = new System.Drawing.Point(6, 43);
            this.rb_SimpleFilter.Name = "rb_SimpleFilter";
            this.rb_SimpleFilter.Size = new System.Drawing.Size(127, 17);
            this.rb_SimpleFilter.TabIndex = 20;
            this.rb_SimpleFilter.TabStop = true;
            this.rb_SimpleFilter.Text = "\"Contains\" Filter";
            this.rb_SimpleFilter.UseVisualStyleBackColor = true;
            // 
            // rb_RegexFilter
            // 
            this.rb_RegexFilter.AutoSize = true;
            this.rb_RegexFilter.Location = new System.Drawing.Point(139, 43);
            this.rb_RegexFilter.Name = "rb_RegexFilter";
            this.rb_RegexFilter.Size = new System.Drawing.Size(97, 17);
            this.rb_RegexFilter.TabIndex = 21;
            this.rb_RegexFilter.Text = "Regex Filter";
            this.rb_RegexFilter.UseVisualStyleBackColor = true;
            // 
            // olv_ModuleView
            // 
            this.olv_ModuleView.AllColumns.Add(this.olvText);
            this.olv_ModuleView.AllColumns.Add(this.olvBSS);
            this.olv_ModuleView.AllColumns.Add(this.olvData);
            this.olv_ModuleView.AllColumns.Add(this.olvModName);
            this.olv_ModuleView.AllowColumnReorder = true;
            this.olv_ModuleView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_ModuleView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvText,
            this.olvBSS,
            this.olvData,
            this.olvModName});
            this.olv_ModuleView.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_ModuleView.FullRowSelect = true;
            this.olv_ModuleView.Location = new System.Drawing.Point(3, 134);
            this.olv_ModuleView.Name = "olv_ModuleView";
            this.olv_ModuleView.SelectColumnsMenuStaysOpen = false;
            this.olv_ModuleView.ShowGroups = false;
            this.olv_ModuleView.Size = new System.Drawing.Size(612, 373);
            this.olv_ModuleView.SortGroupItemsByPrimaryColumn = false;
            this.olv_ModuleView.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.olv_ModuleView.TabIndex = 32;
            this.olv_ModuleView.TintSortColumn = true;
            this.olv_ModuleView.UseAlternatingBackColors = true;
            this.olv_ModuleView.UseCompatibleStateImageBehavior = false;
            this.olv_ModuleView.UseFilterIndicator = true;
            this.olv_ModuleView.UseFiltering = true;
            this.olv_ModuleView.UseHotItem = true;
            this.olv_ModuleView.View = System.Windows.Forms.View.Details;
            this.olv_ModuleView.SelectionChanged += new System.EventHandler(this.olv_ModuleView_SelectionChanged);
            // 
            // olvText
            // 
            this.olvText.AspectName = "TextSize";
            this.olvText.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvText.Text = "TEXT";
            // 
            // olvBSS
            // 
            this.olvBSS.AspectName = "BSSSize";
            this.olvBSS.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvBSS.Text = "BSS";
            // 
            // olvData
            // 
            this.olvData.AspectName = "DataSize";
            this.olvData.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvData.Text = "DATA";
            // 
            // olvModName
            // 
            this.olvModName.AspectName = "ModuleName";
            this.olvModName.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvModName.Text = "Module";
            this.olvModName.Width = 1150;
            // 
            // olv_SymbolView
            // 
            this.olv_SymbolView.AllColumns.Add(this.colSection);
            this.olv_SymbolView.AllColumns.Add(this.columnGlobal);
            this.olv_SymbolView.AllColumns.Add(this.symAddrColumn);
            this.olv_SymbolView.AllColumns.Add(this.olvColumn6);
            this.olv_SymbolView.AllColumns.Add(this.olvColumn7);
            this.olv_SymbolView.AllColumns.Add(this.colFileName);
            this.olv_SymbolView.AllowColumnReorder = true;
            this.olv_SymbolView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_SymbolView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSection,
            this.columnGlobal,
            this.symAddrColumn,
            this.olvColumn6,
            this.olvColumn7,
            this.colFileName});
            this.olv_SymbolView.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_SymbolView.FullRowSelect = true;
            this.olv_SymbolView.Location = new System.Drawing.Point(621, 134);
            this.olv_SymbolView.Name = "olv_SymbolView";
            this.olv_SymbolView.SelectColumnsMenuStaysOpen = false;
            this.olv_SymbolView.ShowGroups = false;
            this.olv_SymbolView.Size = new System.Drawing.Size(489, 373);
            this.olv_SymbolView.SortGroupItemsByPrimaryColumn = false;
            this.olv_SymbolView.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.olv_SymbolView.TabIndex = 33;
            this.olv_SymbolView.TintSortColumn = true;
            this.olv_SymbolView.UseAlternatingBackColors = true;
            this.olv_SymbolView.UseCompatibleStateImageBehavior = false;
            this.olv_SymbolView.UseFilterIndicator = true;
            this.olv_SymbolView.UseFiltering = true;
            this.olv_SymbolView.UseHotItem = true;
            this.olv_SymbolView.View = System.Windows.Forms.View.Details;
            // 
            // colSection
            // 
            this.colSection.AspectName = "SectionName";
            this.colSection.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSection.Text = "SEC";
            this.colSection.Width = 38;
            // 
            // columnGlobal
            // 
            this.columnGlobal.AspectName = "GlobalScope";
            this.columnGlobal.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.columnGlobal.Text = "GLOBAL";
            // 
            // symAddrColumn
            // 
            this.symAddrColumn.AspectName = "LoadAddress";
            this.symAddrColumn.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.symAddrColumn.Text = "ADDRESS";
            this.symAddrColumn.Width = 77;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Size";
            this.olvColumn6.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvColumn6.Text = "SIZE";
            this.olvColumn6.Width = 71;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "SymbolName";
            this.olvColumn7.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvColumn7.Text = "NAME";
            this.olvColumn7.Width = 300;
            // 
            // colFileName
            // 
            this.colFileName.AspectName = "FileName";
            this.colFileName.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFileName.Text = "MODULE";
            this.colFileName.Width = 400;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.olv_ModuleView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.olv_SymbolView, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1113, 510);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox9, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(612, 125);
            this.tableLayoutPanel2.TabIndex = 35;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.31169F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.68831F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel3.Controls.Add(this.txtBx_ElfFilepath, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtBx_MapFilepath, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Analyze, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_BrowseMapFile, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Settings, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_BrowseElfFile, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(606, 56);
            this.tableLayoutPanel3.TabIndex = 28;
            // 
            // btn_BrowseElfFile
            // 
            this.btn_BrowseElfFile.Location = new System.Drawing.Point(474, 31);
            this.btn_BrowseElfFile.Name = "btn_BrowseElfFile";
            this.btn_BrowseElfFile.Size = new System.Drawing.Size(37, 22);
            this.btn_BrowseElfFile.TabIndex = 32;
            this.btn_BrowseElfFile.Text = "...";
            this.btn_BrowseElfFile.UseVisualStyleBackColor = true;
            this.btn_BrowseElfFile.Click += new System.EventHandler(this.btn_BrowseElfFile_Click);
            // 
            // txtBx_ElfFilepath
            // 
            this.txtBx_ElfFilepath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBx_ElfFilepath.Location = new System.Drawing.Point(3, 32);
            this.txtBx_ElfFilepath.Name = "txtBx_ElfFilepath";
            this.txtBx_ElfFilepath.Size = new System.Drawing.Size(465, 20);
            this.txtBx_ElfFilepath.TabIndex = 31;
            // 
            // btn_Settings
            // 
            this.btn_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Settings.Location = new System.Drawing.Point(536, 31);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.Size = new System.Drawing.Size(67, 22);
            this.btn_Settings.TabIndex = 33;
            this.btn_Settings.Text = "Settings";
            this.btn_Settings.UseVisualStyleBackColor = true;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 219F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(621, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(489, 125);
            this.tableLayoutPanel4.TabIndex = 36;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbl_DataSizeActual);
            this.groupBox3.Controls.Add(this.olv_SymbolSum);
            this.groupBox3.Controls.Add(this.lbl_TextSizeActual);
            this.groupBox3.Controls.Add(this.lbl_BssSizeActual);
            this.groupBox3.Location = new System.Drawing.Point(273, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 119);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Actuals";
            // 
            // lbl_DataSizeActual
            // 
            this.lbl_DataSizeActual.AutoSize = true;
            this.lbl_DataSizeActual.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_DataSizeActual.Location = new System.Drawing.Point(6, 52);
            this.lbl_DataSizeActual.Name = "lbl_DataSizeActual";
            this.lbl_DataSizeActual.Size = new System.Drawing.Size(31, 13);
            this.lbl_DataSizeActual.TabIndex = 41;
            this.lbl_DataSizeActual.Text = "DATA";
            // 
            // olv_SymbolSum
            // 
            this.olv_SymbolSum.AllColumns.Add(this.olvColumn4);
            this.olv_SymbolSum.AllColumns.Add(this.olvColumn5);
            this.olv_SymbolSum.AllColumns.Add(this.olvColumn8);
            this.olv_SymbolSum.AllowColumnReorder = true;
            this.olv_SymbolSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_SymbolSum.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn8});
            this.olv_SymbolSum.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_SymbolSum.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olv_SymbolSum.Location = new System.Drawing.Point(1, 70);
            this.olv_SymbolSum.Name = "olv_SymbolSum";
            this.olv_SymbolSum.SelectColumnsMenuStaysOpen = false;
            this.olv_SymbolSum.ShowGroups = false;
            this.olv_SymbolSum.Size = new System.Drawing.Size(213, 48);
            this.olv_SymbolSum.SortGroupItemsByPrimaryColumn = false;
            this.olv_SymbolSum.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.olv_SymbolSum.TabIndex = 38;
            this.olv_SymbolSum.TintSortColumn = true;
            this.olv_SymbolSum.UseAlternatingBackColors = true;
            this.olv_SymbolSum.UseCompatibleStateImageBehavior = false;
            this.olv_SymbolSum.UseFilterIndicator = true;
            this.olv_SymbolSum.UseFiltering = true;
            this.olv_SymbolSum.UseHotItem = true;
            this.olv_SymbolSum.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "TextSize";
            this.olvColumn4.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn4.Text = "TEXT";
            this.olvColumn4.Width = 83;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "BSSSize";
            this.olvColumn5.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvColumn5.Text = "BSS";
            this.olvColumn5.Width = 62;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "DataSize";
            this.olvColumn8.HeaderFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold);
            this.olvColumn8.Text = "DATA";
            this.olvColumn8.Width = 64;
            // 
            // lbl_TextSizeActual
            // 
            this.lbl_TextSizeActual.AutoSize = true;
            this.lbl_TextSizeActual.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_TextSizeActual.Location = new System.Drawing.Point(6, 16);
            this.lbl_TextSizeActual.Name = "lbl_TextSizeActual";
            this.lbl_TextSizeActual.Size = new System.Drawing.Size(31, 13);
            this.lbl_TextSizeActual.TabIndex = 39;
            this.lbl_TextSizeActual.Text = "TEXT";
            // 
            // lbl_BssSizeActual
            // 
            this.lbl_BssSizeActual.AutoSize = true;
            this.lbl_BssSizeActual.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_BssSizeActual.Location = new System.Drawing.Point(6, 34);
            this.lbl_BssSizeActual.Name = "lbl_BssSizeActual";
            this.lbl_BssSizeActual.Size = new System.Drawing.Size(25, 13);
            this.lbl_BssSizeActual.TabIndex = 40;
            this.lbl_BssSizeActual.Text = "BSS";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.45378F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.54622F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(264, 119);
            this.tableLayoutPanel6.TabIndex = 37;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_ResetSyms);
            this.groupBox2.Controls.Add(this.chkBx_ShowStatic);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(261, 34);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            // 
            // btn_ResetSyms
            // 
            this.btn_ResetSyms.Location = new System.Drawing.Point(3, 8);
            this.btn_ResetSyms.Name = "btn_ResetSyms";
            this.btn_ResetSyms.Size = new System.Drawing.Size(75, 23);
            this.btn_ResetSyms.TabIndex = 32;
            this.btn_ResetSyms.Text = "Reset Syms";
            this.btn_ResetSyms.UseVisualStyleBackColor = true;
            this.btn_ResetSyms.Click += new System.EventHandler(this.btn_ResetSyms_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.txtBx_SymFilter);
            this.groupBox1.Controls.Add(this.rb_SimpleFilter2);
            this.groupBox1.Controls.Add(this.rb_RegexFilter2);
            this.groupBox1.Location = new System.Drawing.Point(3, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 58);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // txtBx_SymFilter
            // 
            this.txtBx_SymFilter.Location = new System.Drawing.Point(6, 20);
            this.txtBx_SymFilter.Name = "txtBx_SymFilter";
            this.txtBx_SymFilter.Size = new System.Drawing.Size(246, 20);
            this.txtBx_SymFilter.TabIndex = 0;
            this.txtBx_SymFilter.TextChanged += new System.EventHandler(this.txtBx_SymFilter_TextChanged);
            // 
            // rb_SimpleFilter2
            // 
            this.rb_SimpleFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rb_SimpleFilter2.AutoSize = true;
            this.rb_SimpleFilter2.Checked = true;
            this.rb_SimpleFilter2.Location = new System.Drawing.Point(6, 40);
            this.rb_SimpleFilter2.Name = "rb_SimpleFilter2";
            this.rb_SimpleFilter2.Size = new System.Drawing.Size(127, 17);
            this.rb_SimpleFilter2.TabIndex = 20;
            this.rb_SimpleFilter2.TabStop = true;
            this.rb_SimpleFilter2.Text = "\"Contains\" Filter";
            this.rb_SimpleFilter2.UseVisualStyleBackColor = true;
            // 
            // rb_RegexFilter2
            // 
            this.rb_RegexFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rb_RegexFilter2.AutoSize = true;
            this.rb_RegexFilter2.Location = new System.Drawing.Point(136, 40);
            this.rb_RegexFilter2.Name = "rb_RegexFilter2";
            this.rb_RegexFilter2.Size = new System.Drawing.Size(97, 17);
            this.rb_RegexFilter2.TabIndex = 21;
            this.rb_RegexFilter2.Text = "Regex Filter";
            this.rb_RegexFilter2.UseVisualStyleBackColor = true;
            // 
            // MapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 509);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapViewer";
            this.Text = "MapViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_ModuleSum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_ModuleView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_SymbolView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_SymbolSum)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBx_ShowStatic;
        private System.Windows.Forms.Button btn_BrowseMapFile;
        private System.Windows.Forms.Button btn_Analyze;
        private System.Windows.Forms.TextBox txtBx_MapFilepath;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox textBoxFilterSimple;
        private System.Windows.Forms.RadioButton rb_SimpleFilter;
        private System.Windows.Forms.RadioButton rb_RegexFilter;
        private BrightIdeasSoftware.ObjectListView olv_ModuleView;
        private BrightIdeasSoftware.OLVColumn olvText;
        private BrightIdeasSoftware.OLVColumn olvBSS;
        private BrightIdeasSoftware.OLVColumn olvData;
        private BrightIdeasSoftware.OLVColumn olvModName;
        private BrightIdeasSoftware.ObjectListView olv_SymbolView;
        private BrightIdeasSoftware.OLVColumn colSection;
        private BrightIdeasSoftware.OLVColumn columnGlobal;
        private BrightIdeasSoftware.OLVColumn symAddrColumn;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.OpenFileDialog OFD;
        private BrightIdeasSoftware.ObjectListView olv_ModuleSum;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBx_SymFilter;
        private System.Windows.Forms.RadioButton rb_SimpleFilter2;
        private System.Windows.Forms.RadioButton rb_RegexFilter2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_BrowseElfFile;
        private System.Windows.Forms.TextBox txtBx_ElfFilepath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private BrightIdeasSoftware.ObjectListView olv_SymbolSum;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_ResetSyms;
        private BrightIdeasSoftware.OLVColumn colFileName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbl_TextSizeActual;
        private System.Windows.Forms.Label lbl_DataSizeActual;
        private System.Windows.Forms.Label lbl_BssSizeActual;
        private System.Windows.Forms.Button btn_Settings;
    }
}

