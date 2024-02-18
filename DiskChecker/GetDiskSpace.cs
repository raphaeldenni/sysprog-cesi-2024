namespace DiskChecker;

public class GetDiskSpace
{
    /**
     * Get the drive information
     * 
     * @param getFunc The function to execute
     * @param driveName The drive name
     * @return The drive information
     */
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
    
    /**
     * Get the size of the drive
     * 
     * @param driveName The drive name
     * @return The size of the drive
     */
    public static long GetSize(string driveName)
    {
        return GetDriveInfo((drive) => drive.TotalSize, driveName);
    }
    
    /**
     * Get the free space of the drive
     * 
     * @param driveName The drive name
     * @return The free space of the drive
     */
    public static long GetFreeSpace(string driveName)
    {
        return GetDriveInfo((drive) => drive.TotalFreeSpace, driveName);
    }
}