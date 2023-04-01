using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //needed when we want to change scenes 

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        //https://youtu.be/zc8ac_qUXQY?t=514 link is timestamped
        //SceneManager.LoadScene("Level 1"); Loads the Scene titled "Level 1" in Build Settings
        //SceneManager.LoadScene(1); Loads the Scene of index "1" in Build Settings
        
        //Loads the next level in the queue of the build settings
        //GetActiveScene().buildIndex gets the buildIndex of the scene and adds 1 to progress to next scene
        /**In Build Settings Scenes need to be loaded into the queue beforehand on game launch for this to work**/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void QuitGame()
    {
        //Console print to show command is working while in editor
        Debug.Log("QUIT!");
        //This command closes the applicatiion
        /**THIS WILL NOT CLOSE THE UNITY EDITOR**/
        Application.Quit();
    }

}
