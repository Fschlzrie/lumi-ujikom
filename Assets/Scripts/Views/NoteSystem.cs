using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteSystem : MonoBehaviour
{
    public GameObject notePrefab; // Prefab panel note (tanpa teks)
    private GameObject currentNotePanel; // Panel aktif

    [TextArea(3, 10)]
    public string noteText; // Isi catatan (diisi manual di Inspector)

    public void OpenNote()
    {
        if (currentNotePanel == null)
        {
            currentNotePanel = Instantiate(notePrefab, FindObjectOfType<Canvas>().transform);
            
            // Ambil TMP_Text dari child dengan nama "Text" dan set teksnya
            TMP_Text textComponent = currentNotePanel.transform.Find("Text").GetComponent<TMP_Text>();
            textComponent.text = noteText;

            // Tambahkan tombol close
            currentNotePanel.transform.Find("CloseButton").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CloseNote);

            // Freeze movement
            GameManager.isNoteOpen = true;
        }
    }

    public void CloseNote()
    {
        if (currentNotePanel != null)
        {
            Destroy(currentNotePanel);
            currentNotePanel = null;
            // Unfreeze movement
            GameManager.isNoteOpen = false;
        }
    }
}
