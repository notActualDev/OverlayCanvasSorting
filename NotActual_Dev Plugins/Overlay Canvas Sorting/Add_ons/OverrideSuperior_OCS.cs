#if UNITY_EDITOR
using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting
{
    [DisallowMultipleComponent]
    public class OverrideSuperior_OCS : MonoBehaviour, IEditorDisposable_OCS
    {
        void Awake()
        {
            if (Application.isPlaying)
            {
                DestroyImmediate(this);
                return;
            }
        }

        [SerializeField] BaseSorter_OCS destinationSuperior;
        [SerializeField, HideInInspector] int index;

        [SerializeField, HideInInspector] SubordinateSorter_OCS subordinate;
        [SerializeField, HideInInspector] MasterSorter_OCS master;

        void Reset()
        {
            subordinate = GetComponent<SubordinateSorter_OCS>();
            if (subordinate == null)
            {
                DestroyImmediate(this);
                return;
            }
            index = -1;
            master = FindObjectOfType<MasterSorter_OCS>(true);
            master.editorPart.Unsubscribe_BeforeSystemUpdated(OnBeforeSystemUpdated);
            master.editorPart.Unsubscribe_AfterSystemUpdated(OnAfterSystemUpdated);
            master.editorPart.Subscribe_BeforeSystemUpdated(OnBeforeSystemUpdated);
            master.editorPart.Subscribe_AfterSystemUpdated(OnAfterSystemUpdated);
        }

        void OnDestroy()
        {
            if (Application.isPlaying) return;
            if (Application.isPlaying) return;
            if (master != null) master.editorPart.Unsubscribe_BeforeSystemUpdated(OnBeforeSystemUpdated);
            if (master != null) master.editorPart.Unsubscribe_AfterSystemUpdated(OnAfterSystemUpdated);
        }

        void OnBeforeSystemUpdated()
        {
            if (Application.isPlaying) return;
            if (subordinate == null) return;
            if (destinationSuperior == null) return;
            index = subordinate.GetSubordinateIndex();
        }

        void OnAfterSystemUpdated()
        {
            if (Application.isPlaying) return;
            if (destinationSuperior == null) return;
            bool success = subordinate.TryChangeSuperior(destinationSuperior, index);
            if (!success)
            {
                destinationSuperior = null;
            }
        }

        void IEditorDisposable_OCS.DisposeIfRedundant()
        {
            if (subordinate == null) DestroyImmediate(this);
        }

        void IEditorDisposable_OCS.Dispose()
        {
            DestroyImmediate(this);
        }
    }
}

#endif