namespace CodeFileReader;
public class DirectoryScanner
{
    private readonly IFolderIgnoreService _ignoringFolders;
    private readonly Dictionary<string, string[]> _directoryCache;
    public DirectoryScanner(IFolderIgnoreService ignoringFolders)
    {
        _ignoringFolders = ignoringFolders ?? throw new ArgumentNullException(nameof(ignoringFolders));
        _directoryCache = new Dictionary<string, string[]>();
    }
    public IEnumerable<string> GetSubdirectories(string rootDirectory)
    {
        List<string> subdirectories = new();
        Stack<string> stack = new();
        HashSet<string> ignoringFolders = _ignoringFolders.GetIgnoredFolders().ToHashSet();
        stack.Push(rootDirectory);

        while (stack.Count > 0)
        {
            string currentDirectory = stack.Pop();
            subdirectories.Add(currentDirectory);

            string[] cachedDirectories;
            if (!_directoryCache.TryGetValue(currentDirectory, out cachedDirectories))
            {
                cachedDirectories = Directory.GetDirectories(currentDirectory);
                _directoryCache[currentDirectory] = cachedDirectories;
            }

            foreach (string directory in cachedDirectories)
            {
                if (!ShouldIgnoreDirectory(directory, ignoringFolders))
                {
                    stack.Push(directory);
                }
            }
        }
        return subdirectories;
    }

    private bool ShouldIgnoreDirectory(string directory, HashSet<string> ignoringFolders)
    {
        string[] splitDirectory = directory.Split(Path.DirectorySeparatorChar);
        return splitDirectory.Any(s => ignoringFolders.Contains(s));
    }
}
