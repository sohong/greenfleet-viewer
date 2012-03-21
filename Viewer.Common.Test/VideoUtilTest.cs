////////////////////////////////////////////////////////////////////////////////
// VideoUtilTest.cs
// 2012.03.21, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Viewer.Common.Test
{
    [TestClass()]
    public class VideoUtilTest {

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
        ///A test for RawToMpeg
        ///</summary>
        [TestMethod()]
        public void RawToMpegTest() {
            string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"sample\all_2012_03_11_20_37_31.264");
            string targetFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "work");
            string expected = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"work\all_2012_03_11_20_37_31.mp4");
            if (File.Exists(expected)) {
                File.Delete(expected);
            }
            string actual = VideoUtil.RawToMpeg(sourcePath, targetFolder);
            Assert.AreEqual(expected, actual);
        }
    }
}
