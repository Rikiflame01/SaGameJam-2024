using UnityEngine;

public class ButtonPressIndicator : MonoBehaviour
{
    private bool isInTriggerZone = false;
    private bool buttonPressed = false;
    private CanvasGroup canvasGroup;

    public enum ColorState { Blue, Red, Green, Yellow }
    public ColorState currentColor;

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
            EventsManager.TriggerEvent("ResetPlayer");
            canvasGroup.alpha = 0;
            isInTriggerZone = false;
        }
    }

    private void Update()
    {
        if (!isInTriggerZone && !buttonPressed)
        {
            if (Input.GetKeyDown(KeyCode.I))
            { // Blue -> Up Arrow
                buttonPressed = true;
                ResetPlayer();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            { // Red -> Down Arrow
                buttonPressed = true;
                ResetPlayer();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            { // Green -> Right Arrow
                buttonPressed = true;
                ResetPlayer();
            }
            else if (Input.GetKeyDown(KeyCode.J))
            { // Yellow -> Left Arrow
                buttonPressed = true;
                ResetPlayer();
            }
        }
        if (isInTriggerZone && !buttonPressed)
        {
            switch (currentColor)
            {
                case ColorState.Blue:
                    if (Input.GetKeyDown(KeyCode.I)) // Blue -> Up Arrow
                        buttonPressed = true;
                    break;
                case ColorState.Red:
                    if (Input.GetKeyDown(KeyCode.K)) // Red -> Down Arrow
                        buttonPressed = true;
                    break;
                case ColorState.Green:
                    if (Input.GetKeyDown(KeyCode.L)) // Green -> Right Arrow
                        buttonPressed = true;
                    break;
                case ColorState.Yellow:
                    if (Input.GetKeyDown(KeyCode.J)) // Yellow -> Left Arrow
                        buttonPressed = true;
                    break;
                default:
                    break;
            }

            if (buttonPressed)
            {
                HandleButtonPress();
            }
        }
    }

    private void ResetPlayer()
    {
        EventsManager.TriggerEvent("ResetPlayer");
        buttonPressed = false;
    }

    private void HandleButtonPress()
    {
        Debug.Log("Correct button pressed for color: " + currentColor);
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}
