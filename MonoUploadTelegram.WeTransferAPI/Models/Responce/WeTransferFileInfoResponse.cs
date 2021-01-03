using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Response
{	
    [JsonObject(MemberSerialization.OptIn)]
    public class WeTransferFileInfoResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public ulong Size { get; set; }
        
        [JsonProperty("retries")]
        public int Retries { get; set; }
        
        [JsonProperty("chunk_size")]
        public int ChunkSize { get; set; }
        
        [JsonProperty("item_type")]
        public string ItemType { get; set; }
        
    }
}