namespace DiskChecker;

public class DiskChecker
{
    private GetDiskSpace GetDiskSpace { get; set; }
    private LogWriter LogWriter { get; set; }
    private DisplayToUser DisplayToUser { get; set; }
    
    public DiskChecker(string diskLetter, int nSeconds)
    {
        GetDiskSpace = new GetDiskSpace();
        LogWriter = new LogWriter();
        DisplayToUser = new DisplayToUser();

        while (true)
        {
            // Get the disk size and free space
            var diskSize = GetDiskSpace.GetSize(diskLetter + ":\\");
            var freeDiskSpace = GetDiskSpace.GetFreeSpace(diskLetter + ":\\");
            
            // Write to the log file and display to the user
            LogWriter.WriteLog(diskLetter, diskSize, freeDiskSpace);
            DisplayToUser.Display(diskLetter, diskSize, freeDiskSpace);
            
            Thread.Sleep(nSeconds * 1000);
        }
    }
}