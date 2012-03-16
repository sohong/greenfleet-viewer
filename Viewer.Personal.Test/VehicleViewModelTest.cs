////////////////////////////////////////////////////////////////////////////////
// VehicleViewModelTest.cs
// 2012.03.16, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Personal.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Viewer.Personal.Model;

namespace Viewer.Personal.Test
{
    /// <summary>
    ///This is a test class for VehicleViewModelTest and is intended
    ///to contain all VehicleViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VehicleViewModelTest {

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
        ///A test for VehicleViewModel SubmitCommand
        ///</summary>
        [TestMethod()]
        public void SubmitCommandTest() {
            Vehicle source = null;
            VehicleViewModel vmodel = new VehicleViewModel(source);
            // TODO Mock ui 필요.
            vmodel.Vehicle.Name = "XXXXXXXXXXX";
            vmodel.Vehicle.VehicleId = "XXX";
        }
    }
}
