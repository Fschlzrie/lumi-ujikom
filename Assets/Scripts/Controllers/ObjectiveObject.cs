using UnityEngine;

public class ObjectiveObject : MonoBehaviour
{
    public string objectiveID;
    public string objectiveMessage;
    public int objectiveTargetCount = 1; // Default 1 kalau cuma 1 item

    // Fungsi untuk trigger UnityEvent (bisa dipanggil di Inspector)
    public void TriggerObjective()
    {
        GameManager.instance.ShowObjectiveWithCount(objectiveID, objectiveMessage, objectiveTargetCount);
    }

    public void AddObjectiveProgress()
    {
        GameManager.instance.AddObjectiveProgress(objectiveID);
    }
}
