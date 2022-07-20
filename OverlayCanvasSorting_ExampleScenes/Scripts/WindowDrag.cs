using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NotActual_Dev.OverlayCanvasSorting.HowToUse
{
    public class WindowDrag : MonoBehaviour
    {
        Camera cam;
        Vector2 distanceFromCenter;

        void Awake()
        {
            cam = Camera.main;
        }

        public void InitializeDrag(BaseEventData baseEventData)
        {
            PointerEventData pointer = baseEventData as PointerEventData;
            distanceFromCenter = pointer.position - (Vector2)transform.position;
        }

        public void Drag(BaseEventData baseEventData)
        {
            PointerEventData pointer = baseEventData as PointerEventData;
            transform.SetPositionAndRotation(pointer.position - distanceFromCenter, Quaternion.identity);
        }
    }

}