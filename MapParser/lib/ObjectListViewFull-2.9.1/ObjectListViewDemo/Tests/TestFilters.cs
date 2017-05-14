
using System.Collections;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestFilters
    {
        [SetUp]
        public void SetupTest() {
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

        [Test]
        public void Test_Filter_UseFiltering() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return false; });

            this.olv.UseFiltering = true;
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ModelFilter_None() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            Assert.AreNotEqual(0, this.olv.GetItemCount());

            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return false; });
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ModelFilter_All() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            int originalCount = this.olv.GetItemCount();

            this.olv.ModelFilter = new ModelFilter(delegate(object x) { return true; });
            Assert.AreEqual(originalCount, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_None() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            Assert.AreNotEqual(0, this.olv.GetItemCount());

            this.olv.ListFilter = new ListFilter(delegate(IEnumerable x) { return new ArrayList(); });
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_All() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            int originalCount = this.olv.GetItemCount();

            this.olv.ListFilter = new ListFilter(delegate(IEnumerable x) { return x; });
            Assert.AreEqual(originalCount, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_Tail() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ListFilter = new TailFilter(2);
            Assert.AreEqual(2, this.olv.GetItemCount());
            Assert.AreEqual(PersonDb.All[PersonDb.All.Count - 2], this.olv.GetModelObject(0));
            Assert.AreEqual(PersonDb.All[PersonDb.All.Count - 1], this.olv.GetModelObject(1));
        }

        [Test]
        public virtual void Test_ListFilter_Tail_Zero() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ListFilter = new TailFilter(0);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_ListFilter_Tail_Large() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ListFilter = new TailFilter(1000000);
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_CaseInsensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant());
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant(), StringComparison.InvariantCulture);
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = TextMatchFilter.Prefix(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(0, 4));
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(0, 4));
            filter.StringComparison = StringComparison.InvariantCulture;
            this.olv.ModelFilter = filter;
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Prefix_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = TextMatchFilter.Prefix(this.olv, PersonDb.LastAlphabeticalName.ToLowerInvariant().Substring(1, 4));
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = new TextMatchFilter(this.olv, PersonDb.LastAlphabeticalName + "WILL NOT MATCH");
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = TextMatchFilter.Regex(this.olv, "[z]+");
            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_CaseInsensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "Z+");
            filter.StringComparison = StringComparison.CurrentCultureIgnoreCase;
            this.olv.ModelFilter = filter;

            Assert.AreEqual(1, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_CaseSensitive() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "Z+");
            filter.StringComparison = StringComparison.CurrentCulture;
            this.olv.ModelFilter = filter;
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Regex_Invalid() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            this.olv.ModelFilter = TextMatchFilter.Regex(this.olv, @"[\*");
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Columns() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "occup");
            filter.Columns = new OLVColumn[] { this.olv.GetColumn(1), this.olv.GetColumn(2) };
            this.olv.ModelFilter = filter;
            Assert.AreEqual(PersonDb.All.Count, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_Columns_NoMatch() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.UseFiltering = true;
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "occup");
            filter.Columns = new OLVColumn[] { this.olv.GetColumn(0), this.olv.GetColumn(2) };
            this.olv.ModelFilter = filter;
            Assert.AreEqual(0, this.olv.GetItemCount());
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text() {
            TextMatchFilter filter = new TextMatchFilter(this.olv, "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(2, ranges.Count);
            Assert.AreEqual(2, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(7, ranges[1].First);
            Assert.AreEqual(3, ranges[1].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "abc", "DE");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCDE"));
            Assert.AreEqual(3, ranges.Count);
            Assert.AreEqual(2, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(7, ranges[1].First);
            Assert.AreEqual(3, ranges[1].Length);
            Assert.AreEqual(10, ranges[2].First);
            Assert.AreEqual(2, ranges[2].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text_NoMatch() {
            TextMatchFilter filter = new TextMatchFilter(this.olv, "xyz");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Text_NoMatch_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Contains(this.olv, "xyz", "jpp");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_StringStart() {
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("abcd-ABCD"));
            Assert.AreEqual(1, ranges.Count);
            Assert.AreEqual(0, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_StringStart_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, "xyz", "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("abcd-ABCD"));
            Assert.AreEqual(1, ranges.Count);
            Assert.AreEqual(0, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_StringStart_NoMatch() {
            TextMatchFilter filter = TextMatchFilter.Prefix(this.olv, "abc");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Regex() {
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "[abcd]+");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("nada-abcd-ab-ABCD"));
            Assert.AreEqual(4, ranges.Count);
            Assert.AreEqual(1, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(5, ranges[1].First);
            Assert.AreEqual(4, ranges[1].Length);
            Assert.AreEqual(10, ranges[2].First);
            Assert.AreEqual(2, ranges[2].Length);
            Assert.AreEqual(13, ranges[3].First);
            Assert.AreEqual(4, ranges[3].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Regex_Multiple() {
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "x.*z", "a.*c");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("rst-ABC-rst-xyz"));
            Assert.AreEqual(2, ranges.Count);
            Assert.AreEqual(12, ranges[0].First);
            Assert.AreEqual(3, ranges[0].Length);
            Assert.AreEqual(4, ranges[1].First);
            Assert.AreEqual(3, ranges[1].Length);
        }

        [Test]
        public virtual void Test_TextFilter_FindAll_Regex_NoMatch() {
            TextMatchFilter filter = TextMatchFilter.Regex(this.olv, "[yz]+");
            List<CharacterRange> ranges = new List<CharacterRange>(filter.FindAllMatchedRanges("x-abcd-ABCD"));
            Assert.AreEqual(0, ranges.Count);
        }

        [TestFixtureSetUp]
        public void Init() {
            this.olv = MyGlobals.mainForm.objectListView1;
        }

        protected ObjectListView olv;
        protected MainForm mainForm;
    }

    [TestFixture]
    public class TestFastFilters : TestFilters
    {
        protected override ObjectListView GetObjectListView() {
            return mainForm.fastObjectListView1;
        }
    }

    [TestFixture]
    public class TestTreeListFilters : TestFilters
    {
        // ListFilters don't work on TreeListViews

        [Test]
        override public void Test_ListFilter_None() {
        }

        [Test]
        override public void Test_ListFilter_All() {
        }

        [Test]
        override public void Test_ListFilter_Tail() {
        }

        [Test]
        override public void Test_ListFilter_Tail_Zero() {
        }

        [Test]
        override public void Test_ListFilter_Tail_Large() {
        }

        protected override ObjectListView GetObjectListView() {
            return mainForm.treeListView1;
        }
    }
}
