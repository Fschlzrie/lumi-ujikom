using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public GameObject keyObject;
    public Transform viewObject;
    
    public bool oneShot = false;
    public bool showKey = false;
    public Vector3 offset = new Vector3(-0.5f, 1.5f, 0f);
    private bool alreadyEntered = false;
    private bool alreadyExited = false;
    public bool isEnter = false;

    public string collisionTag;
    
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    void Update()
    {
         if (showKey && isEnter)
        {
            keyObject.SetActive(true);
            // Update posisi bubble chat agar selalu di kiri atas NPC
            transform.position = viewObject.position + offset;
        }else if (showKey && !isEnter)
        {
            keyObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        
        if (alreadyEntered)
            return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return;

        onTriggerEnter?.Invoke();

        if(oneShot)
            alreadyEntered = true;
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (alreadyExited)
            return;

        if(!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
            return;

        onTriggerExit?.Invoke();

        if (oneShot)
            alreadyExited = true;
    }
    
    public void setEnter(){
        isEnter = true;
    }
    public void setNotEnter(){
        isEnter = false;
    }
}
