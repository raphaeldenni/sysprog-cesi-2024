namespace DiskChecker;

public class LogWriter
{
    private static LogWriter Instance { get; } = new LogWriter();
    
    /// <summary>
    /// The constructor for the LogWriter class
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private LogWriter()
    {
        // If LogWriter is already instantiated, throw an exception (Singleton pattern)
        // This is to prevent multiple instances of LogWriter to write to the same log file
        if (Instance != null)
        {
            throw new InvalidOperationException("LogWriter is already instantiated");
        }
    }
    
    /// <summary>
    /// Write to the log file
    /// </summary>
    /// <param name="diskLetter"> The disk letter </param>
    /// <param name="diskSize"> The disk size </param>
    /// <param name="freeDiskSpace"> The free disk space </param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void WriteLog(string diskLetter, long diskSize, long freeDiskSpace)
    {
        // Build the path to the log file
        var userEnv = Environment.SpecialFolder.UserProfile;
        var userFolder = Environment.GetFolderPath(userEnv);
        var logPath = Path.Combine(userFolder, "DiskChecker", "DiskChecker.log");
        
        // Create the directory if it doesn't exist
        if (!Directory.Exists(Path.GetDirectoryName(logPath)))
        {
            var logDirectory = Path.GetDirectoryName(logPath) ?? throw new InvalidOperationException();
            Directory.CreateDirectory(logDirectory);
        }
        
        // Use a StreamWriter to append a new line to the log file
        using var writer = new StreamWriter(logPath, true);
        writer.WriteLine($"Disk space on {diskLetter}: {freeDiskSpace} / {diskSize} bytes at {DateTime.Now}");
    }
}