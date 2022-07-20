using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public partial class SubordinateSorter_OCS : BaseSorter_OCS
    {
        void Reset()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying) Editor_Reset();
            #endif
        }
    }
}
