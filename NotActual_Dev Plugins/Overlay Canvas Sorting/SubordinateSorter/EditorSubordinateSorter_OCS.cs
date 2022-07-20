#if UNITY_EDITOR
using NotActual_Dev.CustomInspectorLists;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting
{
    public partial class SubordinateSorter_OCS : BaseSorter_OCS, IEditorDisposable_OCS
    {
        [SerializeField, HideInInspector] internal EditorPart editorPart;

        void Editor_Reset()
        {
            editorPart = new EditorPart(this);
        }

        void IEditorDisposable_OCS.DisposeIfRedundant()
        {
            if (ApplicationIsPlaying(nameof(IEditorDisposable_OCS.DisposeIfRedundant))) return;
            if (canvas == null) DestroyImmediate(this);
        }

        void IEditorDisposable_OCS.Dispose()
        {
            if (ApplicationIsPlaying(nameof(IEditorDisposable_OCS.Dispose))) return;
            DestroyImmediate(this);
        }

        bool ApplicationIsPlaying(string method)
        {
            if (Application.isPlaying)
            {
                Debug.LogError($"Method \"{method}\" is invoked in runtime, even though it's an editor method.");
                return true;
            }
            return false;
        }

        [Serializable]
        internal class EditorPart
        {
            [SerializeField, HideInInspector] SubordinateSorter_OCS instance;
            
            void Reset()
            {
                if (instance.ApplicationIsPlaying(nameof(Reset))) return;
                instance.canvas = instance.GetComponent<Canvas>();
                instance.master = FindObjectOfType<MasterSorter_OCS>(true);
                if (instance.canvas == null || instance.master == null)
                {
                    DestroyImmediate(instance);
                    return;
                }
                instance.inspectorSubordinateSorters = new ReorderableList_InspectorReadonly<SubordinateSorter_OCS>();
            }

            void GetSubordinates()
            {
                if (instance.ApplicationIsPlaying(nameof(GetSubordinates))) return;
                List<SubordinateSorter_OCS> foundSorters = instance.transform.GetHighestComponentsInChildren<SubordinateSorter_OCS>();
                instance.SubordinateSorters = foundSorters.OrderByTemplate(instance.SubordinateSorters);
            }

            internal void GetSubordinateSortersCascade()
            {
                if (instance.ApplicationIsPlaying(nameof(GetSubordinateSortersCascade))) return;
                GetSubordinates();
                instance.SubordinateSorters.ForEach(subordinate => subordinate?.editorPart.GetSubordinateSortersCascade());
            }

            void InitialSort()
            {
                if (instance.ApplicationIsPlaying(nameof(InitialSort))) return;
                List<SubordinateSorter_OCS> sortersOverridingSorting = new List<SubordinateSorter_OCS>();
                List<SubordinateSorter_OCS> sortersNotOverridingSorting = new List<SubordinateSorter_OCS>();
                foreach (var subordinate in instance.SubordinateSorters)
                {
                    if (subordinate == null) continue;
                    if (subordinate.Canvas.overrideSorting) sortersOverridingSorting.Add(subordinate);
                    else sortersNotOverridingSorting.Add(subordinate);
                }
                sortersOverridingSorting = sortersOverridingSorting.OrderBy((sorter) => sorter.canvas.sortingOrder).ToList();
                sortersOverridingSorting.Reverse();
                sortersNotOverridingSorting.Reverse();
                instance.SubordinateSorters.Clear();

                foreach (var sorter in sortersOverridingSorting)
                {
                    if (sorter != null) instance.SubordinateSorters.Add(sorter);
                }
                foreach (var sorter in sortersNotOverridingSorting)
                {
                    if (sorter != null) instance.SubordinateSorters.Add(sorter);
                }
            }

            internal void InitialSortCascade()
            {
                if (instance.ApplicationIsPlaying(nameof(InitialSortCascade))) return;
                InitialSort();
                instance.SubordinateSorters.ForEach(sorter => sorter?.editorPart.InitialSortCascade());
            }

            void SetSuperior(BaseSorter_OCS superior)
            {
                if (instance.ApplicationIsPlaying(nameof(SetSuperior))) return;
                instance.superior = superior;
            }

            internal void SetSuperiorCascade(BaseSorter_OCS superior)
            {
                if (instance.ApplicationIsPlaying(nameof(SetSuperiorCascade))) return;
                SetSuperior(superior);
                instance.SubordinateSorters.ForEach(sorter => sorter?.editorPart.SetSuperiorCascade(instance));
            }

            public EditorPart(SubordinateSorter_OCS instance)
            {
                this.instance = instance;
                Reset();
            }
        }
    }
}
#endif
