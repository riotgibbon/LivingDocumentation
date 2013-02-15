using System.Threading.Tasks;

namespace SpecFlowLivingDoc
{
    public interface IHttpRestRequestService
    {
        //TODO - these properties don't make sense, all clumped and universal, move to a paramter object
        string TargetResource { get; set; }
        string RequestMethod { get; set; }
        string ContentType { get; set; }
        HttpDecompressionMethods HttpDecompressionMethod { get; set; }
        string BaseUrl { get; set; }
        HttpRestResponse GetRestResponse();//TODO - not accurate
        void AddAcceptHeader(string acceptHeaderValue);
        void SetContent(string content, string contentType);
    }

    public class HttpRestResponse
    {
        public HttpStatusType ResponseStatusType { get; set; }
        public bool IsSuccessfulStatusCode { get; set; }
        public string ResponseText { get; set; }
    }

    public enum HttpStatusType
    {
        Ok,
        Created,
        Conflict,
        NotFound
    }

    public enum HttpDecompressionMethods
    {
        Gzip
    }
}