using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static bool isNoteOpen = false;

    public GameObject lockpickMinigamePrefab;
    private GameObject currentMinigameInstance;

    public Transform uiCanvas;
    public GameObject objectivePrefab;
    public Transform objectiveContainer;
    private Dictionary<string, GameObject> activeObjectives = new Dictionary<string, GameObject>();

    void Update()
    {
        int width = Screen.width;
        int height = Screen.height;
        if (width % 2 != 0) width++;
        if (height % 2 != 0) height++;
        Screen.SetResolution(width, height, false);
    }

    public void SpawnLockpickMinigame(string resultItem)
    {
        if (currentMinigameInstance == null)
        {
            currentMinigameInstance = Instantiate(lockpickMinigamePrefab, uiCanvas);
            RectTransform rt = currentMinigameInstance.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
            }

            LockpickManager lockpickScript = currentMinigameInstance.GetComponent<LockpickManager>();
            if (lockpickScript != null)
            {
                lockpickScript.resultItem = resultItem; // Set hasil minigame
                lockpickScript.onLockpickCompleted.AddListener(HandleMinigameResult);
            }
        }
    }

    private void HandleMinigameResult(string result)
    {
        Debug.Log("Received minigame result: " + result);

        // Contoh: Berdasarkan hasil, lakukan sesuatu
        if (result == "gold_key")
        {
            ShowObjective("found_key", "You found a golden key!");
        }
        else if (result == "secret_code")
        {
            ShowObjective("secret_code", "You discovered a secret code!");
        }

        // Bersihkan instance
        DestroyMinigame();
    }

    public void DestroyMinigame()
    {
        if (currentMinigameInstance != null)
        {
            Destroy(currentMinigameInstance);
            currentMinigameInstance = null;
        }
    }
    //Testing Debug
    public void ShowObjectiveFromButton(string idAndMessage)
    {
        // Pisahkan id dan message pakai delimiter
        string[] parts = idAndMessage.Split('|');
        if (parts.Length >= 2)
        {
            string id = parts[0];
            string message = parts[1];
            ShowObjective(id, message);
        }
    }
    public void ShowObjective(string id, string message)
    {
        if (!activeObjectives.ContainsKey(id))
        {
            GameObject newObjective = Instantiate(objectivePrefab, objectiveContainer);
            ObjectiveUI objectiveUI = newObjective.GetComponent<ObjectiveUI>();
            if (objectiveUI != null)
            {
                objectiveUI.SetObjective(id, message);
            }
            activeObjectives.Add(id, newObjective);
        }
    }

    public void CompleteObjective(string id)
    {
        if (activeObjectives.ContainsKey(id))
        {
            Destroy(activeObjectives[id]);
            activeObjectives.Remove(id);
        }
    }
}
