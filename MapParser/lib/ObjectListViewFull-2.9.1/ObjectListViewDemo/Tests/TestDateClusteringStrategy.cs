using System;
using NUnit.Framework;

namespace BrightIdeasSoftware.Tests {

    [TestFixture]
    public class TestDateClusteringStrategy {

        readonly DateTime DATE1 = new DateTime(1998, 11, 30, 22, 23, 24);
        readonly DateTime DATE2 = new DateTime(1999, 12, 31, 22, 23, 24);

        [Test]
        public void Test_Construction_Empty() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy();
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE2; };
            object result = strategy.GetClusterKey(null);

            Assert.AreEqual(new DateTime(1999, 12, 1), result);
        }

        [Test]
        public void Test_Construction_WithPortions() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Hour | DateTimePortion.Minute, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            object result = strategy.GetClusterKey(null);

            Assert.AreEqual(new DateTime(1, 1, 1, 22, 23, 0), result);
        }

        [Test]
        public void Test_Extracting_FromNull() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Hour | DateTimePortion.Minute, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return null; };
            object result = strategy.GetClusterKey(null);
            Assert.IsNull(result);
        }

        [Test]
        public void Test_GetClusterDisplayLabel_Plural() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Hour | DateTimePortion.Minute, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            ICluster cluster = new Cluster(strategy.GetClusterKey(null));
            cluster.Count = 2;
            string result = strategy.GetClusterDisplayLabel(cluster);
            Assert.AreEqual("22:23 (2 items)", result);
        }

        [Test]
        public void Test_GetClusterDisplayLabel_Singular() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Year | DateTimePortion.Month, "MM-yy");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            ICluster cluster = new Cluster(strategy.GetClusterKey(null));
            cluster.Count = 1;
            string result = strategy.GetClusterDisplayLabel(cluster);
            Assert.AreEqual("11-98 (1 item)", result);
        }

        [Test]
        public void Test_GetClusterDisplayLabel_NullValue() {
            DateTimeClusteringStrategy strategy = new DateTimeClusteringStrategy(DateTimePortion.Year | DateTimePortion.Month, "HH:mm");
            strategy.Column = new OLVColumn();
            strategy.Column.AspectGetter = delegate(object x) { return DATE1; };
            ICluster cluster = new Cluster(null);
            cluster.Count = 1;
            string result = strategy.GetClusterDisplayLabel(cluster);
            Assert.AreEqual(ClusteringStrategy.NULL_LABEL + " (1 item)", result);
        }
    }
}