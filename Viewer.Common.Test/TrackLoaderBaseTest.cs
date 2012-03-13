using Viewer.Common.Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Viewer.Common.Model;
using System.IO;

namespace Viewer.Common.Test
{
    /// <summary>
    ///This is a test class for TrackLoaderBaseTest and is intended
    ///to contain all TrackLoaderBaseTest Unit Tests
    ///</summary>
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
            StreamReader reader = new StreamReader(Path.Combine(path, "points.inc"));
            target.LoadPoints(track, reader);
            Assert.AreEqual(track.Points.Count, 26);
        }
    }
}
