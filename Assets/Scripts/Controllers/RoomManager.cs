using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject currentRoom; // Ruangan saat ini
    public GameObject targetRoom;  // Ruangan tujuan
    public Transform spawnLocation; // Tempat spawn Lumi di ruangan tujuan
    public GameObject player; // Referensi ke player

    public void SwitchRoom()
    {
        if (currentRoom != null && targetRoom != null && player != null && spawnLocation != null)
        {
            currentRoom.SetActive(false); // Matikan ruangan saat ini
            targetRoom.SetActive(true);   // Aktifkan ruangan tujuan
            
            // Pindahkan pemain ke spawnLocation di ruangan tujuan
            player.transform.position = spawnLocation.position;
        }
        else
        {
            Debug.LogWarning("RoomManager: Ada referensi yang belum diatur!");
        }
    }
}

