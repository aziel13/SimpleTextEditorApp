using SimpleTextEditor.Utilities;

namespace SimpleTextEditor;

public class TextData 
{        
    private GapBuffer _gapBuffer;
    public GapBuffer GapBuffer => _gapBuffer;
    
    public TextData (int initialCapacity) {
        
        _gapBuffer = new GapBuffer(initialCapacity);
    
    }
    
}