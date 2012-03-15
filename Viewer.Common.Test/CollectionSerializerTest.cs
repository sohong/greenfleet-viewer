////////////////////////////////////////////////////////////////////////////////
// XmlModelTransformer.cs
// 2012.03.15, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Viewer.Common.Test
{
    /// <summary>
    ///This is a test class for CollectionSerializerTest and is intended
    ///to contain all CollectionSerializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CollectionSerializerTest {

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


        private IList CreateModels() {
            List<TestModel> models = new List<TestModel>();
            models.Add(new TestModel() { Name = "name1", Address = "addr1" });
            models.Add(new TestModel() { Name = "name2", Address = "addr2" });
            return models;
        }

        /// <summary>
        ///A test for Serialize
        ///</summary>
        [TestMethod()]
        public void SerializeTest() {
            CollectionSerializer target = new CollectionSerializer();
            IList models = CreateModels();

            string rootName = "testModels";
            string modelType = "testModel";
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test",  "test_models.xml");
            target.Serialize(models, rootName, modelType, fileName);

            Assert.IsTrue(File.Exists(fileName));
        }

        [TestMethod]
        public void DeserializeTest() {
            SerializeTest();

            CollectionSerializer target = new CollectionSerializer();
            IList models = new List<TestModel>();

            string modelType = "testModel";
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test", "test_models.xml");
            models = target.Deserialize(fileName, modelType, typeof(TestModel), models);

            Assert.AreEqual(models.Count, 2);

            IList models2 = CreateModels();
            for (int i = 0; i < models2.Count; i++) {
                Assert.IsTrue(models2[i].Equals(models[i]));
            }
        }


        /////////////////////////////////////////////////////////////////////////////
        // class TestModel
        //
        public class TestModel {

            public string Name { get; set; }
            public string Address { get; set; }

            public override bool Equals(object obj) {
                TestModel m = obj as TestModel;
                return (m != null) && m.Name.Equals(Name) && m.Address.Equals(Address);
            }
        }
    }
}
