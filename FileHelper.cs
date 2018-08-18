using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExtractOcrApi
{
    public class FileHelper
    {
        public async Task<FileResult> GetAndSaveFile(string url, string type, string rootPath)
        {
            using (var client = new HttpClient())
            {
                using (var result = await client.GetAsync(url))
                {
                    if (!result.IsSuccessStatusCode) return new FileResult { Success = false, Error = "Error to download file" };

                    var fileName = $"{Guid.NewGuid().ToString()}.{type}";
                    var filepath = Path.Combine(rootPath, $"{fileName}");
                    if (!await SaveFile(result, filepath))
                        return new FileResult { Success = false, Error = "Error to save file" };

                    return new FileResult { Success = true, FilePath = filepath };   
                }
            }
        }

        private static async Task<bool> SaveFile(HttpResponseMessage result, string filepath)
        {
            try {
                var byteArray = await result.Content.ReadAsByteArrayAsync();
                System.IO.File.WriteAllBytes(filepath, byteArray);
                return true;
            } catch { return false; }
        }

        public void DeleteFile(string path)
        {
            System.IO.File.Delete(path);
        }
    }

    public class FileResult
    {
        public string FilePath { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}