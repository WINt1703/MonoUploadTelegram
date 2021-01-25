using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace MonoUploadTelegram.Parser
{
    public class WeTransferParser : IParser
    {
        public string GetCSRFToken(IHtmlDocument page)=> page.QuerySelectorAll<IHtmlMetaElement>("meta").First(item => item?.Name == "csrf-token").Content;
        public string GetUserId(IHtmlDocument page)
        {
            var script = page
                .QuerySelectorAll<IHtmlScriptElement>("script")
                .Where(s => s.Text.Contains("var __launch_darkly__"))?
                .FirstOrDefault();

            var valueRegex = "\"key\":\"" + @"\S{36}" + "\"";
            Regex regex = new Regex(valueRegex);
            MatchCollection matches = regex.Matches(script.Text);
            
            return matches[0].Value.Remove(0,6).Replace("\"", null);
        } 
    }
}