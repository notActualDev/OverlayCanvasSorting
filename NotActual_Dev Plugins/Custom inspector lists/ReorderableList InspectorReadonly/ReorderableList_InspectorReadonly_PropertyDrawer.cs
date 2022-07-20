#if UNITY_EDITOR
// This code is strongly based on a code posted by PrefabEvolution here: https://forum.unity.com/threads/unity-4-5-reorderablelist.249279/#post-1648768

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace NotActual_Dev.CustomInspectorLists.UnderlyingSystem
{
    [CustomPropertyDrawer(typeof(ReorderableList_InspectorReadonly<>), true)]
    public class ReorderableList_InspectorReadonly_PropertyDrawer : PropertyDrawer
    {
        private ReorderableList reorderableList;

        private ReorderableList getList(SerializedProperty property)
        {
            if (reorderableList == null)
            {
                reorderableList = new ReorderableList(property.serializedObject, property, true, true, false, false);
                reorderableList.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, fieldInfo.Name);
                };
                reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    rect.width -= 40;
                    rect.x += 20;
                    GUI.enabled = false;
                    EditorGUI.PropertyField(rect, property.GetArrayElementAtIndex(index), true);
                    GUI.enabled = true;
                };
            }
            return reorderableList;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return getList(property.FindPropertyRelative("List")).GetHeight();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;

            var listProperty = property.FindPropertyRelative("List");
            var list = getList(listProperty);
            var height = 0f;
            for (var i = 0; i < listProperty.arraySize; i++)
            {
                height = Mathf.Max(height, EditorGUI.GetPropertyHeight(listProperty.GetArrayElementAtIndex(i)));
            }
            list.elementHeight = height;
            list.DoList(position);

            GUI.enabled = true;
        }
    } 
}

#endif