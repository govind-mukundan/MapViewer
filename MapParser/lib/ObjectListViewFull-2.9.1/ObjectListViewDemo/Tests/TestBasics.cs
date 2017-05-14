/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/25/2008 11:06 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/25/2008 JPP  Initial Version
 */

using System.Collections;
using System.Drawing;
using NUnit.Framework;
using System.Collections.Generic;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestOlvBasics
    {
        [SetUp]
        public void SetupTest() {
            PersonDb.Reset();
            mainForm = new MainForm();
            mainForm.Size = new Size();
            mainForm.Show();
            this.olv = GetObjectListView();
        }

        protected virtual ObjectListView GetObjectListView() {
            return mainForm.objectListView1;
        }

        [TearDown]
        public void TearDownTest() {
            mainForm.Close();
        }
        protected ObjectListView olv;
        protected MainForm mainForm;

        [Test]
        public void Test_SetObjects_All() {
            this.olv.SetObjects(PersonDb.All);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public void Test_SetObjects_Null() {
            this.olv.SetObjects(null);
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public void Test_GetModelObject() {
            this.olv.SetObjects(PersonDb.All);
            for (int i = 0; i < PersonDb.All.Count; i++)
                Assert.AreEqual(PersonDb.All[i], this.olv.GetModelObject(i));
        }

        [Test]
        public void Test_AddObject() {
            this.olv.SetObjects(null);
            this.olv.AddObject(PersonDb.All[0]);
            Assert.AreNotEqual(-1, this.olv.IndexOf(PersonDb.All[0]));
            this.olv.AddObject(PersonDb.All[1]);
            Assert.AreNotEqual(-1, this.olv.IndexOf(PersonDb.All[1]));
            Assert.AreEqual(2, this.olv.GetItemCount());
        }

        [Test]
        public void Test_AddObjects() {
            this.olv.SetObjects(null);
            this.olv.AddObjects(PersonDb.All);
            foreach (object x in PersonDb.All)
                Assert.AreNotEqual(-1, this.olv.IndexOf(x));
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_AddObject_ModelFilter() {
            ArrayList somePeople = new ArrayList(PersonDb.All);
            Person first = PersonDb.All[0];
            somePeople.Remove(first);
            this.olv.SetObjects(somePeople);

            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return false; });

            this.olv.AddObject(first);

            this.olv.UseFiltering = false;
            ArrayList contents = ObjectListView.EnumerableToArray(this.olv.Objects, false);
            Assert.AreEqual(somePeople.Count + 1, contents.Count);

            // The added object should have been added at the end of all contents,
            // not at the end of the filtered contents
            Assert.AreEqual(somePeople.Count, this.olv.IndexOf(first));
        }

        [Test]
        public void Test_RemoveObject() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.RemoveObject(PersonDb.All[1]);
            Assert.AreEqual(-1, this.olv.IndexOf(PersonDb.All[1]));
            Assert.AreEqual(PersonDb.All.Count - 1, this.olv.GetItemCount());
        }

        [Test]
        public void Test_RemoveObjects() {
            this.olv.SetObjects(PersonDb.All);
            List<Person> toRemove = new List<Person>();
            toRemove.Add(PersonDb.All[1]);
            toRemove.Add(PersonDb.All[2]);
            toRemove.Add(PersonDb.All[5]);
            this.olv.RemoveObjects(toRemove);
            foreach (Person x in toRemove)
                Assert.AreEqual(-1, this.olv.IndexOf(x));
            Assert.AreEqual(PersonDb.All.Count - toRemove.Count, this.olv.GetItemCount());
        }

        [Test]
        public void Test_EffectiveRowHeight() {
            this.olv.RowHeight = 32;
            Assert.AreEqual(32, this.olv.RowHeightEffective);
            this.olv.RowHeight = -1;
        }

        [Test]
        public void Test_RefreshObject() {
            this.olv.SetObjects(PersonDb.All);
            Person another = new Person(PersonDb.All[1].Name);
            another.Occupation = "a new occupation";

            OLVListItem item = this.olv.ModelToItem(another);
            Assert.IsNotNull(item);
            Assert.AreNotEqual(another.Occupation, item.SubItems[1].Text);

            this.olv.RefreshObject(another);

            OLVListItem item2 = this.olv.ModelToItem(another);
            Assert.IsNotNull(item2);
            Assert.AreEqual(another.Occupation, item2.SubItems[1].Text);
        }

        [Test]
        public void Test_UpdateObject_AddsWhenNew() {
            List<Person> people = new List<Person>();
            people.Add(PersonDb.All[0]);
            people.Add(PersonDb.All[1]);
            people.Add(PersonDb.All[2]);
            this.olv.SetObjects(people);

            Person newGuy = PersonDb.All[3];
            OLVListItem item = this.olv.ModelToItem(newGuy);
            Assert.IsNull(item);

            this.olv.UpdateObject(newGuy);

            OLVListItem item2 = this.olv.ModelToItem(newGuy);
            Assert.IsNotNull(item2);
            Assert.AreEqual(newGuy.Occupation, item2.SubItems[1].Text);
        }

        [Test]
        public void Test_UpdateObject_AddsWhenNew_WithFilterInstalled() {
            List<Person> people = new List<Person>();
            people.Add(PersonDb.All[0]);
            people.Add(PersonDb.All[1]);
            people.Add(PersonDb.All[2]);
            this.olv.SetObjects(people);

            Person newGuy = PersonDb.All[3];
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return ((Person) x).Name == newGuy.Name; });
            Assert.AreEqual(0, this.olv.GetItemCount());

            this.olv.UpdateObject(newGuy);

            OLVListItem item2 = this.olv.ModelToItem(newGuy);
            Assert.IsNotNull(item2);
            Assert.AreEqual(newGuy.Occupation, item2.SubItems[1].Text);
        }

    }

    [TestFixture]
    public class TestFastOlvBasics : TestOlvBasics
    {
        protected override ObjectListView GetObjectListView() {
            return mainForm.fastObjectListView1;
        }
    }

    [TestFixture]
    public class TestTreeListViewBasics : TestOlvBasics
    {
        protected override ObjectListView GetObjectListView() {
            return mainForm.treeListView1;
        }
    }
}
