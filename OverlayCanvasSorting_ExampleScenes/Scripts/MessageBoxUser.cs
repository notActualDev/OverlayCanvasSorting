using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting.HowToUse
{
    public class MessageBoxUser : MonoBehaviour
    {
        [SerializeField] string itemName;
        [SerializeField] [TextArea] string itemDescription;

        [SerializeField] MessageBox messageBox;

        public void OpenMessageBox()
        {
            messageBox.Open(itemName, itemDescription);
        }

        public void CloseMessageBox()
        {
            messageBox.Close();
        }
    } 
}
