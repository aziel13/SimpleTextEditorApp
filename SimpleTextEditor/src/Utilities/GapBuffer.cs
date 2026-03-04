namespace SimpleTextEditor.Utilities;



public class GapBuffer
{
    private int _capacity;
    private char[] _buffer;
    private int _gapStart;
    private int _gapEnd;
    
    private int _gapLength => _gapEnd - _gapStart;
    private int _contentLength => _capacity - _gapLength;
    
    public char[] Buffer => _buffer;
    
    public int Capacity => _capacity;
    public int GapStart => _gapStart;
    public int GapEnd => _gapEnd;
    
    public GapBuffer(int capacity) {
        
        _capacity = capacity;
        _buffer = new char[_capacity];
        _gapStart = 0;
        _gapEnd = _capacity - 1;
        
    }

    public void Insert(char input)
    {
        if (_gapStart == _gapEnd)
        {
            GrowBuffer(1);
        }
 
        
        _buffer[_gapStart] = input;
    
        _gapStart++;            
        
    }
    public void Insert(string input)
    {
        if (_gapStart == _gapEnd)
        {
            GrowBuffer(input.Length);
        }
        
        char[] inputToCharArray = input.ToCharArray();

        foreach (char thisChar in inputToCharArray)
        {
           Insert(thisChar); 
        }
    }
    public void Insert(string[] input)
    {

        int neededLength = 0;
        
        foreach (string s in input)
        {

            neededLength += s.Length;

        }

        if (_gapStart == _gapEnd)
        {
            GrowBuffer(input.Length);
        }
        
        foreach (string s in input)
        {
            char[] inputToCharArray = s.ToCharArray();

            foreach (char thisChar in inputToCharArray)
            {
                Insert(thisChar);
            }            
        }
        
    }
    public void Insert(char[] input)
    {
        if (_gapStart == _gapEnd)
        {
         GrowBuffer(input.Length);
        }
        
        char[] inputToCharArray = input;

        foreach (char thisChar in inputToCharArray)
        {
              Insert(thisChar);
        }
        
    }
    
    public void GrowBuffer(int requiredSpace) {
        
        
        int new_capacity =  Math.Max(  _capacity * 2 , _contentLength + requiredSpace);
        
        if (new_capacity > 0)
        {

            char[] newBuffer = new char[new_capacity];
            
            
            for (int i = 0; i < GapStart; i++)
            {
                newBuffer[i] = _buffer[i];
            }
            for (int i = GapEnd+1; i < _capacity; i++)
            {
                newBuffer[i] = _buffer[i];
            }
            _gapEnd += _capacity;
            _capacity = new_capacity;
            _buffer = newBuffer;
           
        }

    }

    public void LeftHandRemove(int dist = 1)
    {
        if(dist > GapStart) 
        {
            throw new Exception($"Distance:{dist} is greater than {GapStart}");
        }
        
        _gapStart -= dist;
    }

    public void RightHandRemove(int dist = 1)
    {
        if (dist > GapEnd)
        {
            throw new Exception($"Distance:{dist} is greater than {GapEnd}");
        }
        
        _gapEnd += dist;
    }

    public void MoveGap(int index)
    {
        string bufferBeforeMove = string.Empty;
       
        Console.WriteLine($"Buffer Before GapMove: {bufferBeforeMove}");
        if (_gapStart > index)
        {

            while (_gapStart > index)
            {
                _gapStart--;
                
                _gapEnd--;

                _buffer[_gapEnd] = _buffer[_gapStart];

            }
            
        } else if (index > _gapStart)
        {
            
            _gapStart++;
            
            if (_gapEnd < _contentLength-1)
                _gapEnd++;

            _buffer[_gapStart] = _buffer[_gapEnd];
            
        }
        
        string bufferAfterMove = string.Empty;
       
    }

    public override String ToString()
    {
       string result = "";
       
       
       return result;

    }


}