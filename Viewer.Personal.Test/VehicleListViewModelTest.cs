////////////////////////////////////////////////////////////////////////////////
// VehicleListViewModelTest.cs
// 2012.03.16, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using Viewer.Personal.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Practices.Prism.Commands;

namespace Viewer.Personal.Test
{
    /// <summary>
    ///This is a test class for VehicleListViewModelTest and is intended
    ///to contain all VehicleListViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VehicleListViewModelTest {

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
        ///A test for AddCommand
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Viewer.Personal.dll")]
        public void AddCommandTest() {
            // VehicleViewMode에서 테스트한다.
        }
    }
}
