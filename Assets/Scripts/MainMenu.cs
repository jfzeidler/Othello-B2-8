using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class MainMenu : MonoBehaviour
{
    public Slider slider;
    public TMP_Dropdown dropdown;
    public Toggle toggle;

    void Awake()
    {
        // Get values from PlayerPrefs, else default to \/ 
        slider.value = PlayerPrefs.GetInt("maxDepth", 2);
        dropdown.value = PlayerPrefs.GetInt("playMode", 0);
        toggle.isOn = (PlayerPrefs.GetInt("moveGuide", 0) != 0);
    }

    public void PlayButton()
    {
        // If the play button is pressed, go to the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        // If the exit button is pressed, Quit the game. To debug in editer, print EXIT to the log
        Application.Quit();
        Debug.Log("EXIT");
    }

    public void ChangeMaxDepth()
    {
        // When slider value changes, change the value in PlayerPrefs, and save it
        PlayerPrefs.SetInt("maxDepth", (int)slider.value);
        Debug.Log("New maxDepth: " + slider.value);
        PlayerPrefs.Save();
    }

    public void ChangePlayingMode()
    {
        // When dropdown menu value changes, change the value in PlayerPrefs, and save it
        PlayerPrefs.SetInt("playMode", dropdown.value);
        Debug.Log("New PlayMode: " + dropdown.value);
        PlayerPrefs.Save();
    }

    public void ChangeShowMoves()
    {
        // When the toggle value changes, change the value in PlayerPrefs, and save it
        PlayerPrefs.SetInt("moveGuide", (toggle.isOn ? 1 : 0));
        Debug.Log("New moveGuide: " + (toggle.isOn ? 1 : 0));
        PlayerPrefs.Save();
    }
}
