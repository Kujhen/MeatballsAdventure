using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour, Interactable {

    // [SerializeField] Dialog dialog;

    public event Action OnBattle;

    public void InteractNPC() {
        Debug.Log("Interacting with NPC");
        OnBattle();
        //StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
    public void InteractDoor() {
        // Do nothing
    }
}
