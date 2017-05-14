
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;


namespace BrightIdeasSoftware.Tests
{
    class GeneratorTestModelEmpty
    {
    }

    class GeneratorTestModel1
    {
        [OLVColumn("Primary",
            AspectToStringFormat = "AspectToStringFormat",
            CheckBoxes = true,
            DisplayIndex = 0,
            FillsFreeSpace = true,
            FreeSpaceProportion = 98,
            GroupWithItemCountFormat = "GroupWithItemCountFormat",
            GroupWithItemCountSingularFormat = "GroupWithItemCountSingularFormat",
            Hyperlink = true,
            ImageAspectName = "ImageAspectName",
            IsEditable = true,
            IsTileViewColumn = true,
            IsVisible = true,
            MaximumWidth = 1000,
            MinimumWidth = 100,
            Name="ColumnName",
            Tag = "Tag",
            TextAlign = HorizontalAlignment.Right,
            ToolTipText = "ToolTipText",
            TriStateCheckBoxes = true,
            UseInitialLetterForGroup = true,
            Width = 500)]
        public string OLVPrimaryColumn {
            get { return ""; }
            set { }
        }

        public string NotUsedProperty {
            get { return ""; }
            set { }
        }

        [OLVColumn("Secondary",
            AspectToStringFormat = "!AspectToStringFormat",
            CheckBoxes = false,
            DisplayIndex = 1,
            FillsFreeSpace = false,
            GroupWithItemCountFormat = "!GroupWithItemCountFormat",
            GroupWithItemCountSingularFormat = "!GroupWithItemCountSingularFormat",
            Hyperlink = false,
            ImageAspectName = "!ImageAspectName",
            IsEditable = false,
            IsTileViewColumn = false,
            IsVisible = false,
            MaximumWidth = -1,
            MinimumWidth = -1,
            Tag = "!Tag",
            TextAlign = HorizontalAlignment.Left,
            ToolTipText = "!ToolTipText",
            TriStateCheckBoxes = false,
            UseInitialLetterForGroup = false,
            Width = -1)]
        public string OLVSecondaryColumn {
            get { return ""; }
            set { }
        }

    }

    class GeneratorTestModelSorting
    {
        [OLVColumn("Secondary", DisplayIndex = 1)]
        public string OLVSecondaryColumn {
            get { return ""; }
            set { }
        }

        [OLVColumn("Primary", DisplayIndex = 0)]
        public string OLVShouldBeFirst {
            get { return ""; }
            set { }
        }

        [OLVColumn("SecondLast", DisplayIndex = 3)]
        public string OLVSecondLast {
            get { return ""; }
            set { }
        }

        [OLVColumn("Last")]
        public string OLVMustBeLast {
            get { return ""; }
            set { }
        }

        [OLVColumn("Hidden", DisplayIndex = 2, IsVisible=false)]
        public string OLVMustNotBeVisible {
            get { return ""; }
            set { }
        }
    }


    class GeneratorTestModelGroupies
    {
        [OLVColumn("Secondary", 
            DisplayIndex = 1,
            GroupCutoffs = new object[] { 10, 20, 30 },
            GroupDescriptions = new string[] { "Ten", "Twenty", "Thirty", "Above thirty"} )
        ]
        public int OLVGroupy {
            get { return groupy; }
            set { groupy = value;  }
        }
        private int groupy;
    }

    class GeneratorTestPropertiesWithoutOlvColumnAttribute {
        public int Property1 {
            get { return this.property1; }
            set { this.property1 = value; }
        }
        private int property1;

        public string Property2 {
            get { return this.property2; }
        }
#pragma warning disable 649
        private string property2;
#pragma warning restore 649

        public bool CheckBoxProperty {
            get { return this.checkBoxProperty; }
            set { this.checkBoxProperty = value; }
        }
        private bool checkBoxProperty;

        public bool? TriStateCheckBox {
            get { return this.triStateCheckBox; }
            set { this.triStateCheckBox = value; }
        }
        private bool? triStateCheckBox;
    }

    class ClassWithChildren
    {
        public int Property1 {
            get { return this.property1; }
            set { this.property1 = value; }
        }
        private int property1;

        public string Property2 {
            get { return this.property2; }
        }
        private string property2="value";

        [OLVChildren]
        public IList<ClassWithChildren> MyChildren {
            get { return this.myChildren; }
            set { this.myChildren = value; }
        }
        private IList<ClassWithChildren> myChildren;
    }

    class ClassWithUntypedChildren
    {
        public int Property1 {
            get { return this.property1; }
            set { this.property1 = value; }
        }
        private int property1;

        public string Property2 {
            get { return this.property2; }
        }
        private string property2 = "property2";

