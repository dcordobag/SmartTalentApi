namespace SmartTalent.Hotel.Api.Base.Models
{
    using System.Net;

    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
