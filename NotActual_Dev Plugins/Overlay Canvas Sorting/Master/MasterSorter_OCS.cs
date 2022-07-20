namespace NotActual_Dev.OverlayCanvasSorting
{
    public partial class MasterSorter_OCS : BaseSorter_OCS
    {
        public void SetSortOrders()
        {
            int sortOrder = 0;
            for (int i = SubordinateSorters.Count - 1; i >= 0; i--)
            {
                SubordinateSorter_OCS currentSubordinate = SubordinateSorters[i];
                if (currentSubordinate == null)
                {
                    SubordinateSorters.RemoveAt(i);
                    i++;
                    continue;
                }
                sortOrder = currentSubordinate.SetSortOrderCascade(sortOrder);
            }
        }
    } 
}

