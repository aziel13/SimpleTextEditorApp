namespace SimpleTextEditor.Utilities;

public interface IFileReader
{
    
    bool FileExists(string fileName);
    string ReadDataFromFile(string path);

    Stream OpenReadStream(string fileName);

}