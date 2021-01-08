using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WeTransferFileInfoRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public ulong Size { get; set; }
        
        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        public WeTransferFileInfoRequest(string name, ulong size, string itemType) : base()
        {
            this.Name = name;
            this.Size = size;
            this.ItemType = itemType;
        }
    }
}