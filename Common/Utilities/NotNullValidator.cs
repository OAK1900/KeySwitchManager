using KeySwitchManager.Common.Exceptions;

namespace KeySwitchManager.Common.Utilities
{
    public static class NotNullValidator
    {
        public static void Validate<T>( T obj )
        {
            if( obj == null )
            {
                throw new ObjectIsNullException();
            }
        }
    }
}