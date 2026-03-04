using SimpleTextEditor.Utilities;

namespace SimpleTextEditor;

public class ViewDelegate : IViewDelegate
{
    private bool exitFlag = false;
    
    private ApplicationLogicManager? _ApplicationLogicManager;

    public event EventHandler<OnOpenFileEventArgs> OnOpenFile;
    public class OnOpenFileEventArgs : EventArgs
    {
        public string filePath;
    }

    public event EventHandler OnUITriggeredExit;
    
    public event EventHandler<OnSaveFileEventArgs> OnSave;
    
    public class OnSaveFileEventArgs : EventArgs
    {
        public string filePath;
        public string text;
    }
    
    public ViewDelegate(Controller applicationLogicManager)
    {
        _ApplicationLogicManager = applicationLogicManager as  ApplicationLogicManager;
        _ApplicationLogicManager.OnExit += ApplicationLogicManager_OnExitEvent;

    }

    public event EventHandler<IViewDelegate.OnGapBufferEventArgs>? OnGapBufferUpdate;

    public bool GetExitFlag()
    {
        return  exitFlag;
    }

    public void FileSaveEvent(string filePath)
    {
        throw new NotImplementedException();
    }

    public void FileOpenEvent(string filePath)
    {
        throw new NotImplementedException();
    }

    public void UpdateFileTextInMemory(string content)
    {
        throw new NotImplementedException();
    }

    public void UpdateFileTextInMeomory(string content)
    {
        throw new NotImplementedException();
    }

    public void ApplicationLogicManager_OnExitEvent(object sender, EventArgs e)
    {
       Console.WriteLine($"Application Logic Manager Received On UI Triggered Exit");
       exitFlag = true;
    }

    public void FireUiExitEvent()
    {
        OnUITriggeredExit?.Invoke(this, EventArgs.Empty);
         
    }

    public void Insert(char  character, int cursorPosition)
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }
    
    public void Backspace()
    {
        throw new NotImplementedException();
    }

    public void MoveCursor(int cursorPosition) 
    {
        throw new NotImplementedException();
    }

    public IGapBuffer InitializeFrontEndIGapBuffer()
    {
        throw new NotImplementedException();
    }

    public void UpdateUIGapBuffer()
    {
        throw new NotImplementedException();
       // return ; 
    }


}