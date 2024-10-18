using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

    private Dictionary<string, UnityEventBase> eventDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            eventDictionary = new Dictionary<string, UnityEventBase>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region Generic methods
    public static void StartListening<T>(string eventName, UnityAction<T> listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEventBase thisEvent))
        {
            if (thisEvent is UnityEvent<T> unityEvent)
            {
                unityEvent.AddListener(listener);
            }
        }
        else
        {
            UnityEvent<T> newEvent = new UnityEvent<T>();
            newEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, newEvent);
        }
    }

    public static void StopListening<T>(string eventName, UnityAction<T> listener)
    {
        if (Instance == null) return;

        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEventBase thisEvent))
        {
            if (thisEvent is UnityEvent<T> unityEvent)
            {
                unityEvent.RemoveListener(listener);
            }
        }
    }

    public static void TriggerEvent<T>(string eventName, T parameter)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEventBase thisEvent))
        {
            if (thisEvent is UnityEvent<T> unityEvent)
            {
                unityEvent.Invoke(parameter);
            }
        }
    }
    #endregion

    #region General events (no parameters)
    public static void StartListening(string eventName, UnityAction listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEventBase thisEvent))
        {
            if (thisEvent is UnityEvent unityEvent)
            {
                unityEvent.AddListener(listener);
            }
        }
        else
        {
            UnityEvent newEvent = new UnityEvent();
            newEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, newEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (Instance == null) return;

        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEventBase thisEvent))
        {
            if (thisEvent is UnityEvent unityEvent)
            {
                unityEvent.RemoveListener(listener);
            }
        }
    }

    public static void TriggerEvent(string eventName)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEventBase thisEvent))
        {
            if (thisEvent is UnityEvent unityEvent)
            {
                unityEvent.Invoke();
            }
        }
    }
    #endregion
}

//Example usage

//public class GenericEventListener : MonoBehaviour
//{
//    void OnEnable()
//    {
//        Start listening for the "ScoreUpdated" event when this script is enabled (Which creates the new event in the dictionary)
//        EventsManager.StartListening<int>("ScoreUpdated", OnScoreUpdated);
//        EventsManager.StartListening<string>("PlayerJoined", OnPlayerJoined);
//    }

//    void OnDisable()
//    {
//        Stop listening for the "ScoreUpdated" event when this script is disabled
//        EventsManager.StopListening<int>("ScoreUpdated", OnScoreUpdated);
//        EventsManager.StopListening<string>("PlayerJoined", OnPlayerJoined);
//    }

//    void OnScoreUpdated(int score)
//    {
//        Debug.Log("Score updated: " + score);
//    }

//    void OnPlayerJoined(string playerName)
//    {
//        Debug.Log("Player joined: " + playerName);
//    }
//}


//public class GenericEventTrigger : MonoBehaviour
//{
//    void Update()
//    {
//        Trigger the "ScoreUpdated" event with a random value when the player presses the S key
//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            int newScore = Random.Range(0, 100);
//            EventsManager.TriggerEvent("ScoreUpdated", newScore);
//        }

//        Trigger the "PlayerJoined" event with a player name when the player presses the P key
//        if (Input.GetKeyDown(KeyCode.P))
//        {
//            string playerName = "Player_" + Random.Range(1, 100);
//            EventsManager.TriggerEvent("PlayerJoined", playerName);
//        }
//    }
//}