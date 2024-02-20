namespace DiskCheckerGUI;

public class LoopRunner
{
    
    private static LoopRunner Instance { get; } = new LoopRunner();
    
    /// <summary>
    /// The constructor for the LoopRunner class
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    private LoopRunner()
    {
        if (Instance != null)
        {
            throw new InvalidOperationException("LoopRunner is already instantiated");
        }
    }

    /// <summary>
    /// The function to run DiskChecker
    /// </summary>
    /// <param name="token"> The CancellationToken </param>
    /// <param name="logPath"> The log path </param>
    /// <param name="diskSize"> The disk size </param>
    /// <param name="freeDiskSpace"> The free disk space </param>
    /// <param name="diskLetter"> The disk letter </param>
    /// <param name="nSeconds"> The number of seconds </param>
    public static void RunLoop(
        CancellationToken token, string logPath, long diskSize, long freeDiskSpace, string diskLetter, int nSeconds)
    {
        while (!token.IsCancellationRequested)
        {
            if (diskSize == -1 || freeDiskSpace == -1)
            {
                throw new InvalidOperationException("Invalid disk letter");
            }
            
            DiskChecker.LogWriter.WriteLog(logPath, diskLetter, diskSize, freeDiskSpace);
            
            Thread.Sleep(nSeconds * 1000);
        }
    }
}