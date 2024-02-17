namespace DiskChecker;

public class DiskChecker
{
    private GetDiskSpace GetDiskSpace;
    private LogWriter LogWriter;
    private DisplayDiskSpace DisplayDiskSpace;
    
    public DiskChecker(int nSeconds)
    {
        GetDiskSpace = new GetDiskSpace();
        LogWriter = new LogWriter();
        DisplayDiskSpace = new DisplayDiskSpace();

        while (true)
        {
            var diskSpace = GetDiskSpace.DiskSpace();
            
            //LogWriter.WriteLog(diskSpace);
            //DisplayDiskSpace.Display(diskSpace);
            
            Thread.Sleep(nSeconds * 1000);
        }
    }
}