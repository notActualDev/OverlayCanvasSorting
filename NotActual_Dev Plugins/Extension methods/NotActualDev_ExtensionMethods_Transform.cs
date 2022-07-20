using System.Collections.Generic;
using UnityEngine;

public static class NotActualDev_ExtensionMethods_Transform
{
    /// <summary>
    /// Returns all first children under given transform. It does not return any nested children (children of children).
    /// </summary>
    /// <param name="caller"></param>
    /// <returns></returns>
    public static Transform[] GetFirstDepthChildren(this Transform caller)
    {
        Transform[] firstDepthChildren = new Transform[caller.childCount];

        for (int i = 0; i < firstDepthChildren.Length; i++)
        {
            firstDepthChildren[i] = caller.GetChild(i);
        }

        return firstDepthChildren;
    }

    /// <summary>
    /// Returns all T components found in first children. If the child has more than one T component, only one will be returned.
    /// If T component is not found in some of the first children, it goes deeper into them and returns all T components in first children of these previous "empty first children".
    /// If T component is not found again, it all repeats. And it goes recursively to the deepest child.
    /// It's better depicted on a visual representation in the folder that this class is in.
    /// </summary>
    /// <typeparam name="T">Type of component to look for</typeparam>
    /// <param name="caller"></param>
    /// <returns></returns>
    public static List<T> GetHighestComponentsInChildren<T>(this Transform caller) where T : Component
    {
        List<T> foundComponents = new List<T>();

        foreach (var child in GetFirstDepthChildren(caller))
        {
            T component = child.GetComponent<T>();
            if (component != null)
            {
                foundComponents.Add(component);
                continue;
            }
            foreach (var comp in GetHighestComponentsInChildren<T>(child))
            {
                foundComponents.Add(comp);
            }
        }
        return foundComponents;
    }

    public static T AddMissingComponent<T>(this Transform transform) where T : Component
    {
        T component = transform.GetComponent<T>();
        if (component == null)
        {
            component = transform.gameObject.AddComponent<T>();
        }
        return component;
    }

    public static List<T> GetShallowComponentsInFirstDepthChildren<T>(this Transform caller) where T : Component
    {
        Transform[] firstDepthChildren = GetFirstDepthChildren(caller);
        List<T> foundComponents = new List<T>(firstDepthChildren.Length);

        foreach (var child in firstDepthChildren)
        {
            T component = child.GetComponent<T>();
            if (component != null)
            {
                foundComponents.Add(component);
            }
        }

        return foundComponents;
    }
}
