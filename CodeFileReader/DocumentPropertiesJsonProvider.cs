using Newtonsoft.Json;

namespace CodeFileReader;

public class DocumentPropertiesJsonProvider : IDocumentPropertiesProvider
{
    string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"config\doucmentProperties.json");

    public DocumnetProperties CreateAndInitializeDocument()
    {
        DocumnetProperties documnetProperties = new();
        InitializeDocument(documnetProperties);
        return documnetProperties;
    }   

    public DocumnetProperties GetDocumentProperties()
    {
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            DocumnetProperties documnetProperties = JsonConvert.DeserializeObject<DocumnetProperties>(jsonContent);
            return documnetProperties;
        }
        throw new ArgumentException("Document properties file not found.");
    }

    public void InitializeDocument(DocumnetProperties documnetProperties)
    {
        if (!File.Exists(filePath))
        {
            string jsonContent = JsonConvert.SerializeObject(documnetProperties, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}
