using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public UnityEngine.UI.Image questionImage;
    public TMP_InputField answerInput;

    private int correctAnswer;

    public void ShowQuiz(QuizData quiz)
    {
        quizPanel.SetActive(true);
        questionImage.sprite = quiz.questionImage;
        correctAnswer = quiz.correctAnswer;
        Time.timeScale = 0;
    }

    public void SubmitAnswer()
    {
        Time.timeScale = 1;
        if (int.TryParse(answerInput.text, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("✅ Benar! Kasih reward.");
                quizPanel.SetActive(false);
                // TODO: kasih reward / event lain
            }
            else
            {
                Debug.Log("❌ Salah! Meledak.");
                quizPanel.SetActive(false);
                // Play ledakan + game over
                FindObjectOfType<PlayerMovement>().Explode();
            }
        }
    }
}
