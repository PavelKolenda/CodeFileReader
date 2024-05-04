namespace CodeFileReader;

public class FileFinder : IFileFinder
{
    public IEnumerable<string> GetFiles(string fileExtension, IEnumerable<string> directories)
    {
        ValidateParameters(fileExtension, directories);
        fileExtension.ToLower();
        if (fileExtension.StartsWith("."))
        {
            fileExtension = fileExtension.Substring(1);
        }

        List<string> matchingFiles = new();
        foreach (string directory in directories)
        {
            IEnumerable<string> files = GetFilesFromDirectory(directory);
            matchingFiles.AddRange(files.Where(file => Path.GetExtension(file) == $".{fileExtension}"));
        }
        return matchingFiles;
    }

    private IEnumerable<string> GetFilesFromDirectory(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentNullException(nameof(directory), "Directory path cannot be null or whitespace.");

        try
        {
            return Directory.EnumerateFiles(directory);
        }
        catch (IOException ex)
        {
            Console.WriteLine("An error occurred while retrieving files: " + ex.Message);
            return Enumerable.Empty<string>();
        }
    }

    private void ValidateParameters(string fileExtension, IEnumerable<string> directories)
    {
        if (string.IsNullOrWhiteSpace(fileExtension))
        {
            throw new ArgumentNullException(nameof(fileExtension), "Extension cannot be null or whitespace.");
        }

        if (directories == null || !directories.Any())
        {
            throw new ArgumentNullException(nameof(directories), "Directories cannot be null or empty.");
        }
    }
}