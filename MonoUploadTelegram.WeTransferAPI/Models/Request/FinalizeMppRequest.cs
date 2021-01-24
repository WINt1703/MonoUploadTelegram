using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    internal  class FinalizeMppRequest
    {
        [JsonProperty("chunk_size")]
        public int ChunkSize { get; set; }
    }
}