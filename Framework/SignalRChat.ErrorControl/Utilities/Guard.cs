using System;

namespace SignalRChat.ErrorControl.Utilities
{
    public class Guard
    {
        public static T ArgumentNotNullAndReturn<T>(T argument, string argumentName)
        {
            if (argument == null)
            {
                throw new Exception("Argument is null: " + argumentName);
            }

            return argument;
        }
    }
}
