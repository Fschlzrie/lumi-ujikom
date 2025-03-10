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

            // Tambahkan event saat berhasil membuka kunci
            LockpickManager lockpickScript = currentMinigameInstance.GetComponent<LockpickManager>();
            if (lockpickScript != null)
            {
                lockpickScript.onLockpickSuccess.AddListener(DestroyMinigame);
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
}
