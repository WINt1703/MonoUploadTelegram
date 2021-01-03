using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Responce
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FilesFinalizeMppResponse
    {
        [JsonProperty("id")] 
        public string Id { get; set; }
        
        [JsonProperty("name")] 
        public string Name { get; set; }
        
        [JsonProperty("retries")] 
        public uint Retries { get; set; }
        
        [JsonProperty("size")] 
        public ulong Size { get; set; }
        
        [JsonProperty("item_type")] 
        public string ItemType { get; set; } 
        
        [JsonProperty("chunk_size")] 
        public ulong ChunkSize { get; set; }
    }
}