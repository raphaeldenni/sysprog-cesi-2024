namespace DiskChecker;

public class GetDiskSpace
{
    public long DiskSpace()
    {
        return GetTotalFreeSpace("C:\\");
    }
    
    public GetDiskSpace()
    {
    }
    
    private long GetTotalFreeSpace(string driveName)
    {
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady && drive.Name == driveName)
            {
                return drive.TotalFreeSpace;
            }
        }
        return -1;
    }
}