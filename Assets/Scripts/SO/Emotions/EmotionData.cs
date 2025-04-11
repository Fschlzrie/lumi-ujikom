using UnityEngine;

[CreateAssetMenu(fileName = "EmotionData", menuName = "Lumi/Emotion Data")]
public class EmotionData : ScriptableObject
{
    public EmotionType emotionType;
    public string emotionName;
    public string description;
    public Sprite icon;
    public Color emotionColor;
}
