namespace RegistrationApp.ServiceRepository
{
    public static class WriteLogHelper
    {
        public static void WriteLog(string strLog)
        {
            string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
            // Ensure the directory exists, create if it doesn't
            DirectoryInfo logDirInfo = new DirectoryInfo(logFolderPath);
            if (!logDirInfo.Exists)
            {
                logDirInfo.Create();
            }

            string logFilePath = Path.Combine(logFolderPath, $"Log-{DateTime.Now:MM-dd-yyyy-HH-mm-ss}.txt");
            using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
            {
                using (StreamWriter log = new StreamWriter(fileStream))
                {
                    log.WriteLine($"{strLog}");
                }
            }
        }
    }
}
