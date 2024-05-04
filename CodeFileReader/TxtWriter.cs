namespace CodeFileReader;

public class TxtWriter : ITextWriter
{
    private readonly string _filePath;
    public TxtWriter(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException($"{nameof(filePath)} can't be null");
    }
    public void Write(string text)
    {
        try
        {
            File.WriteAllText(_filePath, text);
        }
        catch(IOException ex)
        {
            Console.WriteLine("Error occurred while reading a file");
        }
    }
}