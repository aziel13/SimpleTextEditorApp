namespace SimpleTextEditor.Utilities;

public interface IFileWriter
{
    bool FileExists(string fileName);
    void WriteDataToFile(string path,string content);
    void WriteDataToFile(string path,string[] content);

    void StreamWriteDataToFile(string path,string content);
    
    StreamWriter OpenWriteStream(string fileName);
    
    
}