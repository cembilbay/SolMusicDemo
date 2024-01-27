using System.Diagnostics;

namespace Bitirme_projesi.Helpers
{
    public static class PythonHelper
    {
        public static string RunPythonScript(string scriptPath, string arguments)
        {
            try
            {
                string pythonPath = @"C:\Users\ASUS\AppData\Local\Programs\Python\Python312\python.exe"; // Python yolu, eğer sistemde PYTHONPATH ayarı varsa buraya ekleyebilirsiniz

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"{scriptPath} {arguments}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    string result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Hata kontrolü
                return $"Error: {ex.Message}";
            }
        }
    }
}
