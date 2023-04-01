using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    public LayerMask interactableLayer;

    public float moveSpeed = 1f;

    private bool isMoving;
    private Vector2 movementInput;

    Animator animator;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void HandleUpdate() {
        if (movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);

            // Attempt to move horizontally.
            if (!success) {
                success = TryMove(new Vector2(movementInput.x, 0));   
            }

            // Attempt to move vertically.
            if (!success) {
                success = TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
            animator.SetBool("isMoving", success);
        } else {
            animator.SetBool("isMoving", false);
        }

        // Used for NPC interaction.
        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
            Debug.Log("Pressed E");
        }
    }
    
    // Attempt to move in given direction.
    private bool TryMove(Vector2 direction) {
        if (direction != Vector2.zero) {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if (count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    // Currently unused function.
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    // Handles NPC interactions.
    void Interact() {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);

        // Checks if there is actually an NPC next to character.
        if (collider != null) {
            animator.SetBool("isMoving", false);

            if (collider.tag == "NPC")
                collider.GetComponent<Interactable>()?.InteractNPC();
            
            if (collider.tag == "Door") {
                collider.GetComponent<Interactable>()?.InteractDoor();
            }
        }
    }
}
