using System.Net;

namespace homeMaintenance.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode ErrorCode;
        public string ErrorMessage;

        public CustomException(HttpStatusCode code, string message)
        {
            ErrorCode = code;
            ErrorMessage = message;
        }
    }
}
