using System;

namespace Task2.Exceptions
{
    internal class BirthdayInFutureException: Exception
    {
        private string _message;

        internal BirthdayInFutureException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }

    }
}
