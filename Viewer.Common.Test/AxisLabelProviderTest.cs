////////////////////////////////////////////////////////////////////////////////
// AxisLabelProviderTest.cs
// 2012.04.20, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Common.UI.Timeline;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Viewer.Common.Model;

namespace Viewer.Common.Test
{
    /// <summary>
    ///This is a test class for AxisLabelProviderTest and is intended
    ///to contain all AxisLabelProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AxisLabelProviderTest
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
        ///A test for BuildLabels
        ///</summary>
        [TestMethod()]
        public void BuildLabelsTest()
        {
            TrackCollection tracks = LoadTracks();
            AxisLabelProvider target = new AxisLabelProvider();
            DateTime startTime = tracks.First.StartTime;
            DateTime endTime = tracks.Last.EndTime;
            target.BuildLabels(startTime, endTime);

            Assert.AreEqual(target.Count, 2);
            Assert.AreEqual(target.GetTime(0), new DateTime(2012, 3, 1, 11, 0, 0));
            Assert.AreEqual(target.GetTime(1), new DateTime(2012, 3, 1, 12, 0, 0));
        }

        private TrackCollection LoadTracks()
        {
            TrackCollection tracks = new TrackCollection();
            Track track = new Track() { StartTime = new DateTime(2012, 3, 1, 11, 11, 12), EndTime = new DateTime(2012, 3, 1, 11, 11, 59) };
            tracks.Add(track);

            track = new Track() { StartTime = new DateTime(2012, 3, 1, 11, 1, 0), EndTime = new DateTime(2012, 3, 1, 11, 1, 59) };
            tracks.Add(track);

            return tracks;
        }
    }
}
