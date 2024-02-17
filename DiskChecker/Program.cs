namespace DiskChecker;
internal static class Program
{
    private static void Main(string[] args)
    {
        var nSeconds = args.Length > 0 ? int.Parse(args[0]) : 5;

		_ = new DiskChecker(nSeconds);
    }
}
