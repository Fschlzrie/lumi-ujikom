using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    [Header("Collectible Buttons")]
    public List<Button> collectibleButtons;     // Drag semua tombol collectible dari UI
    public List<string> collectibleIDs;         // ID unik per collectible, urutan harus sama
    public List<string> collectibleContents;    // Isi teks collectible, urutan sama juga

    [Header("UI Viewer")]
    public GameObject collectibleViewerPanel;   // Panel pop-up buat lihat isi collectible
    public TextMeshProUGUI collectibleContentText;         // UI Text buat isi collectible

    void Start()
    {
        InitCollectibles();
    }

    void InitCollectibles()
    {
        for (int i = 0; i < collectibleButtons.Count; i++)
        {
            int index = i;
            bool isCollected = PlayerPrefs.GetInt("COLLECTIBLE_" + collectibleIDs[i], 0) == 1;

            Button btn = collectibleButtons[i];
            TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();

            if (isCollected)
            {
                btn.interactable = true;
                btnText.text = "Funfact " + (i + 1);
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => OpenCollectible(collectibleContents[index]));
            }
            else
            {
                btn.interactable = false;
                btnText.text = "???";
            }
        }
    }

    public void Collect(string collectibleID)
    {
        PlayerPrefs.SetInt("COLLECTIBLE_" + collectibleID, 1);
        PlayerPrefs.Save();
        InitCollectibles();
    }

    public void OpenCollectible(string content)
    {
        collectibleViewerPanel.SetActive(true);
        collectibleContentText.text = content;
    }

    public void CloseViewer()
    {
        collectibleViewerPanel.SetActive(false);
    }

    public void ResetAll()
    {
        foreach (var collectibleID in collectibleIDs)
        {
            PlayerPrefs.DeleteKey("COLLECTIBLE_" + collectibleID);
        }
        PlayerPrefs.Save();
        InitCollectibles();
    }
}
