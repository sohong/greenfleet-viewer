using Viewer.Personal.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Input;
using Viewer.Personal.Model;
using System.IO;
using Viewer.Personal.Test.Mocks;
using Viewer.Common.Model;
using Viewer.Common;

namespace Viewer.Personal.Test
{
    
    
    /// <summary>
    ///This is a test class for DeviceRepositoryViewModelTest and is intended
    ///to contain all DeviceRepositoryViewModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DeviceRepositoryViewModelTest {


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
        ///A test for LoadCommand
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Viewer.Personal.dll")]
        public void LoadCommandTest() {
            //DeviceRepositoryViewModel_Accessor target = new DeviceRepositoryViewModel_Accessor(); // TODO: Initialize to an appropriate value
            DeviceRepositoryViewModel target = new DeviceRepositoryViewModel();
            target.DriveManager = new MockDriveManager();
            target.SelectedVehicle = new Vehicle();
            target.LoadCommand.Execute(null);
        }

        /// <summary>
        ///A test for SearchCommand
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Viewer.Personal.dll")]
        public void SearchCommandTest() {
            //DeviceRepositoryViewModel_Accessor target = new DeviceRepositoryViewModel_Accessor();
            DeviceRepositoryViewModel target = new DeviceRepositoryViewModel();
            target.DriveManager = new MockDriveManager();
            target.SelectedVehicle = new Vehicle();
            target.LoadCommand.Execute(null);

            DateTime d1 = new DateTime(2012, 3, 11, 20, 37, 11).StripSeconds();
            DateTime d2 = new DateTime(2012, 3, 11, 20, 38, 13).StripSeconds();

            int count = 0;
            foreach (Track t in target.Tracks) {
                if (t.CreateDate.StripSeconds() >= d1 &&
                    t.CreateDate.StripSeconds() <= d2) {
                        count++;
                }
            }

            target.SearchFrom = d1;
            target.SearchTo = d2;
            target.SearchCommand.Execute(null);

            int expected = count;
            int actual = target.Tracks.Count;
            Assert.AreEqual(actual, expected);
        }
    }
}
