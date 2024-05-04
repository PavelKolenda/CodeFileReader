namespace CodeFileReader;

public interface IFolderIgnoreService
{
    IEnumerable<string> GetIgnoredFolders();
}