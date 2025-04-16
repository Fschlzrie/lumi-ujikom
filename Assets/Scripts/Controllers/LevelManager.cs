using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public List<Button> levelButtons; // Drag semua tombol level ke sini lewat Inspector

    void Start()
    {
        // Jika belum pernah ada progress, set default unlock = 1 (atau 0 jika ingin semua tombol mati)
        if (!PlayerPrefs.HasKey("MaxLevelUnlocked"))
        {
            PlayerPrefs.SetInt("MaxLevelUnlocked", 1);
            PlayerPrefs.Save();
        }

        InitProgress();
    }

    public void ResetProgress()
    {
        // Atur ulang progress
        PlayerPrefs.SetInt("MaxLevelUnlocked", 0); // Ubah ke 1 kalau mau level 1 tetap aktif
        PlayerPrefs.Save();

        Debug.Log("Progress direset.");
        InitProgress();
    }

    private void InitProgress()
    {
        int maxLevelUnlocked = PlayerPrefs.GetInt("MaxLevelUnlocked", 0); // Ubah ke 1 kalau ingin default aktif level 1

        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].interactable = (i + 1) <= maxLevelUnlocked;
        }
    }

    // Jika nanti ingin load level:
    // public void LoadLevel(string levelName)
    // {
    //     SceneManager.LoadScene(levelName);
    // }
}
