namespace DiskChecker;

public class DiskChecker
{
    private GetDiskSpace GetDiskSpace { get; set; }
    private DisplayToUser DisplayToUser { get; set; }
    
    /**
     * The constructor for the DiskChecker class
     * 
     * @param diskLetter The disk letter
     * @param nSeconds The number of seconds
     */
    public DiskChecker(string diskLetter, int nSeconds)
    {
        GetDiskSpace = new GetDiskSpace();
        DisplayToUser = new DisplayToUser();

        while (true)
        {
            // Get the disk size and free space
            var diskSize = GetDiskSpace.GetSize(diskLetter + ":\\");
            var freeDiskSpace = GetDiskSpace.GetFreeSpace(diskLetter + ":\\");
            
            if (diskSize == -1 || freeDiskSpace == -1)
            {
                throw new InvalidOperationException("Invalid disk letter");
            }
            
            // Write to the log file and display to the user
            try
            {
                LogWriter.WriteLog(diskLetter, diskSize, freeDiskSpace);
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