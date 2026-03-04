namespace SimpleTextEditor.Utilities;

public interface IGapBuffer
{
    
    public void Insert(char input);
    public void Insert(string input);
    public void Insert(string[] input);
    public void Insert(char[] input);
    public void GrowBuffer(int requiredSpace);
    public void LeftHandRemove(int dist = 1);
    public void RightHandRemove(int dist = 1);
    public void MoveGap(int index);
    
    public String ToString();
    
}