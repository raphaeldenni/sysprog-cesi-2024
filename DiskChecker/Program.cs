namespace DiskChecker;
internal static class Program
{
    /// <summary>
    /// The main entry point for the application
    /// </summary>
    /// <param name="args"> The command line arguments </param>
    /// <exception cref="ArgumentException"></exception>
    private static void Main(string[] args)
    {
        if (args.Length is > 0 and < 3)
        {
            // Get the disk letter and the number of seconds
            var diskLetter = args[0].ToUpper();
            var nSeconds = int.Parse(args[1]);
            
            // Create a new DiskChecker object
            _ = new DiskChecker(diskLetter, nSeconds);
        } else
        {
            throw new ArgumentException("Usage: DiskChecker <disk letter> <n seconds>");
        }
    }
}
