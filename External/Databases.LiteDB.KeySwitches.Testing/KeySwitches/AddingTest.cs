using System.Collections.Generic;
using System.IO;
using System.Linq;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Common.Testing;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Databases.LiteDB.KeySwitches.Testing.KeySwitches
{
    [TestFixture]
    public class AddingTest
    {
        [Test]
        public void AddTest()
        {
            var repository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var articulation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn>()
                {
                    new MidiNoteOn( new MidiNoteNumber( 1 ), new MidiVelocity( 100 ) )
                },
                new List<MidiControlChange>(),
                new List<MidiProgramChange>()
            );
            var record = TestDataGenerator.CreateKeySwitch( articulation );

            repository.Save( record );

            var seq = repository.Find( record.ProductName );
            var cmp = seq.First();
            Assert.AreEqual( record, cmp );

            seq = repository.Find( record.DeveloperName );
            cmp = seq.First();
            Assert.AreEqual( record, cmp );

            seq = repository.Find( record.ProductName );
            cmp = seq.First();
            Assert.AreEqual( record, cmp );

            seq = repository.Find( record.DeveloperName, record.ProductName, record.InstrumentName );
            cmp = seq.First();
            Assert.AreEqual( record, cmp );

        }
    }
}