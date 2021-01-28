using System;
using System.IO;
using System.Threading.Tasks;
using MonoUploadTelegram.WeTransferAPI.Models.Response;

namespace MonoUploadTelegram.WeTransferAPI
{
    public static class WeTransferCloud
    {
        private static WeTransferClient _client;

        public static async Task<string> UploadFileAsync(FileInfo[] files) => await Task.Run(() => UploadFiles(files));
        
        public static string UploadFiles(FileInfo[] files)
        {
            _client = new WeTransferClient(files);

            var transferId = _client.GetTransferId();

            for (int i = 0; i < files.Length; i++)
            {
                var file = _client.SendInfoAboutFiles(transferId, i);

                for (int indexChunk = 0, j = 1; j <= GetCountChunks(file); j++)
                {
                    var chunk = Convert.ToInt32(GetChunk(j, file));
                    var partPutUrl = _client.GetPartPutUrl(transferId.Id, file.Id, j, chunk);
                    _client.LoadFileToCloud(partPutUrl.Url, GetFileBytes(files[i], chunk, Convert.ToInt32(indexChunk)));
                    indexChunk += chunk;
                }
            }
                
            return null;
        }

        private static int GetCountChunks(FilesFinalizeMppResponse file) => Convert.ToInt32(file.Size / file.ChunkSize + file.Size % file.ChunkSize);

        private static long GetChunk(int countIteration, FilesFinalizeMppResponse file)
        {
            long chunk;
            
            if (file.Size < file.ChunkSize) chunk = file.Size;
            else
            {
                chunk = file.ChunkSize * countIteration;
                if (file.Size - chunk < 0)
                    chunk = file.Size - file.ChunkSize * (countIteration - 1);
            }
            
            return chunk;
        }

        private static byte[] GetFileBytes(FileInfo file, int chunkSize, int index)
        {
            var bytes = new byte[chunkSize];
            
            using (var binaryReader = new BinaryReader(new StreamReader(Path.Join(file.DirectoryName, file.Name)).BaseStream))
                binaryReader.Read(bytes, index, chunkSize);
            
            return bytes;
        }
    }
}