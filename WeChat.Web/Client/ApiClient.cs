using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using XData.Diagnostics.Log;

namespace WeChat.Api.Client
{
    public partial class ApiClient
    {
        protected HttpClient HttpClient = new HttpClient();

        // Initialize HttpClient
        protected ApiClient()
        {
            string baseAddress = ConfigurationManager.AppSettings["BaseAddress"];
            HttpClient.BaseAddress = new Uri(baseAddress);
        }

        // IP Address List
        public Dictionary<string, List<string>> GetCallbackIP()
        {
            string accessToken = GetAccessToken();

            string relativeUri = string.Format("/cgi-bin/getcallbackip?access_token={0}", accessToken);

            // {"ip_list":["127.0.0.1","127.0.0.1"]}
            // {"errcode":40013,"errmsg":"invalid appid"}
            string json = ApiGet(relativeUri);
            Dictionary<string, List<string>> result = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            return result;
        }

        protected string ApiGet(string relativeUri)
        {
            HttpResponseMessage response = HttpClient.GetAsync(relativeUri).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            Log(json);
            return json;
        }

        protected string ApiPost(string relativeUri, HttpContent content)
        {
            HttpResponseMessage response = HttpClient.PostAsync(relativeUri, content).Result;
            string json = response.Content.ReadAsStringAsync().Result;
            Log(json);
            return json;
        }

        protected void Log(string json)
        {
            if (json.Contains("errcode"))
            {
                Log4.Logger.Error(json);
            }
            else
            {
                Log4.Logger.Debug(json);
            }
        }

        private static ApiClient Instance = null;
        public static ApiClient Create()
        {
            if (Instance == null)
            {
                Instance = new ApiClient(); 
            }
            return Instance;
        }


    }
}