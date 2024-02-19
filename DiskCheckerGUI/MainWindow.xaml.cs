﻿using System.IO;
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
    
    private readonly string _logPath;
    private readonly FileSystemWatcher _logFileWatcher;
    
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
        
        // Build the path to the log file
        var userEnv = Environment.SpecialFolder.UserProfile;
        var userFolder = Environment.GetFolderPath(userEnv);
        _logPath = Path.Combine(userFolder, "DiskChecker", "DiskChecker.log");

        
        // Initialize the FileSystemWatcher
        _logFileWatcher = new FileSystemWatcher
        {
            Path = Path.GetDirectoryName(_logPath) ?? throw new InvalidOperationException(),
            Filter = Path.GetFileName(_logPath),
            NotifyFilter = NotifyFilters.LastWrite
        };

        _logFileWatcher.Changed += OnChanged;
        _logFileWatcher.EnableRaisingEvents = true;
    }
    
    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        // Use Dispatcher to update the UI from a non-UI thread
        Dispatcher.Invoke(ReadLogFile);
    }

    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
        // If the thread is still running, cancel it
        if (_diskCheckerThread.IsAlive)
        {
            _cts.Cancel();
        }
        
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
        
        // Get the disk size and free space
        var diskSize = DiskChecker.DiskInfo.GetSize(diskLetter + ":\\");
        var freeDiskSpace = DiskChecker.DiskInfo.GetFreeSpace(diskLetter + ":\\");
        
        // Start a new thread with a new CancellationTokenSource
        _cts = new CancellationTokenSource();
        _diskCheckerThread = new Thread(() => RunDiskChecker(_cts.Token, diskSize, freeDiskSpace, diskLetter, nSeconds));
        _diskCheckerThread.Start();
    }
    
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        // Cancel the thread
        _cts.Cancel();
    }
    
    private void RunDiskChecker(CancellationToken token, long diskSize, long freeDiskSpace, string diskLetter, int nSeconds)
    {
        while (!token.IsCancellationRequested)
        {
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
        // Use a StreamReader to read the log file
        using var reader = new StreamReader(_logPath);
        LogFileTextBox.Text = File.Exists(_logPath) ? reader.ReadToEnd() : "No log file found";
    }
}