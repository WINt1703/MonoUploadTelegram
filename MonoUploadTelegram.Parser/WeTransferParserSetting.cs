namespace MonoUploadTelegram.Parser
{
    public class WeTransferParserSetting : IParserSetting
    {
        public string BaseURL { get; set; } 
        public string File { get; set; }

        public WeTransferParserSetting(string baseUrl = "https://wetransfer.com", string file = null)
        {
            this.File = file;
            this.BaseURL = baseUrl;
        }
    }
}