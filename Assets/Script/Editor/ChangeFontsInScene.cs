using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class ChangeFontsInScene : EditorWindow
{


    [SerializeField] private TMP_FontAsset font;
    

    private void OnGUI()
    {

        EditorGUILayout.BeginVertical();
        //TMP_FontAsset newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("ParentObject", newFont, typeof(GameObject), true);


        if (GUILayout.Button("Change Fonts"))
        {
            if (font != null)
            {
                List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
                foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
                {
                    texts.AddRange(root.GetComponentsInChildren<TextMeshProUGUI>(true));
                }
                foreach (TextMeshProUGUI text in texts)
                {
                    text.font = this.font;
                }
            }
        }

        EditorGUILayout.EndVertical();


    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGui;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGui;
    }


    private void OnSceneGui(SceneView obj)
    {

    }
    [MenuItem("Tools/TextFont Changer")]
    private static void OpenWindow()
    {
        ChangeFontsInScene window = new ChangeFontsInScene();
        window.titleContent = new GUIContent("Change Font in Scene");
        window.Show();
    }
}