        [OLVChildren]
        public ArrayList UntypedChildList {
            get { return this.untypedChildList; }
            set { this.untypedChildList = value; }
        }
        private ArrayList untypedChildList;

        [OLVChildren]
        public IList<ClassWithChildren> MyNotUsedChildren {
            get { return this.myChildren; }
            set { this.myChildren = value; }
        }
        private IList<ClassWithChildren> myChildren;
    }

    class ClassWithIgnoredProperties {

        [OLVIgnore]
        public string PublicProperty {
            get { return this.publicProperty; }
            set { this.publicProperty = value; }
        }
        private string publicProperty;
    }

    [TestFixture]
    public class TestGenerator
    {
        [TestFixtureSetUp]
        public void Init() {
            this.olv = MyGlobals.mainForm.objectListView2;
        }
        protected ObjectListView olv;

        [SetUp]
        public void InitEachTest() {
            this.olv.Clear();
        }

        [Test]
        public void TestEmpty() {
            IList<OLVColumn> columns = Generator.GenerateColumns(typeof(GeneratorTestModelEmpty));
            Assert.AreEqual(0, columns.Count);
        }

        [Test]
        public void TestBasics() {
            IList<OLVColumn> columns = Generator.GenerateColumns(typeof(GeneratorTestModel1));
            Assert.AreEqual(2, columns.Count);
            Assert.AreEqual("OLVPrimaryColumn", columns[0].AspectName);
            Assert.AreEqual("AspectToStringFormat", columns[0].AspectToStringFormat);
            Assert.AreEqual(true, columns[0].CheckBoxes);
            Assert.AreEqual(0, columns[0].DisplayIndex);
            Assert.AreEqual(true, columns[0].FillsFreeSpace);
            Assert.AreEqual(98, columns[0].FreeSpaceProportion);
            Assert.AreEqual("GroupWithItemCountFormat", columns[0].GroupWithItemCountFormat);
            Assert.AreEqual("GroupWithItemCountSingularFormat", columns[0].GroupWithItemCountSingularFormat);
            Assert.AreEqual(true, columns[0].Hyperlink);
            Assert.AreEqual("ImageAspectName", columns[0].ImageAspectName);
            Assert.AreEqual(true, columns[0].IsEditable);
            Assert.AreEqual(true, columns[0].IsTileViewColumn);
            Assert.AreEqual(true, columns[0].IsVisible);
            Assert.AreEqual(1000, columns[0].MaximumWidth);
            Assert.AreEqual(100, columns[0].MinimumWidth);
            Assert.AreEqual("ColumnName", columns[0].Name);
            Assert.AreEqual("Primary", columns[0].Text);
            Assert.AreEqual("Tag", columns[0].Tag);
            Assert.AreEqual(HorizontalAlignment.Right, columns[0].TextAlign);
            Assert.AreEqual("ToolTipText", columns[0].ToolTipText);
            Assert.AreEqual(true, columns[0].TriStateCheckBoxes);
            Assert.AreEqual(true, columns[0].UseInitialLetterForGroup);
            Assert.AreEqual(500, columns[0].Width);

            Assert.AreEqual("OLVSecondaryColumn", columns[1].AspectName);
            Assert.AreEqual("!AspectToStringFormat", columns[1].AspectToStringFormat);
            Assert.AreEqual(false, columns[1].CheckBoxes);
            Assert.AreEqual(1, columns[1].DisplayIndex);
            Assert.AreEqual(false, columns[1].FillsFreeSpace);
            Assert.AreEqual("!GroupWithItemCountFormat", columns[1].GroupWithItemCountFormat);
            Assert.AreEqual("!GroupWithItemCountSingularFormat", columns[1].GroupWithItemCountSingularFormat);
            Assert.AreEqual(false, columns[1].Hyperlink);
            Assert.AreEqual("!ImageAspectName", columns[1].ImageAspectName);
            Assert.AreEqual(false, columns[1].IsEditable);
            Assert.AreEqual(false, columns[1].IsTileViewColumn);
            Assert.AreEqual(false, columns[1].IsVisible);
            Assert.AreEqual(-1, columns[1].MaximumWidth);
            Assert.AreEqual(-1, columns[1].MinimumWidth);
            Assert.AreEqual("OLVSecondaryColumn", columns[1].Name);
            Assert.AreEqual("Secondary", columns[1].Text);
            Assert.AreEqual("!Tag", columns[1].Tag);
            Assert.AreEqual(HorizontalAlignment.Left, columns[1].TextAlign);
            Assert.AreEqual("!ToolTipText", columns[1].ToolTipText);
            Assert.AreEqual(false, columns[1].TriStateCheckBoxes);
            Assert.AreEqual(false, columns[1].UseInitialLetterForGroup);
            Assert.AreEqual(-1, columns[1].Width);
        }

