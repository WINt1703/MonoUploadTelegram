using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Responce
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PartPutUrlResponce
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}