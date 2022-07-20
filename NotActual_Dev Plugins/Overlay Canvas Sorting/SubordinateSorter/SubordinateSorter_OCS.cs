using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting
{
    public partial class SubordinateSorter_OCS : BaseSorter_OCS
    {
        internal Canvas Canvas => canvas;
        [SerializeField, HideInInspector] Canvas canvas;
        [SerializeField, HideInInspector] BaseSorter_OCS superior;
        [SerializeField, HideInInspector] MasterSorter_OCS master;

        public void MasterSetSortOrders()
        {
            master.SetSortOrders();
        }

        public void PutOnTOP()
        {
            if (superior == null) return;
            superior.SubordinateSorters.Remove(this);
            superior.SubordinateSorters.Insert(0, this);
        }

        public void PutAtTheBOTTOM()
        {
            if (superior == null) return;
            superior.SubordinateSorters.Remove(this);
            superior.SubordinateSorters.Add(this);
        }

        public void PutAtSpecificIndex(int index)
        {
            if (superior == null) return;
            superior.SubordinateSorters.Remove(this);
            if (index < 0 || index > superior.SubordinateSorters.Count) index = superior.SubordinateSorters.Count;
            superior.SubordinateSorters.Insert(index, this);
        }

        public bool TryChangeSuperior(BaseSorter_OCS targetSuperior, int index = -1)
        {
            if (!targetSuperior.IsAllowedToBeSuperiorOf(this)) return false;
            if (superior != null) superior.SubordinateSorters.Remove(this);
            if (index < 0 || index > targetSuperior.SubordinateSorters.Count) index = targetSuperior.SubordinateSorters.Count;
            targetSuperior.SubordinateSorters.Insert(index, this);
            superior = targetSuperior;
            return true;
        }

        public int GetSubordinateIndex()
        {
            int index = -1;
            if (superior != null) index = superior.SubordinateSorters.IndexOf(this);
            return index;
        }

        int SetSortOrder(int sortOrder)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = sortOrder;
            sortOrder++;
            return sortOrder;
        }

        internal int SetSortOrderCascade(int sortOrder)
        {
            sortOrder = SetSortOrder(sortOrder);
            for (int i = SubordinateSorters.Count - 1; i >= 0; i--)
            {
                SubordinateSorter_OCS currentSorter = SubordinateSorters[i];
                if (currentSorter == null)
                {
                    SubordinateSorters.RemoveAt(i);
                    i++;
                    continue;
                }
                sortOrder = currentSorter.SetSortOrderCascade(sortOrder);
            }
            return sortOrder;
        }
    } 
}
