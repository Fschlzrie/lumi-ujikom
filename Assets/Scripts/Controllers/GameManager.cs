using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static bool isNoteOpen = false;

    public GameObject lockpickMinigamePrefab; // Prefab minigame
    private GameObject currentMinigameInstance; // Instance minigame aktif

    public Transform uiCanvas;
    public GameObject objectivePrefab;
    public Transform objectiveContainer;
     public static GameManager instance;

    

    // Struct Objective Progress
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

    private Dictionary<string, ObjectiveProgress> activeObjectives = new Dictionary<string, ObjectiveProgress>();
    void Awake()
    {
        // Singleton Setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Biar gak hilang pas pindah scene
        }
        else
        {
            Destroy(gameObject); // Kalau ada lebih dari satu, destroy duplikat
        }
    }
    void Update()
    {
        int width = Screen.width;
        int height = Screen.height;

        if (width % 2 != 0) width++;
        if (height % 2 != 0) height++;

        Screen.SetResolution(width, height, false);
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
    // Objective Biasa
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

    // Objective dengan jumlah target (misal: 3/3 bahan)
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

    // Tambah progress
    public void AddObjectiveProgress(string id)
    {
        if (activeObjectives.ContainsKey(id))
        {
            ObjectiveProgress progress = activeObjectives[id];
            progress.currentAmount++;

            // Update UI text
            ObjectiveUI objectiveUI = progress.uiObject.GetComponent<ObjectiveUI>();
            if (objectiveUI != null)
            {
                objectiveUI.SetObjective(id, $"Progress({progress.currentAmount}/{progress.targetAmount})");
            }

            // Jika sudah selesai
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

    // Debug Manual (optional)
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
}
