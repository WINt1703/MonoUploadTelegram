using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using MonoUploadTelegram.WeTransferAPI;
using RestSharp;

namespace MonoUploadTelegram.Parser
{
    public class ParserWorker
    {
        private readonly IParser _parser;
        private readonly IParserSetting _settingParser;
        private readonly IHtmlDocument _document;

        public ParserWorker(IParser parser, IParserSetting settingParser)
        {
            this._parser = parser;
            this._settingParser = settingParser;
            var loader = new HtmlLoader(_settingParser, new RestClient());
            
            _document = new HtmlParser().ParseDocument(loader.GetSourcePage());
        }

        public void GetSessionInfoFromPage()
        {
            SessionInfo.Token = _parser.GetCSRFToken(_document);
            SessionInfo.UserId = _parser.GetUserId(_document);
        }
    }
}