using ServiceLayerFluentMigrator.Connection;
using System.Globalization;
using System.Net;

namespace SLAssginBatch
{
    public class Connection
    {
        private static IServiceLayer _serviceLayerConn;
        private static string _sessionId;
        public static CultureInfo Culture = new CultureInfo("en-US");
        public static void SetConnection(
            string url,
            string company,
            string user,
            string pwd
            )
        {
            _serviceLayerConn = new ServiceLayer(url);
            _serviceLayerConn.Connect(company, user, pwd);
            SetSession();
        }
        private static void SetSession()
        {
            var sessionId = _serviceLayerConn.SLConnection.LoginResponse.SessionId;
            if (string.IsNullOrEmpty(sessionId))
            {
                var reuslt = _serviceLayerConn.SLConnection.LoginAsync().Result;
                sessionId = _serviceLayerConn.SLConnection.LoginResponse.SessionId;
            }
            _sessionId = sessionId;
        }
        public static string Post(string serviceLayerResource, string content = null, bool throwExceptions = true)
        {
            string sessionId = _sessionId;
            var handlerClient = new HttpClientHandler();

            handlerClient.ServerCertificateCustomValidationCallback = (s, cert, chain, ssl) => { return true; };
            using (var httpClient = new HttpClient(handlerClient))
            {
                httpClient.BuildDefaultHeaders(_serviceLayerConn.BaseUri, sessionId);
                var request = new HttpRequestMessage(HttpMethod.Post, serviceLayerResource);
                request.Headers.Add("Connection", "keep-alive");

                request.Content = new StringContent(content);

                var result = httpClient.SendAsync(request).Result;
                var resultContentString = result.Content.ReadAsStringAsync().Result;

                if (throwExceptions && result.StatusCode != HttpStatusCode.NoContent && result.StatusCode != HttpStatusCode.Created) throw new Exception(resultContentString);
                return resultContentString;
            }
        }
    }
    public static class ExtensionMethods
    {
        public static void BuildDefaultHeaders(this HttpClient httpClient, string baseUri, string sessionId)
        {
            httpClient.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cookie", $"B1SESSION={sessionId}");

            httpClient.BaseAddress = baseUri.EndsWith("/") ? new Uri(baseUri) : new Uri($"{baseUri}/");
        }
    }
}
