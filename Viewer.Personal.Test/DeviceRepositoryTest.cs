////////////////////////////////////////////////////////////////////////////////
// DeviceRepositoryTest.cs
// 2012.03.20, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Personal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Data;
using Viewer.Common.Model;

namespace Viewer.Personal.Test
{
    /// <summary>
    ///</summary>
    [TestClass()]
    public class DeviceRepositoryTest {

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
        ///A test for Open
        ///</summary>
        [TestMethod()]
        public void OpenTest() {
            DeviceRepository repo = new DeviceRepository();
            Vehicle vehicle = new Vehicle();
            string rootPath = @"C:\GreenFleet\test\samples";
            repo.Open(vehicle, rootPath);

            string[] files = Directory.GetFiles(rootPath, "*.inc");

            Assert.AreEqual(files.Length, repo.TrackCount);
        }

        /// <summary>
        ///A test for LoadGroups
        ///</summary>
        [TestMethod()]
        public void LoadGroupsTest() {
            DeviceRepository repo = new DeviceRepository();
            Vehicle vehicle = new Vehicle();
            string rootPath = @"C:\GreenFleet\test\samples";
            repo.Open(vehicle, rootPath);

            TrackGroup root = repo.LoadGroups(repo.GetTracks());
            Assert.IsNotNull(root);
        }
    }
}
