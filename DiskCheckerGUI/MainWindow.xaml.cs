using System.IO;
using System.Windows;
using static DiskChecker.DiskChecker;

namespace DiskCheckerGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private CancellationTokenSource _cts;
    private Thread _diskCheckerThread;
    
    public MainWindow()
    {
        InitializeComponent();
        
        // Add the drives to the DriveListBox
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                DriveListBox.Items.Add(drive.Name);
            }
        }
        
        // Set the defaults
        _cts = new CancellationTokenSource();
        _diskCheckerThread = new Thread(() => {});
    }

    private void RunDiskChecker(CancellationToken token)
    {
        // Get the disk letter and the number of seconds
        var diskLetter = DriveListBox.SelectedItem.ToString()?.Remove(1) ?? "C";
        var nSeconds = int.Parse(
            SecondsTextBox.Text == "" 
                ? "1" 
                : SecondsTextBox.Text);
        
        // Make sure the number of seconds is at least 1
        if (nSeconds < 1)
        {
            nSeconds = 1;
        }
        
        while (!token.IsCancellationRequested)
        {
            // Get the disk size and free space
            var diskSize = DiskChecker.DiskInfo.GetSize(diskLetter + ":\\");
            var freeDiskSpace = DiskChecker.DiskInfo.GetFreeSpace(diskLetter + ":\\");
            
            if (diskSize == -1 || freeDiskSpace == -1)
            {
                throw new InvalidOperationException("Invalid disk letter");
            }
            
            DiskChecker.LogWriter.WriteLog(diskLetter, diskSize, freeDiskSpace);
            
            Thread.Sleep(nSeconds * 1000);
        }
    }
    
    private void ReadLogFile()
    {
        // Build the path to the log file
        var userEnv = Environment.SpecialFolder.UserProfile;
        var userFolder = Environment.GetFolderPath(userEnv);
        var logPath = Path.Combine(userFolder, "DiskChecker", "DiskChecker.log");
        
        // Use a StreamReader to read the log file
        using var reader = new StreamReader(logPath);
        LogFileTextBox.Text = File.Exists(logPath) ? reader.ReadToEnd() : "No log file found";
    }

    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
        // If the thread is still running, cancel it (Singleton pattern)
        if (_diskCheckerThread.IsAlive)
        {
            _cts.Cancel();
        }
        
        // Start a new thread with a new CancellationTokenSource
        _cts = new CancellationTokenSource();
        _diskCheckerThread = new Thread(() => RunDiskChecker(_cts.Token));
        _diskCheckerThread.Start();
    }
    
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        // Cancel the thread
        _cts.Cancel();
    }
}