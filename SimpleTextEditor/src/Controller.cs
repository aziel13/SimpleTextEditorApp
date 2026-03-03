namespace SimpleTextEditor;

public interface Controller
{
    void ReadAndProcessFile(string path);
    void WriteTextBoxDataToFile(string path,string content);

}