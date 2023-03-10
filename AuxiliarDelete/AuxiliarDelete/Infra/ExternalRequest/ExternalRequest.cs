using AuxiliarDelete.Infra.Enum;
using AuxiliarDelete.Infra.ExternalRequest.Interface;
using EnumsNET;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AuxiliarDelete.Infra.ExternalRequest
{
    public class ExternalRequest : IExternalRequest
    {
        public static string? _BASE_URL;
        public static string? _AUTH_URL;
        public static string? _scope;
        public static string? _clientId;
        public static string? _clientSecret;
        public static string? _acessToken;

        private readonly ILogger<Worker> _logger;
        public ExternalRequest(
            IConfiguration _conf,
            ILogger<Worker> logger
            )
        {
            _BASE_URL = _conf["ConectionSettings:BASE_URL"];
            _AUTH_URL = _conf["ConectionSettings:AUTH_URL"];
            _scope = _conf["ConectionSettings:scope"];
            _clientId = _conf["ConectionSettings:identifier"];
            _clientSecret = _conf["ConectionSettings:secret"];
            _logger = logger;
        }

        #region REQUEST | API | OAUTH2 | REQUEST
        public async Task<string> Request(string BodyJson, string url, TipoRequest Method)
        {
            try
            {
                _acessToken = await GetToken();
                using (var client = new HttpClient())
                {
                    var baseUri = new Uri(_BASE_URL);
                    var requestData = new HttpRequestMessage
                    {
                        Method = new HttpMethod(Method.AsString(EnumFormat.Description)),
                        RequestUri = new Uri(baseUri, url)
                    };

                    if (!string.IsNullOrEmpty(BodyJson))
                        requestData.Content = new StringContent(BodyJson);

                    requestData.Headers.Add("Accept", "application/json");
                    requestData.Headers.TryAddWithoutValidation(
                        "Authorization",
                        string.Format("Bearer {0}", _acessToken)
                    );

                    var resultadoRequest = client.SendAsync(requestData).Result;
                    string RequestResultString = resultadoRequest.Content.ReadAsStringAsync().Result;

                    return RequestResultString;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<dynamic> RequestFile(string BodyJson, string url, TipoRequest Method)
        {
            try
            {
                _acessToken = await GetToken();
                using (var client = new HttpClient())
                {
                    var baseUri = new Uri(_BASE_URL);
                    var requestData = new HttpRequestMessage
                    {
                        Method = new HttpMethod(Method.AsString(EnumFormat.Description)),
                        RequestUri = new Uri(baseUri, url)
                    };

                    if (!string.IsNullOrEmpty(BodyJson))
                        requestData.Content = new StringContent(BodyJson);

                    requestData.Headers.Add("Accept", "application/json");
                    requestData.Headers.TryAddWithoutValidation(
                        "Authorization",
                        string.Format("Bearer {0}", _acessToken)
                    );

                    return client.SendAsync(requestData).Result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region GET TOKEN | HTTPCLIENT | RETURN TOKEN | STRING
        public async Task<string> GetToken()
        {
            using (var client = new HttpClient())
            {
                var credentials = ConfigureRequestCredentials();
                HttpContent body = new FormUrlEncodedContent(credentials);
                body.Headers.ContentType = ConfigureHeader();
                var responseResult = client.PostAsync(_AUTH_URL, body).Result;

                string retornoApi = responseResult.Content.ReadAsStringAsync().Result;

                dynamic results;
                results = JsonConvert.DeserializeObject<dynamic>(retornoApi);
                return results.access_token;
            }
        }
        #endregion

        #region REQUEST CREDENTIALS | SET THE REQUEST CREDENTIALS | ICONFIGURATION | CLIENT_CREDENTIALS
        public List<KeyValuePair<string, string>> ConfigureRequestCredentials()
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            postData.Add(new KeyValuePair<string, string>("client_id", _clientId));
            postData.Add(new KeyValuePair<string, string>("client_secret", _clientSecret));
            postData.Add(new KeyValuePair<string, string>("scope", _scope));

            return postData;
        }
        #endregion

        #region CONFIGURE HEADER | RETURN HEADER | MediaTypeHeaderValue
        public MediaTypeHeaderValue ConfigureHeader()
        {
            return new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }
        #endregion
    }
}
