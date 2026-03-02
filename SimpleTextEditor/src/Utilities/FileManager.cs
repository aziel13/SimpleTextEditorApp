namespace SimpleTextEditor.Utilities;
using System.IO;

public class FileManager 
{

    private readonly IFileReader _FileReader;
    
    public FileManager(IFileReader fileReader)
    {
        _FileReader =  fileReader;
    }

    public bool FileExists(string fileName)
    {
        return _FileReader.FileExists(fileName);
    }

    public string ReadDataFromFile(string path)
    {
        if (!FileExists(path))
            return "File not found";
        
        return _FileReader.ReadDataFromFile(path);

    }

    public Stream GetStream(string fileName)
    {
        return _FileReader.GetStream(fileName);
    }

    public void writeDataToFile(Stream stream)
    {
        
    }


}