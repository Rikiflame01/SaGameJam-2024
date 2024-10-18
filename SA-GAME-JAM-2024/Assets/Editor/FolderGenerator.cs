#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class FolderGenerator : EditorWindow
{
    [MenuItem("Tools/Generate Initial Folders")]
    public static void ShowWindow()
    {
        GetWindow<FolderGenerator>("Folder Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Generate Initial Folders for Unity 3D Game", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate Folders"))
        {
            GenerateFolders();
        }
    }

    private static void GenerateFolders()
    {
        string[] folderNames = {
            "Scenes",
            "Scripts",
            "Prefabs",
            "Materials",
            "Textures",
            "Models",
            "Audio",
            "Animations",
            "Resources",
            "Editor"
        };

        foreach (string folderName in folderNames)
        {
            CreateFolder("Assets", folderName);
        }

        AssetDatabase.Refresh();
        Debug.Log("Initial folders created successfully.");
    }

    private static void CreateFolder(string parent, string newFolder)
    {
        string folderPath = Path.Combine(parent, newFolder);

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder(parent, newFolder);
        }
        else
        {
            Debug.LogWarning($"Folder '{newFolder}' already exists.");
        }
    }
}
#endif
