using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PartPutURLRequest
    {
        [JsonProperty("chunk_number")]
        public uint ChunkNumber { get; set; }
        
        [JsonProperty("chunk_size")] 
        public ulong ChunkSize { get; set; }

        [JsonProperty("chunk_size")] 
        public int ChunkCrc { get; set; }

        public PartPutURLRequest(uint chunkNumber, ulong chunkSize, int chunkCrc)
        {
            this.ChunkCrc = chunkCrc;
            this.ChunkNumber = chunkNumber;
            this.ChunkSize = chunkSize;
        }
    }
}