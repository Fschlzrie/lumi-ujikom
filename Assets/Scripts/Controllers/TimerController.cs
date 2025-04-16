using UnityEngine;
using UnityEngine.UI;
using TMPro; // Jika pakai TextMeshPro
using UnityEngine.Events;

public class TimerController : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 10f;
    private float currentTime;
    private bool isTimerRunning = false;

    [Header("UI Components")]
    public TextMeshProUGUI timerText; // Ganti ke Text kalau pakai UI biasa

    [Header("Timer Events")]
    public UnityEvent OnTimerExpired;

    void Update()
    {
        if (!isTimerRunning) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0)
        {
            isTimerRunning = false;
            currentTime = 0;
            UpdateTimerUI();
            OnTimerExpired?.Invoke(); // Invoke event saat waktu habis
        }
    }

    public void StartTimer()
    {
        currentTime = timeLimit;
        isTimerRunning = true;
        timerText.gameObject.SetActive(true);
        UpdateTimerUI();
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        currentTime = 0;
        timerText.gameObject.SetActive(false);
    }

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(currentTime).ToString(); // Bisa tambahin format waktu kalau mau
    }
}
