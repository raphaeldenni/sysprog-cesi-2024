namespace DiskChecker;

public class GetDiskSpace
{
    private static long GetDriveInfo(Func<DriveInfo, long> getFunc, string driveName)
    {
        // Loop through all the drives and return the executed function with the drive as a parameter
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady && drive.Name == driveName)
            {
                return getFunc(drive);
            }
        }
        return -1;
    }
    
    public static long GetSize(string driveName)
    {
        return GetDriveInfo((drive) => drive.TotalSize, driveName);
    }
    
    public static long GetFreeSpace(string driveName)
    {
        return GetDriveInfo((drive) => drive.TotalFreeSpace, driveName);
    }
}