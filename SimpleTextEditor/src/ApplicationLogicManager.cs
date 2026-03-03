namespace SimpleTextEditor;

public class ApplicationLogicManager : Controller
{
    
    public event EventHandler<OnFileReadEventArgs> OnFileRead;
    public class OnFileReadEventArgs : EventArgs
    {
      
    }
    public ApplicationLogicManager()
    {
         
    }

    public void ReadAndProcessFile(string path)
    {
        throw new NotImplementedException();
    }

    public void WriteTextBoxDataToFile(string path, string content)
    {
        throw new NotImplementedException();
    }
}