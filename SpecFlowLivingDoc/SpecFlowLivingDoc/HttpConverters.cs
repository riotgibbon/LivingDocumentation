using System.Collections.Generic;
using System.Net;

namespace SpecFlowLivingDoc
{
    public static class HttpConverters
    {
        public static HttpStatusType ToStatusType(this HttpStatusCode httpStatusCode)
        {
            var statusList = new Dictionary<HttpStatusCode, HttpStatusType>();
            statusList.Add(HttpStatusCode.OK, HttpStatusType.Ok);
            statusList.Add(HttpStatusCode.Conflict, HttpStatusType.Conflict);
            statusList.Add(HttpStatusCode.Created, HttpStatusType.Created);
            statusList.Add(HttpStatusCode.NotFound, HttpStatusType.NotFound);
            return statusList[httpStatusCode];
        }
    }
}
