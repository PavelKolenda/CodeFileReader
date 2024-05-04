using System.Text;

namespace CodeFileReader;
public class FileReader : IFileReader
{
    public string GetTextFromFiles(IEnumerable<string> filesPaths)
    {
        if (filesPaths == null)
        {
            throw new ArgumentNullException(nameof(filesPaths));
        }

        StringBuilder textFromFiles = new();
        try
        {
            foreach (string filePath in filesPaths)
            {
                textFromFiles.Append(File.ReadAllText(filePath));
                textFromFiles.AppendLine();
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException($"Unauthorized access to file: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error reading file: {ex.Message}");
        }

        return textFromFiles.ToString();
    }
}
