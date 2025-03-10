using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class LockpickManager : MonoBehaviour
{
    public List<Button> pinButtons; // List button yang akan diklik oleh pemain
    private List<int> buttonOrder = new List<int>(); // Urutan angka pada tombol (diacak)
    private List<int> correctOrder = new List<int>(); // Urutan yang harus ditebak pemain
    private int currentIndex = 0; // Posisi urutan yang sedang dicek
    private int selectedIndex = 0; // Untuk navigasi tombol

    public UnityEvent onLockpickSuccess; // Event ketika berhasil membuka kunci
    public GameObject minigamePanel; // Panel minigame untuk ditutup setelah sukses

    void Start()
    {
        SetupLockpickGame();
        HighlightButton(); // Mulai dengan menyorot tombol pertama
        Time.timeScale = 0;
    }

    void Update()
    {
        HandleInput();
    }

    private void SetupLockpickGame()
    {
        buttonOrder = GenerateRandomOrder(5);
        correctOrder = GenerateRandomOrder(5);

        // Debug.Log("Button Order: " + string.Join(", ", buttonOrder));
        // Debug.Log("Correct Order: " + string.Join(", ", correctOrder));

        for (int i = 0; i < pinButtons.Count; i++)
        {
            int assignedValue = buttonOrder[i];
            pinButtons[i].GetComponentInChildren<TMP_Text>().text = assignedValue.ToString();
            pinButtons[i].onClick.RemoveAllListeners();
            int index = i;
            pinButtons[i].onClick.AddListener(() => CheckPin(index, assignedValue));
            pinButtons[i].interactable = true; // Pastikan tombol bisa ditekan di awal
        }
    }

    private List<int> GenerateRandomOrder(int count)
    {
        List<int> numbers = new List<int>();
        for (int i = 1; i <= count; i++)
        {
            numbers.Add(i);
        }

        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (numbers[i], numbers[randomIndex]) = (numbers[randomIndex], numbers[i]);
        }
        return numbers;
    }

    private void CheckPin(int buttonIndex, int selectedValue)
    {
        if (!pinButtons[buttonIndex].interactable) return; // Jika sudah ditekan, tidak bisa dipilih lagi

        if (selectedValue == correctOrder[currentIndex])
        {
            // Debug.Log("Correct: " + selectedValue);
            pinButtons[buttonIndex].interactable = false; // Matikan tombol yang sudah ditekan
            currentIndex++;

            if (currentIndex >= correctOrder.Count)
            {
                LockpickSuccess();
            }
        }
        else
        {
            // Debug.Log("Incorrect! Reset progress.");
            ResetGame();
        }
    }

    private void ResetGame()
    {
        currentIndex = 0;
        foreach (var button in pinButtons)
        {
            button.interactable = true; // Aktifkan kembali semua tombol setelah reset
        }
    }

    private void LockpickSuccess()
    {
        Debug.Log("Lockpick successful!");
        onLockpickSuccess?.Invoke();
        minigamePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (pinButtons[selectedIndex].interactable)
            {
                pinButtons[selectedIndex].onClick.Invoke();
            }
        }
    }

    private void MoveSelection(int direction)
    {
        SetButtonHighlight(false);
        selectedIndex += direction;

        if (selectedIndex < 0) selectedIndex = pinButtons.Count - 1;
        if (selectedIndex >= pinButtons.Count) selectedIndex = 0;

        HighlightButton();
    }

    private void HighlightButton()
    {
        if (pinButtons.Count == 0) return;
        SetButtonHighlight(true);
    }

    private void SetButtonHighlight(bool active)
    {
        Color highlightColor = active ? Color.red : Color.white;
        pinButtons[selectedIndex].GetComponent<Image>().color = highlightColor;
    }
}
