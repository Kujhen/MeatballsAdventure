using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour, Interactable {

    // [SerializeField] Dialog dialog;
    public string sceneName;

    public void InteractNPC() {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Interacting with NPC");
        //StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
