using Xceed.Document.NET;
using Xceed.Words.NET;

namespace CodeFileReader;

public class WordWriter : ITextWriter
{
    private readonly string _pathToFile;
    private readonly DocumnetProperties _documnetProperties;
    public WordWriter(string pathToFile, DocumnetProperties documnetProperties)
    {
        _pathToFile = pathToFile ?? throw new ArgumentNullException(nameof(pathToFile));
        _documnetProperties = documnetProperties ?? throw new ArgumentNullException(nameof(documnetProperties));
    }
    public void Write(string text)
    {
        try
        {
            DocX document = DocX.Load(_pathToFile);
            using(document)
            {
                document
                        .InsertParagraph(text)
                        .Font(new Font(_documnetProperties.FontName))
                        .FontSize(int.Parse(_documnetProperties.FontSize))
                        .Alignment = Alignment.left;
                document.Save();
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Error: Invalid FontSize format. " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
