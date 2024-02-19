using System.IO;
using System.Windows;
using static DiskChecker.DiskChecker;

namespace DiskCheckerGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ReaderWriterLockSlim LogFileLock { get; } = new ReaderWriterLockSlim();
    
    public MainWindow()
    {
        InitializeComponent();
        
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                DriveListBox.Items.Add(drive.Name);
            }
        }
    }

    private void RunDiskChecker()
    {
        var diskLetter = DriveListBox.SelectedItem.ToString()?.ToUpper() ?? throw new InvalidOperationException();
        var nSeconds = int.Parse(SecondsTextBox.Text);
        

        var diskChecker = new DiskChecker.DiskChecker(diskLetter, nSeconds);
    }
    
    private void ReadLogFile()
    {
        var userEnv = Environment.SpecialFolder.UserProfile;
        var userFolder = Environment.GetFolderPath(userEnv);
        var logPath = Path.Combine(userFolder, "DiskChecker", "DiskChecker.log");
        
        using var reader = new StreamReader(logPath);
        LogFileTextBox.Text = File.Exists(logPath) ? reader.ReadToEnd() : "No log file found";
    }

    private void RunButton_Click(object sender, RoutedEventArgs e)
    {
        RunDiskChecker();
    }
}