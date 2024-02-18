namespace DiskChecker;

public class DisplayToUser
{
    public static string AskToContinue()
    {
        Console.WriteLine("Log file isn't accessible, do you want to continue? (y/n)");
        var response = Console.ReadLine() ?? "n";
        
        return response;
    }
    
    public static void Display(string diskLetter, long diskSize, long freeDiskSpace)
    {
        Console.WriteLine($"Disk space on {diskLetter}: {freeDiskSpace} / {diskSize} bytes");
    }
}