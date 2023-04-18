using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerDialogue : MonoBehaviour {

    public GameObject dialogBox;
    public string dialog;
    public bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else{
                dialogBox.SetActive(true);
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Player"))
        {
           playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){

        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false); 
        }
    }

    private void OnTriggerStay2D(Collider2D other){

        if(other.CompareTag("Player"))
        {
           // do something while the player is in the trigger zone
        }
    }
}
