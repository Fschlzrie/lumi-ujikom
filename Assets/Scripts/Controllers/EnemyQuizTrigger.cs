using UnityEngine;

public class EnemyQuizTrigger : MonoBehaviour
{
    [Header("Quiz Data Collection")]
    public QuizData[] quizPool; // Kumpulan quiz yang bisa muncul

    [Header("Quiz Manager Reference")]
    public QuizManager quizManager; // Drag dari scene

    private bool triggered = false; // Biar nggak double trigger

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (quizManager == null || quizPool.Length == 0)
            {
                Debug.LogWarning("QuizManager belum di-assign atau quizPool kosong!");
                return;
            }

            // Ambil quiz random dari array
            QuizData randomQuiz = quizPool[Random.Range(0, quizPool.Length)];

            // Tampilkan quiz dan kirim referensi enemy ini
            quizManager.ShowQuiz(randomQuiz, gameObject);
        }
    }
}
