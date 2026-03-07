using System.Diagnostics;

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
        string originalBuffer = _pieceTable.originalBuffer;
        
        List<Piece> piecesTablePiecesListDeepCopy =  ListUtilities.ListDeepCopy(_pieceTable.pieces);
        
        int newStart = _pieceTable.addBuffer.Length;
        
        int totalTextLength = _pieceTable.originalBuffer.Length + _pieceTable.addBuffer.Length; 
        
        Piece newPiece = new Piece(newStart, text.Length, PieceEnum.ADD);
        
        if (index == 0)
        {
            piecesTablePiecesListDeepCopy.Insert(0, newPiece);
            
        } else if (index >= totalTextLength) {
            
            piecesTablePiecesListDeepCopy.Add(newPiece);
            
        }
        else
        {

            int stringLengthSofar = 0;
            
            for (int i = 0; i < _pieceTable.pieces.Count ; i++)
            {

                Piece thisPiece = _pieceTable.pieces[i];
                
                stringLengthSofar += thisPiece.Length;
                if (index == stringLengthSofar)
                {
                    
                    piecesTablePiecesListDeepCopy.Insert(i+1, newPiece);
                    
                    break;
                }
                else if (index < stringLengthSofar)
                {
                    
                    int beforeLength;

                    int beforeIndex = _pieceTable.pieces[i].Start;
                    int afterLength;
                    int afterIndex;
                    
                    if ((_pieceTable.pieces[i].Source == PieceEnum.ORIGINAL && _pieceTable.originalBuffer.Length > index)) 
                    {
                        beforeLength = index;
                        afterLength = _pieceTable.pieces[i].Length - beforeLength;
                        afterIndex = beforeLength;
                    }
                    else
                    {
                        if (_pieceTable.pieces[i].Source == PieceEnum.ORIGINAL)
                        {
                            beforeLength = Math.Abs(_pieceTable.originalBuffer.Length  - text.Length);
                            afterLength =  Math.Abs( _pieceTable.pieces[i].Length - beforeLength);
                            afterIndex = beforeLength;

                        }
                        else
                        {
                            int relativeIndex = stringLengthSofar - (index);
                            int indexAfterNewAddition = relativeIndex + text.Length;
                            
                            Console.WriteLine($"relativeIndex: {relativeIndex} + text.length = index at end of new add {indexAfterNewAddition} after length { thisPiece.Length - relativeIndex}"); 

                           
                            // Console.WriteLine($"stringLengthSofar-index: {stringLengthSofar-index-1}");
                            
                            Console.WriteLine($"index: {index}");

                            
                            beforeLength = relativeIndex; 
                            afterLength =  thisPiece.Length - relativeIndex;
                            afterIndex = beforeLength;
                            
                        }
                    } 
                    
                    Console.WriteLine($"beforeLength: {beforeLength}");
                    
                    Console.WriteLine($"afterLength: {afterLength}");
                    Console.WriteLine($"_pieceTable.pieces[i].Length: {_pieceTable.pieces[i].Length}");
                    
                    
                    Piece beforePiece = new Piece(beforeIndex , beforeLength, _pieceTable.pieces[i].Source);;
                    Piece afterPiece = new Piece(afterIndex, afterLength, _pieceTable.pieces[i].Source  );

                    piecesTablePiecesListDeepCopy.RemoveAt(i);
                    piecesTablePiecesListDeepCopy.Insert(i, beforePiece);
                    piecesTablePiecesListDeepCopy.Insert(i+1, newPiece);
                    piecesTablePiecesListDeepCopy.Insert(i+2, afterPiece);
                    
                    break;
                }
            
            }
            
        }
        

        _pieceTable.addBuffer += text;

        _pieceTable.pieces = piecesTablePiecesListDeepCopy;

    }

    public void deleteText(int index, string text)
    {
        throw new NotImplementedException();
    }

    public string getText()
    {
         string result = string.Empty;
         
         List<string> documentConstructionList = new List<string>();
         
        //$"{_pieceTable.originalBuffer}{_pieceTable.addBuffer}"
         for (int i = 0; i < _pieceTable.pieces.Count; i++) {

             Piece piece = _pieceTable.pieces[i];
             
             if (piece.Source == PieceEnum.ORIGINAL)
             {
                 result += _pieceTable.originalBuffer.Substring(piece.Start,  piece.Length); 
             }
             else
             {
                 result += _pieceTable.addBuffer.Substring(piece.Start,  piece.Length); 
             }

         }
         
         return result;
    }
}