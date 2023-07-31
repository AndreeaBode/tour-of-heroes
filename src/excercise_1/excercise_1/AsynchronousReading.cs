namespace ConsoleApp
{
    public class AsynchronousReading
    {
        /// <summary>
        /// Reads a file asynchronously 
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReadAsync(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                var fileContent = await File.ReadAllTextAsync(filePath);
            }
        }
    }
}
