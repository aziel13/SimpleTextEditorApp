namespace SimpleTextEditor.Utilities;


public struct Piece
{
    public int Start;
    public int Length;
    public string Source;
    
    public Piece(int start,  int length, string source)
    {
        Start = start;
        Length = length;
        Source = source;
    }

    public readonly override string ToString() => $"{Start},{Length},{Source}";
}

public struct PieceTable 
{
    public string originalBuffer;
    public string addBuffer;
    public Piece[] pieces;

    public PieceTable(string originalBuffer, string addBuffer, Piece[] pieces)
    {
        this.originalBuffer = originalBuffer;
        this.addBuffer = addBuffer;
        
        this.pieces = new Piece[pieces.Length];
        for (int i=0;i< pieces.Length; i++)
        {
            this.pieces[i] =  pieces[i];
        }
    }

    public readonly override string ToString()
    {
        string result = "";
        
        string piecesString = "";
        for (int i=0;i< pieces.Length; i++)
        {
            
            piecesString += $"{pieces[i].ToString()}";
            if (i < pieces.Length - 1)
            {
                piecesString += $"\n";
            } 
            
        }
        
        result = $"{originalBuffer},{addBuffer},{piecesString}";
        
        return result;
    }
   
}

public class PieceTableDataStructure : IPieceTable
{
    private PieceTable _pieceTable;

    public PieceTable PieceTable => _pieceTable;


    public PieceTableDataStructure(string filePath = "")
    {
        FileReader fileReader = new FileReader();
        string originText = string.Empty;
        if (fileReader.FileExists(filePath))
        {
            originText = fileReader.ReadDataFromFile(filePath);
        }
        
        Piece[] expectedPieces = [new Piece(start:0,length:0,source:originText)];
        
        _pieceTable = new PieceTable(string.Empty,string.Empty,expectedPieces);
    }
    
    public void insertText(string text)
    {
        throw new NotImplementedException();
    }

    public void deleteText(string text)
    {
        throw new NotImplementedException();
    }

    public string getText()
    {
         string result = $"{_pieceTable.originalBuffer}{_pieceTable.addBuffer}";
         
         return result;
    }
}