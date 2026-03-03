namespace SimpleTextEditor;

public interface IViewDelegate
{
    void fileSaveEvent();
    void fileOpenEvent();
    void exitEvent();
    
}