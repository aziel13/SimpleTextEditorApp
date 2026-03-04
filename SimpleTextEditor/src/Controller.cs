namespace SimpleTextEditor;

public interface Controller
{
    public event EventHandler OnExit;
    void ReadAndProcessFile(string path);
    void WriteTextBoxDataToFile(string path,string content);

    void ViewDelegate_OnFileOpen(object sender, EventArgs e);
    void ViewDelegate_OnSave(object sender, EventArgs e);
    
    void ViewDelegateOnUiTriggeredExit(object sender, EventArgs e);

}