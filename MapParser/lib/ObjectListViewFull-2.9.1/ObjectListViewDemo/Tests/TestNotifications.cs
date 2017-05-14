/*
 * TestNotifications - Test that INotifyPropertyChanged subscriptions are correctly managed
 *
 * Author: Phillip Piper
 * Date: 4 June 2012, 8:39am
 *
 * Change log:
 * 2012-06-04  JPP  Initial code
 * 
 * Copyright (C) 2012 Phillip Piper
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * If you wish to use this code in a closed source application, please contact phillip.piper@gmail.com.
 */

using System.Drawing;
using NUnit.Framework;
using System.Collections.Generic;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestNotifications
    {
        [SetUp]
        public void InitEachTest() {
            mainForm = new MainForm();
            mainForm.Size = new Size();
            mainForm.Show();
            this.olv = GetObjectListView();
            this.olv.UseNotifyPropertyChanged = true;
        }
        protected ObjectListView olv;
        protected MainForm mainForm;

        protected virtual ObjectListView GetObjectListView() {
            return mainForm.objectListView1;
        }

        [TearDown]
        public void TestTearDown() {
            PersonDb.Reset();
            mainForm.Close();
        }

        [Test]
        public void Test_UseNotifyPropertyChangedIsTrue_PropertyChange_CellChanges() {
            this.olv.SetObjects(PersonDb.All);
            this.SetAndCheckOccupationColumnValue(1, "new occupation 2");
        }

        [Test]
        public void Test_UseNotifyPropertyChangedIsFalse_PropertyChange_CellDoesntChanges() {
            // This test can't work on virtual lists, since the list doesn't hold any text itself
            // but fetches it as required from the underlying models. So, we can never test for
            // a difference between the list's value and the model's value.
            if (this.olv.VirtualMode)
                return;

            this.olv.UseNotifyPropertyChanged = false;
            this.olv.SetObjects(PersonDb.All);
            PersonDb.All[1].Occupation = "new value 2";
            int row = this.olv.IndexOf(PersonDb.All[1]);
            Assert.AreNotEqual("new value 2", this.olv.GetItem(row).SubItems[1].Text);
        }

        private void SetAndCheckOccupationColumnValue(int personIndex, string newOccupation) {
            PersonDb.All[personIndex].Occupation = newOccupation;
            int row = this.olv.IndexOf(PersonDb.All[personIndex]);
            Assert.AreEqual(newOccupation, this.olv.GetItem(row).SubItems[1].Text);
        }

        [Test]
        public void Test_SetObjects_SubscriptionCountMaintained() {
            this.olv.SetObjects(PersonDb.All);
            foreach (Person x in PersonDb.All)
                Assert.AreEqual(1, x.CountNotifyPropertyChangedSubscriptions);
        }

        [Test]
        public void Test_SetObjects_Subset_SubscriptionCountMaintained() {
            this.olv.SetObjects(PersonDb.All);
            List<Person> subset = new List<Person>();
            subset.Add(PersonDb.All[0]);
            subset.Add(PersonDb.All[2]);
            subset.Add(PersonDb.All[4]);
            this.olv.SetObjects(subset);

            Assert.AreEqual(1, PersonDb.All[0].CountNotifyPropertyChangedSubscriptions);
            Assert.AreEqual(1, PersonDb.All[2].CountNotifyPropertyChangedSubscriptions);
            Assert.AreEqual(1, PersonDb.All[4].CountNotifyPropertyChangedSubscriptions);

            Assert.AreEqual(0, PersonDb.All[1].CountNotifyPropertyChangedSubscriptions);
            Assert.AreEqual(0, PersonDb.All[3].CountNotifyPropertyChangedSubscriptions);
            Assert.AreEqual(0, PersonDb.All[5].CountNotifyPropertyChangedSubscriptions);
        }

        [Test]
        public void Test_SetObjects_Superset_SubscriptionCountMaintained() {
            List<Person> subset = new List<Person>(); 
            subset.Add(PersonDb.All[0]);
            subset.Add(PersonDb.All[2]);
            subset.Add(PersonDb.All[4]);
            this.olv.SetObjects(subset);

            this.olv.SetObjects(PersonDb.All);

            foreach (Person x in PersonDb.All)
                Assert.AreEqual(1, x.CountNotifyPropertyChangedSubscriptions);
        }

        [Test]
        public void Test_SetObjects_Multiple_SubscriptionCountMaintained() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.SetObjects(PersonDb.All);
            this.olv.SetObjects(PersonDb.All);
            foreach (Person x in PersonDb.All)
                Assert.AreEqual(1, x.CountNotifyPropertyChangedSubscriptions);
        }

        [Test]
        public void Test_SetObjects_WithFiltering_SubscriptionCountMaintained() {
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return false; });
            this.olv.SetObjects(PersonDb.All);
            Assert.AreEqual(0, this.olv.GetItemCount());
            foreach (Person x in PersonDb.All)
                Assert.AreEqual(1, x.CountNotifyPropertyChangedSubscriptions);
            this.olv.UseFiltering = false;
        }

        [Test]
        public void Test_SetObjects_Unsubscribes() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.SetObjects(null);
            foreach (Person x in PersonDb.All)
                Assert.AreEqual(0, x.CountNotifyPropertyChangedSubscriptions);
        }

        [Test]
        public void Test_AddObject_SubscribesToChanges() {
            this.olv.SetObjects(null);
            this.olv.AddObject(PersonDb.All[0]);
            this.olv.AddObject(PersonDb.All[1]);
            this.olv.AddObject(PersonDb.All[2]);
            this.SetAndCheckOccupationColumnValue(0, "new name 2");
            this.SetAndCheckOccupationColumnValue(2, "new name 3");
        }

        [Test]
        public void Test_RemoveObject_SubscriptionsRemoved() {
            this.olv.SetObjects(PersonDb.All);
            Assert.AreNotEqual(0, PersonDb.All[1].CountNotifyPropertyChangedSubscriptions);
            this.olv.RemoveObject(PersonDb.All[1]);
            Assert.AreEqual(0, PersonDb.All[1].CountNotifyPropertyChangedSubscriptions);
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

    }

    [TestFixture]
    public class TestFastTestNotifications : TestNotifications
    {
        protected override ObjectListView GetObjectListView() {
            return mainForm.fastObjectListView1;
        }
    }

    [TestFixture]
    public class TestTreeListViewTestNotifications : TestNotifications
    {
        protected override ObjectListView GetObjectListView() {
            return mainForm.treeListView1;
        }
    }
}
