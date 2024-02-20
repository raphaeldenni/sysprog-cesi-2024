using System.Diagnostics;
using System.IO;
using System.Windows.Controls;

namespace DiskCheckerGUI;

public class LogReader
{
    private static readonly LogReader Instance = new LogReader();
    private static object ConsoleLock { get; } = new object();
    
    /// <summary>
    /// The constructor for the LogReader class
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private LogReader()
    {
        if (Instance != null)
        {
            throw new InvalidOperationException("LogWriter is already instantiated");
        }
    }
    
    /// <summary>
    /// The function to read the log file
    /// </summary>
    /// <param name="logPath"> The log path </param>
    /// <param name="logFileTextBox"> The log file TextBox </param>
    public static void ReadLogFile(string logPath, TextBox logFileTextBox)
    {
        lock (ConsoleLock)
        {
            // Use a StreamReader to read the log file
            using var reader = new StreamReader(logPath);
            logFileTextBox.Text = File.Exists(logPath) ? reader.ReadToEnd() : "No log file found";
            
            logFileTextBox.ScrollToEnd();
        }
    }
}