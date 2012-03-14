////////////////////////////////////////////////////////////////////////////////
// TrackLoaderBaseTest.cs
// 2012.03.14, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Viewer.Common.Model;
using System.IO;

namespace Viewer.Common.Test
{
    [TestClass()]
    public class TrackLoaderBaseTest {


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
        ///A test for LoadPoints
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Viewer.Common.dll")]
        public void LoadPointsTest() {
            TrackLoaderBase_Accessor target = new TrackLoaderBase_Accessor();
            Track track = new Track();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            StreamReader reader = new StreamReader(Path.Combine(path, @"sample\track_log.inc"));
            target.LoadPoints(track, reader);
            Assert.AreEqual(track.Points.Count, 26);

            // 시간 순으로 정렬되어 있는가?
            for (int i = 1; i < track.Points.Count; i++) {
                Assert.IsTrue(track.Points[i].PointTime >= track.Points[i - 1].PointTime);
            }
        }
    }
}
