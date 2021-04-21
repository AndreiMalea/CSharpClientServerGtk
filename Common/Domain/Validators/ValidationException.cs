using System;

namespace Common.Domain.Validators
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(String message): base(message)
        {
        }
    }
}