using UnityEngine;

public class DieManager : MonoBehaviour
{
    [Header("Object yang dipindahkan (biasanya Player)")]
    public GameObject targetObject;

    [Header("Target titik tujuan")]
    public Transform teleportTarget;

    [Header("Sound efek ketika teleport")]
    public AudioClip teleportSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource belum ditambahkan ke GameObject ini.");
        }
    }

    // Fungsi untuk memindahkan targetObject ke titik tujuan
    public void Teleport()
    {
        if (teleportTarget != null && targetObject != null)
        {
            targetObject.transform.position = teleportTarget.position;

            // Mainkan suara jika tersedia
            if (audioSource != null && teleportSound != null)
            {
                audioSource.PlayOneShot(teleportSound);
            }
        }
        else
        {
            Debug.LogWarning("Teleport gagal: Target object atau teleport point belum di-assign.");
        }
    }
}
