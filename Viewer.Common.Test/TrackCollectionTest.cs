////////////////////////////////////////////////////////////////////////////////
// TrackCollectionTest.cs
// 2012.04.02, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Viewer.Common.Test
{
    /// <summary>
    ///This is a test class for TrackCollectionTest and is intended
    ///to contain all TrackCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TrackCollectionTest
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
        ///A test for Length
        ///</summary>
        [TestMethod()]
        public void LengthTest()
        {
            TrackCollection target = new TrackCollection();
            Track track = new Track() { StartTime = new DateTime(2012, 3, 1, 0, 0, 0) };
            target.Add(track);

            long actual = target.Length;
            Assert.AreEqual(1, actual);

            track = new Track() { StartTime = new DateTime(2012, 3, 2, 3, 1, 0) };
            target.Add(track);

            actual = target.Length;
            Assert.AreEqual(24 * 60 + 3 * 60 + 2, actual);
        }
    }
}
