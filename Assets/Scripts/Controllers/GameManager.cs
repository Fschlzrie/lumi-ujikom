using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool isNoteOpen = false;

    public GameObject lockpickMinigamePrefab; // Prefab minigame
    private GameObject currentMinigameInstance; // Instance yang sedang aktif

    public Transform uiCanvas; // **Pastikan ini adalah Canvas utama**
    public GameObject objectivePrefab; // Prefab Objective UI
    public Transform objectiveContainer; // Tempatkan di pojok kiri atas di Canvas
    
    private Dictionary<string, GameObject> activeObjectives = new Dictionary<string, GameObject>(); // Simpan objective aktif


    void Update()
    {
        int width = Screen.width;
        int height = Screen.height;

        // Pastikan resolusi selalu genap
        if (width % 2 != 0) width++;
        if (height % 2 != 0) height++;

        Screen.SetResolution(width, height, false); // false = windowed mode
    }

        public void SpawnLockpickMinigame()
    {
        if (currentMinigameInstance == null)
        {
            currentMinigameInstance = Instantiate(lockpickMinigamePrefab, uiCanvas); // Spawn di Canvas UI

            // Reset posisi agar muncul di tengah Canvas
            RectTransform rt = currentMinigameInstance.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero; // **Pastikan muncul di tengah Canvas**
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

    public void ShowObjective(string id, string message)
    {
        if (!activeObjectives.ContainsKey(id)) // Jangan spawn kalau sudah ada ID yg sama
        {
            SoundEffectManager.Play("notif");
            GameObject newObjective = Instantiate(objectivePrefab, objectiveContainer);
            ObjectiveUI objectiveUI = newObjective.GetComponent<ObjectiveUI>();

            if (objectiveUI != null)
            {
                objectiveUI.SetObjective(id, message);
            }

            activeObjectives.Add(id, newObjective);
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


    public void CompleteObjective(string id)
    {
        if (activeObjectives.ContainsKey(id))
        {
            SoundEffectManager.Play("compObj");
            Destroy(activeObjectives[id]);
            activeObjectives.Remove(id);
        }
    }

}
