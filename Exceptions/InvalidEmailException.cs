using System;

namespace Task2.Exceptions
{
    internal class InvalidEmailException:Exception
    {
        private string _message;

        internal InvalidEmailException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }
    }
}
