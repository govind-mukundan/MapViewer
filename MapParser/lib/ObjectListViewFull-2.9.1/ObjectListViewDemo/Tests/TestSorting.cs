/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 11/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 11/25/2008 JPP  Initial Version
 */

using System;
using System.Collections;
using System.Windows.Forms;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestSorting
    {
        [SetUp]
        public void InitEachTest()
        {
            this.olv.LastSortColumn = null;
            this.olv.LastSortOrder = SortOrder.None;
            this.olv.SetObjects(PersonDb.All);
        }

        [TearDown]
        public void TearDownEachTest()
        {
        }

        [Test]
        public void TestInitialConditions()
        {
        }

        [Test]
        public void TestSecondarySorting()
        {
            this.olv.SecondarySortColumn = this.olv.GetColumn(0);
            this.olv.SecondarySortOrder = SortOrder.Descending;
            this.olv.Sort(this.olv.GetColumn(3), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.LastAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);

            this.olv.SecondarySortColumn = this.olv.GetColumn(0);
            this.olv.SecondarySortOrder = SortOrder.Ascending;
            this.olv.Sort(this.olv.GetColumn(3), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
        }

        [Test]
        public void TestSortingByStringColumn()
        {
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Descending);
            Assert.AreEqual(PersonDb.LastAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
        }

        [Test]
        public void TestSortingByIntColumn()
        {
            OLVColumn columnToSort = this.olv.GetColumn(2);
            this.olv.Sort(columnToSort, SortOrder.Ascending);
            Assert.AreEqual(PersonDb.All[PersonDb.All.Count - 1].Name, ((Person)this.olv.GetModelObject(0)).Name);
            this.olv.Sort(columnToSort, SortOrder.Descending);
            Assert.AreEqual(PersonDb.All[0].Name, ((Person)this.olv.GetModelObject(0)).Name);
        }

        [Test]
        public void TestNoSorting()
        {
            ArrayList beforeContents = GetContents();

            this.olv.Sort();

            Assert.AreEqual(beforeContents, GetContents());

            this.olv.LastSortColumn = this.olv.GetColumn(0);
            this.olv.LastSortOrder = SortOrder.Descending;
            this.olv.Sort();

            Assert.AreNotEqual(beforeContents, GetContents());
        }

        private ArrayList GetContents()
        {
            ArrayList contents = new ArrayList();
            for (int i = 0; i < this.olv.GetItemCount(); i++)
                contents.Add(this.olv.GetModelObject(i));
            return contents;
        }

        [Test]
        virtual public void TestCustomSorting()
        {
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);

            try {
                this.olv.CustomSorter = delegate(OLVColumn column, SortOrder order) {
                    this.olv.ListViewItemSorter = new ColumnComparer(new OLVColumn("dummy", "BirthDate"), SortOrder.Descending);
                };
                this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
                Assert.AreNotEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
            }
            finally {
                this.olv.CustomSorter = null;
            }
        }

        [Test]
        public void TestAfterSortingEvent()
        {
            try {
                this.olv.AfterSorting += new EventHandler<AfterSortingEventArgs>(olvAfterSorting1);
                this.afterSortingCount = 0;
                this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
                this.olv.Sort();
                this.olv.Sort(this.olv.GetColumn(0));
            }
            finally {
                this.olv.AfterSorting -= new EventHandler<AfterSortingEventArgs>(olvAfterSorting1);
            }
            Assert.AreEqual(3, this.afterSortingCount);
        }
        int afterSortingCount;

        void olvAfterSorting1(object sender, AfterSortingEventArgs e)
        {
            this.afterSortingCount++;
        }

        [Test]
        public void TestBeforeSortingEvent()
        {
            try {
                this.olv.BeforeSorting += new EventHandler<BeforeSortingEventArgs>(olvBeforeSorting1);
                this.olv.Sort(this.olv.GetColumn(2), SortOrder.Ascending);

                // The BeforeSorting event should have changed the sort to descending by name
                Assert.AreEqual(PersonDb.LastAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
            }
            finally {
                this.olv.BeforeSorting -= new EventHandler<BeforeSortingEventArgs>(olvBeforeSorting1);
            }
        }

        void olvBeforeSorting1(object sender, BeforeSortingEventArgs e)
        {
            Assert.AreEqual(this.olv.GetColumn(2), e.ColumnToSort);
            Assert.AreEqual(SortOrder.Ascending, e.SortOrder);

            e.ColumnToSort = this.olv.GetColumn(0);
            e.SortOrder = SortOrder.Descending;
        }

        [Test]
        public void TestCancelSorting()
        {
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Descending);
            Assert.AreEqual(PersonDb.LastAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);

            try {
                this.olv.BeforeSorting += new EventHandler<BeforeSortingEventArgs>(olvBeforeSorting2);
                this.olv.Sort(this.olv.GetColumn(2), SortOrder.Ascending);

                // The BeforeSorting event should have cancelled the sort so the second Sort() should not have had an effect
                Assert.AreEqual(PersonDb.LastAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
            }
            finally {
                this.olv.BeforeSorting -= new EventHandler<BeforeSortingEventArgs>(olvBeforeSorting2);
            }
        }

        void olvBeforeSorting2(object sender, BeforeSortingEventArgs e)
        {
            Assert.AreEqual(this.olv.GetColumn(2), e.ColumnToSort);
            Assert.AreEqual(SortOrder.Ascending, e.SortOrder);

            e.Canceled = true;
        }

        [Test]
        public void TestPreserveSelection()
        {
            this.olv.SelectedObject = PersonDb.All[0];
            this.olv.Sort(this.olv.GetColumn(2), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.All[0], this.olv.SelectedObject);
        }

        [Test]
        public void TestPreserveSelectionMultiple()
        {
            this.olv.SelectedObjects = PersonDb.All;
            this.olv.Sort(this.olv.GetColumn(1), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.All.Count, this.olv.SelectedObjects.Count);
            foreach (Object x in this.olv.SelectedObjects)
                Assert.Contains(x, PersonDb.All);
        }

        [Test]
        virtual public void TestUnsort() {
            this.olv.Sort(this.olv.GetColumn(0), SortOrder.Ascending);
            Assert.AreEqual(PersonDb.FirstAlphabeticalName, ((Person)this.olv.GetModelObject(0)).Name);
            this.olv.Unsort();
            Assert.IsNull(this.olv.PrimarySortColumn);
            //Assert.AreEqual(SortOrder.None, this.olv.PrimarySortOrder);
            Assert.AreEqual(PersonDb.All[0].Name, ((Person)this.olv.GetModelObject(0)).Name);
        }

        [TestFixtureSetUp]
        public void Init()
        {
            this.olv = MyGlobals.mainForm.objectListView1;
        }
        protected ObjectListView olv;
    }

    [TestFixture]
    public class TestFastOlvSorting : TestSorting
    {
        [Test]
        override public void TestCustomSorting()
        {
        }
        
        [Test]
        override public void TestUnsort() {
            // FastObjectListViews don't really support Unsort()
        }

        [TestFixtureSetUp]
        new public void Init()
        {
            this.olv = MyGlobals.mainForm.fastObjectListView1;
        }
    }
}
