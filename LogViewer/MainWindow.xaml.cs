using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace LogViewer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var userEnv = Environment.SpecialFolder.UserProfile;
        var userFolder = Environment.GetFolderPath(userEnv);
        var logPath = Path.Combine(userFolder, "DiskChecker", "DiskChecker.log");
        
        using var reader = new StreamReader(logPath);
        LogFileTextBox.Text = File.Exists(logPath) ? reader.ReadToEnd() : "No log file found";
    }
}