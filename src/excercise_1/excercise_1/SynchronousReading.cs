namespace ConsoleApp
{
    public class SynchronousReading
    {
        /// <summary>
        /// Reads the entire content of a file synchronously
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        public void Read(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                var fileContent = File.ReadAllText(filePath);
            }
        }
    }
}
