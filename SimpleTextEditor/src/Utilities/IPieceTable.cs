namespace SimpleTextEditor.Utilities;

public interface IPieceTable
{
    
    void insertText(string text);
    void deleteText(string text);
    string getText();

}