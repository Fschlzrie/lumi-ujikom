using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool oneShot = false; // Apakah trigger hanya bisa dipicu sekali?
    private bool triggered = false; // Flag untuk mengunci trigger jika oneShot aktif

    public string collisionTag; // Tag untuk filter objek yang bisa memicu trigger
    
    public UnityEvent onTriggerEnter; // Event saat masuk trigger
    public UnityEvent onTriggerExit; // Event saat keluar dari trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return; // Jika oneShot aktif dan sudah dipicu, tidak lakukan apa-apa

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return; // Hanya lanjutkan jika tag objek sesuai

        onTriggerEnter?.Invoke(); // Panggil event masuk

        if (oneShot) triggered = true; // Kunci trigger jika oneShot aktif
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggered) return; // Jika sudah dipicu dan oneShot aktif, abaikan

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return; // Pastikan hanya objek dengan tag tertentu yang bisa memicu trigger

        onTriggerExit?.Invoke(); // Panggil event keluar

        if (oneShot) triggered = true; // Kunci trigger jika oneShot aktif
    }
}
