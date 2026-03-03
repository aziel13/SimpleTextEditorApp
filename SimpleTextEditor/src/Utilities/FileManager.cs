namespace SimpleTextEditor.Utilities;
using System.IO;

public class FileManager 
{

    private readonly IFileReader _FileReader;
    
    private readonly IFileWriter _FileWriter;

    public FileManager(IFileReader? fileReader,IFileWriter? fileWriter)
    {
        _FileReader =  fileReader;
        _FileWriter = fileWriter;
    }

    public bool FileExists(string fileName)
    {
        if (_FileReader != null)
        {
            return _FileReader.FileExists(fileName);
        } 
        
        if (_FileWriter != null)
        {
            return _FileWriter.FileExists(fileName);
        }
        
        return false;
    }

    public string ReadDataFromFile(string path)
    {
        if (!FileExists(path))
            return "File not found";
        
        return _FileReader.ReadDataFromFile(path);

    }

    public Stream OpenReadStream(string fileName)
    {
        return _FileReader.OpenReadStream(fileName);
    }

    public void WriteDataToFile(String path, string content)
    {
        if (!FileExists(path))
        {
            _FileWriter.WriteDataToFile(path, content);
        }
    }
    
    public void WriteDataToFile(String path, string[] content)
    {
        if (!FileExists(path))
        {
            _FileWriter.WriteDataToFile(path, content);
        }
    }
    
    public void StreamWriteDataToFile(String path, string content)
    {
        if (!FileExists(path))
        {
            _FileWriter.StreamWriteDataToFile(path, content);
        }
    }

}