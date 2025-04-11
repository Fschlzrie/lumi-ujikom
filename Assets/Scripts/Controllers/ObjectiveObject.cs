using UnityEngine;

public class ObjectiveObject : MonoBehaviour
{
    public string objectiveID;
    public string objectiveMessage;
    public int objectiveTargetCount = 1; // Default 1 kalau cuma 1 item

    private GameManager gameManager;

    private void Start()
    {
        // Cari GameManager di dalam scene
        gameManager = FindObjectOfType<GameManager>();

        // Pastikan GameManager ditemukan
        if (gameManager == null)
        {
            Debug.LogError("GameManager tidak ditemukan di scene!");
        }
    }

    // Fungsi untuk trigger UnityEvent (bisa dipanggil di Inspector)
    public void TriggerObjective()
    {
        if (gameManager != null)
        {
            gameManager.ShowObjectiveWithCount(objectiveID, objectiveMessage, objectiveTargetCount);
        }
    }

    public void AddObjectiveProgress()
    {
        if (gameManager != null)
        {
            gameManager.AddObjectiveProgress(objectiveID);
        }
    }
}
