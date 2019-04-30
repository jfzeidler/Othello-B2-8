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
    void Awake()
    {
        slider.value = PlayerPrefs.GetInt("maxDepth", 2);
        dropdown.value = PlayerPrefs.GetInt("playMode", 0);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    }

    public void ChangePlayingMode()
    {
        PlayerPrefs.SetInt("playMode", dropdown.value);
        Debug.Log("New PlayMode: " + dropdown.value);
    }
}
