using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class MostSignificantByteTest
    {
        [TestCase( MidiMostSignificantByte.MinValue - 1 )]
        [TestCase( MidiMostSignificantByte.MaxValue + 1 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new MidiMostSignificantByte( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var byte1 = new MidiMostSignificantByte( 1 );
            var byte2 = new MidiMostSignificantByte( 2 );
            Assert.IsTrue( byte1.Equals( new MidiMostSignificantByte( 1 ) ) );
            Assert.IsFalse( byte1.Equals( byte2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiMostSignificantByte( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiMostSignificantByte( 1 ).ToString() == "1" );
        }
    }
}