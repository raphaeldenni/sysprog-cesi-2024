using System.Windows;
using System.Windows.Controls;

namespace DiskCheckerGUI;

public class LoopRunner
{
    
    private static LoopRunner Instance { get; } = new LoopRunner();
    
    /// <summary>
    /// The constructor for the LoopRunner class
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private LoopRunner()
    {
        if (Instance != null)
        {
            throw new InvalidOperationException("LoopRunner is already instantiated");
        }
    }
    
    /// <summary>
    /// The function to run DiskChecker
    /// </summary>
    /// <param name="token"> The CancellationToken </param>
    /// <param name="logPath"> The log path </param>
    /// <param name="elements"> The UI elements </param>
    public static void RunLoop(CancellationToken token, string logPath, List<UIElement> elements)
    {
        // Here we reuse the RunDiskChecker functions from DiskChecker (Bridge pattern)
        
        var driveListBox = (ListBox) elements[0];
        var secondsTextBox = (TextBox) elements[1];
        
        // Get the disk letter and the number of seconds
        var diskLetter = driveListBox.SelectedItem?.ToString()?.Remove(1) ?? "C";
        var nSeconds = int.TryParse(secondsTextBox.Text, out var result) && result >= 1 
            ? result
            : 1;
        
        // Get the disk size and free space
        var diskSize = DiskChecker.DiskInfo.GetSize(diskLetter + ":\\");
        var freeDiskSpace = DiskChecker.DiskInfo.GetFreeSpace(diskLetter + ":\\");
        
        while (!token.IsCancellationRequested)
        {
            if (diskSize == -1 || freeDiskSpace == -1)
            {
                throw new InvalidOperationException("Invalid disk letter");
            }
            
            DiskChecker.LogWriter.WriteLog(logPath, diskLetter, diskSize, freeDiskSpace);
            
            Thread.Sleep(nSeconds * 1000);
        }
    }
}