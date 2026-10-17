using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Notifikasi")]
    public GameObject notifPanel;
    public TMP_Text notifText;
    [TextArea]
    public string message = "AR Praktikum P05/P07\nTarget tersentuh. Praktikum berhasil.";
    public float showDuration = 3f;

    void Start()
    {
        if (notifPanel != null) notifPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        HandleContact(other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleContact(collision.gameObject);
    }

    private void HandleContact(GameObject touchedObject)
    {
        if (touchedObject.CompareTag("Target"))
        {
            Debug.Log("Target tersentuh. Praktikum berhasil.");
            touchedObject.SetActive(false);
            ShowNotification();
        }
        else if (touchedObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle tersentuh: " + touchedObject.name);
        }
    }

    void ShowNotification()
    {
        if (notifPanel == null) return;
        if (notifText != null) notifText.text = message;
        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        notifPanel.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        notifPanel.SetActive(false);
    }
}