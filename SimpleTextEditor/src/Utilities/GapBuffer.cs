namespace SimpleTextEditor.Utilities;

public class GapBuffer<T>
{
    private int _capacity;
    private T[] _buffer;
    private int _gapStart;
    private int _gapEnd;
    
    public T[] Buffer => _buffer;
    
    public int Capacity => _capacity;
    public int GapStart => _gapStart;
    public int GapEnd => _gapEnd;
    
    public GapBuffer(int capacity) {
        
        _capacity = capacity;
        _buffer = new T[_capacity];
        _gapStart = 0;
        _gapEnd = _capacity - 1;
        
    }

    public void Insert(T input)
    {
        if (_gapStart == _gapEnd)
        {
            GrowBuffer();
        }

        _buffer[_gapStart] = input;
        
        _gapStart++;
    }

    public void GrowBuffer() {
        
        
        int new_capacity =  _capacity * 2;
        if (new_capacity > 0)
        {

            T[] newBuffer = new T[new_capacity];
            
            /*
            for (int i = 0; i < GapStart; i++)
            {
                newBuffer[i] = _buffer[i];
            }
            for (int i = GapEnd+1; i < _capacity; i++)
            {
                newBuffer[i] = _buffer[i];
            }*/


            _capacity = new_capacity;
            _buffer = newBuffer;
        }

    }


}