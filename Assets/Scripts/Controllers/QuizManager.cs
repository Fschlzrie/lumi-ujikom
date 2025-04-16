using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject quizPanel;
    public Image questionImage;
    public TMP_InputField answerInput;

    [Header("Reward Panel")]
    public GameObject rewardPanel;

    [Header("Quiz Event")]
    public UnityEvent onCorrectAnswer;

    private GameObject currentQuizObject;
    private int correctAnswer;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); // Cari komponen PlayerMovement
    }

    public void ShowQuiz(QuizData quiz, GameObject quizObject)
    {
        quizPanel.SetActive(true);
        questionImage.sprite = quiz.questionImage;
        correctAnswer = quiz.correctAnswer;
        currentQuizObject = quizObject;

        if (playerMovement != null)
        {
            playerMovement.SetTalking(true); // ❌ Stop player movement
        }
    }

    public void SubmitAnswer()
    {
        if (int.TryParse(answerInput.text, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("✅ Benar! Kasih reward.");

                quizPanel.SetActive(false);

                if (playerMovement != null)
                {
                    playerMovement.SetTalking(false); // ✅ Aktifkan lagi gerak
                }

                if (currentQuizObject != null)
                {
                    Destroy(currentQuizObject);
                }

                onCorrectAnswer?.Invoke(); // Panggil event reward
            }
            else
            {
                Debug.Log("❌ Salah! Meledak.");
                quizPanel.SetActive(false);

                // Ga perlu aktifin gerak karena bakal meledak
                FindObjectOfType<PlayerMovement>().Explode();
            }
        }
    }
}
