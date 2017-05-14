namespace ObjectListViewDemo
{
    partial class TabDescribedTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabDescribedTask));
            this.olvColumnAction = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.checkBoxIncomplete = new System.Windows.Forms.CheckBox();
            this.checkBoxHighPriority = new System.Windows.Forms.CheckBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageListTasks = new System.Windows.Forms.ImageList(this.components);
            this.olvTasks = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnTask = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPriority = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // olvColumnAction
            // 
            this.olvColumnAction.AspectName = "Action";
            this.olvColumnAction.Text = "Action";
            this.olvColumnAction.Width = 90;
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.checkBoxIncomplete);
            this.groupBox9.Controls.Add(this.checkBoxHighPriority);
            this.groupBox9.Controls.Add(this.textBoxFilter);
            this.groupBox9.Location = new System.Drawing.Point(683, 57);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(117, 109);
            this.groupBox9.TabIndex = 40;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Filter";
            // 
            // checkBoxIncomplete
            // 
            this.checkBoxIncomplete.AutoSize = true;
            this.checkBoxIncomplete.Location = new System.Drawing.Point(7, 76);
            this.checkBoxIncomplete.Name = "checkBoxIncomplete";
            this.checkBoxIncomplete.Size = new System.Drawing.Size(102, 17);
            this.checkBoxIncomplete.TabIndex = 2;
            this.checkBoxIncomplete.Text = "Incomplete Only";
            this.checkBoxIncomplete.UseVisualStyleBackColor = true;
            this.checkBoxIncomplete.CheckedChanged += new System.EventHandler(this.checkBoxIncomplete_CheckedChanged);
            // 
            // checkBoxHighPriority
            // 
            this.checkBoxHighPriority.AutoSize = true;
            this.checkBoxHighPriority.Location = new System.Drawing.Point(7, 52);
            this.checkBoxHighPriority.Name = "checkBoxHighPriority";
            this.checkBoxHighPriority.Size = new System.Drawing.Size(106, 17);
            this.checkBoxHighPriority.TabIndex = 1;
            this.checkBoxHighPriority.Text = "&High Priority Only";
            this.checkBoxHighPriority.UseVisualStyleBackColor = true;
            this.checkBoxHighPriority.CheckedChanged += new System.EventHandler(this.checkBoxHighPriority_CheckedChanged);
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(7, 20);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilter.TabIndex = 0;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(797, 46);
            this.label1.TabIndex = 37;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // imageListTasks
            // 
            this.imageListTasks.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTasks.ImageStream")));
            this.imageListTasks.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageListTasks.Images.SetKeyName(0, "download");
            this.imageListTasks.Images.SetKeyName(1, "backandforth");
            this.imageListTasks.Images.SetKeyName(2, "faq");
            this.imageListTasks.Images.SetKeyName(3, "windows");
            this.imageListTasks.Images.SetKeyName(4, "filter");
            this.imageListTasks.Images.SetKeyName(5, "printer");
            this.imageListTasks.Images.SetKeyName(6, "electronics");
            this.imageListTasks.Images.SetKeyName(7, "film");
            // 
            // olvTasks
            // 
            this.olvTasks.AllColumns.Add(this.olvColumnTask);
            this.olvTasks.AllColumns.Add(this.olvColumnPriority);
            this.olvTasks.AllColumns.Add(this.olvColumnStatus);
            this.olvTasks.AllColumns.Add(this.olvColumnAction);
            this.olvTasks.AllowColumnReorder = true;
            this.olvTasks.AllowDrop = true;
            this.olvTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvTasks.CheckBoxes = true;
            this.olvTasks.CheckedAspectName = "";
            this.olvTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnTask,
            this.olvColumnPriority,
            this.olvColumnStatus,
            this.olvColumnAction});
            this.olvTasks.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvTasks.FullRowSelect = true;
            this.olvTasks.HeaderWordWrap = true;
            this.olvTasks.HideSelection = false;
            this.olvTasks.IncludeColumnHeadersInCopy = true;
            this.olvTasks.Location = new System.Drawing.Point(3, 57);
            this.olvTasks.Name = "olvTasks";
            this.olvTasks.OverlayText.Alignment = System.Drawing.ContentAlignment.BottomLeft;
            this.olvTasks.OverlayText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.olvTasks.OverlayText.BorderWidth = 2F;
            this.olvTasks.OverlayText.Rotation = -20;
            this.olvTasks.OverlayText.Text = "";
            this.olvTasks.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.olvTasks.ShowCommandMenuOnRightClick = true;
            this.olvTasks.ShowGroups = false;
            this.olvTasks.ShowHeaderInAllViews = false;
            this.olvTasks.ShowItemToolTips = true;
            this.olvTasks.Size = new System.Drawing.Size(675, 439);
            this.olvTasks.SortGroupItemsByPrimaryColumn = false;
            this.olvTasks.TabIndex = 36;
            this.olvTasks.TriStateCheckBoxes = true;
            this.olvTasks.UseAlternatingBackColors = true;
            this.olvTasks.UseCellFormatEvents = true;
            this.olvTasks.UseCompatibleStateImageBehavior = false;
            this.olvTasks.UseFilterIndicator = true;
            this.olvTasks.UseFiltering = true;
            this.olvTasks.UseHotItem = true;
            this.olvTasks.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnTask
            // 
            this.olvColumnTask.AspectName = "TaskName";
            this.olvColumnTask.MinimumWidth = 40;
            this.olvColumnTask.Text = "Task";
            this.olvColumnTask.ToolTipText = "";
            this.olvColumnTask.Width = 400;
            // 
            // olvColumnPriority
            // 
            this.olvColumnPriority.AspectName = "Priority";
            this.olvColumnPriority.Text = "Priority";
            this.olvColumnPriority.Width = 110;
            // 
            // olvColumnStatus
            // 
            this.olvColumnStatus.AspectName = "Status";
            this.olvColumnStatus.IsTileViewColumn = true;
            this.olvColumnStatus.MinimumWidth = 30;
            this.olvColumnStatus.Text = "Status";
            this.olvColumnStatus.ToolTipText = "";
            this.olvColumnStatus.Width = 120;
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "Add");
            this.imageListSmall.Images.SetKeyName(1, "Cancel");
            this.imageListSmall.Images.SetKeyName(2, "Heart");
            this.imageListSmall.Images.SetKeyName(3, "Tick");
            this.imageListSmall.Images.SetKeyName(4, "Coin");
            this.imageListSmall.Images.SetKeyName(5, "Lamp");
            // 
            // TabDescribedTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.olvTasks);
            this.Name = "TabDescribedTask";
            this.Size = new System.Drawing.Size(804, 499);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvTasks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Label label1;
        private BrightIdeasSoftware.ObjectListView olvTasks;
        private BrightIdeasSoftware.OLVColumn olvColumnTask;
        private BrightIdeasSoftware.OLVColumn olvColumnStatus;
        private System.Windows.Forms.ImageList imageListTasks;
        private System.Windows.Forms.CheckBox checkBoxIncomplete;
        private System.Windows.Forms.CheckBox checkBoxHighPriority;
        private BrightIdeasSoftware.OLVColumn olvColumnPriority;
        private BrightIdeasSoftware.OLVColumn olvColumnAction;
        private System.Windows.Forms.ImageList imageListSmall;
    }
}
