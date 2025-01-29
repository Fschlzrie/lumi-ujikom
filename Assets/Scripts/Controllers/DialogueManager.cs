using UnityEngine;
using TMPro; // Menggunakan TextMeshPro
using UnityEngine.UI;
using System.Collections; // Perlu import sistem koleksi untuk IEnumerator

public class DialogueManager : MonoBehaviour
{
    public DialogueFlow currentFlow; // Flow dialog aktif
    public Transform choiceButtonParent; // Panel tempat tombol akan di-*spawn*
    public GameObject choiceButtonPrefab; // Prefab tombol pilihan UI
    public TextMeshProUGUI dialogueText; // TMP Text untuk menampilkan dialog
    public float typingSpeed = 0.05f; // Kecepatan mengetik (waktu antara setiap karakter)

    private bool isNearby = false;
    private int currentLineIndex = 0;
    private DialogSO currentDialogue; // Menyimpan dialog yang sedang berlangsung



    void Start()
    {
        dialogueText.text = "";
        if (currentFlow != null && currentFlow.dialogueSO != null)
        {
            // Menginisialisasi currentDialogue dengan firstDialogue
            currentDialogue = currentFlow.dialogueSO;
        }
    }

    public void StartDialogue()
    {
        if (currentDialogue != null)
        {
            currentLineIndex = 0;
            ShowCurrentLine();
        }
    }

    public void ShowCurrentLine()
    {
        if (currentDialogue == null) return;

        // Cek jika dialog masih memiliki teks
        if (currentLineIndex < currentDialogue.textArray.Length)
        {
            // Mulai coroutine untuk efek mengetik
            StartCoroutine(TypeText(currentDialogue.textArray[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            // Dialog selesai, tampilkan pilihan (jika ada)
            ShowChoices();
        }
    }

    // Coroutine untuk mengetikkan teks satu per satu
    private IEnumerator TypeText(string textToType)
    {
        dialogueText.text = ""; // Kosongkan teks sebelumnya
        foreach (char letter in textToType)
        {
            dialogueText.text += letter; // Tambahkan satu karakter per loop
            yield return new WaitForSeconds(typingSpeed); // Tunggu sesuai kecepatan typing
        }
    }

    public void ShowChoices()
    {
        // Bersihkan tombol pilihan yang sebelumnya
        foreach (Transform child in choiceButtonParent)
        {
            Destroy(child.gameObject);
        }

        if (currentFlow.choices == null || currentFlow.choices.Count == 0)
        {
            Debug.Log("Dialog selesai!");
            return;
        }

        // Spawn tombol untuk setiap pilihan
        foreach (var choice in currentFlow.choices)
        {
            GameObject buttonInstance = Instantiate(choiceButtonPrefab, choiceButtonParent); // Buat tombol dari prefab
            TextMeshProUGUI buttonText = buttonInstance.GetComponentInChildren<TextMeshProUGUI>(); // Temukan komponen TMP Text di dalam prefab

            if (buttonText != null)
            {
                buttonText.text = choice.choiceText; // Atur teks tombol sesuai dengan teks pilihan
            }

            // Tambahkan event listener ke tombol
            Button buttonComponent = buttonInstance.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnChoiceSelected(choice.nextDialogueFlow));
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowCurrentLine();
        }
    }

    private void OnChoiceSelected(DialogueFlow nextDialogue)
    {
        if (nextDialogue == null)
        {
            Debug.LogError("Dialog berikutnya tidak ditemukan!");
            return;
        }

        // Mengubah currentDialogue ke dialog berikutnya yang dipilih
        currentFlow = nextDialogue;
        currentDialogue = nextDialogue.dialogueSO   ;
        currentLineIndex = 0;
        StartDialogue();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Mengecek apakah objek memiliki tag "Player"
        {
            isNearby = true;
            Debug.Log(isNearby);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Mengecek apakah objek memiliki tag "Player"
        {
            isNearby = false;
            Debug.Log(isNearby);
            
        }
    }


}
