namespace SimpleTextEditor.Utilities;
using System.IO;
    
public class FileReader : IFileReader
{
    public bool FileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    public string ReadDataFromFile(string path)
    {
        return  File.ReadAllText(path);
    }

    public Stream OpenReadStream(string fileName)
    {
        return new FileStream(fileName, FileMode.Open, FileAccess.Read);
    }
}