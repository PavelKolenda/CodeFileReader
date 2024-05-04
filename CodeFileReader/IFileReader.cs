namespace CodeFileReader;

public interface IFileReader
{
    public string GetTextFromFiles(IEnumerable<string> filesPaths);
}
