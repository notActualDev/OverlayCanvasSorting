using System.Collections.Generic;

public static class NotActualDev_ExtensionMethods_List
{
    public static List<T> OrderByTemplate<T>(this List<T> sourceList, List<T> template)
    {
        List<T> sourceListCopy = new List<T>(sourceList);
        List<T> orderedList = new List<T>(sourceList.Count);

        foreach (var orderTemplateElement in template)
        {
            //if (orderTemplateElement == null) continue;
            if (sourceListCopy.Contains(orderTemplateElement))
            {
                orderedList.Add(orderTemplateElement);
                sourceListCopy.Remove(orderTemplateElement);
            }
        }

        foreach (var leftover in sourceListCopy)
        {
            //if (leftover == null) continue;
            orderedList.Add(leftover);
        }

        return orderedList;
    }
}
