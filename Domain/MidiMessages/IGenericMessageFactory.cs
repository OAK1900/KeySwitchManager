using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface IGenericMessageFactory
    {
        public GenericMessage Create( int status, int data1, int data2 );

        public class DefaultFactory : IGenericMessageFactory
        {
            public GenericMessage Create( int status, int data1, int data2 )
            {
                return new GenericMessage(
                    new StatusCode( status ),
                    new GenericData( data1 ),
                    new GenericData( data2 )
                );
            }
        }
    }
}