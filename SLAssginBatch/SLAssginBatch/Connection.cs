using ServiceLayerFluentMigrator.Connection;
using System.Globalization;
using System.Net;

namespace SLAssginBatch
{
    public class Connection
    {
        private static IServiceLayer _serviceLayerConn;
        private static string _sessionId;
        private static IServiceLayer _serviceLayerConn2;
        private static string _sessionId2;
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
        public static void SetConnection2(
            string url,
            string company,
            string user,
            string pwd
            )
        {
            _serviceLayerConn2 = new ServiceLayer(url);
            _serviceLayerConn2.Connect(company, user, pwd);
            SetSession2();
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
        private static void SetSession2()
        {
            var sessionId = _serviceLayerConn2.SLConnection.LoginResponse.SessionId;
            if (string.IsNullOrEmpty(sessionId))
            {
                var reuslt = _serviceLayerConn2.SLConnection.LoginAsync().Result;
                sessionId = _serviceLayerConn2.SLConnection.LoginResponse.SessionId;
            }
            _sessionId2 = sessionId;
        }
        public static string Post(string serviceLayerResource, string content = null, bool secondConnection = false, bool throwExceptions = true)
        {
            var result = Execute(HttpMethod.Post, serviceLayerResource, content, secondConnection);
            var resultContentString = result.Content.ReadAsStringAsync().Result;

            if (throwExceptions && result.StatusCode != HttpStatusCode.NoContent && result.StatusCode != HttpStatusCode.Created) throw new Exception(resultContentString);
            return resultContentString;
        }
        private static HttpResponseMessage Execute(HttpMethod method, string serviceLayerResource, string content = null, bool secondConnection = false)
        {
            string sessionId = secondConnection ? _sessionId : _sessionId2;
            var handlerClient = new HttpClientHandler();

            handlerClient.ServerCertificateCustomValidationCallback = (s, cert, chain, ssl) => { return true; };
            using (var httpClient = new HttpClient(handlerClient))
            {
                httpClient.BuildDefaultHeaders(_serviceLayerConn.BaseUri, sessionId);
                var request = new HttpRequestMessage(method, serviceLayerResource);
                request.Headers.Add("Connection", "keep-alive");

                request.Content = new StringContent(content);

                var result = httpClient.SendAsync(request).Result;

                return result;
            }
        }
        public static void Patch(string serviceLayerResource, string content = null, bool secondConnection = false, bool throwExceptions = true)
        {
            var result = Execute(HttpMethod.Patch, serviceLayerResource, content, secondConnection);

            if (throwExceptions && result.StatusCode != HttpStatusCode.NoContent && result.StatusCode != HttpStatusCode.Created)
            {
                var resultContentString = result.Content.ReadAsStringAsync().Result;
                throw new Exception(resultContentString);
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
