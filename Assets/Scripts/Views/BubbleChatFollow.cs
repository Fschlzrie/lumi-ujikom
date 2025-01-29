using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleChatFollow : MonoBehaviour
{
    public Transform npcTransform; // Referensi ke transform NPC
    public Vector3 offset = new Vector3(-0.5f, 1.5f, 0f); // Offset posisi bubble chat (default: kiri atas)

    private void Update()
    {
        if (npcTransform != null)
        {
            // Update posisi bubble chat agar selalu di kiri atas NPC
            transform.position = npcTransform.position + offset;
        }
    }
}
