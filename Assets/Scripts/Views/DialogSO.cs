using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue/Dialogue Data")]
public class DialogSO : ScriptableObject
{
    [TextArea(3, 10)] // Memungkinkan input lebih banyak teks di Inspector
    public string[] dialogueLines;  // Array untuk menyimpan baris dialog

    public bool hasOptions;          // Menandakan apakah dialog ini memiliki pilihan
    public string optionAText;       // Teks untuk pilihan A
    public string optionBText;       // Teks untuk pilihan B

    public DialogSO optionAResult;   // Dialog yang akan dipilih jika opsi A dipilih
    public DialogSO optionBResult;   // Dialog yang akan dipilih jika opsi B dipilih
}

