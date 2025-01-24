using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cutscene", menuName = "Cutscene")]   
public class CutsceneSO : ScriptableObject
{
    public CutsceneEntry[] cutsceneEntries;  // Array untuk menyimpan entri dialog dan gambar
}


[System.Serializable] // Agar bisa dilihat di Unity Inspector
public class CutsceneEntry
{
    public string dialogue;  // Teks dialog
    public Sprite image;     // Gambar yang akan ditampilkan bersama dialog
}
