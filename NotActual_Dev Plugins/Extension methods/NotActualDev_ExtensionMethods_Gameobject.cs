using UnityEngine;

public static class NotActualDev_ExtensionMethods_Gameobject
{
    public static T AddMissingComponent<T>(this GameObject gameobject) where T : Component
    {
        T component = gameobject.GetComponent<T>();
        if (component == null)
        {
            component = gameobject.AddComponent<T>();
        }
        return component;
    }
}
