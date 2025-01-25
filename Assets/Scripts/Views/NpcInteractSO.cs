using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCInteract", menuName = "Game/NPC Interact")]
public class NPCInteractSO : ScriptableObject
{
    public string interactionMessage = "Press 'E' to interact."; // Pesan saat dekat NPC
    public string npcDialogue = "Hello! How can I help you?"; // Dialog NPC
    public DialogSO dialogueData;
    public DialogSO dialogueStop;

    public void Interact()
    {
        
        Debug.Log($"NPC says: {npcDialogue}");
        
    }
}
