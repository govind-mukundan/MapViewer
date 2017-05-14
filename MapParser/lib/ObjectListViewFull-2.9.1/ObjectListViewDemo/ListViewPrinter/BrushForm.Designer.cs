namespace BrightIdeasSoftware
{
    partial class BrushForm
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
            if (disposing && (components != null)) {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbHatch = new System.Windows.Forms.RadioButton();
            this.rbGradient = new System.Windows.Forms.RadioButton();
            this.rbSolid = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.examplePanel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbNone);
            this.groupBox1.Controls.Add(this.rbHatch);
            this.groupBox1.Controls.Add(this.rbGradient);
            this.groupBox1.Controls.Add(this.rbSolid);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Style";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(261, 20);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 4;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "&None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
            // 
            // rbHatch
            // 
            this.rbHatch.AutoSize = true;
            this.rbHatch.Location = new System.Drawing.Point(173, 19);
            this.rbHatch.Name = "rbHatch";
            this.rbHatch.Size = new System.Drawing.Size(54, 17);
            this.rbHatch.TabIndex = 2;
            this.rbHatch.TabStop = true;
            this.rbHatch.Text = "&Hatch";
            this.rbHatch.UseVisualStyleBackColor = true;
            this.rbHatch.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
            // 
            // rbGradient
            // 
            this.rbGradient.AutoSize = true;
            this.rbGradient.Location = new System.Drawing.Point(75, 20);
            this.rbGradient.Name = "rbGradient";
            this.rbGradient.Size = new System.Drawing.Size(65, 17);
            this.rbGradient.TabIndex = 1;
            this.rbGradient.TabStop = true;
            this.rbGradient.Text = "&Gradient";
            this.rbGradient.UseVisualStyleBackColor = true;
            this.rbGradient.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
            // 
            // rbSolid
            // 
            this.rbSolid.AutoSize = true;
            this.rbSolid.Location = new System.Drawing.Point(7, 20);
            this.rbSolid.Name = "rbSolid";
            this.rbSolid.Size = new System.Drawing.Size(48, 17);
            this.rbSolid.TabIndex = 0;
            this.rbSolid.TabStop = true;
            this.rbSolid.Text = "&Solid";
            this.rbSolid.UseVisualStyleBackColor = true;
            this.rbSolid.CheckedChanged += new System.EventHandler(this.rbCheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.propertyGrid1);
            this.groupBox2.Location = new System.Drawing.Point(12, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 237);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Properties";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.CommandsVisibleIfAvailable = false;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(7, 20);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.Size = new System.Drawing.Size(305, 211);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            this.propertyGrid1.SelectedObjectsChanged += new System.EventHandler(this.propertyGrid1_SelectedObjectsChanged);
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.examplePanel);
            this.groupBox3.Location = new System.Drawing.Point(12, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(318, 76);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Example";
            // 
            // examplePanel
            // 
            this.examplePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.examplePanel.Location = new System.Drawing.Point(7, 20);
            this.examplePanel.Name = "examplePanel";
            this.examplePanel.Size = new System.Drawing.Size(305, 50);
            this.examplePanel.TabIndex = 0;
            this.examplePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.examplePanel_Paint);
            // 
            // BrushForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 403);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BrushForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "BrushForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel examplePanel;
        protected System.Windows.Forms.RadioButton rbHatch;
        protected System.Windows.Forms.RadioButton rbGradient;
        protected System.Windows.Forms.RadioButton rbSolid;
        protected System.Windows.Forms.RadioButton rbNone;
        protected System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}