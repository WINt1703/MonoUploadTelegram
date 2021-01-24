using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Response
{
    [JsonObject(MemberSerialization.OptIn)]
    internal  class FilesFinalizeMppResponse
    {
        [JsonProperty("id")] 
        public string Id { get; set; }
        
        [JsonProperty("name")] 
        public string Name { get; set; }
        
        [JsonProperty("retries")] 
        public int Retries { get; set; }
        
        [JsonProperty("size")] 
        public long Size { get; set; }
        
        [JsonProperty("item_type")] 
        public string ItemType { get; set; } 
        
        [JsonProperty("chunk_size")] 
        public int ChunkSize { get; set; }
    }
}