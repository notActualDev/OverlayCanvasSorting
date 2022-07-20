using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public partial class MasterSorter_OCS : BaseSorter_OCS
    {
        void Reset()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) Editor_Reset();
            #endif
        }

        void Update()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) editorPart.Update();
            #endif
        }

        void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) editorPart.OnDestroy();
            #endif
        }
    }
}
