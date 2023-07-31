namespace ConsoleApp
{
    internal class FileReaderReport
    {
        private Report _report { get; set; }
        private List<string> _mostFrequentWords;

        /// <summary>
        /// Constructor for FileReaderReport class. Initializes a new Report object based on the specified file path.
        /// </summary>
        public FileReaderReport()
        {
            this._report = new Report();
        }

        /// <summary>
        /// Generates and prints a report for the specified file.
        /// </summary>
        /// <param name="filePath">The path of the file to generate the report for.</param>
        public void GenerateReport(string filePath)
        {
            // Get the file size in bytes using the FileInfo class
            var fileInfo = new FileInfo(filePath);
            var fileSize = fileInfo.Length;
            this._report.GetDictionaryElement(filePath);
            this._mostFrequentWords = _report.GetMostFrequentWords();

            Console.WriteLine();
            Console.WriteLine($"Report for the file {Path.GetFileName(filePath)}:");
            Console.WriteLine($"1. Number of lines in the file: {_report.GetNumberOfLines()}");
            Console.WriteLine($"2. Total size of the file: {fileSize} bytes");
            Console.WriteLine($"3. Number of paragraphs in the file: {_report.GetNumberOfParagraphs()}");
            Console.WriteLine($"4. Number of words in the file content: {_report._words.Length}");
            Console.WriteLine($"5. Number of unique words in the file content: {_report.GetNumberOfUniqueWord()}");
            Console.WriteLine($"6. Most common word(s) in the file: {string.Join(", ", _mostFrequentWords)}");
            Console.WriteLine($"7. Longest word in the file: {_report.GetLongestWord()}");
        }
    }
}
