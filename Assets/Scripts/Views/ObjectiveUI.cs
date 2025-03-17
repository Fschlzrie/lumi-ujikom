using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public string objectiveID;
    public TMP_Text objectiveText; // Ganti jadi TMP_Text kalau pakai TextMeshPro

    public void SetObjective(string id, string text)
    {
        objectiveID = id;
        objectiveText.text = text;
    }
    public void Complete(){
        Destroy(gameObject);
    }
}
