using System.Diagnostics;

namespace ConsoleApp
{
    public class Program
    {
        /// <summary>
        /// Main method to handle the interactive menu for file reading options.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            DisplayMenu();
        }

        /// <summary>
        /// Displays an interactive menu for various file reading options and generates reports for the specified file.
        /// </summary>
        public static void DisplayMenu()
        {
            var menu = -1;
            do
            {
                // Display the main menu options
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Read file synchronously");
                Console.WriteLine("2. Read file asynchronously");
                Console.WriteLine("3. Read in both modes");
                Console.WriteLine("4. Close");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                int.TryParse(Console.ReadLine(), out menu);

                Console.Write("Enter the full path of the file: ");
                var filePath = Console.ReadLine().Trim();

                FileReaderReport fileReaderReport = new FileReaderReport();

                switch (menu)
                {
                    case 1:

                        // Synchronous File Reading
                        Stopwatch stopwatchSync = new Stopwatch();
                        var fileReaderSync = new SynchronousReading();

                        // Start measuring time for synchronous file reading
                        stopwatchSync.Start();
                        fileReaderSync.Read(filePath);
                        stopwatchSync.Stop();

                        // Generate and display the report for the file
                        fileReaderReport.GenerateReport(filePath);

                        // Display the time taken for synchronous file reading
                        Console.WriteLine($"The file was read in: {stopwatchSync.Elapsed.TotalMilliseconds} milliseconds");
                        Console.WriteLine();
                        break;

                    case 2:

                        // Generate and display the report for the file
                        Stopwatch stopwatchAsync = new Stopwatch();
                        var fileReaderAsync = new AsynchronousReading();

                        // Start measuring time for asynchronous file reading
                        stopwatchAsync.Start();
                        fileReaderAsync.ReadAsync(filePath).Wait();
                        stopwatchAsync.Stop();

                        // Generate and display the report for the file
                        fileReaderReport.GenerateReport(filePath);

                        Console.WriteLine($"The file was read in: {stopwatchAsync.Elapsed.TotalMilliseconds} milliseconds");
                        Console.WriteLine();
                        break;

                    case 3:

                        // Read in Both Modes (Synchronous and Asynchronous)
                        Stopwatch stopwatchSync1 = new Stopwatch();
                        var fileReaderSync1 = new SynchronousReading();

                        // Start measuring time for synchronous file reading
                        stopwatchSync1.Start();
                        fileReaderSync1.Read(filePath);
                        stopwatchSync1.Stop();

                        Stopwatch stopwatchAsync1 = new Stopwatch();
                        var fileReaderAsync1 = new AsynchronousReading();

                        // Start measuring time for asynchronous file reading
                        stopwatchAsync1.Start();
                        fileReaderAsync1.ReadAsync(filePath).Wait();
                        stopwatchAsync1.Stop();

                        // Generate and display the report for the file
                        fileReaderReport.GenerateReport(filePath);

                        Console.WriteLine($"The file was read synchronously in: {stopwatchSync1.Elapsed.TotalMilliseconds} milliseconds");
                        Console.WriteLine($"The file was read asynchronously in: {stopwatchAsync1.Elapsed.TotalMilliseconds} milliseconds");
                        Console.WriteLine();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            } while (menu != 4);
        }
    }
}

