using Newtonsoft.Json;

namespace CodeFileReader;
public class FolderIgnoreManagerJson : IFolderIgnoreService
{
    private const string JsonFileName = "ignoringFolders.json";
    private List<string> IgnoringFolders { get; set; } = new List<string> { ".git" };

    public IEnumerable<string> GetIgnoredFolders()
    {
        string jsonFilePath = GetConfigFilePath();

        if (!File.Exists(jsonFilePath))
        {
            CreateDefaultJsonFile(jsonFilePath);
        }

        return LoadIgnoringFoldersFromJson(jsonFilePath);
    }

    private string GetConfigFilePath()
    {
        var configFolder = Path.Combine(Directory.GetCurrentDirectory(), "config");
        return Path.Combine(configFolder, JsonFileName); 
    } 

    private void CreateDefaultJsonFile(string filePath)
    {
        try
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(IgnoringFolders, Formatting.Indented));
        }
        catch (IOException ex)
        {
            Console.WriteLine("An error occurred while accessing the file system: " + ex.Message);
        }
        catch (JsonException ex)
        {
            Console.WriteLine("An error occurred while serializing the JSON: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    private IEnumerable<string> LoadIgnoringFoldersFromJson(string filePath)
    {
        try
        {
            string jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<string>>(jsonContent);
        }
        catch (IOException ex)
        {
            Console.WriteLine("An error occurred while accessing the file: " + ex.Message);
        }
        catch (JsonException ex)
        {
            Console.WriteLine("An error occurred while deserializing the JSON: " + ex.Message);
        }
        throw new FileNotFoundException("");
    }
}
