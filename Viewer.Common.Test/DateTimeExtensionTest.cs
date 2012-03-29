﻿////////////////////////////////////////////////////////////////////////////////
// TrackGroupTest.cs
// 2012.03.28, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Viewer.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Viewer.Common.Test
{
    /// <summary>
    ///This is a test class for DateTimeExtensionTest and is intended
    ///to contain all DateTimeExtensionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateTimeExtensionTest {


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
        ///A test for StripSeconds
        ///</summary>
        [TestMethod()]
        public void StripSecondsTest() {
            DateTime d = new DateTime(2012, 3, 11, 11, 12, 13);
            DateTime expected = new DateTime(2012, 3, 11, 11, 12, 0);
            DateTime actual = DateTimeExtension.StripSeconds(d);
            Assert.AreEqual(expected, actual);
        }
    }
}