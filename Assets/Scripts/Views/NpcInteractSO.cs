using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCInteract", menuName = "Game/NPC Interact")]
public class NPCInteractSO : ScriptableObject
{
    public string interactionMessage = "Press 'E' to interact."; // Pesan saat dekat NPC
    public string npcDialogue = "Hello! How can I help you?"; // Dialog NPC

    public void Interact()
    {
        // Logika interaksi saat player menekan tombol interaksi
        Debug.Log($"NPC says: {npcDialogue}");
        // Tambahkan logika tambahan di sini (dialog system, quest system, dll.)
    }
}
