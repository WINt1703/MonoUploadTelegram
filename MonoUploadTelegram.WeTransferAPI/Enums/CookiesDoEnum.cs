using System;

namespace MonoUploadTelegram.WeTransferAPI.Enums
{
    [Flags]
    internal enum CookiesDo
    {
        None = 0,
        Clear = 1,
        Add = 2,
    }
}