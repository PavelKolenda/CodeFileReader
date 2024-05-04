using CodeFileReader;

Console.Write("Enter a project directory: ");
string? projectDirectory = Console.ReadLine();
if (!Directory.Exists(projectDirectory))
    throw new DirectoryNotFoundException("Directory is not found");
Console.Write("Enter a file type: ");
string? fileType = Console.ReadLine();
IFolderIgnoreService ignoringFoldersJson = new FolderIgnoreManagerJson();
DirectoryScanner directoriesWorker = new DirectoryScanner(ignoringFoldersJson);
IFileFinder fileFinder = new FileFinder();
List<string> mathcingFiles = fileFinder.GetFiles(fileType, directoriesWorker.GetSubdirectories(projectDirectory))
                                       .ToList();
IFileReader fileReader = new FileReader();
string filesText = fileReader.GetTextFromFiles(mathcingFiles);
IDocumentPropertiesProvider jsonProvider = new DocumentPropertiesJsonProvider();
DocumnetProperties documnetProperties = jsonProvider.CreateAndInitializeDocument();
documnetProperties = jsonProvider.GetDocumentProperties();

ITextWriter textWriter;
Console.WriteLine("Where to write:\n1.Word \n2.Text");
bool textWriterInput = int.TryParse(Console.ReadLine(), out int textWriterObject);
switch (textWriterObject)
{
    case 1:
        textWriter = new WordWriter(GetFilePath(), documnetProperties);
        textWriter.Write(filesText);
        break;
    case 2:
        textWriter = new TxtWriter(GetFilePath());
        textWriter.Write(filesText);
        break;
}

static string GetFilePath()
{
    Console.Write("Enter a file path: ");
    string filePath = Console.ReadLine();
    if(!File.Exists(filePath))
        throw new FileNotFoundException(filePath);
    return filePath;
}