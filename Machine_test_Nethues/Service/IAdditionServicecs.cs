namespace Machine_test_Nethues.Service
{
    public interface IAdditionService
    {
        Task<(string sum, string sumInWords)> AddAndConvertToWords(string input);
    }

    public class AdditionService : IAdditionService
    {
        private readonly ILogService _logService;
        public AdditionService(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<(string sum, string sumInWords)> AddAndConvertToWords(string input)
        {
            var numbers = ParseInput(input);
            var sum = numbers.Sum().ToString();

            var sumInWords = ConvertToWords(int.Parse(sum));

            await _logService.LogOperation(input, sumInWords);

            return (sum, sumInWords);
        }


        private IEnumerable<int> ParseInput(string input)
        {
            return input.Split('+').Select(int.Parse);
        }
        


        private string ConvertToWords(int number)
        {
            return new NumberToWordsConverter().Convert(number);
        }
    }
}
