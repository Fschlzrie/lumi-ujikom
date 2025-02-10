using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using System;

public class NpcManager : MonoBehaviour
{
   public NPCConversation myConversation;
   public PlayerMovement player;
   public Animator bgAnimator;
   public bool canPress = false; // Default false
   public string key = "e";

   void Awake(){
      ConversationManager.Instance.EndConversation();
      
   }

   void Update(){
      if (canPress && !(ConversationManager.Instance.IsConversationActive) && Input.GetKey(key))
      {
         startConversation();
      }
      if(ConversationManager.Instance.IsConversationActive){
         // Debug.Log("Yahahaha gabisa mencet!!");
         bgAnimator.SetBool("isAnimate",true);
         player.SetTalking(true);

      }else{
         bgAnimator.SetBool("isAnimate",false);
         player.SetTalking(false);
      }
   }

   public void startConversation(){
      ConversationManager.Instance.StartConversation(myConversation);
   }

   public void EnablePress()
    {
        canPress = true;
    }

   public void DisablePress()
    {
        canPress = false;
    }
    
}
