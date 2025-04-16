using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CodePuzzleManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField[] digitInputs; // Harus ada 4 input field
    public GameObject puzzlePanel;

    [Header("Kode Jawaban")]
    public string correctCode = "1234";

    [Header("Events")]
    public UnityEvent onCodeCorrect;
    public UnityEvent onClose;

        // Buka panel puzzle
    public void OpenPuzzle()
    {
        puzzlePanel.SetActive(true);
        ResetCode(); // reset input saat panel dibuka
    }

    // Panggil saat player menekan tombol "Submit"
    public void SubmitCode()
    {
        string inputCode = "";

        foreach (var input in digitInputs)
        {
            inputCode += input.text;
        }

        if (inputCode == correctCode)
        {
            Debug.Log("✅ Kode benar!");
            puzzlePanel.SetActive(false);
            onCodeCorrect?.Invoke();
        }
        else
        {
            Debug.Log("❌ Kode salah, reset input.");
            ResetCode();
        }
    }

    // Reset semua input
    public void ResetCode()
    {
        foreach (var input in digitInputs)
        {
            input.text = "";
        }
    }

    // Panggil ketika pemain menutup panel
    public void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        onClose?.Invoke();
    }
}
