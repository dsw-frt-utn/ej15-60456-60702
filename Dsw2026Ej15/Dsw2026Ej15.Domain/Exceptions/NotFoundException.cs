using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Exceptions
{
    public class NotFoudException : Exception
    {
        public NotFoudException(string message) : base(message) { }
    }
}
