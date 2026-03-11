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
            int originBufferSofar = 0;
            int addBufferSofar = 0;
            int priorAddlength = -1;
            int priorOriginLength = -1;
            
                
            for (int i = 0; i < _pieceTable.pieces.Count ; i++)
            {

                Piece thisPiece = _pieceTable.pieces[i];
                
                stringLengthSofar += thisPiece.Length;
                
                if (thisPiece.Source == PieceEnum.ORIGINAL)
                {
                    originBufferSofar +=  thisPiece.Length;
                }
                else
                {
                    addBufferSofar += thisPiece.Length;
                }
                
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
                            //int relativeIndex = stringLengthSofar - (index);
                           // int relativeIndex = addBufferSofar;
                            int relativeIndex = index - originBufferSofar;
                            
                            int indexAfterNewAddition = relativeIndex + text.Length;
                        //    Console.WriteLine($"index: {index} relativeIndex {relativeIndex} addBufferSofar: {addBufferSofar} originBufferSofar: {originBufferSofar} thisPiece.Length: {thisPiece.Length} priorAddlength {priorAddlength}");
                            
                            beforeLength = relativeIndex;

                         
                            afterLength =  thisPiece.Length - relativeIndex;
                            afterIndex = beforeLength;
                            
                            if (priorAddlength != -1)
                            {
                                beforeLength -= priorAddlength;
                                afterLength += priorAddlength;
                            }

                        }
                    } 
                    
                 //   Console.WriteLine($"beforeLength: {beforeLength}");
                    
                 //   Console.WriteLine($"afterLength: {afterLength}");
                  //  Console.WriteLine($"_pieceTable.pieces[i].Length: {_pieceTable.pieces[i].Length}");
                    
                    
                    Piece beforePiece = new Piece(beforeIndex , beforeLength, _pieceTable.pieces[i].Source);;
                    Piece afterPiece = new Piece(afterIndex, afterLength, _pieceTable.pieces[i].Source  );

                    piecesTablePiecesListDeepCopy.RemoveAt(i);
                    piecesTablePiecesListDeepCopy.Insert(i, beforePiece);
                    piecesTablePiecesListDeepCopy.Insert(i+1, newPiece);
                    piecesTablePiecesListDeepCopy.Insert(i+2, afterPiece);
                    
                    break;
                }
            
                if (thisPiece.Source == PieceEnum.ORIGINAL)
                {
                    if (priorOriginLength == -1)
                    {
                        priorOriginLength = thisPiece.Length;
                    }
                    else
                    {
                        priorOriginLength += thisPiece.Length;
                    }

                   
                    
                    
                }
                else
                {
                    if (priorAddlength == -1)
                    {
                        priorAddlength = thisPiece.Length;
                    }
                    else
                    {
                        priorAddlength += thisPiece.Length;
                    }
                }
                
            }
            
        }
        
       

        _pieceTable.addBuffer += text;

        _pieceTable.pieces = piecesTablePiecesListDeepCopy;

    }

    public void deleteText(int fromIndex, int toIndex )
    {
        int stringLengthSofar = 0;
        int originBufferSofar = 0;
        int addBufferSofar = 0;
        List<Piece> piecesTablePiecesListDeepCopy =  ListUtilities.ListDeepCopy(_pieceTable.pieces);

        string thisText = getText();
        int thisTextLength = getText().Length;
        
        int adjustedFromIndex = fromIndex;
         
        if (adjustedFromIndex <= getText().Length - 1)
        {
            
            for (int i = 0; i < _pieceTable.pieces.Count; i++)
            {
                Piece thisPiece = _pieceTable.pieces[i];

                stringLengthSofar += thisPiece.Length;

                if (thisPiece.Source == PieceEnum.ORIGINAL)
                {
                    originBufferSofar +=  thisPiece.Length;
                }
                else
                {
                    addBufferSofar += thisPiece.Length;
                }
                
                if (adjustedFromIndex < stringLengthSofar)
                {

                    // if stringLength is now greater than the fromIndex the delete region starts with this piece.
                    //check if the toIndex is in this segement

                    if (toIndex < thisPiece.Length + 1)
                    {
                        //if from index and start are the same only at most one new piece needs be made 
                        if (adjustedFromIndex == thisPiece.Start)
                        {
                            //if the delete spans the entire piece, we can just remove it.
                            if (toIndex == thisPiece.Length)
                            {
                                piecesTablePiecesListDeepCopy.RemoveAt(i);
                            }
                            else
                            {
                                int newIndex = toIndex;
                                
                                var newLength = thisPiece.Length-toIndex;
                                Piece adjustedPiece = new Piece(newIndex, newLength, thisPiece.Source);
                            
                                piecesTablePiecesListDeepCopy.RemoveAt(i);
                                piecesTablePiecesListDeepCopy.Insert(i, adjustedPiece);
                                
                            }

                        }
                        else
                        {
                            if (toIndex == thisPiece.Length)
                            {
                                int newLength = fromIndex;
                                
                                Piece adjustedPiece = new Piece(thisPiece.Start, newLength, thisPiece.Source);
                            
                                piecesTablePiecesListDeepCopy.RemoveAt(i);
                                piecesTablePiecesListDeepCopy.Insert(i, adjustedPiece);
                                
                                
                            }
                            else
                            {
                                int beforeLength = adjustedFromIndex;
                                int afterLength = this.getText().Length - toIndex;
                                
                                Piece beforePiece = new Piece(thisPiece.Start, beforeLength, thisPiece.Source);
                                Piece afterPiece = new Piece(toIndex, afterLength, thisPiece.Source);

                                piecesTablePiecesListDeepCopy.RemoveAt(i);
                                piecesTablePiecesListDeepCopy.Insert(i, beforePiece);
                                piecesTablePiecesListDeepCopy.Insert(i+1, afterPiece);
                                
                               
                            }
                        } 
                    }
                    else
                    {
                        
                        
                        //find the other pieces that make up this delete
                        int thisPieceIndex = thisPiece.Start;
                        
                        int thisPieceLength = fromIndex - originBufferSofar;

                        if (thisPiece.Source == PieceEnum.ORIGINAL)
                        {
                            thisPieceLength = adjustedFromIndex - addBufferSofar;
                        }
                        else
                        {
                            thisPieceLength = adjustedFromIndex - originBufferSofar;
                        }

                        int breakhere = 0;
                    
                        Piece thisAdjustedPiece = new Piece(thisPieceIndex,thisPieceLength, thisPiece.Source);

                        stringLengthSofar -= thisPiece.Length;
                        stringLengthSofar += thisAdjustedPiece.Length;
                        piecesTablePiecesListDeepCopy.RemoveAt(i);
                        piecesTablePiecesListDeepCopy.Insert(i, thisAdjustedPiece);
                        
                        adjustedFromIndex = stringLengthSofar;

                        List<Piece> otherPieces = getFuturePieces(i+1,toIndex);
                        
                        int localAddBufferSofar = addBufferSofar;
                        int localOriginBufferSofar = originBufferSofar;
                        int index = i+1;
                        
                        for (int x = 0; x < otherPieces.Count; x++)
                        {
                            PieceEnum localSource =  otherPieces[x].Source;
                            int startIndex = 0;
                            int otherLength = 0;
                            
                            if (otherPieces[x].Source == PieceEnum.ORIGINAL)
                            {
                                localOriginBufferSofar += otherPieces[x].Length;
                               
                            }
                            else
                            {
                                localAddBufferSofar += otherPieces[x].Length;
                            }

                            stringLengthSofar += piecesTablePiecesListDeepCopy[index].Length;

                            if (toIndex <  stringLengthSofar)
                            {
                                
                                if (otherPieces[x].Source == PieceEnum.ORIGINAL)
                                {
                                    startIndex = toIndex - addBufferSofar - piecesTablePiecesListDeepCopy[index].Start + originBufferSofar;
                                    otherLength = _pieceTable.originalBuffer.Length-startIndex;
                                }
                                else
                                {
                                    startIndex = toIndex - originBufferSofar - piecesTablePiecesListDeepCopy[index].Start + addBufferSofar ;
                                    otherLength = _pieceTable.addBuffer.Length-startIndex;
                                }
                                
                                string addBuffer = _pieceTable.addBuffer;
                                int addBufferLength = _pieceTable.addBuffer.Length;
                                
                                Piece lastAdjustedPiece = new Piece(startIndex,otherLength, localSource);

                                string thisOrg = _pieceTable.originalBuffer;
                                int thisOrgLength = thisOrg.Length;
                                
                                string adjThisOrg = thisOrg.Substring(startIndex, otherLength);
                                
                                piecesTablePiecesListDeepCopy.RemoveAt(index);
                                piecesTablePiecesListDeepCopy.Insert(index, lastAdjustedPiece);
                                
                            }
                            else
                            {
                                piecesTablePiecesListDeepCopy.RemoveAt(index);
                                index--;
                            }

                            index++;
                        }
                        
                        break;
                    }
                } 
            }
        }
        _pieceTable.pieces = piecesTablePiecesListDeepCopy;
    }

    public List<Piece> getFuturePieces(int currentStart,int toIndex)
    {
        int endIndex = 0;
        int startIndex = 0;
        
        int localIndex = 0;
        
        List<Piece> futurePieces = new List<Piece>();

        int index = currentStart;
        
        while ( index < _pieceTable.pieces.Count)
        {

            endIndex += _pieceTable.pieces[index].Length;
            
            futurePieces.Add(_pieceTable.pieces[index]);
            
            if ( endIndex > toIndex)
            {
                break;
            }

            
            startIndex = endIndex;
            index++;
        }

        return futurePieces;
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