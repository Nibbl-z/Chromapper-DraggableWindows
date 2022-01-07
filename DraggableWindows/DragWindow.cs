using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using SimpleJSON;

public class DragWindow : MonoBehaviour, IDragHandler
{

    public RectTransform dragRectTransform;
    public Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform = gameObject.GetComponent<RectTransform>();
        if (UseShift() == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
        if (UseShift() == false)
        {
            dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    private bool UseShift()
    {
        string path = Application.persistentDataPath + "/DraggableWindowsSettings.json";
        string settingsJsonFile = File.ReadAllText(path);
        JSONObject settingsJson = (JSONObject)JSON.Parse(settingsJsonFile);
        return settingsJson["ShiftEnabled"];
    }
}
