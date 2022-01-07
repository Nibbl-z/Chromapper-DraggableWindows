using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;

namespace DraggableWindows
{

    [Plugin("Draggable Windows")]
    public class Plugin
    {
        public bool shiftEnabled = false;
        [Init]
        [Obsolete]
        private void Init()
        {
            Sprite buttonSprite;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ChromapperPlugin.ShiftToggleButton.png"))
            {
                var len = (int)stream.Length;
                var bytes = new byte[len];
                stream.Read(bytes, 0, len);

                Texture2D texture2D = new Texture2D(512, 512);
                texture2D.LoadImage(bytes);

                buttonSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0), 100.0f, 0, SpriteMeshType.Tight);
            }
            SceneManager.sceneLoaded += SceneLoaded;
            ExtensionButton shiftToggleButton = ExtensionButtons.AddButton(buttonSprite, "Toggle Shift+Drag when dragging windows", ToggleShiftDrag);
        }

        [Obsolete]
        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 3)
            {
                GameObject mapEditorUI = GameObject.Find("MapEditorUI");

                List<Transform> children = new List<Transform>(mapEditorUI.GetComponentsInChildren<Transform>());
                foreach (var child in children)
                {
                    if (child.GetComponent<CanvasScaler>() != null)
                    {
                        switch (child.name)
                        {
                            case "Node Editor Canvas":
                                AddDraggable(child.Find("Node Editor").gameObject, child.gameObject);
                                break;
                            case "Strobe Generator Canvas":
                                AddDraggable(child.Find("Strobe Generator").gameObject, child.gameObject);
                                break;
                            case "Chroma Colour Selector":
                                AddDraggable(child.Find("Chroma Colour Selector").Find("Picker 2.0").gameObject, child.gameObject);
                                break;
                            case "BPM Tapper Canvas":
                                AddDraggable(child.Find("BPM Tapper").Find("Background Panel").gameObject, child.gameObject);
                                break;
                        }
                    }
                }
            }
        }
        public void ToggleShiftDrag()
        {
            shiftEnabled = !shiftEnabled;
            if (shiftEnabled == true)
            {
                PersistentUI.Instance.DisplayMessage("Shift + Drag has been enabled", PersistentUI.DisplayMessageType.Bottom);
            }
            else
            {
                PersistentUI.Instance.DisplayMessage("Shift + Drag has been disabled", PersistentUI.DisplayMessageType.Bottom);
            }

            JSONObject saveJson = new JSONObject();
            saveJson.Add("ShiftEnabled", shiftEnabled);

            string path = Application.persistentDataPath + "/DraggableWindowsSettings.json";
            File.WriteAllText(path, saveJson.ToString());
        }

        public void AddDraggable(GameObject ui, GameObject parentUi)
        {
            Debug.Log("creating draggable");
            ui.AddComponent<DragWindow>();
            ui.GetComponent<DragWindow>().canvas = parentUi.GetComponent<Canvas>();
        }
    }
}
