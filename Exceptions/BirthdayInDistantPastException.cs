using System;

namespace Task2.Exceptions
{  
    internal class BirthdayInDistantPastException:Exception
    {
        private string _message;

        internal BirthdayInDistantPastException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }
    }
}
