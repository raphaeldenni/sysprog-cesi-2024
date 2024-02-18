namespace DiskChecker;

public class DisplayToUser
{
    /**
     * Ask the user if they want to continue
     * 
     * @return The user's response
     */
    public static string AskToContinue()
    {
        Console.WriteLine("Log file isn't accessible, do you want to continue? (y/n)");
        var response = Console.ReadLine() ?? "n";
        
        return response;
    }
    
    /**
     * Display the disk space to the user
     * 
     * @param diskLetter The disk letter
     * @param diskSize The disk size
     * @param freeDiskSpace The free disk space
     */
    public static void Display(string diskLetter, long diskSize, long freeDiskSpace)
    {
        Console.WriteLine($"Disk space on {diskLetter}: {freeDiskSpace} / {diskSize} bytes");
    }
}