using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MonoUploadTelegram.WeTransferAPI;
using RestSharp;

namespace MonoUploadTelegram.Parser
{
    public class HtmlLoader
    {
        private RestClient _client;
        private string _url;

        public HtmlLoader(IParserSetting setting, RestClient client)
        {
            this._url = $"{setting.BaseURL}/{setting.File}";
            this._client = client;
            SessionInfo.Cookies = new Dictionary<string, string>();
        }

        public string GetSourcePage()
        {
            _client.BaseUrl = new Uri(_url);
            var request = new RestRequest();
            request.AddHeaders(new Dictionary<string, string>
            {
                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Accept-Language"] = "en-US,en;q=0.5",
            });
            
            var response = (RestResponse)_client.Execute(request, Method.GET);
            string source = null;

            if (response?.StatusCode == HttpStatusCode.OK)
                source = response.Content;

            foreach (var item in response.Cookies)
                SessionInfo.Cookies.Add(item.Name, item.Value);
                
            return source;
        }
    }
}