namespace SimpleTextEditor.Utilities;
using System.IO;
    
public class FileReader : IFileReader
{
    
    
    public FileReader()
    {
        
    }

    public bool FileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    public string ReadDataFromFile(string path)
    {
        throw new NotImplementedException();
    }

    public Stream GetStream(string fileName)
    {
        throw new NotImplementedException();
    }
}