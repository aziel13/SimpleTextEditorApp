namespace SimpleTextEditor.Utilities;

public interface IPieceTable
{
    
    void insertText(int index,string text);
    void deleteText(int index, string text);
    string getText();

}