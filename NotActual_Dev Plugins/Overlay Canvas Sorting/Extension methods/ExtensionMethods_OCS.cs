namespace NotActual_Dev.OverlayCanvasSorting
{
    public static class ExtensionMethods_OCS
    {
        public static bool IsAllowedToBeSuperiorOf(this BaseSorter_OCS targetSuperior, SubordinateSorter_OCS subordinate)
        {
            if (targetSuperior == null) return false;
            if (targetSuperior == subordinate) return false;                                                              // can't add a subordinate to itself
            if (subordinate.SubordinateSorters.Contains(targetSuperior as SubordinateSorter_OCS)) return false;           // can't add a subordinate to its own subordinates
            return true;
        }
    } 
}
