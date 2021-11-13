using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragWindow : MonoBehaviour, IDragHandler
{

    public RectTransform dragRectTransform;
    public GameObject canvas;
    public CanvasScaler canvasScaler;
    public float scaleFactor;

    

    public void OnDrag(PointerEventData eventData)
    {
        canvasScaler = canvas.GetComponent<CanvasScaler>();
        scaleFactor = Screen.width / canvasScaler.referenceResolution.x; // It's not quite perfect but it's the best I could do
        dragRectTransform = gameObject.GetComponent<RectTransform>();
        
        dragRectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }
}
