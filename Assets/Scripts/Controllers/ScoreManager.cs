using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [Header("UI Reference")]
    public Image progressBar; // Image dengan type Filled

    [Header("Scoring")]
    public float maxScore = 100f; // Maksimal score untuk full bar
    private float currentScore = 0f;

    [Header("Events")]
    public UnityEvent onProgressFull; // Event yang akan terpanggil ketika bar penuh

    void Start()
    {
        UpdateProgressBar();
    }

    // Tambahkan skor
    public void AddScore(float amount)
    {
        currentScore += amount;

        // Clamp agar tidak lebih dari maxScore
        currentScore = Mathf.Clamp(currentScore, 0f, maxScore);

        UpdateProgressBar();

        Debug.Log("Score added: " + amount + " | Total Score: " + currentScore);

        // Jika penuh, panggil event
        if (currentScore >= maxScore)
        {
            TriggerReward();
        }
    }

    // Update UI progress bar
    private void UpdateProgressBar()
    {
        float fillAmount = currentScore / maxScore;
        progressBar.fillAmount = fillAmount;
    }

    // Reset score
    public void ResetScore()
    {
        currentScore = 0f;
        UpdateProgressBar();
        Debug.Log("Score reset.");
    }

    // Fungsi pemicu reward
    private void TriggerReward()
    {
        Debug.Log("Progress bar full! Reward triggered.");
        
        onProgressFull?.Invoke(); // Panggil event jika sudah di-assign
    }

    // Ambil skor saat ini
    public float GetCurrentScore()
    {
        return currentScore;
    }
}
