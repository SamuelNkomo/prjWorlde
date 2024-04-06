namespace prjWorlde
{
    public class AnswerProvide
    {
        private static readonly List<string> WordList = new();
        private static readonly Random Random = new();
        private readonly HttpClient _httpClient;
        private readonly string _filePath;
        public AnswerProvide(HttpClient httpClient, string filePath)
        {
            _httpClient = httpClient;
            _filePath = filePath;
        }
        public async Task<string> GetNewAnswer()
        {
            if (WordList.Count == 0)
            {
                await LoadAnswersAsync();
            }

            var idx = Random.Next(WordList.Count - 1);
            return WordList[idx];
        }
        private async Task LoadAnswersAsync()
        {
            WordList.Clear();
            var fileContent = await _httpClient.GetStringAsync(_filePath);
            WordList.AddRange(fileContent.Split(Environment.NewLine));
        }
    }
}