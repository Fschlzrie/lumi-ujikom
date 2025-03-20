using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowKey : MonoBehaviour
{
    public Transform viewObject;
    public UnityEvent onInteract;
    public UnityEvent onInteract2;
    public UnityEvent onLockpickSuccess;
    public GameObject keyObject;

    public Vector3 offset = new Vector3(-0.5f, 1.5f, 0f);

    public bool isShowing;

  
    void Start()
    {
        keyObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        keyObject.SetActive(isShowing);

        if (isShowing && Input.GetKeyDown(KeyCode.E))
        {
            onInteract.Invoke();
        }
        if (isShowing && Input.GetKeyDown(KeyCode.Escape))
        {
            onInteract2.Invoke();
        }
    }

    public void Show(){
        isShowing = true;
                // Update posisi bubble chat agar selalu di kiri atas NPC
        keyObject.transform.position = viewObject.position + offset;
    }
    public void Hide(){
        isShowing = false;
    }
    // Buat fungsi public untuk dipanggil dari LockpickManager
    public void InvokeLockpickSuccessEvent()
    {
        onLockpickSuccess?.Invoke();
    }
}
