using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public NPCInteractSO interactionData; // Referensi ke Scriptable Object
    private bool isPlayerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang masuk ke trigger adalah player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log(interactionData.interactionMessage); // Tampilkan pesan interaksi
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Cek apakah yang keluar dari trigger adalah player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the NPC.");
        }
    }

    void Update()
    {
        // Cek input interaksi (misalnya tombol 'E') dan jika player dekat
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            interactionData.Interact(); // Panggil fungsi interaksi dari Scriptable Object
        }
    }
}

