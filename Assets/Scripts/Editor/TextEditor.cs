using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Lumi_Namespace.Text))]
public class TextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Menampilkan UI default dari inspector
        DrawDefaultInspector();

        // Mendapatkan referensi ke script View yang sedang di-edit
        Lumi_Namespace.Text text = (Lumi_Namespace.Text)target;

        // Menambahkan tombol "Configure Now" di Inspector
        if (GUILayout.Button("Configure Now"))
        {
            // Memanggil fungsi Init() dari script View
            text.Init();

        }
    }
}
