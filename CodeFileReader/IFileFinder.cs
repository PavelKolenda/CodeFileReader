namespace CodeFileReader;

public interface IFileFinder
{
    IEnumerable<string> GetFiles(string fileExtension, IEnumerable<string> directories);
}
