using UnityEngine;

public class Quizzz : MonoBehaviour
{
    [Header("Quiz Data")]
    public QuizData quizData;

    [Header("Panel Manager")]
    public QuizManager quizManager;
    
    public void TriggerQuiz()
    {
        if (quizManager == null)
        {
            Debug.LogError("QuizManager belum di-assign di QuizActivator!");
            return;
        }

        quizManager.ShowQuiz(quizData, this.gameObject);
    }
    public void Splat()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
        else
        {
            Debug.LogWarning("🎵 AudioSource belum ditambahkan di GameObject ini!");
        }
    }

}
