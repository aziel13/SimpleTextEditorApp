namespace SimpleTextEditor.Utilities;

public enum PieceEnum
{
    ORIGINAL,
    ADD
}

public struct Piece
{
    public int Start;
    public int Length;
    public PieceEnum Source;
    
    public Piece(int start,  int length, PieceEnum source)
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
    public List<Piece> pieces;

    public PieceTable(string originalBuffer, string addBuffer, List<Piece> pieces)
    {
        this.originalBuffer = originalBuffer;
        this.addBuffer = addBuffer;
        
        this.pieces = [..pieces];
       
    }

    public readonly override string ToString()
    {
        string result = "";
        
        string piecesString = "";
        for (int i=0;i< pieces.Count; i++)
        {
            
            piecesString += $"{pieces[i].ToString()}";
            if (i < pieces.Count - 1)
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
        
        List<Piece> expectedPieces =
        [
            new Piece(start: 0, length: originText.Length, source: PieceEnum.ORIGINAL)
        ];

        _pieceTable = new PieceTable(originText,string.Empty,expectedPieces);
    }
    
    public void insertText(int index, string text)
    {
        
        int newStart = _pieceTable.addBuffer.Length;
        
        _pieceTable.addBuffer += text;

        _pieceTable.pieces.Add(new Piece(newStart,text.Length,PieceEnum.ADD));

    }

    public void deleteText(int index, string text)
    {
        throw new NotImplementedException();
    }

    public string getText()
    {
         string result = string.Empty;
         
        //$"{_pieceTable.originalBuffer}{_pieceTable.addBuffer}"
         for (int i = 0; i < _pieceTable.pieces.Count; i++) {

             Piece piece = _pieceTable.pieces[i];
             
             
             if (piece.Source == PieceEnum.ORIGINAL)
             {
                 result += _pieceTable.originalBuffer.Substring(piece.Start, piece.Start + piece.Length); 
             }
             else
             {
                 result += _pieceTable.addBuffer.Substring(piece.Start, piece.Start + piece.Length); 
             }

         }
         
         return result;
    }
}