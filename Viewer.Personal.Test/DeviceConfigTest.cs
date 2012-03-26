////////////////////////////////////////////////////////////////////////////////
// DeviceConfigTest.cs
// 2012.03.26, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Personal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Viewer.Personal.Test
{
    [TestClass()]
    public class DeviceConfigTest {

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


        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod()]
        public void CloneTest() {
            DeviceConfig config = new DeviceConfig() {
                RecordingResolution = 1,
                RecordingQuality = 2,
                TransmitResolution = 3,
                TransmitQuality = 4,
                ApSsid = "aaa",
                ApKey = "bbb",
                ClientApSsid = "ccc",
                ClientApKey = "ddd"
            };

            DeviceConfig target = config.Clone();
            Assert.IsTrue(config.EqualsTo(target));
        }
    }
}
