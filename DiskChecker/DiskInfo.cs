namespace DiskChecker;

public class DiskInfo
{
    /// <summary>
    /// Get the drive information
    /// </summary>
    /// <param name="getFunc"> The function to execute </param>
    /// <param name="driveName"> The drive name </param>
    /// <returns> The drive information </returns>
    private static long GetInfo(Func<DriveInfo, long> getFunc, string driveName)
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
    
    /// <summary>
    /// Get the size of the drive
    /// </summary>
    /// <param name="driveName"></param>
    /// <returns> The size of the drive </returns>
    public static long GetSize(string driveName)
    {
        return GetInfo((drive) => drive.TotalSize, driveName);
    }
    
    /// <summary>
    /// Get the free space of the drive
    /// </summary>
    /// <param name="driveName"></param>
    /// <returns> The free space of the drive </returns>
    public static long GetFreeSpace(string driveName)
    {
        return GetInfo((drive) => drive.TotalFreeSpace, driveName);
    }
}