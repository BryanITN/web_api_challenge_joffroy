using System;

namespace web_api_challenge.Exceptions
{
    public class JoffroyException:Exception
    {
        public JoffroyException(string message) : base(message)
        {
        }

        public JoffroyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}