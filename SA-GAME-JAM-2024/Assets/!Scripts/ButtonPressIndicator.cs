using UnityEngine;

public class ButtonPressIndicator : MonoBehaviour
{
    private bool isInTriggerZone = false;
    private bool buttonPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            buttonPressed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!buttonPressed)
            {
                EventsManager.TriggerEvent("ResetPlayer");
            }
            isInTriggerZone = false;
        }
    }

    private void Update()
    {
        if (isInTriggerZone && !buttonPressed)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttonPressed = true;

                HandleButtonPress();
            }
        }
    }

    private void HandleButtonPress()
    {
        Debug.Log("Button pressed successfully.");
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}
