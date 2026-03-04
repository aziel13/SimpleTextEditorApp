using System.Net.Mime;

namespace SimpleTextEditor;

public class ApplicationLogicManager : Controller
{
    private const int initialSize = 1024;
    
    private IViewDelegate _viewDelegate;

    public IViewDelegate ViewDelegate => _viewDelegate;

    private TextData _textData;
    public event EventHandler OnExit;
    
    public ApplicationLogicManager()
    {
  
        _viewDelegate = new ViewDelegate(this);
        
        _textData = new TextData(initialSize);

        _viewDelegate.OnUITriggeredExit += ViewDelegateOnUiTriggeredExit;
        
    }

    public void ReadAndProcessFile(string path)
    {
        throw new NotImplementedException();
    }

    public void WriteTextBoxDataToFile(string path, string content)
    {
        throw new NotImplementedException();
    }

    public void ViewDelegate_OnFileOpen(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void ViewDelegate_OnSave(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void ViewDelegateOnUiTriggeredExit(object sender, EventArgs e)
    {
        Exit();
    }
    public void Exit()
    {
        OnExit?.Invoke(this, EventArgs.Empty);
    }
    
}