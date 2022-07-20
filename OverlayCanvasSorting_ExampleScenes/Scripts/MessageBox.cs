using TMPro;
using UnityEngine;

namespace NotActual_Dev.OverlayCanvasSorting.HowToUse
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI itemDescription;

        public void Open(string itemName, string itemDescription)
        {
            gameObject.SetActive(true);
            this.itemName.text = itemName;
            this.itemDescription.text = itemDescription;
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    } 
}
