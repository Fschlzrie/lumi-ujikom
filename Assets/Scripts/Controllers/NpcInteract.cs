using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public NPCInteractSO interactionData; // Referensi ke Scriptable Object (NPCInteractSO)
    private bool isPlayerNearby = false;
    public DialogueManager dialog;         // Referensi ke DialogueManager
    public GameObject interactText;

    private bool isInteracted = false;     // Untuk melacak apakah interaksi telah dilakukan
    public bool isRetry = true;            // Untuk mengatur apakah interaksi bisa dilakukan lebih dari sekali
    public bool interact = false;
    private bool canInteract = true;
    public float interactCooldown = 1f;
    


    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang masuk ke trigger adalah player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interact = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Cek apakah yang keluar dari trigger adalah player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialog.StopDialogue();
        }
    }

    void Update()
    {
        interactText.SetActive(isPlayerNearby && !interact);

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && canInteract && !interact)
        {
            if (!isInteracted || isRetry) // Hanya bisa dilakukan jika belum berinteraksi atau jika isRetry true
            {
                dialog.StartDialogue(interactionData.dialogueData); // Pass dialogueData from NPCInteractSO
                isInteracted = true; // Tandai bahwa interaksi telah dilakukan
                interact = true;
            }
            else
            {
                dialog.StartDialogue(interactionData.dialogueStop); 
                interact = true;
            }

            StartCoroutine(HandleCooldown()); // Mulai cooldown setelah tombol ditekan
        }
    }

    IEnumerator HandleCooldown()
    {
        canInteract = false; // Nonaktifkan interaksi sementara
        yield return new WaitForSeconds(interactCooldown); // Tunggu selama waktu cooldown
        canInteract = true; // Aktifkan kembali interaksi
    }

    // Fungsi untuk mereset interaksi (bisa dipanggil jika permainan di-reset)
    public void ResetInteraction()
    {
        isInteracted = false;
    }
}
