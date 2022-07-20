using UnityEngine;

// You can only add or remove elements from code.
// In inspector you can only change the order of the elements.

namespace NotActual_Dev.CustomInspectorLists.ExampleUse
{
    public class ExampleUse_ReorderableList_InspectorReadonly : MonoBehaviour
    {
        [SerializeField] ReorderableList_InspectorReadonly<int> numbers;
        [SerializeField] ReorderableList_InspectorReadonly<MonoBehaviour> monoBehaviours;

        private void Reset()
        {
            numbers = new ReorderableList_InspectorReadonly<int>();
            numbers.List.Add(0);
            numbers.List.Add(1);
            numbers.List.Add(2);

            monoBehaviours = new ReorderableList_InspectorReadonly<MonoBehaviour>();
            monoBehaviours.List.Add(null);
            monoBehaviours.List.Add(null);
            monoBehaviours.List.Add(null);
        }
    } 
}
