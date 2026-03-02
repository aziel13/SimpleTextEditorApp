using System.Text;

namespace SimpleTextEditor.Utilities;
using System.IO;
    
public class FileWriter : IFileWriter
{
    
    
    public FileWriter()
    {
        
    }

    public bool FileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    public void WriteDataToFile(string path,string content)
    {
        File.WriteAllText(path, content);
    }
    
    public void WriteDataToFile(string path,string[] content)
    {
        File.WriteAllLines(path, content);
    }

    public void StreamWriteDataToFile(string path, string content)
    {
        try
        {
            using StreamWriter writer = OpenWriteStream(path);
            writer.WriteLine( content);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public StreamWriter OpenWriteStream(string fileName)
    {
        FileStream fileStream = new FileStream(fileName, FileMode.Create);
        
        StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8);
        
        return writer;
    }
    
}