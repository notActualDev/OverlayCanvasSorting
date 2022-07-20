using NotActual_Dev.CustomInspectorLists;
using System.Collections.Generic;
using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting
{
    public abstract class BaseSorter_OCS : MonoBehaviour
    {
        internal List<SubordinateSorter_OCS> SubordinateSorters
        {
            get { return inspectorSubordinateSorters.List; }
            set { inspectorSubordinateSorters.List = value; }
        }
        [SerializeField] private protected ReorderableList_InspectorReadonly<SubordinateSorter_OCS> inspectorSubordinateSorters;
    } 
}
