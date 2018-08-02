using Nancy;

namespace TryingNancy.Trying.Models
{
    public class ResponseModel<T>
    {
        public T ResponseBody { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
