using UnityEngine;
using TMPro;

public class TimerLoop : MonoBehaviour
{
    public int timerDuration = 10;
    private float currentTime;
    public TextMeshProUGUI timerText;
    private bool timerActive = true;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        EventsManager.StartListening("ResetButtons", ResetTimer);
        ResetTimer();
    }

    private void OnDisable()
    {
        EventsManager.StopListening("ResetButtons", ResetTimer);
    }


    private void Update()
    {
        if (timerActive && playerMovement.endOfLevel == false)
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

            if (playerMovement.endOfLevel == false)
            {
                EventsManager.TriggerEvent("ResetPlayer");
                ResetTimer();
            }
        }
    }

    private void ResetTimer()
    {
        currentTime = timerDuration; 
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }
}
