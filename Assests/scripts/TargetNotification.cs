using System.Collections;
using UnityEngine;
using TMPro;

public class TargetNotification : MonoBehaviour
{
    public GameObject notifPanel;
    public TMP_Text notifText;
    public string targetName = "Target";
    [TextArea]
    public string message = "AR Praktikum P05/P07\nTarget tersentuh. Praktikum berhasil.";
    public float showDuration = 3f;

    void Start()
    {
        if (notifPanel != null) notifPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == targetName) Show();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == targetName) Show();
    }

    void Show()
    {
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