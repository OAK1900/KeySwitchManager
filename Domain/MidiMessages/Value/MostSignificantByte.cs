namespace ArticulationManager.Domain.MidiMessages.Value
{
    public class MostSignificantByte : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public MostSignificantByte( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}