using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Slider Slider;
    public TMP_Dropdown Dropdown;
    public Toggle Toggle;

    void Awake()
    {
        // Get values from PlayerPrefs, else default to \/ 
        Slider.value = PlayerPrefs.GetInt("maxDepth", 2);
        Dropdown.value = PlayerPrefs.GetInt("playMode", 0);
        Toggle.isOn = (PlayerPrefs.GetInt("moveGuide", 0) != 0);
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
        PlayerPrefs.SetInt("maxDepth", (int)Slider.value);
        Debug.Log("New maxDepth: " + Slider.value);
        PlayerPrefs.Save();
    }

    public void ChangePlayingMode()
    {
        // When dropdown menu value changes, change the value in PlayerPrefs, and save it
        PlayerPrefs.SetInt("playMode", Dropdown.value);
        Debug.Log("New PlayMode: " + Dropdown.value);
        PlayerPrefs.Save();
    }

    public void ChangeShowMoves()
    {
        // When the toggle value changes, change the value in PlayerPrefs, and save it
        PlayerPrefs.SetInt("moveGuide", (Toggle.isOn ? 1 : 0));
        Debug.Log("New moveGuide: " + (Toggle.isOn ? 1 : 0));
        PlayerPrefs.Save();
    }
}
