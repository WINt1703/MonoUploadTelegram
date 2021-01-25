using System;
using System.IO;
using MonoUploadTelegram.Parser;
using MonoUploadTelegram.WeTransferAPI;

namespace MonoUploadTelegram
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new FileInfo[]
            {
                new FileInfo(@"/home/wint1703/Desktop/Counter-Strike Global Offensive.desktop"), 
                new FileInfo(@"/home/wint1703/Desktop/TLauncher.desktop"),
            };
            
            var link = WeTransferCloud.UploadFiles(files);
            
            Console.WriteLine(link);

            Console.Read();
        }
    }
}