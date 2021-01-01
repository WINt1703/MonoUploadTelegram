using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoUploadTelegram.WeTransferAPI.Models;
using MonoUploadTelegram.WeTransferAPI.Models.Request;
using MonoUploadTelegram.WeTransferAPI.Models.Responce;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using RestSharp.Serializers.Utf8Json;

namespace MonoUploadTelegram.WeTransferAPI
{
    public class WeTransferClien
    {
        private readonly RestClient _client;
        public WeTransferClien(string baseUrl = "https://wetransfer.com")
        {
            this._client = new RestClient(baseUrl);
            this._client.ThrowOnAnyError = true;
            this._client.Encoding = Encoding.UTF8;
            this._client.UseUtf8Json();
            this._client.UseNewtonsoftJson();
        }

        public LinkFinalizeResponce UploadLink(string file = "/api/v4/transfers/link", string message = "")
        {
            var request = new LinkRequest(message, "en", SessionInfo.UserId,
                new[] {new WeTransferFileInfo("asd.txt", 400, "file")});
            
            return JsonConvert.DeserializeObject<LinkFinalizeResponce>(GetResponce(Method.POST, request, file, CookiesDo.Add | CookiesDo.Clear).Content);
        }

        public FilesFinalizeMppResponce UploadFiles(LinkFinalizeResponce model)
        {
            var file = $"/api/v4/transfers/{model.Id}/files";
            var request = new FilesRequest(model.Files[0].Name, model.Files[0].Size);
            
            return JsonConvert.DeserializeObject<FilesFinalizeMppResponce>(GetResponce(Method.POST, request, file, CookiesDo.Add | CookiesDo.Clear).Content);
        }
        
        private RestResponse GetResponce<T>(Method method, T model, string file, CookiesDo cookiesDo)
        {
            var request = new RestRequest(file);

            request.AddHeaders(GetRequestHeaders(SessionInfo.Token));

            if(cookiesDo == CookiesDo.Add)
                SessionInfo.Cookies?.ToList()
                    .ForEach(cookie =>request.AddCookie(cookie.Key, cookie.Value));
            
            if(cookiesDo == CookiesDo.Clear)
                SessionInfo.Cookies?.Clear();
            
            request.AddJsonBody(model);
            
            var response = _client.Execute(request, method);

            foreach (var item in response.Cookies)
                SessionInfo.Cookies?.TryAdd(item.Name, item.Value);
                
            return (RestResponse)response;
        }

        private ulong CountChunk(ulong chunkSize, FilesFinalizeMppResponce model)
        {
            if (model.Size / chunkSize > 0) return model.ChunkSize;
            else if (model.Size % chunkSize > 0) return model.ChunkSize - chunkSize;
            else return 0;
        }
        
        private Dictionary<string, string> GetRequestHeaders(string token)
        {
            return new Dictionary<string, string>
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
    }
}