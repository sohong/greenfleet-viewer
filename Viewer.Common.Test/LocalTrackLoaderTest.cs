////////////////////////////////////////////////////////////////////////////////
// LocalTrackLoaderTest.cs
// 2012.04.20, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Viewer.Common.Model;

namespace Viewer.Common.Test
{
    /// <summary>
    ///This is a test class for LocalTrackLoaderTest and is intended
    ///to contain all LocalTrackLoaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LocalTrackLoaderTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
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
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            LocalTrackLoader loader = new LocalTrackLoader();
            string source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"sample\all_2012_03_11_20_37_31.inc");
            Track target = loader.Load(source, true);
            Assert.IsNotNull(target);

            DateTime d = new DateTime(2012, 3, 11, 20, 37, 31);
            Assert.AreEqual(d, target.CreateDate);
        }

        /// <summary>
        ///A test for ParseDate
        ///</summary>
        [TestMethod()]
        public void ParseDateTest()
        {
            string s = "2012_04_10_12_00_01";
            DateTime expected = new DateTime(2012, 04, 10, 12, 00, 01);
            DateTime actual = LocalTrackLoader.ParseDate(s);
            Assert.AreEqual(expected, actual);

            s = "2012_04_10_13_00_01";
            expected = new DateTime(2012, 04, 10, 13, 00, 01);
            actual = LocalTrackLoader.ParseDate(s);
            Assert.AreEqual(expected, actual);

            s = "2012_04_10_01_00_01";
            expected = new DateTime(2012, 04, 10, 01, 00, 01);
            actual = LocalTrackLoader.ParseDate(s);
            Assert.AreEqual(expected, actual);
        }
    }
}
