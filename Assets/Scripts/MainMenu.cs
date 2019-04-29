using System.Collections;
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

    /*void WaitForSceneManager()
    {
        float temptime = Time.time + 2.0f; float time = Time.time;

        Debug.Log(temptime + ">" + time);
        
        while (temptime > time)
        {
            time += 0.0001f;
        }
        
        CanvasMenu.SetActive(true);
        Main_Camera.SetActive(false);
        Rotation_Camera.SetActive(true);
        UI.SetActive(false);
        Debug.Log("TESTING");
    }*/

    void Start()
    {
        CanvasMenu.SetActive(true);
        Main_Camera.SetActive(false);
        Rotation_Camera.SetActive(true);
        UI.SetActive(false);
    }

    public void Update()
    {

    }

    public void MenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CanvasMenu.SetActive(true);
        Main_Camera.SetActive(false);
        Rotation_Camera.SetActive(true);
        UI.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //StartCoroutine("WaitForSceneManager", 5.0f);
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
