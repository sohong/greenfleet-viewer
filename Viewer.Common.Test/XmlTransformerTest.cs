////////////////////////////////////////////////////////////////////////////////
// XmlTransformerTest.cs
// 2012.03.09, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;
using System.Diagnostics;

namespace Viewer.Common.Test
{
    /// <summary>
    ///This is a test class for XmlTransformerTest and is intended
    ///to contain all XmlTransformerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class XmlTransformerTest {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void DeserialzeTest() {
            MainObject mo = new MainObject() {
                Name = "name", Age = 33, Region = Region.Bbb,
                Child = new MainObject() {
                    Name = "child", Age = 11, Region = Region.CcC
                },
            };
            mo.Boy.Name = "boy";

            XmlTransformer trans = new XmlTransformer();
            XDocument doc = new XDocument();
            XElement root = new XElement("test");
            doc.Add(root);
            trans.Serialize(mo, root);
            Debug.WriteLine(">>> Serializing...");
            Debug.WriteLine(doc.ToString());

            mo = new MainObject();
            mo = (MainObject)trans.Deserialize(root, mo);
            Assert.AreEqual(mo.Name, "name");
            Assert.AreEqual(mo.Region, Region.Bbb);
            Assert.AreEqual(mo.Age, 33);
            Assert.AreEqual(mo.Child.Name, "child");
            Assert.AreEqual(mo.Boy.Name, "boy");
            Assert.AreEqual(mo.Boy.Region, Region.AAA);

            Type t = typeof(MainObject);
            mo = (MainObject)trans.Deserialize(root, t);
            Assert.AreEqual(mo.Name, "name");
            Assert.AreEqual(mo.Region, Region.Bbb);
            Assert.AreEqual(mo.Age, 33);
            Assert.AreEqual(mo.Child.Name, "child");
            Assert.AreEqual(mo.Boy.Name, "boy");
            Assert.AreEqual(mo.Boy.Region, Region.AAA);
        }


        /////////////////////////////////////////////////////////////////////////////////
        enum Region { AAA, Bbb, CcC };

        class MainObject {
            private Boy m_boy;
            
            public MainObject() {
                m_boy = new Boy();
            }

            public string Name { get; set; }
            public int Age { get; set; }
            public Region Region { get; set; }
            public MainObject Child { get; set; }
            public Boy Boy { get { return m_boy; } }
        }

        class Boy {
            public string Name { get; set; }
            public int Age { get; set; }
            public Region Region { get; set; }
        }
    }
}
