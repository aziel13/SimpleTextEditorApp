namespace SimpleTextEditor.Utilities;

public static class ListUtilities
{
    
    public static List<T> ListDeepCopy<T>(List<T> deepCopy)
    {
        List<T> newList = new List<T>();

        for (int i = 0; i < deepCopy.Count; i++) {
            
            newList.Add(deepCopy[i]);
            
        }

        return newList;
    }

}