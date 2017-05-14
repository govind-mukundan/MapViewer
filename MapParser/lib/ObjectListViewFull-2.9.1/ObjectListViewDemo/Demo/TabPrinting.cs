using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using BrightIdeasSoftware;
using ObjectListViewDemo.Properties;

namespace ObjectListViewDemo
{
    public partial class TabPrinting : OlvDemoTab {
        public TabPrinting() {
            InitializeComponent();
        }

        // Public fields (yuck) to avoid the baggage of wrapping a private field.
        // Necessary until I stop supporting .Net 2.0.

        public ObjectListView SimpleView;
        public ObjectListView ComplexView;
        public ObjectListView FileExplorerView;
        public ObjectListView DataListView;
        public ObjectListView TreeListView;

        protected override void InitializeTab() {

            // listViewPrinter1 is created as a component in the designer.
            // Set the print preview control's Document property to be the listViewPrinter.

            // Listen to these events to give feedback as the printing happens
            listViewPrinter1.PrintPage += delegate(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
                Coordinator.ToolStripStatus1 = String.Format("Printing page #{0}...", this.listViewPrinter1.PageNumber);
            };

            listViewPrinter1.EndPrint += delegate(object sender, System.Drawing.Printing.PrintEventArgs e) {
                Coordinator.ToolStripStatus1 = "Printing done";
            };

            // For some reason the Form Designer loses these settings
            this.printPreviewControl1.Zoom = 1;
            this.printPreviewControl1.AutoZoom = true;

            this.UpdatePrintPreview();
        }

        public void UpdatePrintPreview() {

            // Set the list view printer's ListView property to say which ObjectListView you want to print
            if (this.rbShowSimple.Checked == true)
                this.listViewPrinter1.ListView = this.SimpleView;
            else if (this.rbShowComplex.Checked == true)
                this.listViewPrinter1.ListView = this.ComplexView;
            else if (this.rbShowDataset.Checked == true)
                this.listViewPrinter1.ListView = this.DataListView;
            else if (this.rbShowTree.Checked == true)
                this.listViewPrinter1.ListView = this.TreeListView;
            else if (this.rbShowFileExplorer.Checked == true)
                this.listViewPrinter1.ListView = this.FileExplorerView;

            // Copy settings from UI onto the list view printer
            this.listViewPrinter1.DocumentName = this.tbTitle.Text;
            this.listViewPrinter1.Header = this.tbHeader.Text.Replace("\\t", "\t");
            this.listViewPrinter1.Footer = this.tbFooter.Text.Replace("\\t", "\t");
            this.listViewPrinter1.Watermark = this.tbWatermark.Text;
            this.listViewPrinter1.IsShrinkToFit = this.cbShrinkToFit.Checked;
            this.listViewPrinter1.IsTextOnly = !this.cbIncludeImages.Checked;
            this.listViewPrinter1.IsPrintSelectionOnly = this.cbPrintOnlySelection.Checked;
            this.listViewPrinter1.FirstPage = (int)this.numericFrom.Value;
            this.listViewPrinter1.LastPage = (int)this.numericTo.Value;

            // Give the list view printer the appropriate styling
            if (this.rbStyleMinimal.Checked)
                this.ApplyMinimalFormatting();
            else if (this.rbStyleModern.Checked)
                this.ApplyModernFormatting();
            else if (this.rbStyleTooMuch.Checked)
                this.ApplyOverTheTopFormatting();

            if (this.cbCellGridLines.Checked == false)
                this.listViewPrinter1.ListGridPen = null;

            // Finally, tell the print preview to redraw itself

            this.printPreviewControl1.InvalidatePreview();
        }

        private void ApplyMinimalFormatting() {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Tahoma", 9);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header();
            this.listViewPrinter1.HeaderFormat.TextBrush = Brushes.Black;
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = null;
            this.listViewPrinter1.HeaderFormat.SetBorderPen(Sides.Bottom, new Pen(Color.Black, 0.5f));

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer();
            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            Brush brush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.Gray, Color.White);
            this.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 2, brush);

            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader();
            this.listViewPrinter1.ListHeaderFormat.BackgroundBrush = null;

            this.listViewPrinter1.WatermarkFont = null;
            this.listViewPrinter1.WatermarkColor = Color.Empty;
        }

        private void ApplyModernFormatting() {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Ms Sans Serif", 9);
            this.listViewPrinter1.ListGridPen = new Pen(Color.DarkGray, 0.5f);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header(new Font("Verdana", 24, FontStyle.Bold));
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.DarkBlue, Color.White);

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer();
            this.listViewPrinter1.FooterFormat.BackgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.White, Color.Blue);

            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(new Font("Verdana", 12));

            this.listViewPrinter1.WatermarkFont = null;
            this.listViewPrinter1.WatermarkColor = Color.Empty;
        }

        private void ApplyOverTheTopFormatting() {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Ms Sans Serif", 9);
            this.listViewPrinter1.ListGridPen = new Pen(Color.Blue, 0.5f);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header(new Font("Comic Sans MS", 36));
            this.listViewPrinter1.HeaderFormat.TextBrush = new LinearGradientBrush(new Point(0, 0), new Point(900, 0), Color.Black, Color.Blue);
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = new TextureBrush(Resource1.star16, WrapMode.Tile);
            this.listViewPrinter1.HeaderFormat.SetBorder(Sides.All, 10, new LinearGradientBrush(new Point(0, 0), new Point(300, 0), Color.Purple, Color.Pink));

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer(new Font("Comic Sans MS", 12));
            this.listViewPrinter1.FooterFormat.TextBrush = Brushes.Blue;
            this.listViewPrinter1.FooterFormat.BackgroundBrush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.Gold, Color.Green);
            this.listViewPrinter1.FooterFormat.SetBorderPen(Sides.All, new Pen(Color.FromArgb(128, Color.Green), 5));

            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            Brush brush = new HatchBrush(HatchStyle.LargeConfetti, Color.Blue, Color.Empty);
            this.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 5, brush);

            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(new Font("Comic Sans MS", 12));
            this.listViewPrinter1.ListHeaderFormat.BackgroundBrush = Brushes.PowderBlue;
            this.listViewPrinter1.ListHeaderFormat.TextBrush = Brushes.Black;

            this.listViewPrinter1.WatermarkFont = new Font("Comic Sans MS", 72);
            this.listViewPrinter1.WatermarkColor = Color.Red;
        }

        private void buttonPageSetup_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PageSetup();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintPreview();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintWithDialog();
        }

        private void rbShowSimple_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void rbShowComplex_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void rbShowDataset_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void rbShowFileExplorer_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void rbStyleMinimal_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void rbStyleModern_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void rbStyleTooMuch_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void cbIncludeImages_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void cbShrinkToFit_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void cbPrintOnlySelection_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void cbCellGridLines_CheckedChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void numericFrom_ValueChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void numericTo_ValueChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void tbTitle_TextChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void tbHeader_TextChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void tbFooter_TextChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }

        private void tbWatermark_TextChanged(object sender, EventArgs e) {
            this.UpdatePrintPreview();
        }
    }
}
