using UnityEngine;
using System.Collections.Generic;

public class ButtonPressIndicator : MonoBehaviour
{
    private bool isInTriggerZone = false;
    private bool buttonPressed = false;
    private CanvasGroup canvasGroup;

    public enum ColorState { Blue, Red, Green, Yellow }
    public ColorState currentColor;

    private Dictionary<ColorState, KeyCode> correctKeys = new Dictionary<ColorState, KeyCode>()
    {
        { ColorState.Blue, KeyCode.I },
        { ColorState.Red, KeyCode.K },
        { ColorState.Green, KeyCode.L },
        { ColorState.Yellow, KeyCode.J }
    };

    private Dictionary<ColorState, List<KeyCode>> wrongKeys = new Dictionary<ColorState, List<KeyCode>>()
    {
        { ColorState.Blue, new List<KeyCode> { KeyCode.K, KeyCode.L, KeyCode.J } },
        { ColorState.Red, new List<KeyCode> { KeyCode.I, KeyCode.L, KeyCode.J } },
        { ColorState.Green, new List<KeyCode> { KeyCode.I, KeyCode.K, KeyCode.J } },
        { ColorState.Yellow, new List<KeyCode> { KeyCode.I, KeyCode.K, KeyCode.L } }
    };

    private void OnEnable()
    {
        EventsManager.StartListening("ResetButtons", HandleReset);
    }

    private void OnDisable()
    {
        EventsManager.StopListening("ResetButtons", HandleReset);
    }

    private void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        buttonPressed = false;
        isInTriggerZone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasGroup.alpha = 1;
            isInTriggerZone = true;
            buttonPressed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !buttonPressed)
        {
            ResetPlayer();
            canvasGroup.alpha = 0;
            isInTriggerZone = false;
            buttonPressed = false;
        }
    }

    private void Update()
    {
        if (isInTriggerZone && !buttonPressed)
        {
            if (Input.GetKeyDown(correctKeys[currentColor]))
            {
                buttonPressed = true;
                HandleButtonPress();
                SoundManager.instance.PlaySFX("correct1");
            }
            else
            {
                CheckForWrongKeyPress();
            }
        }
    }

    private void CheckForWrongKeyPress()
    {
        foreach (KeyCode wrongKey in wrongKeys[currentColor])
        {
            if (Input.GetKeyDown(wrongKey))
            {
                buttonPressed = true;
                Debug.Log("Wrong key pressed!");
                isInTriggerZone = false;
                ResetPlayer();
                SoundManager.instance.PlaySFX("WrongButton");
                break;
            }
        }
    }

    private void ResetPlayer()
    {
        SoundManager.instance.PlaySFX("WrongButton");
        EventsManager.TriggerEvent("ResetPlayer");
        buttonPressed = false;
    }

    private void HandleButtonPress()
    {
        canvasGroup.alpha = 0;
    }

    void HandleReset()
    {
        SoundManager.instance.PlaySFX("WrongButton");
        canvasGroup.alpha = 0;
        buttonPressed = false;
        isInTriggerZone = false;
    }
}
