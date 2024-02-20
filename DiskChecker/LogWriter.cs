using System.Diagnostics;

namespace DiskChecker;

public class LogWriter
{
    private static LogWriter Instance { get; } = new LogWriter();
    
    private static object ConsoleLock { get; } = new object();
    
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
    /// <param name="logPath"> The log path </param>
    /// <param name="diskLetter"> The disk letter </param>
    /// <param name="diskSize"> The disk size </param>
    /// <param name="freeDiskSpace"> The free disk space </param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void WriteLog(string logPath, string diskLetter, long diskSize, long freeDiskSpace)
    {
        lock (ConsoleLock)
        {
            // Use a StreamWriter to append a new line to the log file
            Debug.Assert(logPath != null, nameof(logPath) + " != null");
            using var writer = new StreamWriter(logPath, true);
            writer.WriteLine($"{DateTime.Now} Disk space on {diskLetter}: {freeDiskSpace} / {diskSize} bytes");
        }
    }
}