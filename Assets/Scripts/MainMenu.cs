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
        slider.value = PlayerPrefs.GetInt("maxDepth", 2);
        dropdown.value = PlayerPrefs.GetInt("playMode", 0);
        toggle.isOn = (PlayerPrefs.GetInt("MoveGuide", 0) != 0);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }

    public void ChangeMaxDepth()
    {
        PlayerPrefs.SetInt("maxDepth", (int)slider.value);
        Debug.Log("New maxDepth: " + slider.value);
        PlayerPrefs.Save();
    }

    public void ChangePlayingMode()
    {
        PlayerPrefs.SetInt("playMode", dropdown.value);
        Debug.Log("New PlayMode: " + dropdown.value);
        PlayerPrefs.Save();
    }

    public void ChangeShowMoves()
    {
        PlayerPrefs.SetInt("MoveGuide", (toggle.isOn ? 1 : 0));
        Debug.Log("New MoveGuide: " + (toggle.isOn ? 1 : 0));
        PlayerPrefs.Save();
    }
}
