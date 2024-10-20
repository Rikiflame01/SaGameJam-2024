using System.Collections;
using UnityEngine;

public class MentorMistakeHandler : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject damagedObject;
    public float damagedDuration = 2f;
    public float hoverSpeed = 2f;
    public float hoverAmount = 0.5f;
    private Vector3 originalPosition;
    private bool isDamaged = false;

    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        EventsManager.StartListening("ResetButtons", TakeDamage);
        originalPosition = originalObject.transform.position;
        originalObject.SetActive(true);
        damagedObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventsManager.StopListening("ResetButtons", TakeDamage);
    }

    void Update()
    {
        if (!isDamaged && playerMovement.endOfLevel == false)
        {
            HoverObject();
        }
    }

    public void TakeDamage()
    {
        if (!isDamaged)
        {
            StartCoroutine(SwitchToDamagedObject());
        }
    }

    IEnumerator SwitchToDamagedObject()
    {
        isDamaged = true;
        originalObject.SetActive(false);
        damagedObject.SetActive(true);
        yield return new WaitForSeconds(damagedDuration);
        damagedObject.SetActive(false);
        originalObject.SetActive(true);
        isDamaged = false;
    }

    void HoverObject()
    {
        float newZ = originalPosition.z + Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;
        originalObject.transform.position = new Vector3(originalPosition.x, originalPosition.y, newZ);
    }
}
