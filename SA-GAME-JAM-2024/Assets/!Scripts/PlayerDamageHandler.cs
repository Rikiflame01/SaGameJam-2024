using System.Collections;
using UnityEngine;

public class CharacterDamageHandler : MonoBehaviour
{
    public GameObject originalObject;

    public GameObject damagedObject;

    public float damagedDuration = 2f;

    private bool isDamaged = false;

    void Start()
    {
        EventsManager.StartListening("ResetButtons", TakeDamage);

        originalObject.SetActive(true);
        damagedObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventsManager.StopListening("ResetButtons", TakeDamage);
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
}
