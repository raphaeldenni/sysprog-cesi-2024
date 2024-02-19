namespace DiskChecker;

public class DisplayToUser
{
    /// <summary>
    /// Ask the user if they want to continue
    /// </summary>
    /// <returns> The user's response </returns>
    public static string AskToContinue()
    {
        Console.WriteLine("Log file isn't accessible, do you want to continue? (y/n)");
        var response = Console.ReadLine() ?? "n";
        
        return response;
    }
    
    /// <summary>
    /// Display the disk space to the user
    /// </summary>
    /// <param name="diskLetter"> The disk letter </param>
    /// <param name="diskSize"> The disk size </param>
    /// <param name="freeDiskSpace"> The free disk space </param>
    public static void Display(string diskLetter, long diskSize, long freeDiskSpace)
    {
        Console.WriteLine($"Disk space on {diskLetter}: {freeDiskSpace} / {diskSize} bytes");
    }
}