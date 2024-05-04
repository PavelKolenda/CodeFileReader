namespace CodeFileReader;

public interface IDocumentPropertiesProvider
{
    public DocumnetProperties GetDocumentProperties();
    public void InitializeDocument(DocumnetProperties documnetProperties);
    DocumnetProperties CreateAndInitializeDocument();
}