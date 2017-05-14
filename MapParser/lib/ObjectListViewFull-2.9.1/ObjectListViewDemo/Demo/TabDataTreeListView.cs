using System;
using System.Data;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ObjectListViewDemo
{
    public partial class TabDataTreeListView : OlvDemoTab {

        public TabDataTreeListView()
        {
            InitializeComponent();
            this.ListView = this.olvDataTree;
        }

        protected override void InitializeTab() {

            // The whole point of a DataTreeListView is to write no code. So there is very little code here.

            // Put some images against each row
            this.olvColumn41.ImageGetter = delegate(object row) { return "user"; };

            // The DataTreeListView needs to know the key that identifies root level objects.
            // DataTreeListView can handle that key being any data type, but the Designer only deals in strings.
            // Since we want a non-string value to identify keys, we have to set it explicitly here.
            this.olvDataTree.RootKeyValue = 0u;

            // Finally load the data into the UI
            LoadXmlIntoTreeDataListView();

            // This does a better job of auto sizing the columns
            this.olvDataTree.AutoResizeColumns();
        }

        private void LoadXmlIntoTreeDataListView() {
            DataSet ds = Coordinator.LoadDatasetFromXml(@"Data\FamilyTree.xml");

            if (ds.Tables.Count <= 0) {
                Coordinator.ShowMessage(@"Failed to load data set from Data\FamilyTree.xml");
                return;
            }

            this.dataGridView2.DataSource = ds;
            this.dataGridView2.DataMember = "Person";

            // Like DataListView, the DataTreeListView can handle binding to a variety of sources
            // And again, you could create a BindingSource in the designer, and assign that BindingSource
            // to DataSource, removing the need to even write these few lines of code.

            //this.olvDataTree.DataSource = new BindingSource(ds, "Person");
            //this.olvDataTree.DataSource = ds.Tables["Person"];
            //this.olvDataTree.DataSource = new DataView(ds.Tables["Person"]);
            //this.olvDataTree.DataMember = "Person"; this.olvDataTree.DataSource = ds;
            this.olvDataTree.DataMember = "Person";
            this.olvDataTree.DataSource = new DataViewManager(ds);
        }

        #region UI event handlers

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            Coordinator.TimedFilter(this.ListView, ((TextBox)sender).Text);
        }

        private void buttonResetData_Click(object sender, EventArgs e)
        {
            LoadXmlIntoTreeDataListView();
        }

        #endregion
    }
}
