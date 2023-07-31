namespace ConsoleApp
{
    public class Report
    {
        private string _content;
        public string[] _words;
        Dictionary<string, int> wordOccurrences = new Dictionary<string, int>();
        private string _longestWord = String.Empty;

        /// <summary>
        /// Reads the content of the specified file, splits it into words based on whitespace characters, and counts the occurrences of each word.
        /// </summary>
        /// <param name="filePath">The path of the file to process.</param>
        public void GetDictionaryElement(string filePath)
        {
            // Read the content of the file into a string
            _content = File.ReadAllText(filePath);

            // Split the content into words based on whitespace characters and count the number of elements in the resulting array
            _words = _content.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Count the occurrences of each word using a dictionary
            foreach (string word in _words)
            {
                if (wordOccurrences.TryGetValue(word, out int count))
                {
                    wordOccurrences[word] = count + 1;
                }
                else
                {
                    wordOccurrences[word] = 1;
                }
            }
        }

        /// <summary>
        /// Gets the number of lines in the file content.
        /// </summary>
        /// <returns>The number of lines in the file.</returns>
        public int GetNumberOfLines()
        {
            // Split the content into lines based on line breaks and count the number of elements in the resulting array
            var lines = _content.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            return lines.Length;
        }

        /// <summary>
        /// Gets the number of paragraphs in the file content.
        /// </summary>
        /// <returns>The number of paragraphs in the file.</returns>
        public int GetNumberOfParagraphs()
        {
            // Split the content into paragraphs based on empty lines and count the number of elements in the resulting array
            var paragraphs = _content.Split(new string[] { "\r\n\r\n", "\n\n", "\r\r" }, StringSplitOptions.RemoveEmptyEntries);
            var numberOfParagraphs = paragraphs.Length;

            return numberOfParagraphs;
        }


        /// <summary>
        /// Gets the number of unique words in the file content.
        /// </summary>
        /// <returns>The number of unique words in the file.</returns>
        public int GetNumberOfUniqueWord()
        {
            // Count the number of unique words (words with occurrence count equal to 1) in the 'wordOccurrences' dictionary
            var uniqueWordCount = wordOccurrences.Count(kv => kv.Value == 1);
            return uniqueWordCount;
        }

        /// <summary>
        /// Gets a list of the most frequently occurring words in the file content.
        /// </summary>
        /// <returns>A list containing the most frequent words in the file.</returns>
        public List<string> GetMostFrequentWords()
        {
            // Find the maximum frequency of word occurrences in the 'wordOccurrences' dictionary
            var maxFrequency = wordOccurrences.Values.Max();

            // Select all words with the maximum frequency and store them in the 'mostFrequentWords' list
            List<string> mostFrequentWords = wordOccurrences
                .Where(kv => kv.Value == maxFrequency)
                .Select(kv => kv.Key)
                .ToList();

            return mostFrequentWords;
        }

        /// <summary>
        /// Gets the longest word in the file content.
        /// </summary>
        /// <returns>The longest word in the file.</returns>
        public string GetLongestWord()
        {
            // Find the longest word in the 'wordOccurrences' dictionary
            foreach (var word in wordOccurrences)
            {
                if (string.IsNullOrEmpty(_longestWord) || word.Key.Length > _longestWord.Length)
                {
                    _longestWord = word.Key;
                }
            }
            return _longestWord;
        }
    }
}
