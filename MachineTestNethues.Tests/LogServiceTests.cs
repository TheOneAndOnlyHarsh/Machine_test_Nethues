using Machine_test_Nethues.Service;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MachineTestNethues.Tests
{
    public class LogServiceTests
    {
        private readonly LogService _logService;
        private readonly string logFilePath;

        public LogServiceTests()
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "log.txt");
            _logService = new LogService();
        }

        [Fact]
        public async Task LogOperation_WritesToLogFile()
        {
            string input = "3+5";
            string result = "eight";
            await _logService.LogOperation(input, result);
            var logs = await _logService.GetLogs();
            Assert.Contains($"{input} = {result}", logs);
        }

        [Fact]
        public async Task GetLogs_ReturnsEmpty_WhenNoLogs()
        {
            var logs = await _logService.GetLogs();
            Assert.Empty(logs);
        }
    }
}