        [Test]
        public void TestSorting() {
            IList<OLVColumn> columns = Generator.GenerateColumns(typeof(GeneratorTestModelSorting));
            Assert.AreEqual(5, columns.Count);
            Assert.AreEqual("OLVShouldBeFirst", columns[0].AspectName);
            Assert.AreEqual("OLVSecondaryColumn", columns[1].AspectName);
            Assert.AreEqual("OLVMustNotBeVisible", columns[2].AspectName);
            Assert.AreEqual("OLVSecondLast", columns[3].AspectName);
            Assert.AreEqual("OLVMustBeLast", columns[4].AspectName);
        }

        [Test]
        public void TestBuilding() {
            Assert.AreEqual(0, this.olv.Columns.Count);
            Generator.GenerateColumns(this.olv, typeof(GeneratorTestModelSorting));
            Assert.AreEqual(4, this.olv.Columns.Count);
            Assert.AreEqual(5, this.olv.AllColumns.Count);
        }

        [Test]
        public void TestGroupies() {
            IList<OLVColumn> columns = Generator.GenerateColumns(typeof(GeneratorTestModelGroupies));
            Assert.AreEqual(1, columns.Count);
            GeneratorTestModelGroupies model = new GeneratorTestModelGroupies();
            model.OLVGroupy = 5;
            Assert.AreEqual(0, columns[0].GetGroupKey(model));
            model.OLVGroupy = 35;
            Assert.AreEqual(3, columns[0].GetGroupKey(model));
            Assert.AreEqual("Above thirty", columns[0].ConvertGroupKeyToTitle(3));
        }

        [Test]
        public void TestGenerateClearSorting() {
            Generator.GenerateColumns(this.olv, typeof(GeneratorTestModelSorting));
            this.olv.PrimarySortColumn = this.olv.GetColumn(0);

            Generator.GenerateColumns(this.olv, typeof(GeneratorTestModelGroupies));

            Assert.IsNull(this.olv.PrimarySortColumn);
        }

        [Test]
        public void TestEmptyCollection() {
            this.olv.Columns.Add(new OLVColumn("not used", "NoAttribute"));
            Assert.AreNotEqual(0, this.olv.Columns.Count);
            ArrayList models = new ArrayList();
            Generator.GenerateColumns(this.olv, models);
            Assert.AreEqual(0, this.olv.Columns.Count);
        }

        [Test]
        public void TestNonEmptyCollection() {
            this.olv.Columns.Add(new OLVColumn("not used", "NoAttribute"));
            Assert.AreEqual(1, this.olv.Columns.Count);
            ArrayList models = new ArrayList();
            models.Add(new GeneratorTestModelSorting());
            models.Add(new GeneratorTestModelSorting());
            Generator.GenerateColumns(this.olv, models);
            Assert.AreEqual(5, this.olv.AllColumns.Count);
            Assert.AreEqual(4, this.olv.Columns.Count);
        }

        [Test]
        public void TestPropertiesWithoutAttributes_Ignored() {
            Generator.GenerateColumns(this.olv, typeof(GeneratorTestPropertiesWithoutOlvColumnAttribute));
            Assert.AreEqual(0, this.olv.AllColumns.Count);
        }

        [Test]
        public void TestPropertiesWithoutAttributes_Basics() {
            Generator.GenerateColumns(this.olv, typeof(GeneratorTestPropertiesWithoutOlvColumnAttribute), true);
            Assert.AreEqual(4, this.olv.AllColumns.Count);
            Assert.AreEqual(4, this.olv.Columns.Count);
            Assert.AreEqual("Property1", this.olv.GetColumn(0).Text);
            Assert.AreEqual("Property1", this.olv.GetColumn(0).AspectName);
            Assert.AreEqual("Property2", this.olv.GetColumn(1).Text);
            Assert.AreEqual("Property2", this.olv.GetColumn(1).AspectName);
            Assert.AreEqual("Check Box Property", this.olv.GetColumn(2).Text);
            Assert.AreEqual("CheckBoxProperty", this.olv.GetColumn(2).AspectName);
        }

        [Test]
        public void TestPropertiesWithoutAttributes_Editable() {
            Generator.GenerateColumns(this.olv, typeof(GeneratorTestPropertiesWithoutOlvColumnAttribute), true);
            Assert.IsTrue(this.olv.GetColumn("Property1").IsEditable);
            Assert.IsFalse(this.olv.GetColumn("Property2").IsEditable);
        }

