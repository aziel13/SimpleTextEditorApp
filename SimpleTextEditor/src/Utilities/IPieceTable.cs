namespace SimpleTextEditor.Utilities;

public interface IPieceTable
{
    
    void insertText(int index,string text);
    void deleteText(int fromIndex, int toIndex);
    string getText();

}