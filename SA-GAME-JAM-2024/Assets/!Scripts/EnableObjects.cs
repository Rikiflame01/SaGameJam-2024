using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;

    private void Start()
    {
        EventsManager.StartListening("EnableLevelButtons", EnableGameObjects);
    }

    private void OnDisable()
    {
        EventsManager.StopListening("EnableLevelButtons", EnableGameObjects);
    }

    public void EnableGameObjects()
    {
        Debug.Log("Enabling objects");
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
        DisableObjects();
    }

    public void DisableObjects()
    {
        if (objectsToDisable == null) { return; }
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
