using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FinalizeMppRequest
    {
        [JsonProperty("chunk_size")]
        public uint ChunkSize { get; set; }
    }
}