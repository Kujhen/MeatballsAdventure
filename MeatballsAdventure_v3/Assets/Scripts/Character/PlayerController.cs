using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    
    public LayerMask grassLayer;
    public LayerMask interactableLayer;

    public event Action OnBattle;

    public float moveSpeed = 1f;

    private bool isMoving;
    private Vector2 movementInput;

    Animator animator;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    //Usb counter 
    public TextMeshProUGUI usbCounter;
    public int usbAmount;
    public GameObject WinScreen;
    
    public GameObject gameObject;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        usbAmount = 0;
        gameObject.SetActive(false);
        WinScreen.SetActive(false);
       
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

            // Only check for encounters whenever the player moves.
            CheckForEncounters();

            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
            animator.SetBool("isMoving", success);
        } else {
            animator.SetBool("isMoving", false);
        }

        // Used for NPC interaction.
        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
        

        //Used to activate WinScreen
        if(usbAmount == 7){
        
        WinScreen.SetActive(true);
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
    private void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    // Handles NPC interactions.
    private void Interact() {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);

        // Checks if there is a collision object next to character.
        if (collider != null) {
            animator.SetBool("isMoving", false);

            if (collider.tag == "NPC") {
                Debug.Log("Interacting with NPC");
                collider.GetComponent<Interactable>()?.InteractNPC();
            }
            if (collider.tag == "Door") {
                Debug.Log("Interacting with Door");
                collider.GetComponent<Interactable>()?.InteractDoor();
            }
        }
    }

    // Handles random tall grass squirrel encounters. 
    private void CheckForEncounters() {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null) {
            Debug.Log("Overlapping Grass");
            // 10 percent chance to encounter a squirrel.
            if (UnityEngine.Random.Range(1, 1001) == 1) {
                OnBattle();
                Debug.Log("Encountered Squirrel");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Usb")) {
            Debug.Log("Picked up Usb");
            Destroy(other.gameObject);
            usbAmount++;
            Debug.Log("Usb: " + usbAmount);
            usbCounter.text = "Usb: " + usbAmount + "/7";
        }
        if (other.CompareTag("Spawns") || other.CompareTag("NPC") || other.CompareTag("Sign")) {
            Debug.Log("Near Door");
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Spawns") || other.CompareTag("NPC") || other.CompareTag("Sign")) {
            Debug.Log("Away from Door");
            gameObject.SetActive(false);
        }
    }

    

}
