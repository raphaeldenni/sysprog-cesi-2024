namespace DiskChecker;

public class DisplayToUser
{
    public static void Display(string diskLetter, long diskSize, long freeDiskSpace)
    {
        Console.WriteLine($"Disk space on {diskLetter}: {freeDiskSpace} / {diskSize} bytes");
    }
}