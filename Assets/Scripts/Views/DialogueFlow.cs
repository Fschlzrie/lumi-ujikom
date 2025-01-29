using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueFlow", menuName = "Dialogue/DialogueFlow")]
public class DialogueFlow : ScriptableObject
{
    public DialogSO dialogueSO; // Dialog 
    public List<DialogueOption> choices; // Pilihan (jika ada)

    [System.Serializable]
    public class DialogueOption
    {
        public string choiceText; // Teks pilihan (contoh: "Yes", "No")
        public DialogueFlow nextDialogueFlow; // Dialog berikutnya setelah memilih opsi ini
    }
}
