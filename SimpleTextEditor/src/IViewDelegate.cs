using SimpleTextEditor.Utilities;

namespace SimpleTextEditor;

public interface IViewDelegate
{
    event EventHandler OnUITriggeredExit;

    class OnGapBufferEventArgs : EventArgs
    {
        public IGapBuffer? GapBuffer;
 
    };

    event EventHandler<OnGapBufferEventArgs> OnGapBufferUpdate;
    
    bool GetExitFlag();
    
    void FileSaveEvent(string filePath);
    void FileOpenEvent(string filePath);

    void UpdateFileTextInMemory(string content);
    void ApplicationLogicManager_OnExitEvent(object sender, EventArgs e);
    
    void FireUiExitEvent();

    void Insert(char character, int cursorPosition);
    void Delete();
    void Backspace();
    void MoveCursor(int cursorPosition);

    IGapBuffer InitializeFrontEndIGapBuffer();
}