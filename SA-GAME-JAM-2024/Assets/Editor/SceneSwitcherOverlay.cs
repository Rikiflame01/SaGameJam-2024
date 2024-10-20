#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneSwitcherOverlay
{
    static SceneSwitcherOverlay()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();

        GUILayout.BeginArea(new Rect(10, 10, 200, 1000));
        GUILayout.Label("Scene Switcher", EditorStyles.boldLabel);

        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);

            if (GUILayout.Button(sceneName))
            {
                LoadSceneByIndex(i);
            }
        }

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    private static void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(sceneIndex));
            Debug.Log("Scene loaded: " + SceneUtility.GetScenePathByBuildIndex(sceneIndex));
        }
        else
        {
            Debug.LogWarning("Invalid scene index. Ensure the scene is added to Build Settings.");
        }
    }
}
#endif
