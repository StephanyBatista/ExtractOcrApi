using System.Diagnostics;
using System.Threading.Tasks;

namespace ExtractOcrApi
{
    public static class ShellHelper
    {
        public async static Task<string> Bash(this string cmd)
        {
            var process = GetProcess(cmd);
            process.Start();
            string result = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit();
            return result;
        }

        private static Process GetProcess(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            return new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
        }
    }
}