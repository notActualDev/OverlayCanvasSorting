using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotActualDev_ExtensionMethods_MonoBehaviour
{
    public static int Count(this Object caller)
    {
        return Object.FindObjectsOfType(caller.GetType()).Length;
    }
}
