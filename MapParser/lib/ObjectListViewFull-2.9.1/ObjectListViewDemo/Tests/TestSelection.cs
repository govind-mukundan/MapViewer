/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/25/2008 JPP  Initial Version
 */

using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestOlvSelection
    {
        [SetUp]
        public void InitEachTest()
        {
            this.olv.SetObjects(PersonDb.All);
            this.olv.SelectedObjects = null;
        }

        [Test]
        public void TestSelectedObject()
        {
            Assert.IsNull(this.olv.SelectedObject);
            this.olv.SelectedObject = PersonDb.All[1];
            Assert.AreEqual(PersonDb.All[1], this.olv.SelectedObject);
        }

        [Test]
        public void TestSelectedObjects()
        {
            Assert.IsEmpty(this.olv.SelectedObjects);
            this.olv.SelectedObjects = PersonDb.All;
            Assert.AreEqual(PersonDb.All, this.olv.SelectedObjects);
            this.olv.SelectedObjects = null;
            Assert.IsEmpty(this.olv.SelectedObjects);
        }

        [Test]
        public void TestSelectAll()
        {
            this.olv.SelectAll();
            Assert.AreEqual(PersonDb.All, this.olv.SelectedObjects);
        }

        [Test]
        public void TestDeselectAll()
        {
            this.olv.SelectedObject = PersonDb.All[1];
            Assert.IsNotEmpty(this.olv.SelectedObjects);
            this.olv.DeselectAll();
            Assert.IsEmpty(this.olv.SelectedObjects);

            this.olv.SelectAll();
            Assert.IsNotEmpty(this.olv.SelectedObjects);
            this.olv.DeselectAll();
            Assert.IsEmpty(this.olv.SelectedObjects);
        }

        [Test]
        public void TestSortingShouldNotRaiseSelectionChangedEvents() {
            this.olv.SelectionChanged += new EventHandler(olv_SelectionChanged);
            this.olv.SelectedObjects = PersonDb.All;
            Application.RaiseIdle(new EventArgs());
            countSelectionChanged = 0;

            this.olv.Sort(this.olv.GetColumn(1));

            // Force an idle cycle so the selection changed event is processed
            Application.RaiseIdle(new EventArgs());

            Assert.AreEqual(0, countSelectionChanged);

            // Cleanup
            this.olv.SelectionChanged -= new EventHandler(olv_SelectionChanged);
        }

        [Test]
        public void TestAddingAColumnShouldNotRaiseSelectionChangedEvents() {
            this.olv.SelectionChanged += new EventHandler(olv_SelectionChanged);
            this.olv.SelectedObjects = PersonDb.All;
            Application.RaiseIdle(new EventArgs());
            countSelectionChanged = 0;

            this.olv.RebuildColumns();

            // Force an idle cycle so the selection changed event is processed
            Application.RaiseIdle(new EventArgs());

            Assert.AreEqual(0, countSelectionChanged);

            // Cleanup
            this.olv.SelectionChanged -= new EventHandler(olv_SelectionChanged);
        }

        [Test]
        public void TestSelectionEvents() {
            countSelectedIndexChanged = 0;
            countSelectionChanged = 0;
            this.olv.SelectedIndexChanged += new EventHandler(olv_SelectedIndexChanged);
            this.olv.SelectionChanged += new EventHandler(olv_SelectionChanged);
            this.olv.SelectedObjects = PersonDb.All;
            this.olv.SelectedObjects = null;

            // Force an idle cycle so the selection changed event is processed
            Application.RaiseIdle(new EventArgs());

            // On a virtual list, deselecting everything only triggers one event, but on
            // normal lists, there should two selectedIndex events for each object.
            // Regardless of anything, there should be only one selection changed event.
            if (this.olv.VirtualMode)
                Assert.AreEqual(PersonDb.All.Count + 1, countSelectedIndexChanged);
            else
                Assert.AreEqual(PersonDb.All.Count * 2, countSelectedIndexChanged);
            Assert.AreEqual(1, countSelectionChanged);

            // Cleanup
            this.olv.SelectedIndexChanged -= new EventHandler(olv_SelectedIndexChanged);
            this.olv.SelectionChanged -= new EventHandler(olv_SelectionChanged);
        }

        void olv_SelectedIndexChanged(object sender, EventArgs e)
        {
            countSelectedIndexChanged++;
        }
        int countSelectedIndexChanged;

        protected void olv_SelectionChanged(object sender, EventArgs e)
        {
            countSelectionChanged++;
        }
        protected int countSelectionChanged;

        [TestFixtureSetUp]
        public void Init()
        {
            this.olv = MyGlobals.mainForm.objectListView1;
        }
        protected ObjectListView olv;
    }

    [TestFixture]
    public class TestFastOlvSelection : TestOlvSelection
    {
        [TestFixtureSetUp]
        new public void Init()
        {
            this.olv = MyGlobals.mainForm.fastObjectListView1;
        }
    }

    [TestFixture]
    public class TestTreeListViewSelection : TestOlvSelection
    {
        [TestFixtureSetUp]
        new public void Init()
        {
            this.olv = this.treeListView = MyGlobals.mainForm.treeListView1;
            this.treeListView.CanExpandGetter = delegate(Object x) {
                return ((Person)x).Children.Count > 0;
            };
            this.treeListView.ChildrenGetter = delegate(Object x) {
                return ((Person)x).Children;
            };
        }
        private TreeListView treeListView;

        [Test]
        public void TestCollapseExpandShouldNotRaiseSelectionChangedEvents() {
            this.olv.SelectedObjects = PersonDb.All;
            Application.RaiseIdle(new EventArgs());
            this.olv.SelectionChanged += new EventHandler(olv_SelectionChanged);
            countSelectionChanged = 0;

            this.treeListView.Expand(PersonDb.All[0]);
            this.treeListView.ExpandAll();

            // Force an idle cycle so the selection changed event is processed
            Application.RaiseIdle(new EventArgs());

            Assert.AreEqual(0, countSelectionChanged);

            // Cleanup
            this.treeListView.CollapseAll();

            this.olv.SelectionChanged -= new EventHandler(olv_SelectionChanged);
            this.treeListView.CanExpandGetter = null;
            this.treeListView.ChildrenGetter = null;
        }
    }
}
