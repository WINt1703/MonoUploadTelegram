using Newtonsoft.Json;

namespace MonoUploadTelegram.WeTransferAPI.Models.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    internal  class FilesRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        public FilesRequest(string name, long size): base()
        {
            this.Name = name;
            this.Size = size;
        }
    }
}