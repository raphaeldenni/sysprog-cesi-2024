namespace DiskChecker;

public class DisplayToUser
{
    //private static readonly object ConsoleLock = new object();
    
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
        //lock (ConsoleLock)
        //{
            Console.WriteLine($"Disk {diskLetter}: Size = {diskSize}, Free space = {freeDiskSpace}");
            Console.Out.Flush();
        //}
    }
}