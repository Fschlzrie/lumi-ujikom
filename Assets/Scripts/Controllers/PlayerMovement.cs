using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
   public float moveSpeed = 3.0f;

   public Rigidbody2D rb;
   public Animator animator;

   Vector2 movement;

   void Update()
   {
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
    Animate();
   }

   void FixedUpdate()
   {
    rb.MovePosition(rb.position +  movement.normalized * moveSpeed * Time.fixedDeltaTime);
   }

   void Animate(){
      bool isMoving = movement != Vector2.zero;
      if (isMoving)
      {
         animator.SetFloat("Horizontal", movement.x);
         animator.SetFloat("Vertical", movement.y);   
      }
      animator.SetFloat("Speed", movement.magnitude);
      
   }
}
