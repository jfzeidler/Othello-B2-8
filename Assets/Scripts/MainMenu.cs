﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class MainMenu : MonoBehaviour
{

    public GameObject CanvasMenu;
    public GameObject Main_Camera;
    public GameObject Rotation_Camera;
    public GameObject UI;

    public void Update()
    {

    }
    public void MenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayButton()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CanvasMenu.SetActive(false);
        Main_Camera.SetActive(true);
        Rotation_Camera.SetActive(false);
        UI.SetActive(true);
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
}
