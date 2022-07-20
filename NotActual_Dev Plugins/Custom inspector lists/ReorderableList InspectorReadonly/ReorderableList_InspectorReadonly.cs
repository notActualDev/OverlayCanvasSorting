using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotActual_Dev.CustomInspectorLists
{
    [Serializable]
    public class ReorderableList_InspectorReadonly<T>
    {
        [SerializeField] public List<T> List;

        public ReorderableList_InspectorReadonly()
        {
            List = new List<T>();
        }

        public ReorderableList_InspectorReadonly(int capacity)
        {
            List = new List<T>(capacity);
        }

        public ReorderableList_InspectorReadonly(IEnumerable<T> collection)
        {
            List = new List<T>(collection);
        }
    } 
}
