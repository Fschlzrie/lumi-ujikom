using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public CutsceneSO cutsceneData;  // Referensi ke ScriptableObject Cutscene
    public GameObject textPanel;     // Panel untuk menampilkan teks
    public TextMeshProUGUI textUI1;   // Tempat teks dialog 1 (untuk dialog biasa)
    public TextMeshProUGUI textUI2;   // Tempat teks dialog 2 (untuk subtitle jika ada gambar)
    public Image imageUI;            // Tempat gambar cutscene
    public float typingSpeed = 0.05f; // Kecepatan mengetik teks

    private int currentDialogueIndex = 0; // Indeks dialog yang sedang ditampilkan

    void Start()
    {
        textPanel.SetActive(false);
        
    }

    public void TriggerCutscene(){
        if (cutsceneData != null)
        {
            StartCoroutine(PlayCutscene());
        }else{
            Debug.LogError("Cutscene data tidak ditemukan");
        }
    }

    IEnumerator PlayCutscene()
    {
        textPanel.SetActive(true); // Menampilkan panel teks

        while (currentDialogueIndex < cutsceneData.cutsceneEntries.Length)
        {
            CutsceneEntry currentEntry = cutsceneData.cutsceneEntries[currentDialogueIndex];

            // Menampilkan gambar jika ada
            if (currentEntry.image != null)
            {
                imageUI.sprite = currentEntry.image;
                imageUI.enabled = true; // Menampilkan gambar

                // Jika ada gambar, sembunyikan textUI1 dan tampilkan textUI2
                textUI1.gameObject.SetActive(false);
                textUI2.gameObject.SetActive(true);
                textUI2.text = ""; // Reset teks subtitle
            }
            else
            {
                imageUI.enabled = false; // Menyembunyikan gambar jika tidak ada

                // Jika tidak ada gambar, tampilkan di textUI1
                textUI1.gameObject.SetActive(true);
                textUI2.gameObject.SetActive(false);
                textUI1.text = ""; // Reset teks
            }
            
            string currentDialogue = currentEntry.dialogue;
            foreach (char letter in currentDialogue.ToCharArray())
            {
                // Tampilkan huruf satu per satu sesuai dengan panel yang aktif
                if (textUI1.gameObject.activeSelf)
                {
                    textUI1.text += letter;
                }
                else if (textUI2.gameObject.activeSelf)
                {
                    textUI2.text += letter;
                }

                yield return new WaitForSeconds(typingSpeed);
            }


            // Tunggu sampai pemain mengklik untuk melanjutkan ke dialog berikutnya
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // Beralih ke dialog berikutnya
            currentDialogueIndex++;
        }

        // Sembunyikan panel teks setelah cutscene selesai
        textPanel.SetActive(false);
    }
}
