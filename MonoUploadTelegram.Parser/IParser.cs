using AngleSharp.Html.Dom;

namespace MonoUploadTelegram.Parser
{
    public interface IParser
    {
        string GetCSRFToken(IHtmlDocument page);
        string GetUserId(IHtmlDocument page);
    }
}