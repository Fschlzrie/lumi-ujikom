using UnityEngine;

[CreateAssetMenu(fileName = "NewQuiz", menuName = "Quiz/Math Quiz")]
public class QuizData : ScriptableObject
{
    public string quizID;
    public Sprite questionImage;
    public int correctAnswer;
}
