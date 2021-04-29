using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Linq;
using TwoFace.Cache.Abstract;
using TwoFace.Serialization.Concrete;
using TwoFace.Tooling.Abstract;

namespace TwoFace.Client.Abstract
{
    public class BaseClient : RestClient
    {
        protected ICacheService _cache;
        protected IErrorLogger _errorLogger;
        protected IDeserializer _serializer;
        public BaseClient(ICacheService cache,
                          IDeserializer serializer,
                          IErrorLogger errorLogger,
                          string baseUrl)
        {
            _cache = cache;
            _errorLogger = errorLogger;
            _serializer = serializer;

            // Adds JSON serialization handlers
            AddHandler("application/json", () => { return serializer; });
            AddHandler("text/json", () => { return serializer; });
            AddHandler("text/x-json", () => { return serializer; });
            AddHandler("text/javascript", () => { return serializer; });
            AddHandler("*+json", () => { return serializer; });

            BaseUrl = new Uri(baseUrl);
        }

        private void TimeoutCheck(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == 0)
            {
                LogError(BaseUrl, request, response);
            }
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            var response = base.Execute(request);
            TimeoutCheck(request, response);
            return response;
        }
        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            var response = base.Execute<T>(request);
            TimeoutCheck(request, response);
            return response;
        }

        public T Post<T>(IRestRequest request) where T : new()
        {
            request.JsonSerializer = new JsonSerializer();
            var response = Execute<T>(request, Method.POST);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                LogError(BaseUrl, request, response);
                return default(T);
            }
        }

        public T Get<T>(IRestRequest request) where T : new()
        {
            var response = Execute<T>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                LogError(BaseUrl, request, response);
                return default(T);
            }
        }

        public T GetFromCache<T>(IRestRequest request, string cacheKey)
          where T : class, new()
        {
            var item = _cache.Get<T>(cacheKey);
            if (item == null)
            {
                var response = Execute<T>(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _cache.Set(cacheKey, response.Data);
                    item = response.Data;
                }
                else
                {
                    LogError(BaseUrl, request, response);
                    return default(T);
                }
            }
            return item;
        }

        private void LogError(Uri BaseUrl,
                              IRestRequest request,
                              IRestResponse response)
        {
            //Get the values of the parameters passed to the API
            string parameters = string.Join(", ", request.Parameters.Select(x => x.Name.ToString() + "=" + ((x.Value == null) ? "NULL" : x.Value)).ToArray());

            //Set up the information message with the URL, 
            //the status code, and the parameters.
            string info = "Request to " + BaseUrl.AbsoluteUri
                          + request.Resource + " failed with status code "
                          + response.StatusCode + ", parameters: "
                          + parameters + ", and content: " + response.Content;

            //Acquire the actual exception
            Exception ex;
            if (response != null && response.ErrorException != null)
            {
                ex = response.ErrorException;
            }
            else
            {
                ex = new Exception(info);
                info = string.Empty;
            }

            //Log the exception and info message
            _errorLogger.LogError(ex, info);
        }
        }
}
