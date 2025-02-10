using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public bool isTalking = false;

    public Rigidbody2D rb;
    public Animator animator;

    public PlayerInput playerInput;
    public InputActionReference moveActionReference; // Use InputActionReference
    private InputAction moveAction; // Store the actual InputAction here
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            // Get the InputAction from the InputActionReference
            moveAction = moveActionReference.action; // Assign to the moveAction variable

            if (moveAction != null) {
                moveAction.Enable(); // Enable the action here.
            } else {
                Debug.LogError("Move action is null. Check Input Action Asset.");
            }
        }
        else
        {
            Debug.LogError("Player Input component not found on " + gameObject.name);
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on " + gameObject.name);
        }
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (moveAction != null) { // Important null check!
            movement = moveAction.ReadValue<Vector2>();
        }
        if (isTalking)
        {
            movement = Vector2.zero; 
            return;
        }

        Animate();
    }

    void FixedUpdate()
    {
        if (rb != null) { // Check if rb is not null
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Animate()
    {
        bool isMoving = movement != Vector2.zero;
        if (animator != null) { // Check if animator is not null
            if (isMoving)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
            }
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    private void OnEnable() {
        if (moveAction != null) {
            moveAction.Enable();
        }
    }

    private void OnDisable() {
        if (moveAction != null) {
            moveAction.Disable();
        }
    }
    public void SetTalking(bool talking)
    {
        isTalking = talking;
    }
}