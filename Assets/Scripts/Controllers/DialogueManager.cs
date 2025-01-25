using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText;  // Teks dialog yang akan ditampilkan
    public Button optionAButton;          // Opsi A untuk memilih pilihan A
    public Button optionBButton;          // Opsi B untuk memilih pilihan B

    [Header("Dialogue Settings")]
    public float typingSpeed = 0.05f;     // Kecepatan mengetik

    [Header("Dialogue Data")]
    public DialogSO currentDialogueData;  // DialogSO untuk dialog yang sedang ditampilkan

    private int currentDialogueIndex = 0;

    [Header("Events")]
    public UnityEvent onDialogueComplete;  // Event ketika dialog selesai

    void Start()
    {
        dialogueText.text = "";

        // Menyembunyikan pilihan hingga dialog tertentu
        if (optionAButton != null) optionAButton.gameObject.SetActive(false);
        if (optionBButton != null) optionBButton.gameObject.SetActive(false);
    }

    // Fungsi untuk memulai dialog
    public void StartDialogue(DialogSO dialogueData)
    {
        if (dialogueData == null) return;  // Pastikan data dialog tidak null

        currentDialogueData = dialogueData;
        currentDialogueIndex = 0;
        dialogueText.text = "";

        StartCoroutine(PlayDialogue());
    }

    // Fungsi untuk menghentikan dialog
    public void StopDialogue()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        currentDialogueIndex = 0;
    }

    // Coroutine untuk menampilkan dialog
    IEnumerator PlayDialogue()
    {
        dialogueText.text = "";

        // Menampilkan teks satu per satu dengan animasi mengetik
        foreach (char letter in currentDialogueData.dialogueLines[currentDialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Menunggu input untuk melanjutkan dialog
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        currentDialogueIndex++;

        // Jika ada dialog selanjutnya
        if (currentDialogueIndex < currentDialogueData.dialogueLines.Length)
        {
            StartCoroutine(PlayDialogue());
        }
        else
        {
            // Jika dialog selesai, panggil event dan sembunyikan teks
            onDialogueComplete.Invoke();
            dialogueText.text = "";

            // Jika dialog memiliki opsi, tampilkan pilihan
            if (currentDialogueData.hasOptions)
            {
                ShowOptions();
            }
        }
        
        
    }

    // Fungsi untuk menampilkan pilihan
    private void ShowOptions()
    {
        if (optionAButton == null || optionBButton == null || currentDialogueData == null) return;

        optionAButton.gameObject.SetActive(true);
        optionBButton.gameObject.SetActive(true);

        // Menampilkan teks untuk masing-masing pilihan
        optionAButton.GetComponentInChildren<TMP_Text>().text = currentDialogueData.optionAText;
        optionBButton.GetComponentInChildren<TMP_Text>().text = currentDialogueData.optionBText;

        // Menambahkan listener untuk tombol pilihan
        optionAButton.onClick.AddListener(OnOptionASelected);
        optionBButton.onClick.AddListener(OnOptionBSelected);
    }

   // Fungsi ketika Opsi A dipilih
    private void OnOptionASelected()
    {
        // Menyembunyikan pilihan sebelum melanjutkan dialog
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        // Lanjutkan dialog berdasarkan pilihan A
        if (currentDialogueData.optionAResult != null)
        {
            StartDialogue(currentDialogueData.optionAResult);
        }
        else
        {
            StopDialogue();
        }
    }

    // Fungsi ketika Opsi B dipilih
    private void OnOptionBSelected()
    {
        // Menyembunyikan pilihan sebelum melanjutkan dialog
        optionAButton.gameObject.SetActive(false);
        optionBButton.gameObject.SetActive(false);

        // Lanjutkan dialog berdasarkan pilihan B
        if (currentDialogueData.optionBResult != null)
        {
            StartDialogue(currentDialogueData.optionBResult);
        }
        else
        {
            StopDialogue();
        }
    }
}
