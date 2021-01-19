using System;

namespace MonoUploadTelegram.WeTransferAPI.Enums
{
    [Flags]
    public enum CookiesDo
    {
        None = 0,
        Clear = 1,
        Add = 2,
    }
}