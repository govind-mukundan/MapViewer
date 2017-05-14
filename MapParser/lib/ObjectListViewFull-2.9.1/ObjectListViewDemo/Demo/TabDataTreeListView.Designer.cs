namespace ObjectListViewDemo
{
    partial class TabDataTreeListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabDataTreeListView));
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.olvDataTree = new BrightIdeasSoftware.DataTreeListView();
            this.olvColumn41 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.buttonResetData = new System.Windows.Forms.Button();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvDataTree)).BeginInit();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.Controls.Add(this.filterTextBox);
            this.groupBox14.Location = new System.Drawing.Point(685, 4);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(117, 44);
            this.groupBox14.TabIndex = 22;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Filter";
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(7, 17);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(100, 20);
            this.filterTextBox.TabIndex = 0;
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox15.Controls.Add(this.olvDataTree);
            this.groupBox15.Location = new System.Drawing.Point(3, 227);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(799, 268);
            this.groupBox15.TabIndex = 21;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Data List View";
            // 
            // olvDataTree
            // 
            this.olvDataTree.AllColumns.Add(this.olvColumn41);
            this.olvDataTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvDataTree.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn41});
            this.olvDataTree.DataSource = null;
            this.olvDataTree.KeyAspectName = "Id";
            this.olvDataTree.Location = new System.Drawing.Point(6, 19);
            this.olvDataTree.Name = "olvDataTree";
            this.olvDataTree.ParentKeyAspectName = "ParentId";
            this.olvDataTree.RootKeyValueString = "";
            this.olvDataTree.ShowGroups = false;
            this.olvDataTree.ShowKeyColumns = false;
            this.olvDataTree.Size = new System.Drawing.Size(670, 243);
            this.olvDataTree.TabIndex = 0;
            this.olvDataTree.UseCompatibleStateImageBehavior = false;
            this.olvDataTree.UseFilterIndicator = true;
            this.olvDataTree.UseFiltering = true;
            this.olvDataTree.View = System.Windows.Forms.View.Details;
            this.olvDataTree.VirtualMode = true;
            // 
            // olvColumn41
            // 
            this.olvColumn41.AspectName = "Name";
            this.olvColumn41.Text = "Person";
            this.olvColumn41.Width = 154;
            // 
            // groupBox16
            // 
            this.groupBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox16.Controls.Add(this.dataGridView2);
            this.groupBox16.Controls.Add(this.buttonResetData);
            this.groupBox16.Location = new System.Drawing.Point(3, 54);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(799, 167);
            this.groupBox16.TabIndex = 20;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Source Data Table";
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 19);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView2.Size = new System.Drawing.Size(670, 142);
            this.dataGridView2.TabIndex = 0;
            // 
            // buttonResetData
            // 
            this.buttonResetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetData.Location = new System.Drawing.Point(689, 19);
            this.buttonResetData.Name = "buttonResetData";
            this.buttonResetData.Size = new System.Drawing.Size(104, 23);
            this.buttonResetData.TabIndex = 1;
            this.buttonResetData.Text = "&Reset Data";
            this.buttonResetData.UseVisualStyleBackColor = true;
            this.buttonResetData.Click += new System.EventHandler(this.buttonResetData_Click);
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label42.Location = new System.Drawing.Point(3, 4);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(676, 46);
            this.label42.TabIndex = 19;
            this.label42.Text = resources.GetString("label42.Text");
            // 
            // TabDataTreeListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.groupBox15);
            this.Controls.Add(this.groupBox16);
            this.Controls.Add(this.label42);
            this.Name = "TabDataTreeListView";
            this.Size = new System.Drawing.Size(804, 499);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvDataTree)).EndInit();
            this.groupBox16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.GroupBox groupBox15;
        private BrightIdeasSoftware.DataTreeListView olvDataTree;
        private BrightIdeasSoftware.OLVColumn olvColumn41;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button buttonResetData;
        private System.Windows.Forms.Label label42;
    }
}
