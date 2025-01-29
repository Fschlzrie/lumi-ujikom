using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue/Dialogue Data")]
public class DialogSO : ScriptableObject
{
    [TextArea(3, 10)] // Memungkinkan input lebih banyak teks di Inspector
    public string[] textArray;  // Array untuk menyimpan baris dialog
}

