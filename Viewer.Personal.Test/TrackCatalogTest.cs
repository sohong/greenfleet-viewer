////////////////////////////////////////////////////////////////////////////////
// TrackCatalogTest.cs
// 2012.03.19, created by sohong
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
    public class TrackCatalogTest {

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
        ///A test for MakeFileName
        ///</summary>
        [TestMethod()]
        public void MakeFileNameTest() {
            int year = 2012;
            int month = 3;
            string expected = "cat_2012_03.xml";
            string actual = TrackCatalog.MakeFileName(year, month);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetFileData
        ///</summary>
        [TestMethod()]
        public void GetFileDataTest() {
            string fileName = "cat_2012_03.xml";
            int year;
            int month;
            Assert.IsTrue(TrackCatalog.GetFileData(fileName, out year, out month));
            Assert.AreEqual(2012, year);
            Assert.AreEqual(3, month);
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest() {
            Vehicle vehicle = new Vehicle() { VehicleId = "v121212121212" };
            int year = 2012;
            int month = 03;
            TrackCatalog cat = new TrackCatalog(vehicle, year, month);

            List<string> files = new List<string>();
            files.Add("all_2012_03_11_20_37_31");
            files.Add("all_2012_03_12_20_37_31");

            cat.Add(files);
            Assert.AreEqual(cat.Tracks.Count, 2);

            string catalogPath = Path.Combine(PersonalTest.StorageRoot, vehicle.VehicleId, TrackCatalog.MakeFileName(year, month));
            cat.Save(catalogPath);

            Assert.IsTrue(File.Exists(catalogPath));
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void LoadTest() {
            // save
            SaveTest();
            
            // load
            Vehicle vehicle = new Vehicle() { VehicleId = "v121212121212" };
            int year = 2012;
            int month = 03;
            TrackCatalog cat = new TrackCatalog(vehicle, year, month);

            string catalogPath = Path.Combine(PersonalTest.StorageRoot, vehicle.VehicleId, TrackCatalog.MakeFileName(year, month));
            cat.Load(catalogPath);

            Assert.AreEqual(cat.Tracks.Count, 2);
            Assert.AreEqual(cat.Tracks[0].CreateDate, new DateTime(2012, 3, 11, 20, 37, 31));
        }
    }
}
