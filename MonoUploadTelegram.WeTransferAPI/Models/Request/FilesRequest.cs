using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FilesRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public ulong Size { get; set; }

        public FilesRequest(string name, ulong size): base()
        {
            this.Name = name;
            this.Size = size;
        }
    }
}