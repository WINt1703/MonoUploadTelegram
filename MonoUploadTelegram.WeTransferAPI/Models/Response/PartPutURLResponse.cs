using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Response
{
    [JsonObject(MemberSerialization.OptIn)]
    internal  class PartPutUrlResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}