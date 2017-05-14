/*
 * TestExport - Test that exporting rows of ObjectListView
 *
 * Author: Phillip Piper
 * Date: 7 August 2012, 10:35pm
 *
 * Change log:
 * 2012-08-07  JPP  Initial code
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

using NUnit.Framework;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestExport {

        [SetUp]
        public void TestSetup() {
            PersonDb.Reset();
        }

        [Test]
        public void Test_TabSeparated_Simple() {
            this.olv.SetObjects(PersonDb.All);
            OLVExporter exporter = new OLVExporter(this.olv);
            string tabSeparated = exporter.ExportTo(OLVExporter.ExportFormat.TSV);
            Assert.AreEqual(tabSeparated, @"Name	Occupation	Culinary Rating	CanTellJokes	IsActive
name	occupation	300	True	
name2	occupation	200	True	
aaa First Alphabetical Name	occupation3	90	True	
name4	occupation4	80	True	
name5	occupation5	140	True	
name6	occupation6	65	True	
name7	occupation7	62	True	
zzz Last Alphabetical Name	occupation6	60	True	
");
        }

        [Test]
        public void Test_TabSeparated_WithoutHeader() {
            this.olv.SetObjects(PersonDb.All);
            this.olv.SelectedObject = PersonDb.All[0];
            OLVExporter exporter = new OLVExporter(this.olv, this.olv.SelectedObjects);
            exporter.IncludeColumnHeaders = false;
            string tabSeparated = exporter.ExportTo(OLVExporter.ExportFormat.TSV);
            Assert.AreEqual(tabSeparated, @"name	occupation	300	True	
");
        }

        [Test]
        public void Test_Csv_Simple() {
            this.olv.SetObjects(PersonDb.All);
            OLVExporter exporter = new OLVExporter(this.olv);
            string csv = exporter.ExportTo(OLVExporter.ExportFormat.CSV);
            Assert.AreEqual(csv, @"""Name"",""Occupation"",""Culinary Rating"",""CanTellJokes"",""IsActive""
""name"",""occupation"",""300"",""True"",""""
""name2"",""occupation"",""200"",""True"",""""
""aaa First Alphabetical Name"",""occupation3"",""90"",""True"",""""
""name4"",""occupation4"",""80"",""True"",""""
""name5"",""occupation5"",""140"",""True"",""""
""name6"",""occupation6"",""65"",""True"",""""
""name7"",""occupation7"",""62"",""True"",""""
""zzz Last Alphabetical Name"",""occupation6"",""60"",""True"",""""
");
        }

        [Test]
        public void Test_Csv_EscapedValues() {
            PersonDb.All[0].Occupation = @"Some, ""value""";
            this.olv.SetObjects(PersonDb.All);
            this.olv.SelectedObject = PersonDb.All[0];
            OLVExporter exporter = new OLVExporter(this.olv, this.olv.SelectedObjects);
            string csv = exporter.ExportTo(OLVExporter.ExportFormat.CSV);
            Assert.AreEqual(csv, @"""Name"",""Occupation"",""Culinary Rating"",""CanTellJokes"",""IsActive""
""name"",""Some, """"value"""""",""300"",""True"",""""
");
        }

        [Test]
        public void Test_Html_Simple() {
            this.olv.SetObjects(PersonDb.All);
            OLVExporter exporter = new OLVExporter(this.olv);
            string html = exporter.ExportTo(OLVExporter.ExportFormat.HTML);
            Assert.AreEqual(html, @"<table><tr><td>Name</td><td>Occupation</td><td>Culinary Rating</td><td>CanTellJokes</td><td>IsActive</td></tr>
<tr><td>name</td><td>occupation</td><td>300</td><td>True</td><td></td></tr>
<tr><td>name2</td><td>occupation</td><td>200</td><td>True</td><td></td></tr>
<tr><td>aaa First Alphabetical Name</td><td>occupation3</td><td>90</td><td>True</td><td></td></tr>
<tr><td>name4</td><td>occupation4</td><td>80</td><td>True</td><td></td></tr>
<tr><td>name5</td><td>occupation5</td><td>140</td><td>True</td><td></td></tr>
<tr><td>name6</td><td>occupation6</td><td>65</td><td>True</td><td></td></tr>
<tr><td>name7</td><td>occupation7</td><td>62</td><td>True</td><td></td></tr>
<tr><td>zzz Last Alphabetical Name</td><td>occupation6</td><td>60</td><td>True</td><td></td></tr>
</table>
");
        }

        [Test]
        public void Test_Html_EscapedValues() {
            PersonDb.All[0].Occupation = @"Complex <T> "" & string";
            this.olv.SetObjects(PersonDb.All);
            this.olv.SelectedObject = PersonDb.All[0];
            OLVExporter exporter = new OLVExporter(this.olv, this.olv.SelectedObjects);
            string html = exporter.ExportTo(OLVExporter.ExportFormat.HTML);
            Assert.AreEqual(html, @"<table><tr><td>Name</td><td>Occupation</td><td>Culinary Rating</td><td>CanTellJokes</td><td>IsActive</td></tr>
<tr><td>name</td><td>Complex &lt;T&gt; &quot; &amp; string</td><td>300</td><td>True</td><td></td></tr>
</table>
");
        }

        [TestFixtureSetUp]
        public void Init() {
            this.olv = MyGlobals.mainForm.objectListView1;
        }
        protected ObjectListView olv;
    }
}
