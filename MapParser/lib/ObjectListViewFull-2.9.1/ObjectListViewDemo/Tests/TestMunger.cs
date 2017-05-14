
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace BrightIdeasSoftware.Tests
{
    [TestFixture]
    public class TestMunger
    {
        [Test]
        public void Test_GetValue_DottedAspect_Success() {
            // Arrange
            TestBaseClass instance = new TestBaseClass();
            string testValue = "1234567890";
            instance.VirtualTestProperty = testValue;
            Munger munger = new Munger("This.VirtualTestProperty.ToUpperInvariant.Length");

            // Act
            object value = munger.GetValue(instance);

            // Assert
            Assert.AreEqual(testValue.Length, value);
        }

        [Test]
        public void Test_GetValue_DottedAspect_UnknownAspect() {
            // Arrange
            TestBaseClass instance = new TestBaseClass();
            string testValue = "1234567890";
            instance.VirtualTestProperty = testValue;
            Munger munger = new Munger("This.VirtualTestProperty.UnknownProperty.Length");

            // Act
            object value = munger.GetValue(instance);

            // Assert
            string errorMsg = "'UnknownProperty' is not a parameter-less method, property or field of type 'System.String'";
            Assert.AreEqual(errorMsg, value);
        }

        [Test]
        public void Test_GetValue_DottedAspect_NullMidResult() {
            // Arrange
            TestBaseClass instance = new TestBaseClass();
            Munger munger = new Munger("This.NullProperty.ToUpperInvariant");

            // Act
            object value = munger.GetValue(instance);

            // Assert
            Assert.IsNull(value);
        }

        [Test]
        public void Test_GetValue_VirtualPropertyOnBaseClass() {
            // Arrange
            TestBaseClass instance = new TestBaseClass();
            string testValue = "some test value";
            instance.VirtualTestProperty = testValue;
            Munger munger = new Munger("VirtualTestProperty");

            // Act
            object value = munger.GetValue(instance);

            // Assert
            Assert.AreEqual(testValue, value);
        }

        [Test]
        public void Test_GetValue_VirtualProperty_CalledOnBaseClass() {
            // Arrange
            TestBaseClass instance = new TestSubClass();
            string testValue = "some test value";
            instance.VirtualTestProperty = testValue;
            Munger munger = new Munger("VirtualTestProperty");

            // Act
            string value = munger.GetValue(instance) as string;

            // Assert
            Assert.AreEqual(testValue, value);
        }

        [Test]
        public void Test_GetValue_NonVirtualProperty_CalledOnSubClass() {
            // Arrange
            TestSubClass instance = new TestSubClass();
            string testValue = "some test value";
            instance.NonVirtualTestProperty = testValue;
            Munger munger = new Munger("NonVirtualTestProperty");

            // Act
            string value = munger.GetValue(instance) as string;

            // Assert
            Assert.AreEqual(testValue, value);
        }

        [Test]
        public void Test_PutValue_DottedAspect_Success() {
            // Arrange
            TestSubClass instance = new TestSubClass();
            string testValue = "some test value";
            Munger munger = new Munger("This.This.VirtualTestProperty");

            // Act
            munger.PutValue(instance, testValue);

            // Assert
            Assert.AreEqual(testValue, instance.VirtualTestProperty);
        }

        [Test]
        public void Test_PutValue_DottedAspect_UnknownAspect() {
            // Arrange
            TestBaseClass instance = new TestBaseClass();
            string testValue = "1234567890";
            instance.VirtualTestProperty = testValue;
            Munger munger = new Munger("This.VirtualTestProperty.UnknownProperty");

            // Act
            munger.PutValue(instance, testValue);

            // Assert
            Assert.AreEqual(testValue, instance.VirtualTestProperty);
        }

        [Test]
        public void Test_PutValue_DottedAspect_CantModifyReadOnlyProperty() {
            // Arrange
            TestSubClass instance = new TestSubClass();
            string testValue = "some test value";
            string testValue2 = "some test value2";
            Munger munger = new Munger("This.This.ReadOnlyProperty");
            instance.SetReadOnlyProperty(testValue);
            Assert.AreEqual(testValue, instance.ReadOnlyProperty);

            // Act
            munger.PutValue(instance, testValue2);

            // Assert
            Assert.AreEqual(testValue, instance.ReadOnlyProperty);
        }

        [Test]
        public void Test_PutValue_NonVirtualProperty_CalledOnSubClass() {
            // Arrange
            TestSubClass instance = new TestSubClass();
            string testValue = "some test value";
            Munger munger = new Munger("NonVirtualTestProperty");

            // Act
            munger.PutValue(instance, testValue);

            // Assert
            Assert.AreEqual(testValue, instance.NonVirtualTestProperty);
            Assert.IsTrue(instance.subClassPropertyModified);
        }

        [Test]
        public void Test_PutValue_NonVirtualProperty_CalledOnBaseClass() {
            // Arrange
            TestBaseClass instance = new TestSubClass();
            string testValue = "some test value";
            Munger munger = new Munger("NonVirtualTestProperty");

            // Act
            munger.PutValue(instance, testValue);

            // Munger acts on the runtime type of the instance, so it (correctly)
            // calls the subclass' version of this property. The compiler
            // uses the declared type of the instance, so instance.NonVirtualTestProperty
            // returns null, which is correct but perhaps unexpected.

            // Assert
            Assert.AreEqual(null, instance.NonVirtualTestProperty);
            Assert.AreEqual(testValue, (instance as TestSubClass).NonVirtualTestProperty);
            Assert.IsTrue((instance as TestSubClass).subClassPropertyModified);
        }

        [Test]
        public void Test_GetValue_NonVirtualProperty_CalledOnBaseClass() {
            // Arrange
            TestBaseClass instance = new TestSubClass();
            string testValue = "some test value";
            instance.NonVirtualTestProperty = testValue;
            Munger munger = new Munger("NonVirtualTestProperty");

            // Act
            string value = munger.GetValue(instance) as string;

            // Assert
            Assert.IsNull(value);
        }

        [Test]
        public void Test_PutProperty_PropertyOnClass_Success() {
            // Arrange
            TestBaseClass instance = new TestBaseClass();
            string newValue = "new-value";

            // Act
            bool result = Munger.PutProperty(instance, "NonVirtualTestProperty", newValue);

            // Assert
            Assert.True(result);
            Assert.AreEqual(newValue, instance.NonVirtualTestProperty);
        }

        private class TestBaseClass
        {
            public string NonVirtualTestProperty {
                get { return testProperty1; }
                set { testProperty1 = value; }
            }
            private string testProperty1;

            public virtual string VirtualTestProperty {
                get { return testProperty2; }
                set { testProperty2 = value; }
            }
            private string testProperty2;

            public TestBaseClass This {
                get { return this; }
            }

            public object NullProperty {
                get { return null; }
            }

            public string ReadOnlyProperty {
                get { return readOnlyProperty; }
            }

            public void SetReadOnlyProperty(string value) {
                readOnlyProperty = value;
            }
            private string readOnlyProperty;
        }

        private class TestSubClass : TestBaseClass
        {
            public new string NonVirtualTestProperty {
                get { return subTestProperty1; }
                set { 
                    subTestProperty1 = value;
                    subClassPropertyModified = true;
                }
            }
            private string subTestProperty1;

            public override string VirtualTestProperty {
                get { return subTestProperty2; }
                set { 
                    subTestProperty2 = value;
                    subClassPropertyModified = true;
                }
            }
            private string subTestProperty2;

            public bool subClassPropertyModified = false;
        }
    }
}