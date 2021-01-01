using System;
using MonoUploadTelegram.Parser;
using MonoUploadTelegram.WeTransferAPI;

namespace MonoUploadTelegram
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new ParserWorker(new WeTransferParser(), new WeTransferParserSetting());
            loader.GetSessionInfoFromPage();
            var client = new WeTransferClien();
            client.UploadLink();
        }
    }
}