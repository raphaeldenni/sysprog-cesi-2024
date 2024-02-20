namespace DiskChecker;

public class DiskChecker
{
    private DiskInfo DiskInfo { get; set; }
    private DisplayToUser DisplayToUser { get; set; }
    
    private const string LogFolder = "DiskChecker";
    private const string LogFile = "DiskChecker.log";
    
    
    /// <summary>
    /// The constructor for the DiskChecker class
    /// </summary>
    /// <param name="diskLetter"> The disk letter </param>
    /// <param name="nSeconds"> The number of seconds </param>
    /// <exception cref="InvalidOperationException"></exception>
    public DiskChecker(string diskLetter, int nSeconds)
    {
        DiskInfo = new DiskInfo();
        DisplayToUser = new DisplayToUser();
        
        // Build the path to the log file
        var userEnv = Environment.SpecialFolder.UserProfile;
        var userFolder = Environment.GetFolderPath(userEnv);
        var logFolderPath = Path.Combine(userFolder, LogFolder);
        var logPath = Path.Combine(userFolder, LogFolder, LogFile);
        
        // Create the directory if it doesn't exist
        if (!Directory.Exists(logFolderPath))
        {
            Directory.CreateDirectory(logFolderPath);
        }
        
        while (true)
        {
            // Get the disk size and free space
            var diskSize = DiskInfo.GetSize(diskLetter + ":\\");
            var freeDiskSpace = DiskInfo.GetFreeSpace(diskLetter + ":\\");
            
            if (diskSize == -1 || freeDiskSpace == -1)
            {
                throw new InvalidOperationException("Invalid disk letter");
            }
            
            // Write to the log file and display to the user
            try
            {
                LogWriter.WriteLog(logPath, diskLetter, diskSize, freeDiskSpace);
            }
            catch
            {
                var response = DisplayToUser.AskToContinue();
                
                if (response == "n")
                {
                    break;
                }
            }
           
            DisplayToUser.Display(diskLetter, diskSize, freeDiskSpace);
            
            Thread.Sleep(nSeconds * 1000);
        }
    }
}