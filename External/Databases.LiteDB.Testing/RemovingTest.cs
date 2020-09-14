using System.IO;

using ArticulationManager.Common.Testing;
using ArticulationManager.Databases.LiteDB.Articulations;

using NUnit.Framework;

namespace ArticulationManager.Databases.LiteDB.Testing
{
    [TestFixture]
    public class RemovingTest
    {
        [Test]
        public void RemoveTest()
        {
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var record = TestDataGenerator.CreateKeySwitch();

            #region Delete by Id
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record );
            Assert.AreEqual( 0, repository.Count() );
            #endregion

            #region Delete by DeveloperName and ProductName
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.Delete( record.DeveloperName, record.ProductName );
            Assert.AreEqual( 0, repository.Count() );
            #endregion

            #region Delete All
            repository.Save( record );
            Assert.AreEqual( 1, repository.Count() );

            repository.DeleteAll();
            Assert.AreEqual( 0, repository.Count() );
            #endregion

        }
    }
}