using Machine_test_Nethues.Service;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MachineTestNethues.Tests
{
    public class AdditionServiceTests
    {
        private readonly Mock<ILogService> _logServiceMock;
        private readonly AdditionService _additionService;

        public AdditionServiceTests()
        {
            _logServiceMock = new Mock<ILogService>();
            _additionService = new AdditionService(_logServiceMock.Object);
        }

        [Fact]
        public async Task AddAndConvertToWords_ValidInput_ReturnsCorrectSumAndWords()
        {
            string input = "5+3";
            string expectedSumInWords = "eight"; 

            var (sum, sumInWords) = await _additionService.AddAndConvertToWords(input);

            Assert.Equal("8", sum);
            Assert.Equal(expectedSumInWords, sumInWords);
            _logServiceMock.Verify(m => m.LogOperation(input, expectedSumInWords), Times.Once);
        }

        [Fact]
        public async Task AddAndConvertToWords_InvalidInput_LogsError()
        {
            string input = "5+a";
            string expectedLogMessage = "Error: Invalid operation"; 

            await Assert.ThrowsAsync<FormatException>(() => _additionService.AddAndConvertToWords(input));

            _logServiceMock.Verify(m => m.LogOperation(input, expectedLogMessage), Times.Once);
        }

        [Fact]
        public async Task AddAndConvertToWords_MultipleInputs_ReturnsCorrectSumAndWords()
        {
          
            string input = "5+3+2"; 
            string expectedSumInWords = "ten"; 
            var (sum, sumInWords) = await _additionService.AddAndConvertToWords(input);
            Assert.Equal("10", sum);
            Assert.Equal(expectedSumInWords, sumInWords); 
            _logServiceMock.Verify(m => m.LogOperation(input, expectedSumInWords), Times.Once); 
        }

    }
}
