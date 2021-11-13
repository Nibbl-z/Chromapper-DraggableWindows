using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DraggableWindows
{
    [Plugin("Draggable Windows")]
    public class Plugin
    {

        [Init]
        [Obsolete]
        private void Init()
        {
            SceneManager.sceneLoaded += SceneLoaded;         
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

        public void AddDraggable(GameObject ui, GameObject parentUi)
        {
            Debug.Log("creating draggable");
            ui.AddComponent<DragWindow>();
            ui.GetComponent<DragWindow>().canvas = parentUi;
        }
    }
}
