using System.IO;
using System.Windows;
using static DiskCheckerGUI.LoopRunner;
using static DiskCheckerGUI.LogReader;

namespace DiskCheckerGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private CancellationTokenSource _cts;
    private Task _diskCheckerTask;
    
    private const string LogFolder = "DiskChecker";
    private const string LogFile = "DiskChecker.log";

    private readonly string _logPath;
    private readonly FileSystemWatcher _logFileWatcher;
    
    /// <summary>
    /// The constructor for the MainWindow class
    /// </summary>
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
        _diskCheckerTask = new Task(() => {});
        
        // Build the path to the log file
        var userEnv = Environment.SpecialFolder.UserProfile;
        var userFolder = Environment.GetFolderPath(userEnv);
        var logFolderPath = Path.Combine(userFolder, LogFolder);
        
        _logPath = Path.Combine(logFolderPath, LogFile);
        
        // Create the directory if it doesn't exist
        if (!Directory.Exists(logFolderPath))
        {
            Directory.CreateDirectory(logFolderPath);
        }
        
        // Create the log file if it doesn't exist
        if (!File.Exists(_logPath))
        {
            File.Create(_logPath).Close();
        }
        
        // Initialize the FileSystemWatcher
        // This will watch for changes to the log file and update the UI accordingly (Observer pattern)
        _logFileWatcher = new FileSystemWatcher
        {
            Path = logFolderPath,
            Filter = LogFile,
            NotifyFilter = NotifyFilters.LastWrite
        };

        _logFileWatcher.Changed += OnChanged;
        _logFileWatcher.EnableRaisingEvents = true;
        
        // Read the log file
        using var reader = new StreamReader(_logPath);
        LogFileTextBox.Text = File.Exists(_logPath) ? reader.ReadToEnd() : "No log file found";
        
        LogFileTextBox.ScrollToEnd();
    }
    
    /// <summary>
    /// The event handler for the FileSystemWatcher
    /// </summary>
    /// <param name="sender"> The sender </param>
    /// <param name="e"> The FileSystemEventArgs </param>
    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        // Use Dispatcher to update the UI from a non-UI thread (Bridge pattern with LogReader.cs)
        Dispatcher.Invoke(() => ReadLogFile(_logPath, LogFileTextBox));
    }

    /// <summary>
    /// The event handler for the RunButton
    /// </summary>
    /// <param name="sender"> The sender </param>
    /// <param name="e"> The RoutedEventArgs </param>
    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
        // If the thread is still running, cancel it
        if (_diskCheckerTask.Status == TaskStatus.Running)
        {
            _cts.Cancel();
        }
        
        // Here we reuse the RunDiskChecker functions from DiskChecker (Bridge pattern)
        
        // Get the disk letter and the number of seconds
        var diskLetter = DriveListBox.SelectedItem?.ToString()?.Remove(1) ?? "C";
        var nSeconds = int.TryParse(SecondsTextBox.Text, out var result) && result >= 1 
            ? result
            : 1;
        
        // Get the disk size and free space
        var diskSize = DiskChecker.DiskInfo.GetSize(diskLetter + ":\\");
        var freeDiskSpace = DiskChecker.DiskInfo.GetFreeSpace(diskLetter + ":\\");
        
        // Start a new thread with a new CancellationTokenSource
        _cts = new CancellationTokenSource();
        _diskCheckerTask = new Task(
            () => RunLoop(_cts.Token, _logPath, diskSize, freeDiskSpace, diskLetter, nSeconds), _cts.Token);
        
        Dispatcher.Invoke(() => _diskCheckerTask.Start());
    }
    
    /// <summary>
    /// The event handler for the StopButton
    /// </summary>
    /// <param name="sender"> The sender </param>
    /// <param name="e"> The RoutedEventArgs </param>
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        // Cancel the thread
        _cts.Cancel();
    }
    
    /// <summary>
    /// The destructor for the MainWindow class
    /// </summary>
    ~MainWindow()
    {
        _cts.Cancel();
    }
}