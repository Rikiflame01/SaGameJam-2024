using UnityEngine;
using TMPro;

public class TimerLoop : MonoBehaviour
{
    public int timerDuration = 10; // Set in seconds
    private float currentTime; // Use float for accuracy
    public TextMeshProUGUI timerText; // Assign the TextMeshPro component in the Inspector
    private bool timerActive = true;

    private void Start()
    {
        ResetTimer(); // Initialize the timer at the start
    }

    private void Update()
    {
        if (timerActive)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        if (currentTime > 0)
        {
            // Decrease the time by the deltaTime each frame
            currentTime -= Time.deltaTime;

            // Update the UI text (rounded to the nearest whole number)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
        }
        else
        {
            // Timer hit zero, trigger the event
            EventsManager.TriggerEvent("ResetPlayer");

            // Reset the timer for the next loop
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        currentTime = timerDuration; // Reset the time to the original duration
        timerText.text = Mathf.CeilToInt(currentTime).ToString(); // Update UI
    }
}
