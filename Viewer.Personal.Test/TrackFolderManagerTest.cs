////////////////////////////////////////////////////////////////////////////////
// TrackFolderManagerTest.cs
// 2012.03.14, created by sohong
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
    public class TrackFolderManagerTest {

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
        ///A test for GetFolder
        ///</summary>
        [TestMethod()]
        public void GetFolderTest() {
            LocalRepository repo = new LocalRepository();
            repo.Open(PersonalTest.StorageRoot, null);
            TrackFolderManager manager = new TrackFolderManager(repo);
            Vehicle vehicle = new Vehicle() {
                VehicleId = "v121212121212"
            };
            string trackFile = "all_2012_03_11_20_37_31";
            string folder = manager.GetFolder(vehicle, trackFile, true);
            Assert.AreEqual(folder, vehicle.VehicleId + @"\2012\03\11");
        }
    }
}