        [Test]
        public void TestPropertiesWithoutAttributes_CheckBox() {
            Generator.GenerateColumns(this.olv, typeof(GeneratorTestPropertiesWithoutOlvColumnAttribute), true);
            Assert.IsFalse(this.olv.GetColumn("Property1").CheckBoxes);
            Assert.IsTrue(this.olv.GetColumn("Check Box Property").CheckBoxes);
            Assert.IsTrue(this.olv.GetColumn("Tri State Check Box").TriStateCheckBoxes);
        }

        [Test]
        public void TestPropertiesWithoutAttributes_DisplayIndexSetCorrectly() {
            Generator.GenerateColumns(this.olv, typeof(GeneratorTestPropertiesWithoutOlvColumnAttribute), true);
            for (int i = 0; i < this.olv.AllColumns.Count; i++)
                Assert.AreEqual(i, this.olv.GetColumn(i).DisplayIndex);
        }

        [Test]
        public void TestIgnoreAttribute_NoColumnCreated() {
            Generator.GenerateColumns(this.olv, typeof(ClassWithIgnoredProperties), true);
            Assert.AreEqual(0, this.olv.AllColumns.Count);
        }
    }


    [TestFixture]
    public class TestColumnBuildingForTreeListView {

        [TestFixtureSetUp]
        public void Init() {
            this.tolv = MyGlobals.mainForm.treeListView1;
        }
        protected TreeListView tolv;

        [SetUp]
        public void InitEachTest() {
            this.tolv.Reset();
        }

        [Test]
        public void TestDelegatesCreated() {
            Generator.GenerateColumns(this.tolv, typeof(ClassWithChildren), true);
            Assert.IsNotNull(this.tolv.CanExpandGetter);
            Assert.IsNotNull(this.tolv.ChildrenGetter);
        }

        [Test]
        public void TestCanExpandDelegateWorks() {
            Generator.GenerateColumns(this.tolv, typeof(ClassWithChildren), true);
            ClassWithChildren parent = new ClassWithChildren();
            parent.MyChildren = new List<ClassWithChildren>();
            parent.MyChildren.Add(new ClassWithChildren());

            List<ClassWithChildren> roots = new List<ClassWithChildren>();
            roots.Add(parent);
            this.tolv.Objects = roots;

            Assert.IsTrue(this.tolv.CanExpand(parent));
        }

        [Test]
        public void TestGetChildrenDelegateWorks() {
            Generator.GenerateColumns(this.tolv, typeof(ClassWithChildren), true);
            ClassWithChildren parent = new ClassWithChildren();
            parent.MyChildren = new List<ClassWithChildren>();
            parent.MyChildren.Add(new ClassWithChildren());

            List<ClassWithChildren> roots = new List<ClassWithChildren>();
            roots.Add(parent);
            this.tolv.Objects = roots;
            this.tolv.ExpandAll();

            Assert.AreEqual(2, this.tolv.GetItemCount());
        }

        [Test]
        public void TestGetChildrenDelegateWorksWithUntypedChildren_NestedChildren() {
            Generator.GenerateColumns(this.tolv, typeof(ClassWithUntypedChildren), true);
            ClassWithUntypedChildren parent = new ClassWithUntypedChildren();
            ClassWithUntypedChildren child1 = new ClassWithUntypedChildren();
            ClassWithUntypedChildren child2 = new ClassWithUntypedChildren();
            parent.UntypedChildList = new ArrayList();
            parent.UntypedChildList.Add(child1);
            parent.UntypedChildList.Add(child2);
            child1.UntypedChildList = new ArrayList();
            child1.UntypedChildList.Add(new ClassWithUntypedChildren()); 
            child2.UntypedChildList = new ArrayList();
            child2.UntypedChildList.Add(new ClassWithUntypedChildren());

            ArrayList roots = new ArrayList();
            roots.Add(parent);
            this.tolv.Objects = roots;
            this.tolv.ExpandAll();

            Assert.AreEqual(5, this.tolv.GetItemCount());
        }

        [Test]
        public void TestGetChildrenDelegateWorksWithUntypedChildren_WrongTypes() {
            Generator.GenerateColumns(this.tolv, typeof(ClassWithUntypedChildren), true);
            ClassWithUntypedChildren parent = new ClassWithUntypedChildren();
            parent.UntypedChildList = new ArrayList();
            parent.UntypedChildList.Add("string");
            parent.UntypedChildList.Add(1);

            ArrayList roots = new ArrayList();
            roots.Add(parent);
            this.tolv.Objects = roots;
            this.tolv.ExpandAll();

            Assert.AreEqual(3, this.tolv.GetItemCount());
        }
    }
}