#if UNITY_EDITOR
using NotActual_Dev.CustomInspectorLists;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NotActual_Dev.OverlayCanvasSorting
{
    public partial class MasterSorter_OCS : BaseSorter_OCS//
    {
        [SerializeField, HideInInspector] internal EditorPart editorPart;

        void Editor_Reset()
        {
            editorPart = new EditorPart(this);
        }

        [Serializable]
        internal class EditorPart
        {
            [SerializeField, HideInInspector] MasterSorter_OCS instance;

            [SerializeField, HideInInspector] UnityEvent BeforeSystemUpdated;
            [SerializeField, HideInInspector] UnityEvent AfterSystemUpdated;

            void Reset()
            {
                if (ApplicationIsPlaying(nameof(Reset))) return;
                if (instance.Count() > 1)
                {
                    DestroyImmediate(instance);
                    return;
                }
                instance.inspectorSubordinateSorters = new ReorderableList_InspectorReadonly<SubordinateSorter_OCS>();
                BeforeSystemUpdated = new UnityEvent();
                AfterSystemUpdated = new UnityEvent();
                SystemUpdate();
                InitialSort();
                instance.SubordinateSorters.ForEach(subordinate => subordinate?.editorPart.InitialSortCascade());
            }

            internal void Update()
            {
                if (ApplicationIsPlaying(nameof(Update))) return;
                SystemUpdate();
                SetSortOrders();
            }

            internal void OnDestroy()
            {
                if (ApplicationIsPlaying(nameof(OnDestroy))) return;
                if (instance.Count() == 1) FindObjectsOfType<MonoBehaviour>(true).OfType<IEditorDisposable_OCS>().ToList().ForEach(disposable => disposable?.Dispose());
            }

            void SystemUpdate()
            {
                if (ApplicationIsPlaying(nameof(SystemUpdate))) return;

                FindObjectsOfType<MonoBehaviour>(true).OfType<IEditorDisposable_OCS>().ToList().ForEach(disposable => disposable.DisposeIfRedundant());
                BeforeSystemUpdated?.Invoke();

                instance.transform.AddMissingComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                instance.transform.AddMissingComponent<CanvasScaler>();
                instance.transform.AddMissingComponent<GraphicRaycaster>();

                List<Canvas> screenSpaceOverlayCanvases = FindObjectsOfType<Canvas>(true).Where(canvas => canvas.renderMode == RenderMode.ScreenSpaceOverlay).ToList();
                screenSpaceOverlayCanvases.Remove(instance.GetComponent<Canvas>());
                screenSpaceOverlayCanvases.ForEach(canvas => canvas.transform.AddMissingComponent<SubordinateSorter_OCS>());
                List<Canvas> rootOverlayCanvases = screenSpaceOverlayCanvases.Where(canvas => canvas.isRootCanvas).ToList();
                List<SubordinateSorter_OCS> newSubordinateSorters = rootOverlayCanvases.Select(canvas => canvas.GetComponent<SubordinateSorter_OCS>()).ToList();
                instance.SubordinateSorters = newSubordinateSorters.OrderByTemplate(instance.SubordinateSorters);

                instance.SubordinateSorters.ForEach(sorter => sorter.editorPart.GetSubordinateSortersCascade());
                instance.SubordinateSorters.ForEach(sorter => sorter.editorPart.SetSuperiorCascade(instance));

                AfterSystemUpdated?.Invoke();
            }

            void SetSortOrders()
            {
                if (ApplicationIsPlaying(nameof(SetSortOrders))) return;

                int sortOrder = 0;
                for (int i = instance.SubordinateSorters.Count - 1; i >= 0; i--)
                {
                    SubordinateSorter_OCS currentSorter = instance.SubordinateSorters[i];
                    if (currentSorter == null)
                    {
                        instance.SubordinateSorters.RemoveAt(i);
                        i++;
                        continue;
                    }
                    sortOrder = currentSorter.SetSortOrderCascade(sortOrder);
                }
            }

            void InitialSort()
            {
                if (ApplicationIsPlaying(nameof(InitialSort))) return;

                instance.SubordinateSorters = instance.SubordinateSorters.OrderBy(sorter => (sorter).Canvas.sortingOrder).ToList();
                instance.SubordinateSorters.Reverse();
            }

            public void Subscribe_AfterSystemUpdated(UnityAction method)
            {
                if (ApplicationIsPlaying(nameof(Subscribe_AfterSystemUpdated))) return;

                UnityEventTools.AddPersistentListener(AfterSystemUpdated, method);
                int lastIndex = AfterSystemUpdated.GetPersistentEventCount() - 1;
                AfterSystemUpdated.SetPersistentListenerState(lastIndex, UnityEventCallState.EditorAndRuntime);
            }

            public void Subscribe_BeforeSystemUpdated(UnityAction method)
            {
                if (ApplicationIsPlaying(nameof(Subscribe_BeforeSystemUpdated))) return;

                UnityEventTools.AddPersistentListener(BeforeSystemUpdated, method);
                int lastIndex = BeforeSystemUpdated.GetPersistentEventCount() - 1;
                BeforeSystemUpdated.SetPersistentListenerState(lastIndex, UnityEventCallState.EditorAndRuntime);
            }

            public void Unsubscribe_AfterSystemUpdated(UnityAction method)
            {
                if (ApplicationIsPlaying(nameof(Unsubscribe_AfterSystemUpdated))) return;

                UnityEventTools.RemovePersistentListener(AfterSystemUpdated, method);
            }

            public void Unsubscribe_BeforeSystemUpdated(UnityAction method)
            {
                if (ApplicationIsPlaying(nameof(Unsubscribe_BeforeSystemUpdated))) return;

                UnityEventTools.RemovePersistentListener(BeforeSystemUpdated, method);
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

            public EditorPart(MasterSorter_OCS instance)
            {
                this.instance = instance;
                Reset();
            }
        }
    }
}
#endif
