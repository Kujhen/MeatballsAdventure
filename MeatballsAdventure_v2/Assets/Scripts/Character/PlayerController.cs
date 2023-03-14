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

    private void Update() {
        if (movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);

            if (!success) {
                success = TryMove(new Vector2(movementInput.x, 0));   
            }

            if (!success) {
                success = TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
            animator.SetBool("isMoving", success);
        } else {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            InteractNPC();
        }
    }
    
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

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void InteractNPC() {
        //var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        //var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(transform.position, 0.3f, interactableLayer);
        if (collider != null) {
            collider.GetComponent<Interactable>()?.InteractNPC();
        }
    }
}
