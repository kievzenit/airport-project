using System;

namespace AirportProject.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base(message)
        {

        }
    }
}
