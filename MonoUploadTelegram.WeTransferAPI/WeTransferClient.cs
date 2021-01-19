using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MonoUploadTelegram.WeTransferAPI.Enums;
using MonoUploadTelegram.WeTransferAPI.Models.Request;
using MonoUploadTelegram.WeTransferAPI.Models.Responce;
using MonoUploadTelegram.WeTransferAPI.Models.Response;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using RestSharp.Serializers.Utf8Json;

namespace MonoUploadTelegram.WeTransferAPI
{
    internal class WeTransferClient
    {
        #region Fields
        
        private FileInfo _aboutFile;
        private readonly RestClient _client;
        
        #endregion

        #region Properties

        public bool IsUpload { get; private set; }
        public FileInfo AboutFile
        {
            get => _aboutFile;
            set 
            {
                if (!IsUpload)
                    _aboutFile = value;
            }
        }

        #endregion

        public WeTransferClient(FileInfo fileInfo, string baseUrl = "https://wetransfer.com")
        {
            this.IsUpload = false;
            this._client = new RestClient(baseUrl);
            this._client.ThrowOnAnyError = true;
            this._client.Encoding = Encoding.UTF8;
            this._client.UseUtf8Json();
            this._client.UseNewtonsoftJson();
            this.AboutFile = fileInfo;
        }

        #region Methods
        
        public LinkFinalizeResponce UploadLink(string file = "/api/v4/transfers/link", string message = "")
        {            
            var request = new LinkRequest(message, "en", SessionInfo.UserId,
                new[] {new WeTransferFileInfoRequest(AboutFile.Name, (ulong)AboutFile.Length, "file")});
            
            return JsonConvert.DeserializeObject<LinkFinalizeResponce>(GetResponse(Method.POST, request, file, CookiesDo.Add | CookiesDo.Clear).Content);
        }

        public FilesFinalizeMppResponse UploadFiles(LinkFinalizeResponce model)
        {
            var file = $"/api/v4/transfers/{model.Id}/files";
            var request = new FilesRequest(model.Files.First().Name, model.Files.First().Size);
            
            return JsonConvert.DeserializeObject<FilesFinalizeMppResponse>(GetResponse(Method.POST, request, file, CookiesDo.Add | CookiesDo.Clear).Content);
        }

        public PartPutUrlResponse GetPartPutUrl(string transferId, string filesId, uint indexChunk, ulong chunckSize)
        {
            var file = $"/api/v4/transfers/{transferId}files/{filesId}/part-put-url";
            var request = new PartPutURLRequest(indexChunk, chunckSize, 0);
            
            return JsonConvert.DeserializeObject<PartPutUrlResponse>(GetResponse(Method.POST, request, file,CookiesDo.Add | CookiesDo.Clear).Content);
        }

        public void LoadFileToCloud(string url)
        {
            var baseUrl = _client.BaseUrl;
            _client.BaseUrl = new Uri(url);
            
            GetResponse(Method.OPTIONS, default(string), null, CookiesDo.None, HttpBody.None, ResponseDo.PassResponse);
            GetResponse(Method.PUT, default(string), null, CookiesDo.None, HttpBody.Xml, ResponseDo.PassResponse);
        }
        
        private RestResponse GetResponse<T>(Method method, T model, string file, CookiesDo cookiesDo, HttpBody body = HttpBody.Json, ResponseDo responseDo = ResponseDo.GetResponse)
        {
            IsUpload = true;
            var request = new RestRequest(file);

            request.AddHeaders(GetRequestHeaders(SessionInfo.Token));

            CookiesWorker(cookiesDo, request);

            AddHttpBody(body, request, model);

            return ResponseWorker(method, responseDo, _client, request);
        }

        private static RestResponse ResponseWorker(Method method, ResponseDo responseDo, RestClient client, RestRequest request)
        {
            RestResponse response = default;
        
            switch (responseDo)
            {
                case ResponseDo.GetResponse:
                    response = (RestResponse)client.Execute(request, method);
                    
                    foreach (var item in response.Cookies)
                        SessionInfo.Cookies?.TryAdd(item.Name, item.Value);
                    break;
            }

            return response;
        }
        
        private static void AddHttpBody<T>(HttpBody body, RestRequest request, T model)
        {
            switch (body)
            {
                case HttpBody.Json:
                    request.AddJsonBody(model);
                    break;
                case HttpBody.Xml:
                    request.AddXmlBody(model);
                    break;
            }
        }
        
        private static void CookiesWorker(CookiesDo cookiesDo, RestRequest request)
        {
            if((cookiesDo & CookiesDo.Add) == CookiesDo.Add)
                SessionInfo.Cookies?.ToList()
                    .ForEach(cookie =>request.AddCookie(cookie.Key, cookie.Value));
            
            if((cookiesDo & CookiesDo.Clear) == CookiesDo.Clear)
                SessionInfo.Cookies?.Clear();
        }
        
        private static Dictionary<string, string> GetRequestHeaders(string token)
        {
            return new ()
            {
                ["Content-Type"] = "application/json;charset=utf-8",
                ["Connection"] = "keep-alive",
                ["DNT"] = "1",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; rv:78.0) Gecko/20100101 Firefox/78.0",
                ["HOST"] = "wetransfer.com",
                ["Origin"] = "https://wetransfer.com",
                ["Referer"] = "https://wetransfer.com/",
                ["TE"] = "Trailers",
                ["Accept-Language"] = "en-US,en;q=0.5",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Accept"] = "application/json, text/plain, */*",
                ["X-CSRF-Token"] = token,
                ["X-Requested-With"] = "XMLHttpRequest",
            };
        }

        #endregion
    }
}