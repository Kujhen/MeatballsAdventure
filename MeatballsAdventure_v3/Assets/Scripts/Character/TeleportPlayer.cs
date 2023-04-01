using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPlayer : MonoBehaviour, Interactable {

    public Transform teleportTarget;
    public GameObject player;
   
    public void InteractDoor() {
        player.transform.position = teleportTarget.transform.position;
    }
    
    public void InteractNPC() {
        // Do nothing
    }
}