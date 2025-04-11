using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EmotionManager : MonoBehaviour
{
    [Header("Unlocked Emotions for This Chapter")]
    public List<EmotionData> unlockedEmotions;

    [Header("UI")]
    public Image frameCharImage;

    private HashSet<EmotionType> emotionSet = new HashSet<EmotionType>();
    private int currentEmotionIndex = 0;

    void Awake()
    {
        emotionSet.Clear();
        foreach (var emotion in unlockedEmotions)
        {
            if (emotion != null)
            {
                emotionSet.Add(emotion.emotionType);
            }
        }
    }

    void Start()
    {
        UpdateEmotionFrame();
        DebugUnlockedEmotions();
    }

    void Update()
    {
        if (unlockedEmotions.Count == 0) return;

        // Tekan Q = previous emotion
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentEmotionIndex--;
            if (currentEmotionIndex < 0) currentEmotionIndex = unlockedEmotions.Count - 1;
            UpdateEmotionFrame();
        }

        // Tekan E = next emotion
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentEmotionIndex++;
            if (currentEmotionIndex >= unlockedEmotions.Count) currentEmotionIndex = 0;
            UpdateEmotionFrame();
        }
    }

    public bool HasEmotion(EmotionType type)
    {
        return emotionSet.Contains(type);
    }

    public void UnlockEmotion(EmotionData newEmotion)
    {
        if (!emotionSet.Contains(newEmotion.emotionType))
        {
            emotionSet.Add(newEmotion.emotionType);
            unlockedEmotions.Add(newEmotion);
            Debug.Log("Emotion Unlocked: " + newEmotion.emotionType);
        }
    }

    public List<EmotionData> GetUnlockedEmotions()
    {
        return unlockedEmotions;
    }

    public EmotionData GetCurrentEmotion()
    {
        if (unlockedEmotions.Count > 0)
            return unlockedEmotions[currentEmotionIndex];
        return null;
    }

    private void UpdateEmotionFrame()
    {
        if (unlockedEmotions.Count > 0 && frameCharImage != null)
        {
            EmotionData emotion = unlockedEmotions[currentEmotionIndex];
            frameCharImage.sprite = emotion.icon;

            Debug.Log("Current Emotion: " + emotion.emotionType + " (Index: " + currentEmotionIndex + ")");
        }
    }

    public void DebugUnlockedEmotions()
    {
        Debug.Log("=== Emotions Unlocked by Lumi ===");
        foreach (var emotion in unlockedEmotions)
        {
            if (emotion != null)
            {
                Debug.Log("- " + emotion.emotionType.ToString());
            }
        }
    }
}
