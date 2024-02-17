namespace DiskChecker;

public class LogWriter
{
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