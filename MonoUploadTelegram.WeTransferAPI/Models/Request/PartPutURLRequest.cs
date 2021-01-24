using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    internal  class PartPutUrlRequest
    {
        [JsonProperty("chunk_number")]
        public int ChunkNumber { get; set; }
        
        [JsonProperty("chunk_size")] 
        public int ChunkSize { get; set; }

        [JsonProperty("chunk_size")] 
        public int ChunkCrc { get; set; }

        public PartPutUrlRequest(int chunkNumber, int chunkSize, int chunkCrc)
        {
            this.ChunkCrc = chunkCrc;
            this.ChunkNumber = chunkNumber;
            this.ChunkSize = chunkSize;
        }
    }
}