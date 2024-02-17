namespace DiskChecker;
internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length > 0 && args.Length < 2)
        {
            var diskLetter = args[0];
            var nSeconds = int.Parse(args[1]);
            
            _ = new DiskChecker(diskLetter, nSeconds);
        } else
        {
            throw new ArgumentException("Usage: DiskChecker <disk letter> <n seconds>");
        }
    }
}
