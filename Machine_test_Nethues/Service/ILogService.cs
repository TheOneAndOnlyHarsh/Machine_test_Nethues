namespace Machine_test_Nethues.Service
{
    public interface ILogService
    {
        Task LogOperation(string input, string result);
        Task<IEnumerable<string>> GetLogs();
    }

    public class LogService : ILogService
    {
        private readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "log.txt");

        public async Task LogOperation(string input, string result)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); 
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    await sw.WriteLineAsync($"{DateTime.Now}: {input} = {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log operation: {ex.Message}"); 
            }
        }

        public async Task<IEnumerable<string>> GetLogs()
        {
            if (File.Exists(logFilePath))
                return await File.ReadAllLinesAsync(logFilePath);
            return Enumerable.Empty<string>();
        }
    }



}
