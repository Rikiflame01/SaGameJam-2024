using UnityEngine;
using TMPro;

public class TimerLoop : MonoBehaviour
{
    public int timerDuration = 10;
    private float currentTime;
    public TextMeshProUGUI timerText;
    private bool timerActive = true;

    private void Start()
    {
        ResetTimer();
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
            currentTime -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
        }
        else
        {
            EventsManager.TriggerEvent("ResetPlayer");

            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        currentTime = timerDuration; 
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }
}
