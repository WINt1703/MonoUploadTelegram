using System.Collections.Generic;

namespace MonoUploadTelegram.WeTransferAPI
{
    public static class SessionInfo
    {
        public static string Token { get; set; }
        public static string UserId { get; set; }
        public static IDictionary<string, string> Cookies { get; set; }
    }
}