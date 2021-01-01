namespace MonoUploadTelegram.Parser
{
    public interface IParserSetting
    {
        public string BaseURL { get; set; }
        public string File { get; set; }
    }
}