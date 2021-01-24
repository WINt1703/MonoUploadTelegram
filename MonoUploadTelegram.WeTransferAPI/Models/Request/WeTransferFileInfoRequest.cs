using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    internal  class WeTransferFileInfoRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }
        
        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        public WeTransferFileInfoRequest(string name, long size, string itemType) : base()
        {
            this.Name = name;
            this.Size = size;
            this.ItemType = itemType;
        }
    }
}