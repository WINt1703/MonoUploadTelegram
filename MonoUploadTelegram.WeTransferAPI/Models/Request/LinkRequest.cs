using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LinkRequest
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("ui_language")]
        public string Language { get; set; }

        [JsonProperty("domain_user_id")]
        public string UserId { get; set; }
        
        [JsonProperty("files")]
        public WeTransferFileInfoRequest[] Files { get; set; }

        public LinkRequest(string message, string language, string userId, WeTransferFileInfoRequest[] files): base()
        {
            this.Message = message;
            this.Language = language;
            this.UserId = userId;
            this.Files = files;
        }
    }
}