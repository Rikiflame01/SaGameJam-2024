using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtons : MonoBehaviour
{
    public void RestartScene()
    {
        DestroyDontDestroyOnLoadObjects();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            //DestroyDontDestroyOnLoadObjects(); With great power...
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("No scene name provided!");
        }
    }

    public void ExitToDesktop()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void DestroyDontDestroyOnLoadObjects()
    {
        var dontDestroyOnLoadScene = SceneManager.GetSceneByName("DontDestroyOnLoad");

        if (dontDestroyOnLoadScene.IsValid())
        {
            GameObject[] rootObjects = dontDestroyOnLoadScene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                Destroy(obj);
            }
        }
    }
}
