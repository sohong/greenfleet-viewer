////////////////////////////////////////////////////////////////////////////////
// TrackImportHelperTest.cs
// 2012.03.14, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Personal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Viewer.Personal.Test
{
    [TestClass()]
    public class TrackImportHelperTest {

        private TestContext testContextInstance;

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
        ///A test for Import
        ///</summary>
        [TestMethod()]
        public void ImportTest() {
            Repository repo = new Repository();
            repo.Open(PersonalTest.StorageRoot, null);
            Vehicle vehicle = new Vehicle() { VehicleId = "v121212121212" };
            TrackImportHelper helper = new TrackImportHelper(repo);

            List<string> files = new List<string>();
            files.Add(Path.Combine(PersonalTest.DeviceRoot, @"all_2012_03_11_20_37_31"));
            files.Add(Path.Combine(PersonalTest.DeviceRoot, @"all_2012_03_11_20_38_00"));
            files.Add(Path.Combine(PersonalTest.DeviceRoot, @"event_2012_03_11_20_38_31"));

            helper.Import(vehicle, files, true, true);
        }

        /// <summary>
        ///A test for ImportAll
        ///</summary>
        [TestMethod()]
        public void ImportAllTest() {
            string repoDir = PersonalTest.StorageRoot;
            string sourceDir = PersonalTest.DeviceRoot;
            //string sourceDir = @"x:\gfdata";
            Repository repo = new Repository();
            repo.Open(repoDir, null);
            Vehicle vehicle = new Vehicle() { VehicleId = "v121212121212" };
            TrackImportHelper helper = new TrackImportHelper(repo);
            helper.ImportAll(vehicle, sourceDir, false, true);
        }
    }
}
