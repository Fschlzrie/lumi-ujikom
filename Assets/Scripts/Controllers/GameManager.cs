using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static bool isNoteOpen = false;
    public GameObject rewardPanel;

    public GameObject lockpickMinigamePrefab;
    private GameObject currentMinigameInstance;

    public Transform uiCanvas;
    public GameObject objectivePrefab;
    public Transform objectiveContainer;

    public GameObject pauseMenuUI; // Tambahkan ini di inspector
    private bool isGamePaused = false;

    private Dictionary<string, ObjectiveProgress> activeObjectives = new Dictionary<string, ObjectiveProgress>();

    [System.Serializable]
    public class ObjectiveProgress
    {
        public string id;
        
        public int targetAmount;
        public int currentAmount;
        public GameObject uiObject;

        public ObjectiveProgress(string id, int targetAmount, GameObject uiObject)
        {
            this.id = id;
            this.targetAmount = targetAmount;
            this.currentAmount = 0;
            this.uiObject = uiObject;
        }
    }

    void Update()
    {
        // int width = Screen.width;
        // int height = Screen.height;

        // if (width % 2 != 0) width++;
        // if (height % 2 != 0) height++;

        // Screen.SetResolution(width, height, false);

        // Cek tombol Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ========================= PAUSE GAME =========================
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze game
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f; // Pastikan game jalan lagi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu(string menuSceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
        public void ShowRewardPanel()
    {   
        if (rewardPanel != null)
        {
            rewardPanel.SetActive(true);
        }
    }


    // ========================= LOCKPICK =========================
    public void SpawnLockpickMinigame()
    {
        if (currentMinigameInstance == null)
        {
            currentMinigameInstance = Instantiate(lockpickMinigamePrefab, uiCanvas);
            RectTransform rt = currentMinigameInstance.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
            }
        }
    }

    public void DestroyMinigame()
    {
        if (currentMinigameInstance != null)
        {
            Destroy(currentMinigameInstance);
            currentMinigameInstance = null;
        }
    }

    // ========================= OBJECTIVES =========================
    public void ShowObjective(string id, string message)
    {
        if (!activeObjectives.ContainsKey(id))
        {
            SoundEffectManager.Play("notif");
            GameObject newObjective = Instantiate(objectivePrefab, objectiveContainer);
            ObjectiveUI objectiveUI = newObjective.GetComponent<ObjectiveUI>();

            if (objectiveUI != null)
            {
                objectiveUI.SetObjective(id, message);
            }

            ObjectiveProgress progress = new ObjectiveProgress(id, 0, newObjective);
            activeObjectives.Add(id, progress);
        }
    }

    public void ShowObjectiveWithCount(string id, string message, int targetCount)
    {
        if (!activeObjectives.ContainsKey(id))
        {
            SoundEffectManager.Play("notif");
            GameObject newObjective = Instantiate(objectivePrefab, objectiveContainer);
            ObjectiveUI objectiveUI = newObjective.GetComponent<ObjectiveUI>();

            if (objectiveUI != null)
            {
                objectiveUI.SetObjective(id, $"{message} (0/{targetCount})");
            }

            ObjectiveProgress progress = new ObjectiveProgress(id, targetCount, newObjective);
            activeObjectives.Add(id, progress);
        }
    }

    public void AddObjectiveProgress(string id)
    {
        if (activeObjectives.ContainsKey(id))
        {
            ObjectiveProgress progress = activeObjectives[id];
            progress.currentAmount++;

            ObjectiveUI objectiveUI = progress.uiObject.GetComponent<ObjectiveUI>();
            if (objectiveUI != null)
            {
                objectiveUI.SetObjective(id, $"Progress({progress.currentAmount}/{progress.targetAmount})");
            }

            if (progress.currentAmount >= progress.targetAmount && progress.targetAmount > 0)
            {
                CompleteObjective(id);
            }
        }
    }

    public void CompleteObjective(string id)
    {
        if (activeObjectives.ContainsKey(id))
        {
            SoundEffectManager.Play("compObj");
            Destroy(activeObjectives[id].uiObject);
            activeObjectives.Remove(id);
        }
    }

    public void ShowObjectiveFromButton(string idAndMessage)
    {
        string[] parts = idAndMessage.Split('|');
        if (parts.Length >= 2)
        {
            string id = parts[0];
            string message = parts[1];
            ShowObjective(id, message);
        }
    }

    public void CompleteLevel(int nextLevelIndex)
    {
        int currentUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 1);
        Debug.Log($"[DEBUG] Current MaxLevelUnlocked: {currentUnlocked}");

        if (nextLevelIndex > currentUnlocked)
        {
            PlayerPrefs.SetInt("MaxLevelUnlocked", nextLevelIndex);
            PlayerPrefs.Save();

            Debug.Log($"[DEBUG] New level unlocked: {nextLevelIndex}");
        }
        else
        {
            Debug.Log($"[DEBUG] Level {nextLevelIndex} already unlocked or not higher than current.");
        }
    }

}
