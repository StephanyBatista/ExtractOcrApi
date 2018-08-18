using System.Threading.Tasks;

namespace ExtractOcrApi
{
    public class DocumentToText
    {
        public async Task<string> Extract(string type, string filepath)
        {
            if (type == "jpeg" || type == "jpg" || type == "png")
                return await $"tesseract {filepath} stdout".Bash();
            if (type == "pdf")
                return await $"pdf2txt.py {filepath}".Bash();
            if (type == "docx")
                return await $"docx2txt {filepath}".Bash();

            return string.Empty;
        }
    }
}