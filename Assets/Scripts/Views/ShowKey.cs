using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKey : MonoBehaviour
{
    public Transform viewObject;
    public GameObject keyObject;

    public Vector3 offset = new Vector3(-0.5f, 1.5f, 0f);

    public bool isShowing;

  
    void Awake()
    {
        keyObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        keyObject.SetActive(isShowing);
    }

    public void Show(){
        isShowing = true;
                // Update posisi bubble chat agar selalu di kiri atas NPC
        keyObject.transform.position = viewObject.position + offset;
    }
    public void Hide(){
        isShowing = false;
    }
}
