using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SpecFlowLivingDoc;
using DecompressionMethods = System.Net.DecompressionMethods;

namespace HAClient.HapiNet.Services
{
    public class HttpRestRequestService : IHttpRestRequestService
    {
        public HttpRestRequestService()
        {

        }
        private string contentBody;
        private List<string> acceptHeaders = new List<string>();
        public string TargetResource { get; set; }
        public string RequestMethod { get; set; }
        public string ContentType { get; set; }
        //

        public string BaseUrl { get; set; }

        public HttpRestResponse GetRestResponse()
        {

            using (var httpClient = new HttpClient(GetHandler()))
            {
                httpClient.BaseAddress = new Uri(BaseUrl);
                foreach (var acceptHeader in acceptHeaders)
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(GetAcceptHeader(acceptHeader));
                }
                var httpRequestMessage = GetHttpRequestMessage();
                var response = httpClient.SendAsync(httpRequestMessage).Result;
                var httpRestResponse = GetHttpRestResponse(response);
                return httpRestResponse;
            }


        }

        private HttpClientHandler GetHandler()
        {
            return new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                };
        }

        private HttpRequestMessage GetHttpRequestMessage()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, TargetResource)
                                         {
                                             Content =  new StringContent( contentBody)
                                         };
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            //TODO - make conditional
            httpRequestMessage.Headers.Authorization = GetAuthorization();
            return httpRequestMessage;
        }

        private static AuthenticationHeaderValue GetAuthorization()
        {
            
            var userName = ConfigurationManager.AppSettings["UserName"];
            var password = ConfigurationManager.AppSettings["Password"];
            return new AuthenticationHeaderValue("Basic",
                                                 Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password))));
        }

        private static HttpRestResponse GetHttpRestResponse(HttpResponseMessage response)
        {
            var responseContent =  response.Content.ReadAsStringAsync().Result;
            var statusCode = response.StatusCode;
            var httpRestResponse = new HttpRestResponse
                {
                    ResponseStatusType = statusCode.ToStatusType(),
                    IsSuccessfulStatusCode = response.IsSuccessStatusCode,
                    ResponseText = responseContent
                };
            return httpRestResponse;
        }


        private static MediaTypeWithQualityHeaderValue GetAcceptHeader(string acceptHeaderValue)
        {
            return new MediaTypeWithQualityHeaderValue(acceptHeaderValue);
        }


        public void AddAcceptHeader(string acceptHeaderValue)
        {
            acceptHeaders.Add(acceptHeaderValue);
        }

        public void SetContent(string content, string contentType)
        {
            this.contentBody = content;
            ContentType = contentType;
        }





        public HttpDecompressionMethods HttpDecompressionMethod { get; set; }
    }


}
