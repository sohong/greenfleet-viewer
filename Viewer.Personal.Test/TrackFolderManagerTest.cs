using Viewer.Personal.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Viewer.Personal.Test
{
    /// <summary>
    ///This is a test class for TrackFolderManagerTest and is intended
    ///to contain all TrackFolderManagerTest Unit Tests
    ///</summary>
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
            Repository owner = null;
            TrackFolderManager target = new TrackFolderManager(owner);
            string trackFile = string.Empty;
            string expected = string.Empty;
            string actual;
            actual = target.GetFolder(trackFile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
