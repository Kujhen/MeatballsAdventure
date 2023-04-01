using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown; 

    Resolution[] resolutions; //array to store screen resolutions that the computer is capable of

    //method runs on scene load 
    void Start()
    {
        //Reference Link: https://youtu.be/YOaYQrN1oYQ?t=551
        //On scene load this section deals with screen resolution

        //ResolutionDropdown object 'options menu': Canvas > Options Menu > GraphicsDropdown > Dropdown-TextMeshPro > Options
        //*****The values in the options menu will be updated on scene load, it will not say Option A, B, C*****/


        resolutions = Screen.resolutions; //fill array with resolutions that the computer has availible to it
        
        resolutionDropdown.ClearOptions(); //clear dropdown menu options 

        List<string> options = new List<string>(); //create a list of strings that will be used as the new options 

        int currentResolutionIndex = 0; 

        for (int i = 0; i < resolutions.Length; i++) //fill the options list with the screen resolutions found earlier in the start method
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; //get width and height of the resolution[i] and make a string out of it
            options.Add(option); //add the resolution string to the options list 

            if (System.String.Equals(resolutions[i].ToString(), Screen.currentResolution.ToString())) //cant compare resolution objects directly so they need to be converted to strings first
            {
                currentResolutionIndex = i;//store the value of the options menu that corresponds with the computer's screen resolution
            }

        }

        resolutionDropdown.AddOptions(options);//use the options list as our new dropdown menu options (no longer the default options: Option A, Option B, Option C)
        resolutionDropdown.value = currentResolutionIndex; //set dropdown list value to the respective screen resolution index value
        resolutionDropdown.RefreshShownValue();//update value shown in the dropdown list
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Reference Link: https://youtu.be/YOaYQrN1oYQ?t=73
    //This function sets the game's masterVolume audio parameter equal to the VolumeSlider object value.
    //This slider dictate the the master volume setting

    //VolumeSlider object 'value': Canvas > Options Menu > VolumeSlider > Slider > Value
    //Master Volume Parameter 'masterVolume': Window > Audio > Audio Mixer > MainMixer > Exposed Parameters > masterVolume
    public void SetVolume(float volume)
    {
        //"masterVolume" is the name of the exposed parameter found in the Audio Mixer window
        //volume is defined by the value of VolumeSlider
        audioMixer.SetFloat("masterVolume", volume);
    }


    //Reference Link: https://youtu.be/YOaYQrN1oYQ?t=336
    //This function sets the game's quality level depending on the option selected in the GraphicsDropdown object 

    //GraphicsDropdown object 'options menu': Canvas > Options Menu > GraphicsDropdown > Dropdown-TextMeshPro > Options
    //Quality levels are defined in: Edit > Project Settings > Quality
    public void SetQuality(int qualityIndex)
    {
        //(int qualityIndex) represents the index of the quality level (low = 0, medium = 1, high = 2)

        //qualityIndex is incremented by +1 because the project file has 6 levels of quality settings but for the
            //purposes of the GraphicsDropdown menu only the Low, Medium, and High settings are being used.
        QualitySettings.SetQualityLevel(qualityIndex+1);
    }

    //Reference Link: https://youtu.be/YOaYQrN1oYQ?t=469
    //This function toggles fullscreen mode on and off depending on a checkbox in the FullscreenToggle object
    
    //FullscreenToggle object 'checkbox': Canvas > Options Menu > FullscreenToggle > Toggle > Is On (checkbox)
    //Fullscreen Mode option: Edit > Project Settings > Player > Resolution and Presentation (Not too sure of the exact setting being modified)
    
    public void SetFullscreen(bool isFullscreen)
    {
        //checkbox at time of writting is autoselected to be on meaning fullscreen mode if the default state of the game
        //if checkbox is deselected then the game changes to windowed mode
        Screen.fullScreen = isFullscreen;
    }



}
