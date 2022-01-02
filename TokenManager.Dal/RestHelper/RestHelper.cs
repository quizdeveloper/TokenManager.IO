using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Core.Logger;
using TokenManager.Core.Utils;

namespace TokenManager.Dal.RestHelper
{
    public class RestHelper : IRestHelper
    {
        private string _baseUrl = "";

        public RestHelper(string baseurl)
        {
            this._baseUrl = baseurl;
        }

        public T PostRequest<T>(string action, object data)
        {
            try
            {
                RestClient client = new RestClient
                {
                    BaseUrl = new Uri(_baseUrl)

                };
                IRestRequest request = new RestRequest
                {
                    Resource = action,
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST
                };

                request.AddHeader("Accept", "application/json");

                request.AddBody(data);

                IRestResponse response = client.Execute(request);

                if (response == null || response.Content.StartsWith("<!DOCTYPE html>"))
                    return default(T);

                return NewtonJson.Deserialize<T>(response.Content);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }

            return default(T);
        }

        public T GetRequest<T>(string action)
        {
            try
            {
                RestClient client = new RestClient
                {
                    BaseUrl = new Uri(_baseUrl)

                };
                IRestRequest request = new RestRequest
                {
                    Resource = action,
                    RequestFormat = DataFormat.Json,
                    Method = Method.GET
                };

                request.AddHeader("Accept", "application/json");

                IRestResponse response = client.Execute(request);

                return NewtonJson.Deserialize<T>(response.Content);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }

            return default(T);
        }


        public string PostRequest(string action, object data)
        {
            try
            {
                RestClient client = new RestClient
                {
                    BaseUrl = new Uri(_baseUrl)

                };
                IRestRequest request = new RestRequest
                {
                    Resource = action,
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST
                };

                request.AddHeader("Accept", "application/json");

                request.AddBody(data);

                IRestResponse response = client.Execute(request);

                return response.Content;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }

            return string.Empty;
        }

        public string GetRequest(string action)
        {
            try
            {
                RestClient client = new RestClient
                {
                    BaseUrl = new Uri(_baseUrl)

                };
                IRestRequest request = new RestRequest
                {
                    Resource = action,
                    RequestFormat = DataFormat.Json,
                    Method = Method.GET
                };

                request.AddHeader("Accept", "application/json");

                IRestResponse response = client.Execute(request);

                return response.Content;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.ToString());
            }

            return string.Empty;
        }
    }
}
