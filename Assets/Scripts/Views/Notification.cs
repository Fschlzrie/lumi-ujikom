using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public TextMeshProUGUI notificationText;
    public Animator animator;

    private void Awake()
    {

    }

   public void ShowNotification(string message)
{
    if (animator == null)
    {
        Debug.LogError("Animator masih NULL! Cek Prefab dan Inspector!", this);
        return;
    }

    if (animator.runtimeAnimatorController == null)
    {
        Debug.LogError("Animator tidak memiliki Animator Controller! Tambahkan di Inspector!", this);
        return;
    }

    notificationText.text = message;  
    animator.SetTrigger("Show");      
}


    public void DestroyAfterAnimation()
    {
        gameObject.SetActive(false); // Hancurkan objek setelah animasi selesai
    }
}
