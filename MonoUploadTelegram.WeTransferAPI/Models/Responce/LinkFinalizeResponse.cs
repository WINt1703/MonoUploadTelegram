using MonoUploadTelegram.WeTransferAPI.Models.Request;
using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Responce
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LinkFinalizeResponce
    {
        [JsonProperty ("success")]
        public bool Success { get; set; }

        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("message")]
        public string Message { get; set; }
        
        [JsonProperty ("state")]
        public string State { get; set; }

        [JsonProperty ("url")]
        public string Url { get; set; }

        [JsonProperty ("expires_at")]
        public string ExpiresAt { get; set; }
        
        [JsonProperty ("files")]
        public WeTransferFileInfoResponse[] Files { get; set; }
    }
}