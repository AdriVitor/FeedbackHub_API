using System.Net;

namespace FeedbackService_Application.Structs
{
    public struct EndpointReturn
    {
        public string Data { get; }
        public HttpStatusCode StatusCode { get; }

        public EndpointReturn(string data, HttpStatusCode statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }
    }
}
