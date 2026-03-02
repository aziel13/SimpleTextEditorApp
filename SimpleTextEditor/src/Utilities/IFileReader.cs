namespace SimpleTextEditor.Utilities;

public interface IFileManager
{
    
    bool FileExists(string fileName);
    string ReadDataFromFile(string path);

    Stream GetStream(string fileName);
    // void WriteAllText(string path, string text);


}