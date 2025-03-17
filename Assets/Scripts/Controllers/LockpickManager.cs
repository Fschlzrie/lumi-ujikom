using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LockpickResultEvent : UnityEvent<string> { }

public class LockpickManager : MonoBehaviour
{
    public List<Button> pinButtons;
    private List<int> buttonOrder = new List<int>();
    private List<int> correctOrder = new List<int>();
    private int currentIndex = 0;
    private int selectedIndex = 0;

    public GameObject minigamePanel;

    public string resultItem = "DefaultItem"; // Bisa diset dari Inspector atau lewat GameManager
    public LockpickResultEvent onLockpickCompleted; // Event dengan hasil

    void Start()
    {
        SetupLockpickGame();
        HighlightButton();
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

        for (int i = 0; i < pinButtons.Count; i++)
        {
            int assignedValue = buttonOrder[i];
            pinButtons[i].GetComponentInChildren<TMP_Text>().text = assignedValue.ToString();
            pinButtons[i].onClick.RemoveAllListeners();
            int index = i;
            pinButtons[i].onClick.AddListener(() => CheckPin(index, assignedValue));
            pinButtons[i].interactable = true;
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
        if (!pinButtons[buttonIndex].interactable) return;

        if (selectedValue == correctOrder[currentIndex])
        {
            pinButtons[buttonIndex].interactable = false;
            currentIndex++;

            if (currentIndex >= correctOrder.Count)
            {
                LockpickSuccess();
            }
        }
        else
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        currentIndex = 0;
        foreach (var button in pinButtons)
        {
            button.interactable = true;
        }
    }

    private void LockpickSuccess()
    {
        Debug.Log("Lockpick successful! Result: " + resultItem);
        onLockpickCompleted?.Invoke(resultItem);
        minigamePanel.SetActive(false);
        Time.timeScale = 1;
        Destroy(gameObject); // Clean up instance
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
