using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeOverlayToCameraCanvas : EditorWindow
{
    [MenuItem("Tools/Misc/Change Overlay to Camera canvas")]
    public static void ShowWindow()
    {
        var window = GetWindow<ChangeOverlayToCameraCanvas>(); window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Change all the canvas set to overlay to camera and put the main camera as the used camera");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start"))
        {
            Canvas[] canvas;
            canvas = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            foreach (Canvas c in canvas) 
            {
                if (c.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    c.renderMode = RenderMode.ScreenSpaceCamera;
                    c.worldCamera = Camera.main;
                    c.planeDistance = 1;
                    
                }
            }
        }
        GUILayout.EndHorizontal();
    }
}
