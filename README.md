Overlay Canvas Sorting - Version 1
For video tutorials - visit notActual_dev on YouTube.

HOW TO DOWNLOAD

1. Download this repository.
2. Put "NotActual_Dev Plugins" folder from this repository somewhere in your "Assets" folder.
3. If you have previously downloaded some of my plugins and you already have the "NotActual_Dev Plugins" folder,
just open the folder from this repository and copy only the missing folders (e.g. "Overlay Canvas Sorting" folder)
to your existing "NotActual_Dev Plugins" folder.

PROBLEMS AFTER DOWNLOADING (if some errors appear in Unity editor console after you downloaded this)

Problem 1.: error CS0111, CS0579, CS0102, CS8646
Solution 1.: Check if any folder or script is duplicated. E.g. you downloaded "NotActual_Dev Plugins" folder twice.
It can happen when you download more than one plugin from my repositories because they share some global assemblies
(e.g. you can have two folders "Extension methods"). You better leave a folder with higher Version (it's shown in .txt file in a folder).
                 
HOW TO USE (in editor):

1. Create an empty gameObject and call it e.g. "Sorting Master".
2. Add "MasterSorter_OCS" component to it.
3. It will automatically add "SubordinateSorter_OCS" component to all canvases that have RenderMode == ScreenSpaceOverlay.
4. Now you can set sort orders of overlay canvases using "MasterSorter_OCS" and "SubordinateSorter_OCS" components.
5. You have a list of subordinate sorters in there and you can rearange them from there.
6. By default, a sorter is controlling other sorters that are his shallowest children.
7. But you can override it by adding "OverrideSuperior_OCS" component. There you choose a different sorter to be its superior.
8. If you destroy "MasterSorter_OCS" it will automatically destroy all system components.
  
9. GIVE YOURSELF A TRY in the example scenes.
  
HOW TO USE (in runtime):
1. If you want to move a canvas to the top of its group, you can use SubordinateSorter_OCS.PutOnTOP() and then SubordinateSorter_OCS.MasterSetSortOrders()
or MasterSorter_OCS.SetSortOrders().
2. If you want to move to the bottom, use SubordinateSorter_OCS.PutAtTheBOTTOM() and then one of those two above.
3. If you want to move it to a specific index within its group, use SubordinateSorter_OCS.PutAtSpecificIndex(int index) and then one of those two above.
4. If you want to get index of a sorter within its group, use SubordinateSorter_OCS.GetSubordinateIndex().
5. If you want to change superior (it does not move a gameObject in a hierarchy),
use SubordinateSorter_OCS.TryChangeSuperior(BaseSorter_OCS targetSuperior, int index = -1).
       
6. GIVE YOURSELF A TRY in the example scenes.
    
